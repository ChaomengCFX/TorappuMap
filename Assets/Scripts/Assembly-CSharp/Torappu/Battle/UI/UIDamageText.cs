using UnityEngine;

namespace Torappu.Battle.UI
{
	public class UIDamageText : UINumericText
	{
		[SerializeField]
		private Vector2 _upOffset;

		[SerializeField]
		private Vector2 _downOffset;

		[SerializeField]
		private float _fontSizeScale;
	}
}
