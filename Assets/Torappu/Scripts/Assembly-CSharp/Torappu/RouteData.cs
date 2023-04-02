using System;
using UnityEngine;

namespace Torappu
{
	[Serializable]
	public class RouteData
	{
		[Serializable]
		public class CheckpointData
		{
			public CheckpointType type;

			public float time;

			public GridPosition position;

			public Vector2 reachOffset;

			public bool randomizeReachOffset;

			public float reachDistance;
		}

		public MotionMode motionMode;

		public GridPosition startPosition;

		public GridPosition endPosition;

		public Vector2 spawnRandomRange;

		public Vector2 spawnOffset;

		public CheckpointData[] checkpoints;

		public bool allowDiagonalMove;

		public bool visitEveryTileCenter;

		public bool visitEveryNodeCenter;

		public bool visitEveryCheckPoint;
	}
}
