using UnityEngine;

namespace Torappu.UI
{
	public class CancelDragIfFits : MonoBehaviour
	{
		[SerializeField]
		private GameObject _disableTargetIfCancel;

		[SerializeField]
		private GameObject _topArrow;

		[SerializeField]
		private GameObject _downArrow;

		[SerializeField]
		private GameObject _leftArrow;

		[SerializeField]
		private GameObject _rightArrow;
	}
}
