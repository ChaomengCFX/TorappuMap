using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SoftMasking
{
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	public class SoftMask : UIBehaviour
	{
		[Serializable]
		public enum MaskSource
		{
			Graphic = 0,
			Sprite = 1,
			Texture = 2
		}

		[Serializable]
		public enum BorderMode
		{
			Simple = 0,
			Sliced = 1,
			Tiled = 2
		}

		[SerializeField]
		private Shader _defaultShader;

		[SerializeField]
		private Shader _defaultETC1Shader;

		[SerializeField]
		private MaskSource _source;

		[SerializeField]
		private RectTransform _separateMask;

		[SerializeField]
		private Sprite _sprite;

		[SerializeField]
		private BorderMode _spriteBorderMode;

		[SerializeField]
		private Texture2D _texture;

		[SerializeField]
		private Rect _textureUVRect;

		[SerializeField]
		private Color _channelWeights;

		[SerializeField]
		private float _raycastThreshold;
	}
}
