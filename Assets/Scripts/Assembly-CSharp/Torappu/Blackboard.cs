using System;
using System.Collections.Generic;

namespace Torappu
{
	[Serializable]
	public class Blackboard : List<Blackboard.DataPair>
	{
		[Serializable]
		public struct DataPair
		{
			public string key;

			public float value;

			public string valueStr;
		}
	}
}
