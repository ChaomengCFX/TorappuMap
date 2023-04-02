using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	[Serializable]
	[Preserve]
	public sealed class Fog
	{
		public bool enabled;

		public bool excludeSkybox;
	}
}
