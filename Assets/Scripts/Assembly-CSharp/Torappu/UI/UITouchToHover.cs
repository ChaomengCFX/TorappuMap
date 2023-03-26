using System;
using UnityEngine;
using UnityEngine.Events;

namespace Torappu.UI
{
	public class UITouchToHover : MonoBehaviour
	{
		[Serializable]
		public class OnHoverEvent : UnityEvent<bool>
		{
		}

		public OnHoverEvent onHover;
	}
}
