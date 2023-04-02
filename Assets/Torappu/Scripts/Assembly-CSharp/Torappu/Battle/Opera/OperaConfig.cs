using System.Collections.Generic;
using UnityEngine;

namespace Torappu.Battle.Opera
{
	public class OperaConfig : ScriptableObject
	{
		[SerializeField]
		private List<OperaCommand> _commands;
	}
}
