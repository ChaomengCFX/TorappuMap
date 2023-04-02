using Torappu.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle.UI
{
	public class UIFollowSlider : UITextSlider
	{
		[SerializeField]
		private Slider _backSlider;

		[SerializeField]
		private bool _appendBackSliderToEnd;
	}
}
