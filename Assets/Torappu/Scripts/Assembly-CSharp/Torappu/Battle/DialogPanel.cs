using Torappu.AVG;
using Torappu.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Torappu.Battle
{
	public class DialogPanel : MonoBehaviour
	{
		[SerializeField]
		private Button _skipButton;

		[SerializeField]
		private Text _charNameText;

		[SerializeField]
		private CanvasGroup _charNameRoot;

		[SerializeField]
		private EventTrigger _mask;

		[SerializeField]
		private AVGTypeWriterText _contentTypeWriter;

		[SerializeField]
		private GameObject _contentDeco;

		[SerializeField]
		private Image _characterSlotF;

		[SerializeField]
		private RectTransform _slotFOffset;

		[SerializeField]
		private Image _characterSlotB;

		[SerializeField]
		private RectTransform _slotBOffset;

		[SerializeField]
		private UIAtlasImage _characterEmpty;

		[SerializeField]
		private float _blackStart;

		[SerializeField]
		private float _blackEnd;

		[SerializeField]
		private float _fadeDuration;

		[SerializeField]
		private DialogDecisionButton _optButtonPrefab;

		[SerializeField]
		private CanvasGroup _optButtonRoot;

		[SerializeField]
		private DialogPlaybackPanel _playbackPanel;

		[SerializeField]
		private Graphic[] _fadeInOut;
	}
}
