using System;

namespace Torappu
{
	[Serializable]
	public struct Undefinable<T>
	{
		public static readonly Undefinable<T> DEFAULT;

		private bool m_defined;

		private T m_value;
	}
}
