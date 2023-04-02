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

namespace CustomMap
{
    [CreateAssetMenu(fileName = "MapBuildView", menuName = "CustomMap/MapBuildView")]
    public class MapBuildView : ScriptableObject, IResetable
    {
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
        }

        [Space(20)]
        [SerializeField, TableList, LabelText("Tile预制体"), OnCollectionChanged("OnTilesListChanged")]
        private List<CustomTileData> tiles;

        private void OnTilesListChanged(CollectionChangeInfo info, object _)
        {
            if (info.ChangeType == CollectionChangeType.Add)
            {
                tiles[tiles.Count - 1].Init(tiles.Count - 1, SelectTile);
            }
        }

        [ShowInInspector, ReadOnly, LabelText("当前选中"), GUIColor(0f, 0.9f, 1f)]
        private CustomTile tileSelected;
        [SerializeField, LabelText("Tile特殊类型"), EnumToggleButtons]
        private TileInfo.SpecialTileType specialTileType = TileInfo.SpecialTileType.None;

        public void SelectTile(int index)
        {
            tileSelected = tiles[index].tile;
            for (int i = 0; i < tiles.Count; i++)
            {
                if (i == index) continue;
                tiles[i].OnDeSelect();
            }
        }

        [Title("绘制TileMap")]
        [ShowInInspector, TableMatrix(SquareCells = true, DrawElementMethod = "DrawTileMapElement")]
        private TileInfo[,] tileMap;

        private TileInfo DrawTileMapElement(Rect rect, TileInfo value)
        {
            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
            {
                value = new TileInfo(tileSelected, specialTileType);
                GUI.changed = true;
                Event.current.Use();
            }
            if (value != null && value.tile != null)
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
                    if (info == null) continue;
                    CustomTile tilePrefab = info.tile;
                    MeshTileGraphic tileGraphic = Instantiate(tilePrefab.mesh, meshRoot);
                    tileGraphic.transform.localPosition += new Vector3(col, row, 0f);
                    Tile tile = Instantiate(tilePrefab.tile, tileRoot);
                    tile.transform.localPosition += new Vector3(col, row, 0f);
                    string pos = string.Format("({0},{1})#", row, col);
                    tile.gameObject.name = pos + tile.gameObject.name;
                    tileGraphic.gameObject.name = pos + tileGraphic.gameObject.name;

                    tileGraphic._tile = tile;
                    tileGraphic._gridPos = new GridPosition { row = row, col = col };
                    tile._graphic = tileGraphic;
                    tile._allGraphicList.Add(tileGraphic);

                    SpecialTileTypeHandler handler;
                    if (info.TryGetSpecialTileTypeHandler(out handler))
                    {
                        handler.Handle(tile);
                    }

                    tiles.Add(tile);
                    tileGraphics.Add(tileGraphic);
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

        [Serializable]
        private class CustomTileData
        {
            private Color m_guiColor = Color.white;

            private int m_index;
            private Action<int> m_onSelect;

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

            public void Init(int index, Action<int> onSelect)
            {
                m_index = index;
                m_onSelect = onSelect;
            }

            public void OnDeSelect()
            {
                m_guiColor = Color.white;
            }

            [Button("选中", ButtonSizes.Small), GUIColor("$m_guiColor"), TableColumnWidth(80)]
            private void Select()
            {
                m_guiColor = Color.green;
                m_onSelect(m_index);
            }
        }

        [Serializable]
        private class TileInfo
        {
            public TileInfo(CustomTile tile, SpecialTileType specialTileType)
            {
                this.tile = tile;
                this.specialTileType = specialTileType;
            }

            public CustomTile tile;
            public SpecialTileType specialTileType;

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