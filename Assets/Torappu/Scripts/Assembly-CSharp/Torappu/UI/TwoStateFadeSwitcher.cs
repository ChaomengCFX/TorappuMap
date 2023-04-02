using UnityEngine;

namespace Torappu.UI
{
	public class TwoStateFadeSwitcher : MonoBehaviour
	{
		[SerializeField]
		private CanvasGroup _unselect;

		[SerializeField]
		private CanvasGroup _select;

		[SerializeField]
		private bool _setWithAwake;

		[SerializeField]
		private bool _ignoreTimeScale;

		[SerializeField]
		private float _fadeTime;
	}
}
