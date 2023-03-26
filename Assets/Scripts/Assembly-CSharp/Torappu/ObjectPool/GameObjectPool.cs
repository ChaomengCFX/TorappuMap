using System;
using UnityEngine;

namespace Torappu.ObjectPool
{
	public class GameObjectPool
	{
		public enum NotificationType
		{
			NONE = 0,
			SEND_MESSAGE = 1,
			BROADCAST_MESSAGE = 2
		}

		[Serializable]
		public struct Options
		{
			[HideInInspector]
			public static readonly Options DEFAULT;

			public int preloadSize;

			public int maxCapacity;

			public bool allowPoolAutoReuse;

			public NotificationType notificationType;
		}
	}
}
