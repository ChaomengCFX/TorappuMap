using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle.UI.Popup
{
	public class UIInfoToastPanel : UIToastController.UIToastSubPanel
	{
		[SerializeField]
		private Text _nameText;

		[SerializeField]
		private Text _descText;
	}
}
