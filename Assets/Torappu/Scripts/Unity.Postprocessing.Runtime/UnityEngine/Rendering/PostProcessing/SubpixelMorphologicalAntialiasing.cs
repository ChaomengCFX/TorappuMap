using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	[Serializable]
	[Preserve]
	public sealed class SubpixelMorphologicalAntialiasing
	{
		public enum Quality
		{
			Low = 0,
			Medium = 1,
			High = 2
		}

		public Quality quality;
	}
}
