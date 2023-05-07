#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using Torappu;

namespace CustomMap
{
    [Serializable]
    public class LevelData
    {
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

        public PredefinedData predefines;

        public PredefinedData hardPredefines;

        public string[] excludeCharIdList;

        public int randomSeed;

        public string operaConfig;

        public enum Difficulty
        {
            NONE,
            NORMAL,
            FOUR_STAR,
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
            public float preDelay;

            public float postDelay;

            public float maxTimeWaitingForNextWave;

            public FragmentData[] fragments;

            public string advancedWaveTag;

            [Serializable]
            public class FragmentData
            {
                public float preDelay;

                public ActionData[] actions;

                [Serializable]
                public class ActionData
                {
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

                    public enum ActionType
                    {
                        SPAWN,
                        PREVIEW_CURSOR,
                        STORY,
                        TUTORIAL,
                        PLAY_BGM,
                        DISPLAY_ENEMY_INFO,
                        ACTIVATE_PREDEFINED,
                        PLAY_OPERA,
                        TRIGGER_PREDEFINED,
                        BATTLE_EVENTS,
                        WITHDRAW_PREDEFINED,
                        E_NUM
                    }

                    public enum RandomType
                    {
                        ALWAYS,
                        PER_DAY,
                        PER_FULLGAME
                    }
                }
            }
        }

        [Serializable]
        public class BranchData
        {
            public PhaseData[] phases;

            [Serializable]
            public class PhaseData
            {
                public float preDelay;

                public WaveData.FragmentData.ActionData[] actions;
            }
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

        public class PredefinedData
        {
            public PredefinedCharacter[] characterInsts;

            public PredefinedCharacter[] tokenInsts;

            public PredefinedCard[] characterCards;

            public PredefinedTokenCard[] tokenCards;

            public class PredefinedInst : AdvancedCharacterInst
            {
                public bool hidden;

                public string alias;
            }

            public class PredefinedCharacter : PredefinedInst
            {
                public GridPosition position;

                public SharedConsts.Direction direction;
            }

            public class PredefinedCard : PredefinedInst
            {
            }

            public class PredefinedTokenCard : PredefinedCard
            {
                public int initialCnt;
            }
        }
    }
}
#endif