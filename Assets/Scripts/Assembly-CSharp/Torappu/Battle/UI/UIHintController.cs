using System.Collections.Generic;
using UnityEngine;

namespace Torappu.Battle.UI
{
	public class UIHintController : MonoBehaviour
	{
		[SerializeField]
		private List<UIHintBanner> _banners;

		[SerializeField]
		private Sprite[] _bannerBackgroundImage;

		[SerializeField]
		private bool _isDownward;

		[SerializeField]
		private float _scrollSpeed;

		[SerializeField]
		private float _gapHeight;

		[SerializeField]
		private float _midLine;

		[SerializeField]
		private float _fadeInDuration;

		[SerializeField]
		private float _fadeOutDuration;
	}
}
