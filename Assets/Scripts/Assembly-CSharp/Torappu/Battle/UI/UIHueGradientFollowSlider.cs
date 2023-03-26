using System;
using System.Collections.Generic;
using UnityEngine;

namespace Torappu.Battle.UI
{
	public class UIHueGradientFollowSlider : UIFollowSlider
	{
		[Serializable]
		private struct keyColor
		{
			public float progress;

			public Color color;
		}

		[SerializeField]
		private List<keyColor> keyColors;
	}
}
