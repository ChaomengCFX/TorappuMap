using System.Collections.Generic;
using UnityEngine;

namespace Torappu.Battle
{
	public class Tile : VisualObject
	{
		public string TileKey { set { _tileKey = value; } }
		public float Height { set { _height = value; } }
		public float LocateHeightOffset { set { _locateHeightOffset = value; } }
		public bool ForceBoxCollider { set { _forceBoxCollider = value; } }
		public TileGraphic Graphic { set { _graphic = value; } }
		public List<TileGraphic> AllGraphicList { get { return _allGraphicList; } }
		public string Effect { set { _effect = value; } }
		public TileData Data { set { _data = value; } }
		public MapLayer MapLayer { set { _mapLayer = value; } }
		public bool InjectEnvDmgFlagToBlackboard { set { _injectEnvDmgFlagToBlackboard = value; } }

        [SerializeField]
		private string _tileKey;

		[SerializeField]
		private float _height;

		[SerializeField]
		private float _locateHeightOffset;

		[SerializeField]
		private bool _forceBoxCollider;

		[SerializeField]
		private TileGraphic _graphic;

		[SerializeField]
		private List<TileGraphic> _allGraphicList;

		[SerializeField]
		private string _effect;

		[SerializeField]
		private TileData _data;

		[SerializeField]
		private MapLayer _mapLayer;

		[SerializeField]
		private bool _injectEnvDmgFlagToBlackboard;
	}
}
