using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;

namespace CustomMap
{
    [CreateAssetMenu(fileName = "WaveBuildView", menuName = "CustomMap/WaveBuildView")]
    public class WaveBuildView : SerializedScriptableObject
    {
        [SerializeField]
        private List<WaveData> waveDatas = new List<WaveData>();

        public void ApplyTo(ref LevelData.WaveData[] waveData)
        {
            waveData = new LevelData.WaveData[waveDatas.Count];
            for (int i = 0; i < waveDatas.Count; i++)
            {
                waveData[i] = CloneUtil.CloneFields<WaveData, LevelData.WaveData>(waveDatas[i]);
            }
        }

        [Serializable]
        public class WaveData
        {
            [Serializable]
            public class FragmentData
            {
                [Serializable]
                public class ActionData
                {
                    public enum ActionType
                    {
                        SPAWN = 0,
                        PREVIEW_CURSOR = 1,
                        STORY = 2,
                        TUTORIAL = 3,
                        PLAY_BGM = 4,
                        DISPLAY_ENEMY_INFO = 5,
                        ACTIVATE_PREDEFINED = 6,
                        PLAY_OPERA = 7,
                        TRIGGER_PREDEFINED = 8,
                        BATTLE_EVENTS = 9,
                        WITHDRAW_PREDEFINED = 10,
                        E_NUM = 11
                    }

                    public enum RandomType
                    {
                        ALWAYS = 0,
                        PER_DAY = 1,
                        PER_FULLGAME = 2
                    }

                    public ActionType actionType;

                    public bool managedByScheduler;

                    public string key;

                    public int count;

                    public float preDelay;

                    public float interval;

                    public int routeIndex;

                    public bool blockFragment;

                    public bool autoPreviewRoute;

                    public bool autoDisplayEnemyInfo;

                    public bool isUnharmfulAndAlwaysCountAsKilled;

                    public string hiddenGroup;

                    public string randomSpawnGroupKey;

                    public string randomSpawnGroupPackKey;

                    public RandomType randomType;

                    public int weight;

                    public bool dontBlockWave;
                }

                public float preDelay;

                public ActionData[] actions;
            }

            public float preDelay;

            public float postDelay;

            public float maxTimeWaitingForNextWave;

            public FragmentData[] fragments;

            public string advancedWaveTag;
        }
    }
}
