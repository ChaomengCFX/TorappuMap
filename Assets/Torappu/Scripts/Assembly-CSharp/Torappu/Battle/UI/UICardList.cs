using Torappu.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Torappu.Battle.UI
{
	public class UICardList : UIBehaviour
	{
		[SerializeField]
		private EasyInstancePool _instancePool;

		[SerializeField]
		private ToggleGroup _toggleGroup;

		[SerializeField]
		private LayoutGroup _layoutGroup;

		[SerializeField]
		private UIFollower _cardHighlighter;

		[SerializeField]
		private ScrollRect _scrollRect;

		[SerializeField]
		private Vector2 _cardWidthRange;

		[SerializeField]
		private EasyInstancePool _cardEffectHolderPool;
	}
}
