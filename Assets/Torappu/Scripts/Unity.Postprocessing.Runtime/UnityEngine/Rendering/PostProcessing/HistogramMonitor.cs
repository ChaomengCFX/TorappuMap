using System;

namespace UnityEngine.Rendering.PostProcessing
{
	[Serializable]
	public sealed class HistogramMonitor : Monitor
	{
		public enum Channel
		{
			Red = 0,
			Green = 1,
			Blue = 2,
			Master = 3
		}

		public int width;

		public int height;

		public Channel channel;
	}
}
