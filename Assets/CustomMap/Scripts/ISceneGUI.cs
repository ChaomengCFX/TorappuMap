#if UNITY_EDITOR
using UnityEditor;

namespace CustomMap
{
    public interface ISceneGUI
    {
        void OnSceneGUI(SceneView sceneView);
    }
}
#endif