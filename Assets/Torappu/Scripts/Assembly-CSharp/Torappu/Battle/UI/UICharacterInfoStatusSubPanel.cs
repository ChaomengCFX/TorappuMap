using System;
using Torappu.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle.UI
{
	public class UICharacterInfoStatusSubPanel : UICharacterInfoSubPanel
	{
		[Serializable]
		protected struct ProfessionSpritePair
		{
			public ProfessionCategory profession;

			public Sprite sprite;
		}

		[SerializeField]
		private Image _eliteIcon;

		[SerializeField]
		private Text _nameEnLabel;

		[SerializeField]
		private Text _nameCnLabel;

		[SerializeField]
		private Text _lvlLabel;

		[SerializeField]
		private Sprite[] _evolveIcons;

		[SerializeField]
		private Slider _hpSlider;

		[SerializeField]
		private Slider _spSlider;

		[SerializeField]
		private Text _hpLabel;

		[SerializeField]
		private Text _atkLabel;

		[SerializeField]
		private Text _defLabel;

		[SerializeField]
		private Text _magicResistLabel;

		[SerializeField]
		private Text _blockLabel;

		[SerializeField]
		private Text _spLabel;

		[SerializeField]
		private RectTransform _attackRangeContainer;

		[SerializeField]
		private UICharacterAttackRangeWidget _attackRangeWidget;

		[SerializeField]
		protected ProfessionSpritePair[] _professionIcons;

		[SerializeField]
		protected Image _professionImage;

		[SerializeField]
		private Image _uniEquipTypeIcon;
	}
}
