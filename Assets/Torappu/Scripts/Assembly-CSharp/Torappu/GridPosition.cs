using System;

namespace Torappu
{
	[Serializable]
	public struct GridPosition
	{
		[NonSerialized]
		public static readonly GridPosition ZERO;

		[NonSerialized]
		public static readonly GridPosition ONE;

		public int row;

		public int col;
	}
}
