using Torappu.UI;
using UnityEngine;

namespace Torappu.Battle.UI
{
	public class UIFakeBlur : MonoBehaviour
	{
		[SerializeField]
		private UIFullScreenImage _fakeBlurImage;

		[SerializeField]
		private Shader _blurShader;

		[SerializeField]
		private float _fadeTime;
	}
}
