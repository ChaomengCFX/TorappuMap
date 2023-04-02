using UnityEngine;

namespace Torappu.Battle
{
	[SelectionBase]
	public class TileGraphic : MonoBehaviour
	{
		[SerializeField]
        public Tile _tile;

		[SerializeField]
        public GridPosition _gridPos;

		[SerializeField]
        public Vector2 _mapOffset;
	}
}
