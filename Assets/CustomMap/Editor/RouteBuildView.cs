using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Torappu;

namespace CustomMap
{
    [CreateAssetMenu(fileName = "RouteBuildView", menuName = "CustomMap/RouteBuildView")]
    public class RouteBuildView : SerializedScriptableObject, IResetable
    {
        private void OnEnable()
        {
            SceneView.onSceneGUIDelegate += OnSceneGUI;
        }

        private void OnDisable()
        {
            SceneView.onSceneGUIDelegate -= OnSceneGUI;
        }

        public void Reset()
        {
            mapBuildView = AssetDatabase.LoadAssetAtPath<MapBuildView>("Assets/CustomMap/MapBuildView.asset");
        }

        private MapBuildView mapBuildView;

        [SerializeField, TableList, LabelText("路线列表"), OnCollectionChanged("OnRoutesListChanged")]
        private List<RouteData> routes = new List<RouteData>();

        [SerializeField]
        private RouteInfo routeSelected;

        private void OnRoutesListChanged(CollectionChangeInfo info, object _)
        {
            if (info.ChangeType == CollectionChangeType.Add)
            {
                routes[routes.Count - 1].Init(routes.Count - 1, this);
            }
        }

        /// <summary>
        /// 选中路线
        /// </summary>
        private void SelectRoute(RouteData info)
        {
            routeSelected = info.routeInfo;
            foreach (RouteData tile in routes)
            {
                if (tile != info)
                    tile.OnDeSelect();
            }
        }

        private void OnSceneGUI(SceneView sceneView)
        {
            if (mapBuildView?.map)
                routeSelected?.OnSceneGUI(sceneView, mapBuildView.map._anchorTransform);
        }

        public void ApplyTo(ref Torappu.RouteData[] routesTo)
        {
            routesTo = new Torappu.RouteData[routes.Count];
            for (int i = 0; i < routes.Count; i++)
            {
                routes[i].routeInfo.CopyTo(out routesTo[i]);
            }
        }

        /// <summary>
        /// 路线信息
        /// </summary>
        [Serializable]
        public class RouteInfo
        {
            [SerializeField, LabelText("移动类型"), EnumToggleButtons]
            public MotionMode motionMode;

            [SerializeField, LabelText("起始位置")]
            public Vector2Int startPosition;
            [SerializeField, LabelText("结束位置")]
            public Vector2Int endPosition;
            [SerializeField, LabelText("生成随机范围")]
            public Vector2 spawnRandomRange;
            [SerializeField, LabelText("生成位置偏移")]
            public Vector2 spawnOffset;
            [SerializeField, LabelText("检查点")]
            public List<CheckPoint> checkpoints;
            [SerializeField, LabelText("允许对角线移动")]
            public bool allowDiagonalMove = true;

            [Serializable]
            public class CheckPoint
            {
                [SerializeField, LabelText("检查点类型")]
                public CheckPointType type;
                [SerializeField, LabelText("时间")]
                public float time;
                [SerializeField, LabelText("检查点位置")]
                public Vector2Int position;
                [SerializeField, LabelText("检查点位置偏移")]
                public Vector2 reachOffset;
                [SerializeField, LabelText("随机到达偏移")]
                public bool randomizeReachOffset;
                [SerializeField, LabelText("到达距离")]
                public float reachDistance;

                public void CopyTo(out Torappu.RouteData.CheckpointData checkpointData)
                {
                    checkpointData = new Torappu.RouteData.CheckpointData();
                    checkpointData.type = (CheckpointType)Enum.ToObject(typeof(CheckpointType), type);
                    checkpointData.time = time;
                    checkpointData.position = new GridPosition { col = position.x, row = position.y };
                    checkpointData.reachOffset = reachOffset;
                    checkpointData.reachDistance = reachDistance;
                    checkpointData.randomizeReachOffset = randomizeReachOffset;
                }
            }

            public void OnSceneGUI(SceneView sceneView, Transform anchor)
            {
                Vector3 root = anchor.position;
                Vector3 start = new Vector3(startPosition.x, startPosition.y) + new Vector3(spawnOffset.x, spawnOffset.y) + root;
                List<Vector3> points = new List<Vector3>();
                Handles.color = new Color(1f, 0f, 0f, 0.3f);
                Handles.DrawSolidDisc(start, Vector3.forward, 0.25f);
                Handles.Label(start, "起始位置");
                points.Add(start);
                Vector3 end = new Vector3(endPosition.x, endPosition.y) + root;
                Handles.color = new Color(0f, 0f, 1f, 0.3f);
                Handles.DrawSolidDisc(end, Vector3.forward, 0.25f);
                Handles.Label(end, "结束位置");
                for (int i = 0; i < checkpoints.Count; i++)
                {
                    CheckPoint checkpoint = checkpoints[i];
                    Handles.color = new Color(0f, 1f, 0f, 0.3f);
                    Vector3 pos = new Vector3(checkpoint.position.x, checkpoint.position.y) + new Vector3(checkpoint.reachOffset.x, checkpoint.reachOffset.y) + root;
                    Handles.DrawSolidDisc(pos, Vector3.forward, 0.15f);
                    Handles.Label(pos, $"检查点{i}");
                    points.Add(pos);
                }
                points.Add(end);
                for (int i = 1; i < points.Count; i++)
                {
                    Handles.color = Color.green;
                    Handles.DrawDottedLine(points[i - 1], points[i], 2f);
                }
            }

            public void CopyTo(out Torappu.RouteData routeData)
            {
                routeData = new Torappu.RouteData();
                if (motionMode == MotionMode.WALK)
                    routeData.motionMode = Torappu.MotionMode.WALK;
                else
                    routeData.motionMode = Torappu.MotionMode.FLY;
                routeData.startPosition = new GridPosition { col = startPosition.x, row = startPosition.y };
                routeData.endPosition = new GridPosition { col = endPosition.x, row = endPosition.y };
                routeData.spawnRandomRange = spawnRandomRange;
                routeData.spawnOffset = spawnOffset;
                routeData.allowDiagonalMove = allowDiagonalMove;
                routeData.checkpoints = new Torappu.RouteData.CheckpointData[checkpoints.Count];
                for (int i = 0; i < checkpoints.Count; i++)
                {
                    checkpoints[i].CopyTo(out routeData.checkpoints[i]);
                }
            }

            public enum CheckPointType
            {
                MOVE = 0,
                WAIT_FOR_SECONDS = 1,
                WAIT_FOR_PLAY_TIME = 2,
                WAIT_CURRENT_FRAGMENT_TIME = 3,
                WAIT_CURRENT_WAVE_TIME = 4,
                DISAPPEAR = 5,
                APPEAR_AT_POS = 6,
                ALERT = 7,
                PATROL_MOVE = 8
            }

            public enum MotionMode
            {
                WALK,
                FLY
            }
        }

        [Serializable]
        public class RouteData
        {
            [SerializeField, HideInInspector]
            public RouteInfo routeInfo;

            private Color m_guiColor = Color.white;

            [SerializeField, ReadOnly, GUIColor("$m_guiColor")]
            private int m_index;

            [SerializeField, HideInInspector]
            private RouteBuildView m_view;

            public void Init(int index, RouteBuildView view)
            {
                m_index = index;
                m_view = view;
            }

            public void OnDeSelect()
            {
                m_guiColor = Color.white;
            }

            [Button("选中", ButtonSizes.Small), GUIColor("$m_guiColor")]
            private void Select()
            {
                m_guiColor = Color.green;
                m_view.SelectRoute(this);
            }
        }
    }
}