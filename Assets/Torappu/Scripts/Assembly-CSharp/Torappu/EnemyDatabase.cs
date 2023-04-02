using System;

namespace Torappu
{
	public class EnemyDatabase
	{
		[Serializable]
		public class EnemyData
		{
			public Undefinable<string> name;

			public Undefinable<string> description;

			public Undefinable<string> prefabKey;

			public AttributesData attributes;

			public Undefinable<int> lifePointReduce;

			public Undefinable<EnemyLevelType> levelType;

			public Undefinable<float> rangeRadius;

			public Undefinable<int> numOfExtraDrops;

			public Undefinable<float> viewRadius;

			public Blackboard talentBlackboard;

			public LevelData.EnemyData.ESkillData[] skills;

			public LevelData.EnemyData.ESpData spData;
		}

		[Serializable]
		public class AttributesData
		{
			public Undefinable<int> maxHp;

			public Undefinable<int> atk;

			public Undefinable<int> def;

			public Undefinable<float> magicResistance;

			public Undefinable<int> cost;

			public Undefinable<int> blockCnt;

			public Undefinable<float> moveSpeed;

			public Undefinable<float> attackSpeed;

			public Undefinable<float> baseAttackTime;

			public Undefinable<int> respawnTime;

			public Undefinable<float> hpRecoveryPerSec;

			public Undefinable<float> spRecoveryPerSec;

			public Undefinable<int> maxDeployCount;

			public Undefinable<int> massLevel;

			public Undefinable<int> baseForceLevel;

			public Undefinable<int> tauntLevel;

			public Undefinable<bool> stunImmune;

			public Undefinable<bool> silenceImmune;

			public Undefinable<bool> sleepImmune;

			public Undefinable<bool> frozenImmune;

			public Undefinable<bool> levitateImmune;
		}
	}
}
