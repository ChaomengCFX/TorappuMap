using System;

namespace UnityEngine.Rendering.PostProcessing
{
	[Serializable]
	public sealed class TemporalAntialiasing
	{
		public float jitterSpread;

		public float sharpness;

		public float stationaryBlending;

		public float motionBlending;
	}
}
