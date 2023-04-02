using Torappu.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle
{
	public class DialogPlaybackPanel : MonoBehaviour
	{
		[SerializeField]
		private DialogPlaybackTextView _avgPlaybackTextView;

		[SerializeField]
		private ScrollRect _scrollView;

		[SerializeField]
		private UIRecycleLayoutGroup _content;

		[SerializeField]
		private ContentSizeFitterHelper _fitterHelper;

		[SerializeField]
		private GameObject _closeBtn;
	}
}
