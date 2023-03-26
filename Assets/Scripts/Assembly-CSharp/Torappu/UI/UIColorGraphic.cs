using UnityEngine;
using UnityEngine.UI;

namespace Torappu.UI
{
	public class UIColorGraphic : NonDrawingGraphic
	{
		[SerializeField]
		private Graphic[] _colorElements;

		[HideInInspector]
		[SerializeField]
		private Color _color;
	}
}
