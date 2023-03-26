using UnityEngine;

namespace Torappu.Battle
{
	public class TileGraphic : MonoBehaviour
	{
		[SerializeField]
		private Tile _tile;

		[SerializeField]
		private GridPosition _gridPos;

		[SerializeField]
		private Vector2 _mapOffset;
	}
}
