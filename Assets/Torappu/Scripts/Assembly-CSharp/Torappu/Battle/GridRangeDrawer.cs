using UnityEngine;

namespace Torappu.Battle
{
	public class GridRangeDrawer : MonoBehaviour
	{
		[SerializeField]
		private Material _rangeMaterial;

		[SerializeField]
		public Color _attackRangeColor;

		[SerializeField]
		public Color _healRangeColor;

		[SerializeField]
		public Color _skillRangeColor;

		[SerializeField]
		public Color _locateRangeColor;

		[SerializeField]
		public Color _overlapRangeColor;
	}
}
