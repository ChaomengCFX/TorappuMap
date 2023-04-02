using Torappu.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle.UI.Popup
{
	public class UIEnemyToastPanel : UIToastController.UIToastSubPanel
	{
		[SerializeField]
		private Image _enemyIcon;

		[SerializeField]
		private Text _enemyName;

		[SerializeField]
		private Text _enemyDesc;

		[SerializeField]
		private Slider _processSlider;

		[SerializeField]
		private UISwitchToggle _pauseToggle;
	}
}
