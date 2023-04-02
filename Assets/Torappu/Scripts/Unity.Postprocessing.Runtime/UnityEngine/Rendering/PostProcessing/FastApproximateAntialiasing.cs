using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	[Serializable]
	[Preserve]
	public sealed class FastApproximateAntialiasing
	{
		public bool fastMode;

		public bool keepAlpha;
	}
}
