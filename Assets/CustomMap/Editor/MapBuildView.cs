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
using Torappu.Battle.Tiles;
using Random = UnityEngine.Random;
using FullInspector;

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
            _InitMap();
            tiles = new List<CustomTileData>();

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
            tileMap = new TileInfo[width, height];
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    tileMap[j, i] = new TileInfo();
                }
            }
        }
        #endregion

        #region Tile
        [Space(20)]
        [SerializeField, TableList, LabelText("Tile预制体"), OnCollectionChanged("OnTilesListChanged")]
        private List<CustomTileData> tiles;

        private void OnTilesListChanged(CollectionChangeInfo info, object _)
        {
            if (info.ChangeType == CollectionChangeType.Add)
            {
                tiles[tiles.Count - 1].Init(tiles.Count - 1, this);
            }
        }

        [ShowInInspector, ReadOnly, LabelText("当前选中"), GUIColor(0f, 0.9f, 1f)]
        private CustomTile tileSelected;
        [SerializeField, LabelText("Tile特殊类型"), EnumToggleButtons]
        private TileInfo.SpecialTileType specialTileType = TileInfo.SpecialTileType.None;
        [SerializeField, LabelText("Tile方向"), EnumToggleButtons]
        private TileInfo.Direction tileDirection = TileInfo.Direction.Random;

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

        public TileInfo[,] TileMap
        {
            get
            {
                return tileMap;
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

        [SerializeField, TableMatrix(SquareCells = true, DrawElementMethod = "DrawMapElement")]
        private TileInfo[,] tileMap;

        /// <summary>
        /// 绘制TileMap
        /// </summary>
        private TileInfo DrawMapElement(Rect rect, TileInfo value)
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

        private void DrawSpecialTileBG(Rect rect, TileInfo value)
        {
            if (!value.IsNull)
            {
                switch (value.specialTileType)
                {
                    case TileInfo.SpecialTileType.Start:
                        EditorGUI.DrawTextureTransparent(rect.Padding(1), m_startTex);
                        break;
                    case TileInfo.SpecialTileType.FlyStart:
                        EditorGUI.DrawTextureTransparent(rect.Padding(1), m_flystartTex);
                        break;
                    case TileInfo.SpecialTileType.End:
                        EditorGUI.DrawTextureTransparent(rect.Padding(1), m_endTex);
                        break;
                    case TileInfo.SpecialTileType.None:
                        switch (value.tile.tile._tileKey)
                        {
                            case "tile_wall":
                                EditorGUI.DrawTextureTransparent(rect.Padding(1), m_wallTex);
                                break;
                            case "tile_forbidden":
                                EditorGUI.DrawTextureTransparent(rect.Padding(1), m_forbiddenTex);
                                break;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 绘制Tile类型图
        /// </summary>
        private TileInfo DrawTileMapElement(Rect rect, TileInfo value)
        {
            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
            {
                value = new TileInfo(tileSelected, specialTileType, tileDirection);
                GUI.changed = true;
                Event.current.Use();
            }
            DrawSpecialTileBG(rect, value);
            Color color;
            if (value == null || value.tile == null)
            {
                color = new Color(0f, 0f, 0f, 0.5f);
            }
            else
            {
                color = tiles.First(x => x.tile == value.tile).color;
                color.a = 0.8f;
            }
            EditorGUI.DrawRect(rect.Padding(1), color);
            return value;
        }
        
        /// <summary>
        /// 绘制高度图
        /// </summary>
        private TileInfo DrawHeightMapElement(Rect rect, TileInfo value)
        {
            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition) && !value.IsNull)
            {
                value.OnTriggleHeight();
                GUI.changed = true;
                Event.current.Use();
            }
            DrawSpecialTileBG(rect, value);
            Color color;
            if (value == null || value.IsNull)
            {
                color = new Color(0f, 0f, 0f, 0.5f);
            }
            else if (value.data.heightType == TileData.HeightType.LOWLAND)
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
        private TileInfo DrawBuildableMapElement(Rect rect, TileInfo value)
        {
            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition) && !value.IsNull)
            {
                value.data.buildableType = selectedbuildableType;
                GUI.changed = true;
                Event.current.Use();
            }
            DrawSpecialTileBG(rect, value);
            Color color;
            if (value == null || value.IsNull)
            {
                color = new Color(0f, 0f, 0f, 0.5f);
            }
            else
            {
                switch (value.data.buildableType)
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
            }
            EditorGUI.DrawRect(rect.Padding(1), color);

            return value;
        }

        /// <summary>
        /// 绘制通行类型图
        /// </summary>
        private TileInfo DrawPassableMapElement(Rect rect, TileInfo value)
        {
            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition) && !value.IsNull)
            {
                value.data.passableMask = selectedMotionMask;
                GUI.changed = true;
                Event.current.Use();
            }
            DrawSpecialTileBG(rect, value);
            Color color;
            if (value == null || value.IsNull)
            {
                color = new Color(0f, 0f, 0f, 0.5f);
            }
            else
            {
                switch (value.data.passableMask)
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
            }
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
            List<MeshTileGraphic> tileGraphics = new List<MeshTileGraphic>();
            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    TileInfo info = tileMap[col, height - row - 1];
                    if (info == null || info.tile == null) continue;
                    CustomTile tilePrefab = info.tile;
                    MeshTileGraphic tileGraphic = null;
                    if (tilePrefab.mesh)
                    {
                        tileGraphic = Instantiate(tilePrefab.mesh, meshRoot);
                        tileGraphic.transform.localPosition += new Vector3(col, row, 0f);
                        switch (info.dir)
                        {
                            case TileInfo.Direction.Random:
                                tileGraphic.transform.localEulerAngles += new Vector3(Random.Range(0, 4) * 90f, 0f, 0f);
                                break;
                            default:
                                break;
                        }
                    }
                    Tile tile = Instantiate(tilePrefab.tile, tileRoot);
                    tile.transform.localPosition += new Vector3(col, row, 0f);
                    string pos = string.Format("({0},{1})#", row, col);
                    tile.gameObject.name = pos + tile.gameObject.name;
                    if (tileGraphic)
                    {
                        tileGraphic.gameObject.name = pos + tileGraphic.gameObject.name;
                        tileGraphic._tile = tile;
                        tileGraphic._gridPos = new GridPosition { row = row, col = col };
                    }
                    tile._graphic = tileGraphic;
                    tile._allGraphicList.Add(tileGraphic);
                    tile._data = info.data;

                    SpecialTileTypeHandler handler;
                    if (info.TryGetSpecialTileTypeHandler(out handler))
                    {
                        handler.Handle(tile);
                    }

                    tiles.Add(tile);
                    if (tileGraphic)  tileGraphics.Add(tileGraphic);
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

            Debug.Log("Build done");
        }
        #endregion

        [Serializable]
        private class CustomTileData
        {
            private Color m_guiColor = Color.white;

            private int m_index;
            private MapBuildView m_view;

            [GUIColor("$m_guiColor"), TableColumnWidth(100), OnValueChanged("OnTileChanged")]
            public CustomTile tile;

            [GUIColor("$m_guiColor"), TableColumnWidth(300), ReadOnly]
            public string description;

            [ColorPalette(PaletteName = "Tile"), TableColumnWidth(200)]
            public Color color;

            private void OnTileChanged()
            {
                description = tile.description;
            }

            public void Init(int index, MapBuildView view)
            {
                m_index = index;
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

        [Serializable]
        public class TileInfo
        {
            public TileInfo() : this(null, SpecialTileType.None, Direction.Up) { }

            public TileInfo(CustomTile tile, SpecialTileType specialTileType, Direction direction)
            {
                this.tile = tile;
                if (tile == null)
                {
                    data = new TileData
                    {
                        tileKey = "tile_empty",
                        buildableType = BuildableType.NONE,
                        passableMask = MotionMask.NONE
                    };
                    return;
                }
                this.specialTileType = specialTileType;
                dir = direction;
                TileData _data = tile?.tile?._data;
                if (_data != null)
                {
                    data = new TileData
                    {
                        tileKey = _data.tileKey,
                        heightType = _data.heightType,
                        buildableType = _data.buildableType,
                        passableMask = _data.passableMask,
                        playerSideMask = _data.playerSideMask,
                        blackboard = _data.blackboard.Count > 0 ? _data.blackboard : null,
                        effects = _data.effects.Length > 0 ? _data.effects : null
                    };
                }
            }

            public bool IsNull
            {
                get
                {
                    return tile == null;
                }
            }

            public CustomTile tile;
            public SpecialTileType specialTileType;
            public Direction dir;
            public TileData data;

            public bool TryGetSpecialTileTypeHandler(out SpecialTileTypeHandler handler)
            {
                handler = null;
                switch (specialTileType)
                {
                    case SpecialTileType.Start:
                        handler = new StartEndHandler("tile_start");
                        return true;
                    case SpecialTileType.End:
                        handler = new StartEndHandler("tile_end");
                        return true;
                    case SpecialTileType.FlyStart:
                        handler = new StartEndHandler("tile_flystart");
                        return true;
                }
                return false;
            }

            public void OnTriggleHeight()
            {
                if (data.heightType == TileData.HeightType.LOWLAND)
                    data.heightType = TileData.HeightType.HIGHLAND;
                else
                    data.heightType = TileData.HeightType.LOWLAND;
            }

            public enum Direction
            {
                Up,
                Down,
                Left,
                Right,
                Random
            }

            public enum SpecialTileType
            {
                None,
                Start,
                End,
                FlyStart
            }
        }
    }
}