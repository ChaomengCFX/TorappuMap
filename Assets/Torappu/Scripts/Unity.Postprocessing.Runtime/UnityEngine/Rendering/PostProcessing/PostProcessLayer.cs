using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[ImageEffectAllowedInSceneView]
	public class PostProcessLayer : MonoBehaviour
	{
		public enum Antialiasing
		{
			None = 0,
			FastApproximateAntialiasing = 1,
			SubpixelMorphologicalAntialiasing = 2,
			TemporalAntialiasing = 3
		}

		[Serializable]
		public sealed class SerializedBundleRef
		{
			public string assemblyQualifiedName;
		}

		public Transform volumeTrigger;

		public LayerMask volumeLayer;

		public bool stopNaNPropagation;

		public bool finalBlitToCameraTarget;

		public Antialiasing antialiasingMode;

		public TemporalAntialiasing temporalAntialiasing;

		public SubpixelMorphologicalAntialiasing subpixelMorphologicalAntialiasing;

		public FastApproximateAntialiasing fastApproximateAntialiasing;

		public Fog fog;

		public PostProcessDebugLayer debugLayer;

		[SerializeField]
		private PostProcessResources m_Resources;

		[Preserve]
		[SerializeField]
		private bool m_ShowToolkit;

		[Preserve]
		[SerializeField]
		private bool m_ShowCustomSorter;

		public bool breakBeforeColorGrading;

		[SerializeField]
		private List<SerializedBundleRef> m_BeforeTransparentBundles;

		[SerializeField]
		private List<SerializedBundleRef> m_BeforeStackBundles;

		[SerializeField]
		private List<SerializedBundleRef> m_AfterStackBundles;
	}
}
