using System;
using System.Collections.Generic;

namespace Torappu
{
	[Serializable]
	public class TileData
	{
		public enum HeightType
		{
			LOWLAND = 0,
			HIGHLAND = 1,
			E_NUM = 2
		}

		public string tileKey;

		public HeightType heightType;

		public BuildableType buildableType;

		public MotionMask passableMask;

		public PlayerSideMask playerSideMask;

		public List<Blackboard.DataPair> blackboard;

		public MapEffectData[] effects;
	}
}
