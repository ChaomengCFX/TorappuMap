using Torappu.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle.UI
{
	public class UICharacterInfoTabGroupSubPanel : UICharacterInfoSubPanel
	{
		[SerializeField]
		private UICharacterTabGroup _uiCharacterTabGroup;

		[SerializeField]
		private Text _subProfessionTraitDescriptionLabel;

		[SerializeField]
		private Text _traitDescriptionText;

		[SerializeField]
		private Text _subProfessionText;

		[SerializeField]
		private Image _subProfessionImage;

		[SerializeField]
		private UIAutoSlideRect _skillDescAutoSlide;

		[SerializeField]
		private Text _skillNameLabel;

		[SerializeField]
		private Text _skillDescriptionLabel;

		[SerializeField]
		private Image _skillIcon;

		[SerializeField]
		private UISkillTagGroup _skillTagGroup;

		[SerializeField]
		private RectTransform _skillPanel;

		[SerializeField]
		private UICharacterTalentPair _talentTextPair;

		[SerializeField]
		private LayoutGroup _talentLayoutGroup;
	}
}
