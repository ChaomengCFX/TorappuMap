using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	[Serializable]
	[Preserve]
	public sealed class TemporalAntialiasing
	{
		public float jitterSpread;

		public float sharpness;

		public float stationaryBlending;

		public float motionBlending;
	}
}
