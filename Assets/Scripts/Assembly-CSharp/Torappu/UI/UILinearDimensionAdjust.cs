using UnityEngine;

namespace Torappu.UI
{
	public class UILinearDimensionAdjust : MonoBehaviour
	{
		[SerializeField]
		private float _weightWidth;

		[SerializeField]
		private float _biasWidth;

		[SerializeField]
		private float _weightHeight;

		[SerializeField]
		private float _biasHeight;

		[SerializeField]
		private bool _applyWidth;

		[SerializeField]
		private bool _applyHeight;

		[SerializeField]
		private bool _useMinWidth;

		[SerializeField]
		private float _minWidth;

		[SerializeField]
		private bool _useMaxWidth;

		[SerializeField]
		private float _maxWidth;

		[SerializeField]
		private bool _useMinHeight;

		[SerializeField]
		private float _minHeight;

		[SerializeField]
		private bool _useMaxHeight;

		[SerializeField]
		private float _maxHeight;

		[SerializeField]
		private RectTransform _targetRect;
	}
}
