using System;
using Torappu.UI.Atlas;
using UnityEngine;
using UnityEngine.UI;

namespace Torappu.UI
{
	public class UIAtlasImage : MaskableGraphic
	{
		public enum MeshType
		{
			SIMPLE = 0,
			SLICE = 1
		}

		[Serializable]
		private class AtlasSprite
		{
			public Texture2D mainTex;

			public Texture2D alphaTex;

			public AtlasCoord rect;

			public int atlasSize;

			public bool rotate;
		}

		[SerializeField]
		[HideInInspector]
		private UIAtlasObject _initAtlas;

		[SerializeField]
		[HideInInspector]
		private string _initSpriteId;

		[HideInInspector]
		[SerializeField]
		private bool _clipBorder;

		[HideInInspector]
		[SerializeField]
		private MeshType _meshType;

		[HideInInspector]
		[SerializeField]
		private Vector4 _sliceVec;

		[HideInInspector]
		[SerializeField]
		private AtlasSprite m_runtimeSprite;
	}
}
