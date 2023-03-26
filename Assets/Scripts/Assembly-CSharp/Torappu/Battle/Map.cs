using System;
using UnityEngine;

namespace Torappu.Battle
{
	public class Map : SingletonMonoBehaviour<Map>
	{
		[Serializable]
		public class Tiles2D
		{
			[SerializeField]
			[HideInInspector]
			public Tile[] _tiles;

			[SerializeField]
			[HideInInspector]
			private int _width;

			[HideInInspector]
			[SerializeField]
			private int _height;
		}

		[SerializeField]
		private Transform _anchorTransform;

		[SerializeField]
		private Transform _tilesContainer;

		[SerializeField]
		private Transform _graphicContainer;

		[SerializeField]
		private Tiles2D _tiles;

		[SerializeField]
		private MapData.Edge[] _blockedEdges;

		[SerializeField]
		private DeathArea _deathArea;

		[SerializeField]
		private MapGraphic _graphic;
	}
}
