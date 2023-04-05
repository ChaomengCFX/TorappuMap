using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector.Editor;

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
            tree.AddAssetAtPath("地图", "Assets/CustomMap/MapBuildView.asset");
            tree.AddAssetAtPath("路线", "Assets/CustomMap/RouteBuildView.asset");
            tree.AddAssetAtPath("波次", "Assets/CustomMap/WaveBuildView.asset");
            tree.AddAssetAtPath("LevelDataBuild", "Assets/CustomMap/LevelDataBuildView.asset");
            tree.AddAssetAtPath("LevelData", "Assets/CustomMap/LevelData.asset");
            foreach (OdinMenuItem item in tree)
            {
                (item.Value as IResetable)?.Reset();
            }
            return tree;
        }
    }
}