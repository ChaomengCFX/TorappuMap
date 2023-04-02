using UnityEngine;
using UnityEngine.UI;

namespace Torappu.UI
{
	public class UITextSlider : MonoBehaviour
	{
		public enum TextMode
		{
			A_SLASH_B = 0,
			ONLY_NUMBER = 1,
			PERCENTAGE = 2
		}

		[SerializeField]
		protected Slider _slider;

		[SerializeField]
		protected Image _sliderFillImage;

		[SerializeField]
		private Text _text;

		[SerializeField]
		private TextMode _textMode;
	}
}
