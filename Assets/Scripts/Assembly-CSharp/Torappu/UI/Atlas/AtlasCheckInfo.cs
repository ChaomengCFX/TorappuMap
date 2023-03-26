using System;
using System.Collections.Generic;
using UnityEngine;

namespace Torappu.UI.Atlas
{
	[Serializable]
	public class AtlasCheckInfo
	{
		[Serializable]
		private struct Sign
		{
			public static readonly Sign EMPTY;

			public string name;

			public string guid;

			public string md5;
		}

		[SerializeField]
		private List<Sign> m_sprites;

		[SerializeField]
		private List<Sign> m_atlases;

		[SerializeField]
		private List<Sign> m_alphas;
	}
}
