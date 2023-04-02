using System;
using System.Collections.Generic;

namespace Torappu
{
	[Serializable]
	public class ListDict<TKey, TValue> : List<KeyValuePair<TKey, TValue>>
	{
	}
}
