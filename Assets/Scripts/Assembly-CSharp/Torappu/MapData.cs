using System;

namespace Torappu
{
	[Serializable]
	public class MapData
	{
		[Serializable]
		public class Edge
		{
			public GridPosition pos;

			public SharedConsts.Direction direction;

			public MotionMask blockMask;
		}
	}
}
