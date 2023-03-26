using System.Collections.Generic;
using Torappu.ObjectPool;
using UnityEngine;

namespace Torappu.Battle.UI
{
	public class UIController : SingletonMonoBehaviour<UIController>
	{
		[SerializeField]
		private Camera _uiCamera;

		[SerializeField]
		private Canvas _rootCanvas;

		[SerializeField]
		private Canvas _perspectiveCanvas;

		[SerializeField]
		private UITopBar _topBar;

		[SerializeField]
		private UICharacterInfoPanel _characterInfo;

		[SerializeField]
		private UIEnemyGiantBossInfoPanel _enemyBossInfo;

		[SerializeField]
		private UIFakeBlur _fakeBlur;

		[SerializeField]
		private UICardList _cardList;

		[SerializeField]
		private UIToastController _toastController;

		[SerializeField]
		private UIHintController _hintController;

		[SerializeField]
		private UIAutoBattlePanel _autoBattlePanel;

		[SerializeField]
		private UIBattleSystemMenuPanel _systemMenuPanel;

		[SerializeField]
		private UICostPanel _costPanel;

		[SerializeField]
		private RectTransform _hudPanel;

		[SerializeField]
		private RectTransform _tempPanel;

		[SerializeField]
		private UIUnitHUD _characterHud;

		[SerializeField]
		private UIUnitHUD _enemyHud;

		[SerializeField]
		private UIUnitHUD _enemyBossHud;

		[SerializeField]
		private UIUnitHUD _trapHud;

		[SerializeField]
		private UINumericText _damageText;

		[SerializeField]
		private UINumericText _healText;

		[SerializeField]
		private UINumericText _blockText;

		[SerializeField]
		private UIMessageText _messageText;

		[SerializeField]
		private UIMessageText _messageTextSlow;

		[SerializeField]
		private PoolManager.ObjectConfig[] _additionalPreloads;

		[SerializeField]
		private UIStateNode[] _states;

		[SerializeField]
		private UICharacterMenuState _characterMenuState;

		[SerializeField]
		private UIRemainingAvailableCharacter _remainingCharacter;

		[SerializeField]
		private List<RectTransform> _hideWhenDialog;

		[SerializeField]
		private RectTransform _groupRoot;

		[SerializeField]
		private RectTransform _groupStatic;

		[SerializeField]
		private RectTransform _groupTop;

		[SerializeField]
		private RectTransform _groupTopBar;

		[SerializeField]
		private RectTransform _groupDialogue;
	}
}
