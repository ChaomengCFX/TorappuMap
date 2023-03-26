using System;

namespace Torappu.Battle.Opera
{
	[Serializable]
	public struct OperaCommand
	{
		public string key;

		public OperaNodeArray operaNodes;

		public float duration;
	}
}
