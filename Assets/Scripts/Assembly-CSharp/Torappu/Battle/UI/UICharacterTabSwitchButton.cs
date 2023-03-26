using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle.UI
{
	public class UICharacterTabSwitchButton : MonoBehaviour
	{
		[SerializeField]
		private Image _splitImage;

		[SerializeField]
		private CanvasGroup _displayedPanel;

		[SerializeField]
		private Image _highlightTriangleGraphic;

		[SerializeField]
		private Image _highlightGraphic;

		[SerializeField]
		private Text _titleText;

		[SerializeField]
		private Color _textHideColor;

		[SerializeField]
		private Color _textShowColor;

		[SerializeField]
		private UICharacterTabGroup.TabInfomationStyleEnum _buttonType;
	}
}
