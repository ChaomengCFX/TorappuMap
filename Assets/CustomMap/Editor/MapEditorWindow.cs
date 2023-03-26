using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using Torappu;
using Torappu.Battle;
using Torappu.Battle.Tiles;

namespace CustomMap
{
    public class MapEditorWindow : OdinMenuEditorWindow
    {
        [MenuItem("CustomMap/Map Editor")]
        private static void OpenWindow()
        {
            GetWindow<MapEditorWindow>().Show();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.Selection.SupportsMultiSelect = false;
            tree.Add("Map", new MapView());

            //tree.Add("Settings", GeneralDrawerConfig.Instance);
            //tree.Add("Utilities", new TextureUtilityEditor());
            //tree.AddAllAssetsAtPath("Odin Settings", "Assets/Plugins/Sirenix", typeof(ScriptableObject), true, true);
            return tree;
        }
    }

    public class MapView
    {
        public CustomTile tilePrefab;
        public Transform meshRoot;
        public Transform tileRoot;
        public Map map;

        [Button("Fill")]
        public void Fill(int x = 1, int y = 1)
        {
            int count = meshRoot.childCount;
            for (int i = 0; i < count; i++)
            {
                Object.DestroyImmediate(meshRoot.GetChild(0).gameObject);
            }

            count = tileRoot.childCount;
            for (int i = 0; i < count; i++)
            {
                Object.DestroyImmediate(tileRoot.GetChild(0).gameObject);
            }

            List<Tile> tiles = new List<Tile>();
            List<MeshTileGraphic> tileGraphics = new List<MeshTileGraphic>();
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    MeshTileGraphic tileGraphic = Object.Instantiate(tilePrefab.mesh, meshRoot);
                    tileGraphic.transform.localPosition = new Vector3(i, j, 0f);
                    Tile tile = Object.Instantiate(tilePrefab.tile, tileRoot);
                    tile.transform.localPosition = new Vector3(i, j, 0f);
                    string pos = string.Format("({0},{1})#", i, j);
                    tile.gameObject.name = pos + tile.gameObject.name;
                    tileGraphic.gameObject.name = pos + tileGraphic.gameObject.name;

                    tileGraphic.Tile = tile;
                    tileGraphic.GridPos = new GridPosition { row = i, col = j };
                    tile.Graphic = tileGraphic;
                    tile.AllGraphicList.Add(tileGraphic);

                    tiles.Add(tile);
                    tileGraphics.Add(tileGraphic);
                }
            }
            Map.Tiles2D tiles2D = new Map.Tiles2D
            {
                _tiles = tiles.ToArray(),
                _width = x,
                _height = y
            };
            map.Graphic.Graphics = tileGraphics.ToArray();
            map.Tile = tiles2D;
        }
    }

    public class TextureUtilityEditor
    {
        [BoxGroup("Tool"), HideLabel, EnumToggleButtons]
        public Tool Tool;

        public List<Texture> Textures;

        [Button(ButtonSizes.Large), HideIf("Tool", Tool.Rotate)]
        public void SomeAction() { }

        [Button(ButtonSizes.Large), ShowIf("Tool", Tool.Rotate)]
        public void SomeOtherAction() { }
    }
}