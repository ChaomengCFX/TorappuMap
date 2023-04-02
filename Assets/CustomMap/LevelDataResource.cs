using UnityEngine;
using Sirenix.OdinInspector;

namespace CustomMap
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "CustomMap/LevelData")]
    public class LevelDataResource : SerializedScriptableObject
    {
        [ShowInInspector]
        public LevelData LevelData
        {
            get
            {
                return m_levelData;
            }
            set
            {
                m_levelData = value;
            }
        }

        private LevelData m_levelData;
    }
}