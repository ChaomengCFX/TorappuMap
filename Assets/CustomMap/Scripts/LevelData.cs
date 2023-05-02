#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Torappu;

namespace CustomMap
{
    [Serializable]
    public class LevelData
    {
        public enum Difficulty
        {
            NONE = 0,
            NORMAL = 1,
            FOUR_STAR = 2,
            EASY = 4,
            ALL = 7
        }

        [Serializable]
        public class Options
        {
            public int characterLimit;

            public int maxLifePoint;

            public int initialCost;

            public int maxCost;

            public float costIncreaseTime;

            public float moveMultiplier;

            public bool steeringEnabled;

            public bool isTrainingLevel;

            public bool isHardTrainingLevel;

            public bool isPredefinedCardsSelectable;

            public float maxPlayTime;

            public BattleFunctionDisableMask functionDisableMask;

            public Blackboard configBlackBoard;
        }

        [Serializable]
        public class EnemyData
        {
            [Serializable]
            public class ESkillData
            {
                public string prefabKey;

                public int priority;

                public float cooldown;

                public float initCooldown;

                public int spCost;

                public Blackboard blackboard;
            }

            [Serializable]
            public class ESpData
            {
                public SpType spType;

                public int maxSp;

                public int initSp;

                public float increment;
            }

            public string name;

            public string description;

            public string key;

            public AttributesData attributes;

            public bool notCountInTotal;

            public string alias;

            public int lifePointReduce;

            public float rangeRadius;

            public int numOfExtraDrops;

            public float viewRadius;

            public EnemyLevelType levelType;

            public Blackboard talentBlackboard;

            public ESkillData[] skills;

            public ESpData spData;
        }

        [Serializable]
        public class EnemyDataDbReference
        {
            public bool useDb;

            public string id;

            public int level;

            public EnemyDatabase.EnemyData overwrittenData;
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

        [Serializable]
        public class BranchData
        {
            [Serializable]
            public class PhaseData
            {
                public float preDelay;

                public WaveData.FragmentData.ActionData[] actions;
            }

            public PhaseData[] phases;
        }

        [Serializable]
        public class GlobalBuffData
        {
            public string prefabKey;

            public Blackboard blackboard;

            public string overrideCameraEffect;

            public bool passProfessionMaskFlag;

            public ProfessionCategory professionMask;
        }

        public Options options;

        public string levelId;

        public string mapId;

        public string bgmEvent;

        public string environmentSe;

        public MapData mapData;

        public List<GridPosition> tilesDisallowToLocate;

        public LegacyInLevelRuneData[] runes;

        public GlobalBuffData[] globalBuffs;

        public RouteData[] routes;

        public RouteData[] extraRoutes;

        public EnemyData[] enemies;

        public EnemyDataDbReference[] enemyDbRefs;

        public WaveData[] waves;

        public ListDict<string, BranchData> branches;

        public string[] excludeCharIdList;

        public int randomSeed;

        public string operaConfig;
    }
}
#endif