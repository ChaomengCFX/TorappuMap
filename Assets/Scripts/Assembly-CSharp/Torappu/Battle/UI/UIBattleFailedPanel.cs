using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Torappu.Battle.UI
{
	public class UIBattleFailedPanel : MonoBehaviour
	{
		[SerializeField]
		private float _fadeInTime;

		[SerializeField]
		private float _switchPageTime;

		[SerializeField]
		private Button _raycastBtn;

		[SerializeField]
		private RectTransform _continueLabel;

		[SerializeField]
		private RectTransform _practiceHintPanel;

		[SerializeField]
		private Text[] _tips;

		[SerializeField]
		private CanvasGroup _defaultPage;

		[SerializeField]
		private CanvasGroup _apProtectPage;

		[SerializeField]
		private CanvasGroup _powerScoreNotEnoughPage;

		[SerializeField]
		private UnityEvent _onPanelClose;

		[SerializeField]
		private GameObject _apProtectText_firstTry;

		[SerializeField]
		private GameObject _apProtectText_period;
	}
}
