using System;
using UnityEngine;

namespace Torappu.Battle
{
	public class MapGraphic : MonoBehaviour
	{
		[Serializable]
		public class MapSettings
		{
			public CameraViewLevel cameraView;

			public float highlandHeight;

			public float layerHeight;

			public string theme;
		}

		[Serializable]
		public class LightmapSettings
		{
			public bool repackLightmap;

			public bool bakeLightmapInXYPlane;

			public string skyBoxKey;

			public float indirectResolution;

			public float lightmapResolution;

			public float ambientIntensity;

			public float reflectionIntensity;

			public float indirectIntensity;

			public float albedoBoost;

			public bool compressLightmaps;

			public bool ambientOcclusionEnabled;

			public float aoMaxDistance;

			public float aoIndirectContribution;

			public float aoDirectContribution;

			public bool finalGatherEnabled;

			public int finalGatherRayCount;
		}

		[Serializable]
		public class EffectSettings
		{
			public string cameraEffect;

			public MapEffectData[] mapEffects;
		}

		public TileGraphic[] Graphics { set { _graphics = value; } }

        [SerializeField]
		private MapSettings _mapSettings;

		[SerializeField]
		private LightmapSettings _lightmapSettings;

		[SerializeField]
		private EffectSettings _effectSettings;

		[SerializeField]
		private TileGraphic[] _graphics;
	}
}
