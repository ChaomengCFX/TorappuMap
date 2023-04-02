using System.Collections.Generic;
using Torappu.UI.Atlas;
using UnityEngine;

namespace Torappu.UI
{
	public class UIAtlasObject : ScriptableObject
	{
		[HideInInspector]
		[SerializeField]
		private List<AtlasSprite> _sprites;

		[SerializeField]
		private List<AtlasInfo> _atlases;

		[SerializeField]
		private string _workDir;

		[SerializeField]
		private bool _alphaSplit;

		[SerializeField]
		private AtlasSize _maxSize;

		[SerializeField]
		private AtlasCheckInfo _sign;
	}
}
