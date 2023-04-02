using System;
using Torappu.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle.UI
{
	public class UITopBar : MonoBehaviour
	{
		[Serializable]
		public class BasicStatus
		{
			public GameObject container;

			public UILifePoint lifePoint;

			public Text monsterInfoText;

			public Text killCntText;

			public Transform practiceHint;

			public UILifeLostGroup lifeLostContainer;
		}

		[SerializeField]
		private Image _pauseMask;

		[SerializeField]
		private UISwitchToggle _pauseButton;

		[SerializeField]
		private UISpeedSwitcher _speedSwitcher;

		[SerializeField]
		private Toggle _showAllRangeToggle;

		[SerializeField]
		private Button _systemMenuButton;

		[SerializeField]
		private BasicStatus _twoPartStatus;

		[SerializeField]
		private BasicStatus _threePartStatus;

		[SerializeField]
		private SpeedLevel _maxSpeedLevel;
	}
}
