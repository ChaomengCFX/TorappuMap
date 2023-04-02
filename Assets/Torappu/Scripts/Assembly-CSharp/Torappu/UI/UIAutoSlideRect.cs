using UnityEngine;

namespace Torappu.UI
{
	public class UIAutoSlideRect : MonoBehaviour
	{
		[SerializeField]
		private float _horizontalSpeed;

		[SerializeField]
		private float _verticalSpeed;

		[SerializeField]
		private float _beginWaitingTime;

		[SerializeField]
		private float _endWaitingTime;

		[SerializeField]
		private RectTransform _referenceRectTransform;

		[SerializeField]
		private float _textFadeDuration;

		[SerializeField]
		private bool _selfDrive;
	}
}
