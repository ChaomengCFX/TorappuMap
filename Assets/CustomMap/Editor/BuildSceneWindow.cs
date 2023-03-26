using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

public class BuildSceneWindow : OdinEditorWindow
{
    [MenuItem("Assets/打包场景")]
    private static void OpenWindow()
    {
        GetWindow<BuildSceneWindow>().Show();
    }

    [SerializeField]
    private SceneAsset scene;
    [SerializeField]
    private string outPath = "D:/level_custom.unity3d";

    // 打包unity场景文件
    [Button("打包")]
    private void MyBuild(BuildTarget target = BuildTarget.Android)
    {
        if (!scene)
        {
            Debug.LogWarning("Need a scene to build");
            return;
        }
        string[] levels = new string[] { AssetDatabase.GetAssetPath(scene) };
        Debug.Log(string.Format("Building {0}", levels[0]));
        BuildPipeline.BuildPlayer(levels, outPath, target, BuildOptions.BuildAdditionalStreamedScenes);
        AssetDatabase.Refresh();
    }
}