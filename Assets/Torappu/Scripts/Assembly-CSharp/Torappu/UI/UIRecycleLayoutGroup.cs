using System;
using UnityEngine;

namespace Torappu.UI
{
	public abstract class UIRecycleLayoutGroup : MonoBehaviour
	{
		[Serializable]
		protected struct Padding
		{
			public int top;

			public int left;

			public int bottom;

			public int right;
		}

		[SerializeField]
		private RectTransform _viewport;

		[SerializeField]
		private RectTransform _content;

		[SerializeField]
		private int _layoutPriority;

		[SerializeField]
		private Padding _padding;

		[SerializeField]
		private float _spacing;
	}
}
