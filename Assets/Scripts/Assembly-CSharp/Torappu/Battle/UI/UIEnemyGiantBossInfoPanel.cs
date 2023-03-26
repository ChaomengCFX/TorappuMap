using System;
using System.Collections.Generic;
using Torappu.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle.UI
{
	public class UIEnemyGiantBossInfoPanel : MonoBehaviour
	{
		[Serializable]
		private struct GiantBossInfoPrefabs
		{
			[SerializeField]
			public GameObject enemySpWarning;

			[SerializeField]
			public GameObject spSlider;

			[SerializeField]
			public GameObject spCastSlider;
		}

		[SerializeField]
		private RectTransform _scaleBody;

		[SerializeField]
		private UIFollower _follower;

		[SerializeField]
		private UITextSlider _hpSlider;

		[SerializeField]
		private UITextSlider _exHpSlider;

		[SerializeField]
		private Image _hitEffectImage;

		[SerializeField]
		private GameObject _background;

		[SerializeField]
		private GameObject _avatarParent;

		[SerializeField]
		private Image _avatarImage;

		[SerializeField]
		private Image _smallAvatarImage;

		[SerializeField]
		private List<GiantBossInfoPrefabs> _extraInfoType;

		[SerializeField]
		private List<RectTransform> _scaleXTweenTrans;

		[SerializeField]
		private List<RectTransform> _postScaleXTweenTrans;

		[SerializeField]
		private List<Image> _alphaTweenImages;

		[SerializeField]
		private List<Image> _postAlphaTweenImages;
	}
}
