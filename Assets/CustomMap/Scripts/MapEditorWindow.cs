#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;

namespace CustomMap
{
    public class MapEditorWindow : OdinMenuEditorWindow
    {
        private HashSet<ISceneGUI> m_SceneGUIs = new HashSet<ISceneGUI>();

        [MenuItem("CustomMap/Map Editor")]
        private static void OpenWindow()
        {
            GetWindow<MapEditorWindow>().Show();
        }

        private void OnDisable()
        {
            foreach (var item in m_SceneGUIs)
            {
                SceneView.onSceneGUIDelegate -= item.OnSceneGUI;
            }
            m_SceneGUIs.Clear();
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
                if (typeof(ISceneGUI).IsAssignableFrom(item.Value.GetType()))
                {
                    ISceneGUI obj = (ISceneGUI)item.Value;
                    m_SceneGUIs.Add(obj);
                    SceneView.onSceneGUIDelegate += obj.OnSceneGUI;
                }
            }
            return tree;
        }
    }
}
#endif