using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Torappu;
using Sirenix.OdinInspector.Editor;

namespace CustomMap
{
    [CreateAssetMenu(fileName = "RouteBuildView", menuName = "CustomMap/RouteBuildView")]
    public class RouteBuildView : SerializedScriptableObject
    {
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

        /// <summary>
        /// 路线信息
        /// </summary>

        [Serializable]
        public class RouteInfo
        {
            [SerializeField]
            public MotionMode motionMode;

            [SerializeField]
            public Vector2Int startPosition;
            [SerializeField]
            public Vector2Int endPosition;
            [SerializeField]
            public Vector2 spawnRandomRange;
            [SerializeField]
            public Vector2 spawnOffset;
            [SerializeField]
            public CheckPoint[] checkpoints;
            [SerializeField]
            public bool allowDiagonalMove = true;

            [Serializable]
            public class CheckPoint
            {
                [SerializeField]
                public CheckPointType type;
                [SerializeField]
                public float time;
                [SerializeField]
                public Vector2Int position;
                [SerializeField]
                public Vector2 reachOffset;
                [SerializeField]
                public bool randomizeReachOffset;
                [SerializeField]
                public float reachDistance;
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

        public class RouteData
        {
            [HideInInspector]
            public RouteInfo routeInfo;

            private Color m_guiColor = Color.white;

            [ShowInInspector, ReadOnly, GUIColor("$m_guiColor")]
            private int m_index;

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