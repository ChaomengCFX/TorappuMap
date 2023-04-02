using System;
using UnityEngine;

namespace Torappu
{
	[Serializable]
	public class MapEffectData
	{
		public string key;

		public Vector3 offset;

		public SharedConsts.Direction direction;
	}
}
