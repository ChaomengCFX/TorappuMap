using System.Collections.Generic;
using FullInspector.Internal;
using UnityEngine;

namespace FullInspector
{
	public abstract class fiValue<T> : fiValueProxyEditor
	{
		public T Value;

		[SerializeField]
		[HideInInspector]
		private string SerializedState;

		[HideInInspector]
		[SerializeField]
		private List<Object> SerializedObjectReferences;
	}
}
