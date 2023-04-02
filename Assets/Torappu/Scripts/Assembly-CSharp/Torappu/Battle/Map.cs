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

		[SerializeField]
        public Transform _anchorTransform;

		[SerializeField]
        public Transform _tilesContainer;

		[SerializeField]
        public Transform _graphicContainer;

		[SerializeField]
        public Tiles2D _tiles;

		[SerializeField]
        public MapData.Edge[] _blockedEdges;

		[SerializeField]
        public DeathArea _deathArea;

		[SerializeField]
        public MapGraphic _graphic;
	}
}
