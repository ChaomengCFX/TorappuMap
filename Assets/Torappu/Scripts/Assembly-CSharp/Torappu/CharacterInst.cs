using System;
using UnityEngine;

namespace Torappu
{
	[Serializable]
	public class CharacterInst
	{
		[Serializable]
		public struct Metadata
		{
			public string characterKey;

			public int level;

			public EvolvePhase phase;

			public int favorBattlePhase;

			public int potentialRank;

			[NonSerialized]
			public int playerInstId;
		}

		public Metadata inst;

		public int skillIndex;

		public int mainSkillLvl;

		public string skinId;

		public string tmplId;

		[HideInInspector]
		public Blackboard overrideSkillBlackboard;
	}
}
