using System;

namespace Torappu
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
	}
}
