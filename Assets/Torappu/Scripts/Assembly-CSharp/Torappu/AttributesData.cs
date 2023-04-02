using System;
using CodeStage.AntiCheat.ObscuredTypes;

namespace Torappu
{
	[Serializable]
	public class AttributesData
	{
		public ObscuredInt maxHp;

		public ObscuredInt atk;

		public ObscuredInt def;

		public ObscuredFloat magicResistance;

		public ObscuredInt cost;

		public ObscuredInt blockCnt;

		public ObscuredFloat moveSpeed;

		public ObscuredFloat attackSpeed;

		public ObscuredFloat baseAttackTime;

		public ObscuredInt respawnTime;

		public ObscuredFloat hpRecoveryPerSec;

		public ObscuredFloat spRecoveryPerSec;

		public ObscuredInt maxDeployCount;

		public ObscuredInt maxDeckStackCnt;

		public ObscuredInt tauntLevel;

		public ObscuredInt massLevel;

		public ObscuredInt baseForceLevel;

		public ObscuredInt maxEp;

		public ObscuredFloat epRecoveryPerSec;

		public bool stunImmune;

		public bool silenceImmune;

		public bool sleepImmune;

		public bool frozenImmune;

		public bool levitateImmune;
	}
}
