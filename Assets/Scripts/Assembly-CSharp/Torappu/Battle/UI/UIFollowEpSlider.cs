using System;
using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle.UI
{
	public class UIFollowEpSlider : UIFollowSlider
	{
		[Serializable]
		public struct ElementUIData
		{
			public Color elementColor;
		}

		[SerializeField]
		private Image _epAnimImage;

		[SerializeField]
		private Image _sliderFillBackImage;

		[SerializeField]
		private Image _epIconImage;

		[SerializeField]
		private Image _epColorImage;

		[SerializeField]
		private ElementUIData[] _elementDatas;
	}
}
