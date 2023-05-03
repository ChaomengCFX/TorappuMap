#if UNITY_EDITOR
// Author: ChaomengCFX
// Created: 2023/04/02

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Torappu;
using Torappu.Battle;
using CustomMap.TileInformation;

namespace CustomMap
{
    [CreateAssetMenu(fileName = "MapBuildView", menuName = "CustomMap/MapBuildView")]
    public class MapBuildView : SerializedScriptableObject, IResetable
    {
        #region 设置
        public void Reset()
        {
            Reset(false);
        }

        public void Reset(bool force = true)
        {
            if (m_inited && !force) return;

            m_inited = true;

            map = FindObjectOfType<Map>();
            if (force)
            {
                _InitMap();
                tiles = new List<CustomTileData>();
            }

            tileSelected = null;

            m_forbiddenTex = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/CustomMap/Sprite/forbidden.png");
            m_wallTex = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/CustomMap/Sprite/wall.png");
            m_startTex = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/CustomMap/Sprite/start.png");
            m_flystartTex = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/CustomMap/Sprite/flystart.png");
            m_endTex = AssetDatabase.LoadAssetAtPath<Texture2D>("Assets/CustomMap/Sprite/end.png");
        }

        [Button("重置全部"), TitleGroup("地图构建", Order = -1)]
        private void _Reset() => Reset(true);

        private bool m_inited = false;

        private Texture2D m_forbiddenTex;
        private Texture2D m_wallTex;
        private Texture2D m_startTex;
        private Texture2D m_flystartTex;
        private Texture2D m_endTex;

        [BoxGroup("MapRef", LabelText = "场景引用"), GUIColor(1f, 0.9f, 0.6f)]
        public Transform meshRoot;
        [BoxGroup("MapRef"), GUIColor(1f, 0.9f, 0.6f)]
        public Transform tileRoot;
        [BoxGroup("MapRef"), GUIColor(1f, 0.9f, 0.6f)]
        public Map map;

        [BoxGroup("MapSetting", LabelText = "地图初始设置"), HorizontalGroup("MapSetting/MapSize")]
        [SerializeField, LabelText("宽（列）"), LabelWidth(40), GUIColor(1f, 0.9f, 0.6f)]
        private int width = 11;
        [SerializeField, LabelText("高（行）"), HorizontalGroup("MapSetting/MapSize"), LabelWidth(40), GUIColor(1f, 0.9f, 0.6f)]
        private int height = 8;

        [Button("初始化"), HorizontalGroup("MapSetting/MapSize"), GUIColor(1f, 0.9f, 0.6f)]
        private void _InitMap()
        {
            m_tileMap = new CustomTileInfo[width, height];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    m_tileMap[j, i] = new EmptyCustomTileInfo();
                }
            }
        }
        #endregion

        #region Tile
        [Space(20)]
        [SerializeField, TableList, LabelText("Tile预制体"), OnCollectionChanged(nameof(OnTilesListChanged))]
        private List<CustomTileData> tiles;

        private void OnTilesListChanged(CollectionChangeInfo info, object _)
        {
            if (info.ChangeType == CollectionChangeType.Add)
            {
                tiles[tiles.Count - 1].Init(this);
            }
        }

        [ShowInInspector, ReadOnly, LabelText("当前选中"), GUIColor(0f, 0.9f, 1f)]
        private CustomTile tileSelected;
        [SerializeField, LabelText("Tile特殊类型"), EnumToggleButtons]
        private CustomTileInfo.TileType specialTileType = CustomTileInfo.TileType.None;
        [SerializeField, LabelText("Tile方向"), EnumToggleButtons]
        private CustomTileInfo.TileDirection tileDirection = CustomTileInfo.TileDirection.Random;

        private void SelectTile(CustomTileData data)
        {
            tileSelected = data.tile;
            foreach (CustomTileData tile in tiles)
            {
                if (tile != data)
                    tile.OnDeSelect();
            }
        }
        #endregion

        #region TileMap
        [Title("绘制TileMap")]

        public CustomTileInfo[,] TileMap
        {
            get
            {
                return m_tileMap;
            }
        }

        private enum MapDisplayType
        {
            TileMap,
            HeightMap,
            BuildableMap,
            PassableMap
        }

        [SerializeField, LabelText("地图显示类型"), EnumToggleButtons, GUIColor(1f, 0.9f, 0.5f)]
        private MapDisplayType mapDisplayType;

        [SerializeField, LabelText("低地颜色"), ShowIf("@mapDisplayType==MapDisplayType.HeightMap")]
        private Color _lowLandColor = new Color(0.2f, 0.9f, 0.8f);
        [SerializeField, LabelText("高地颜色"), ShowIf("@mapDisplayType==MapDisplayType.HeightMap")]
        private Color _highLandColor = new Color(1f, 0.8f, 0.3f);

        [SerializeField, LabelText("不可部署"), ShowIf("@mapDisplayType==MapDisplayType.BuildableMap")]
        private Color _buildableNoneColor = new Color(0.4f, 0f, 0f);
        [SerializeField, LabelText("仅近战位"), ShowIf("@mapDisplayType==MapDisplayType.BuildableMap")]
        private Color _buildableMeleeColor = new Color(0.4f, 1f, 1f);
        [SerializeField, LabelText("仅远程位"), ShowIf("@mapDisplayType==MapDisplayType.BuildableMap")]
        private Color _buildableRangedColor = new Color(1f, 0.8f, 0.2f);
        [SerializeField, LabelText("所有单位可部署"), ShowIf("@mapDisplayType==MapDisplayType.BuildableMap")]
        private Color _buildableAllColor = new Color(0.3f, 1f, 0.6f);
        [SerializeField, LabelText("设置可部署属性为"), EnumToggleButtons, ShowIf("@mapDisplayType==MapDisplayType.BuildableMap")]
        private BuildableType selectedbuildableType = BuildableType.ALL;

        [SerializeField, LabelText("不可通行"), ShowIf("@mapDisplayType==MapDisplayType.PassableMap")]
        private Color _passableNoneColor = new Color(0.4f, 0f, 0f);
        [SerializeField, LabelText("仅地面单位可通行"), ShowIf("@mapDisplayType==MapDisplayType.PassableMap")]
        private Color _passableWalkColor = new Color(0.4f, 1f, 1f);
        [SerializeField, LabelText("仅飞行可通行"), ShowIf("@mapDisplayType==MapDisplayType.PassableMap")]
        private Color _passableFlyColor = new Color(1f, 0.8f, 0.2f);
        [SerializeField, LabelText("所有单位可通行"), ShowIf("@mapDisplayType==MapDisplayType.PassableMap")]
        private Color _passableAllColor = new Color(0.3f, 1f, 0.6f);
        [SerializeField, LabelText("设置通行属性为"), EnumToggleButtons, ShowIf("@mapDisplayType==MapDisplayType.PassableMap")]
        private MotionMask selectedMotionMask = MotionMask.ALL;

        private enum EditorType
        {
            TileMap,
            ModifyTileData
        }

        [SerializeField, LabelText("编辑类型"), EnumToggleButtons, OnValueChanged(nameof(OnEditorTypeChanged))]
        private EditorType editorType = EditorType.TileMap;

        private void OnEditorTypeChanged()
        {
            if (editorType == EditorType.TileMap)
                m_tileInfoModifyWindow?.Close();
            else
            {
                m_tileInfoModifyWindow = EditorWindow.GetWindow<TileInfoModifyWindow>();
                m_tileInfoModifyWindow.Show();
            }
        }

        [SerializeField, TableMatrix(SquareCells = true, DrawElementMethod = nameof(DrawMapElement))]
        private CustomTileInfo[,] m_tileMap;

        private TileInfoModifyWindow m_tileInfoModifyWindow;

        /// <summary>
        /// 绘制TileMap
        /// </summary>
        private CustomTileInfo DrawMapElement(Rect rect, CustomTileInfo value)
        {
            switch (mapDisplayType)
            {
                case MapDisplayType.TileMap:
                    return DrawTileMapElement(rect, value);
                case MapDisplayType.HeightMap:
                    return DrawHeightMapElement(rect, value);
                case MapDisplayType.BuildableMap:
                    return DrawBuildableMapElement(rect, value);
                case MapDisplayType.PassableMap:
                    return DrawPassableMapElement(rect, value);
            }
            throw new NotImplementedException();
        }

        private void DrawSpecialTileBG(Rect rect, CustomTileInfo value)
        {
            Texture2D texture;
            if (!value.TryGetGUIIcon(out texture))
            {
                switch (value.SpecialTileType)
                {
                    case CustomTileInfo.TileType.Start:
                        texture = m_startTex;
                        break;
                    case CustomTileInfo.TileType.FlyStart:
                        texture = m_flystartTex;
                        break;
                    case CustomTileInfo.TileType.End:
                        texture = m_endTex;
                        break;
                    case CustomTileInfo.TileType.None:
                        switch (value.TileData.tileKey)
                        {
                            case "tile_wall":
                                texture = m_wallTex;
                                break;
                            case "tile_forbidden":
                                texture = m_forbiddenTex;
                                break;
                        }
                        break;
                }
            }
            if (texture)
                EditorGUI.DrawTextureTransparent(rect.Padding(1), texture);
        }

        private bool IsMouseDown(Rect rect)
        {
            return (Event.current.type == EventType.MouseDrag || Event.current.type == EventType.MouseDown)
                && rect.Contains(Event.current.mousePosition);
        }

        private void SetModifyInterface(CustomTileInfo info)
        {
            m_tileInfoModifyWindow.ModifyInterface = info.GetModifyInterface();
            m_tileInfoModifyWindow.Focus();
        }

        /// <summary>
        /// 绘制Tile类型图
        /// </summary>
        private CustomTileInfo DrawTileMapElement(Rect rect, CustomTileInfo value)
        {
            if (IsMouseDown(rect) && tileSelected != null)
            {
                if (editorType == EditorType.TileMap)
                {
                    value = tileSelected.CreateCustomTileInfo();
                    value.SetUp(tileDirection, specialTileType);
                    GUI.changed = true;
                    Event.current.Use();
                }
                else
                {
                    SetModifyInterface(value);
                }
            }

            DrawSpecialTileBG(rect, value);
            Color color;

            if (value.IsNull)
            {
                color = new Color(0f, 0f, 0f, 0.5f);
            }
            else
            {
                color = tiles.First(x => value.IsFrom(x.tile)).color;
                color.a = 0.8f;
            }
            EditorGUI.DrawRect(rect.Padding(1), color);
            return value;
        }

        /// <summary>
        /// 绘制高度图
        /// </summary>
        private CustomTileInfo DrawHeightMapElement(Rect rect, CustomTileInfo value)
        {
            TileData dataRef = value.TileData;
            if (IsMouseDown(rect))
            {
                if (editorType == EditorType.TileMap)
                {
                    if (dataRef.heightType == TileData.HeightType.LOWLAND)
                        dataRef.heightType = TileData.HeightType.HIGHLAND;
                    else
                        dataRef.heightType = TileData.HeightType.LOWLAND;
                    GUI.changed = true;
                    Event.current.Use();
                }
                else
                {
                    SetModifyInterface(value);
                }
            }

            DrawSpecialTileBG(rect, value);

            Color color;
            if (dataRef.heightType == TileData.HeightType.LOWLAND)
            {
                color = _lowLandColor;
                color.a = 0.8f;
            }
            else
            {
                color = _highLandColor;
                color.a = 0.8f;
            }
            EditorGUI.DrawRect(rect.Padding(1), color);

            return value;
        }

        /// <summary>
        /// 绘制可部署类型图
        /// </summary>
        private CustomTileInfo DrawBuildableMapElement(Rect rect, CustomTileInfo value)
        {
            TileData dataRef = value.TileData;
            if (IsMouseDown(rect))
            {
                if (editorType == EditorType.TileMap)
                {
                    dataRef.buildableType = selectedbuildableType;
                    GUI.changed = true;
                    Event.current.Use();
                }
                else
                {
                    SetModifyInterface(value);
                }
            }

            DrawSpecialTileBG(rect, value);

            Color color;
            switch (dataRef.buildableType)
            {
                case BuildableType.NONE:
                    color = _buildableNoneColor;
                    break;
                case BuildableType.MELEE:
                    color = _buildableMeleeColor;
                    break;
                case BuildableType.RANGED:
                    color = _buildableRangedColor;
                    break;
                case BuildableType.ALL:
                    color = _buildableAllColor;
                    break;
                default:
                    throw new NotImplementedException();
            }
            color.a = 0.8f;
            EditorGUI.DrawRect(rect.Padding(1), color);

            return value;
        }

        /// <summary>
        /// 绘制通行类型图
        /// </summary>
        private CustomTileInfo DrawPassableMapElement(Rect rect, CustomTileInfo value)
        {
            TileData dataRef = value.TileData;
            if (IsMouseDown(rect))
            {
                if (editorType == EditorType.TileMap)
                {
                    dataRef.passableMask = selectedMotionMask;
                    GUI.changed = true;
                    Event.current.Use();
                }
                else
                {
                    SetModifyInterface(value);
                }
            }

            DrawSpecialTileBG(rect, value);

            Color color;
            switch (dataRef.passableMask)
            {
                case MotionMask.NONE:
                    color = _passableNoneColor;
                    break;
                case MotionMask.WALK_ONLY:
                    color = _passableWalkColor;
                    break;
                case MotionMask.FLY_ONLY:
                    color = _passableFlyColor;
                    break;
                case MotionMask.ALL:
                    color = _passableAllColor;
                    break;
                default:
                    throw new NotImplementedException();
            }
            color.a = 0.8f;
            EditorGUI.DrawRect(rect.Padding(1), color);

            return value;
        }

        [BoxGroup("MapOp", LabelText = "操作"), HorizontalGroup("MapOp/Button"), Button("清空已存在Tile", ButtonSizes.Large)]
        private void _ClearAllTile()
        {
            if (meshRoot == null || tileRoot == null) return;
            int count = meshRoot.childCount;
            for (int i = 0; i < count; i++)
            {
                DestroyImmediate(meshRoot.GetChild(0).gameObject);
            }

            count = tileRoot.childCount;
            for (int i = 0; i < count; i++)
            {
                DestroyImmediate(tileRoot.GetChild(0).gameObject);
            }
        }

        [BoxGroup("MapOp"), HorizontalGroup("MapOp/Button"), Button("清空并生成地图", ButtonSizes.Large)]
        private void _Build()
        {
            if (meshRoot == null || tileRoot == null) return;
            _ClearAllTile();

            List<Tile> tiles = new List<Tile>();
            List<TileGraphic> tileGraphics = new List<TileGraphic>();
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    CustomTileInfo info = m_tileMap[col, height - row - 1];
                    info.Instantiate(row, col, this, tiles, tileGraphics); // 实例化Tile
                }
            }
            Map.Tiles2D tiles2D = new Map.Tiles2D
            {
                _tiles = tiles.ToArray(),
                _width = width,
                _height = height
            };
            map._graphic._graphics = tileGraphics.ToArray();
            map._tiles = tiles2D;
            map._anchorTransform.localPosition = new Vector3(-(width - 1) / 2f, -(height - 1) / 2f, 0f); // 重设锚点至地图中心

            Debug.Log("Build done");
        }
        #endregion

        [Serializable]
        private class CustomTileData
        {
            private Color m_guiColor = Color.white;

            [SerializeField, HideInInspector]
            private MapBuildView m_view;

            [GUIColor("$m_guiColor"), TableColumnWidth(100)]
            public CustomTile tile;

            [GUIColor("$m_guiColor"), TableColumnWidth(300), ShowInInspector]
            public string Description => tile?.Description;

            [ColorPalette(PaletteName = "Tile"), TableColumnWidth(200)]
            public Color color;

            public void Init(MapBuildView view)
            {
                m_view = view;
            }

            public void OnDeSelect()
            {
                m_guiColor = Color.white;
            }

            [Button("选中", ButtonSizes.Small), GUIColor("$m_guiColor"), TableColumnWidth(80)]
            private void Select()
            {
                m_guiColor = Color.green;
                m_view.SelectTile(this);
            }
        }
    }
}
#endif