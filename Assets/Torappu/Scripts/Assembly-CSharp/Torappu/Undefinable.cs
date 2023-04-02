using Newtonsoft.Json;
using System;

namespace Torappu
{
	[Serializable]
	public struct Undefinable<T>
	{
		public static readonly Undefinable<T> DEFAULT;

		[JsonProperty]
		private bool m_defined;

        [JsonProperty]
        private T m_value;
	}
}
