using UnityEngine;

namespace CustomMap
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "CustomMap/LevelData")]
    public class LevelDataResource : ScriptableObject
    {
        public LevelData levelData;
    }
}