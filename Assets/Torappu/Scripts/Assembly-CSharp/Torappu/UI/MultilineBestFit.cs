using UnityEngine;
using UnityEngine.EventSystems;

namespace Torappu.UI
{
	public class MultilineBestFit : UIBehaviour
	{
		[SerializeField]
		private bool _binary;

		[SerializeField]
		private bool _ignoreBlankEnd;

		[SerializeField]
		private int _minSize;

		[SerializeField]
		private bool _autoAlignmentWhenOnlyOneLine;

		[SerializeField]
		private TextAnchor _textAnchorWhenOnlyOneLine;
	}
}
