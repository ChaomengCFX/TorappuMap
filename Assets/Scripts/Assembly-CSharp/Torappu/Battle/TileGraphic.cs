using UnityEngine;

namespace Torappu.Battle
{
	public class TileGraphic : MonoBehaviour
	{
		public Tile Tile { set { _tile = value; } }

		public GridPosition GridPos { set { _gridPos = value; } }

		public Vector2 MapOffset { set { _mapOffset = value; } }

        [SerializeField]
		private Tile _tile;

		[SerializeField]
		private GridPosition _gridPos;

		[SerializeField]
		private Vector2 _mapOffset;
	}
}
