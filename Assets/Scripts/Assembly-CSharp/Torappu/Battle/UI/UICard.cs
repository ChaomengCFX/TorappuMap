using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle.UI
{
	public class UICard : MonoBehaviour
	{
		[Serializable]
		public struct ProfessionData
		{
			public ProfessionCategory profession;

			public Color color;
		}

		[SerializeField]
		private Image _avatarImage;

		[SerializeField]
		private Image _professionIcon;

		[SerializeField]
		private Image _professionMark;

		[SerializeField]
		private Image _rarityMark;

		[SerializeField]
		private Image _eliteIcon;

		[SerializeField]
		private Image _assistCharIcon;

		[SerializeField]
		private Text _costLabel;

		[SerializeField]
		private Image _remainingBackImage;

		[SerializeField]
		private Text _remainingCntLabel;

		[SerializeField]
		private Slider _respawnSlider;

		[SerializeField]
		private Text _respawnLabel;

		[SerializeField]
		private Toggle _toggle;

		[SerializeField]
		private ProfessionData[] _professionData;

		[SerializeField]
		private Sprite[] _rarityColors;

		[SerializeField]
		private Sprite[] _evolveIcons;

		[SerializeField]
		private Graphic[] _tintTargets;

		[SerializeField]
		private Color _defaultColor;

		[SerializeField]
		private Color _disableColor;

		[SerializeField]
		private float _toggleDuration;

		[SerializeField]
		private float _toggleOffset;

		[SerializeField]
		private float _fadeDuration;

		[SerializeField]
		private float _blinkDuration;

		[SerializeField]
		private float _blinkEasePart;

		[SerializeField]
		private Ease _blinkEaseStart;

		[SerializeField]
		private Ease _blinkEaseEnd;

		[SerializeField]
		private Image _comboImageUnder;

		[SerializeField]
		private Image _comboImageIcon;

		[SerializeField]
		private Image _mhImageUnder;

		[SerializeField]
		private Image _mhImageIcon;
	}
}
