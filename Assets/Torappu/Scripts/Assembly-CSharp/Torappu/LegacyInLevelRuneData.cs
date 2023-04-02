using System;

namespace Torappu
{
	[Serializable]
	public class LegacyInLevelRuneData
	{
		public LevelData.Difficulty difficultyMask;

		public string key;

		public ProfessionCategory professionMask;

		public BuildableType buildableMask;

		public Blackboard blackboard;
	}
}
