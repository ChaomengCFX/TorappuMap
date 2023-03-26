using System.Collections.Generic;
using UnityEngine;

namespace Torappu.Battle
{
	public class Tile : VisualObject
	{
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
