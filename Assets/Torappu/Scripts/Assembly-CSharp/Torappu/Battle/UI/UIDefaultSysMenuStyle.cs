using Torappu.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle.UI
{
	public class UIDefaultSysMenuStyle : UIBattleSystemMenuPanel.StyleController
	{
		[SerializeField]
		private UITextSlider _progressSlider;

		[SerializeField]
		private Text _descriptionLabel;

		[SerializeField]
		private UIBattleSystemMenuCostReturn _costReturnItem;
	}
}
