using System.Collections.Generic;
using UnityEngine;

namespace Torappu.Battle
{
	public class Tile : VisualObject
	{
        [SerializeField]
        public string _tileKey;

		[SerializeField]
        public float _height;

		[SerializeField]
        public float _locateHeightOffset;

		[SerializeField]
        public bool _forceBoxCollider;

		[SerializeField]
        public TileGraphic _graphic;

		[SerializeField]
        public List<TileGraphic> _allGraphicList;

		[SerializeField]
        public string _effect;

		[SerializeField]
        public TileData _data;

		[SerializeField]
        public MapLayer _mapLayer;

		[SerializeField]
        public bool _injectEnvDmgFlagToBlackboard;
	}
}
