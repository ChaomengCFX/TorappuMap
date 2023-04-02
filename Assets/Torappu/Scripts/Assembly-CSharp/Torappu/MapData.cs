using UnityEngine;
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

		[HideInInspector]
		public short[,] map;

		public TileData[] tiles;

		public Edge[] blockEdges;

		public string[] tags;

		public MapEffectData[] effects;

		public string[] layerRects;
	}
}
