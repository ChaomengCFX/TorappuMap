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
			//[HideInInspector]
			public Tile[] _tiles;

			[SerializeField]
            //[HideInInspector]
            public int _width;

            //[HideInInspector]
            [SerializeField]
            public int _height;
		}

		public Tiles2D Tile { set { _tiles = value; } }

		public MapGraphic Graphic { get { return _graphic; } }

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
