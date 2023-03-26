using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle.UI
{
	public class UICharacterMenuPanel : MonoBehaviour
	{
		[SerializeField]
		private Button _skillButton;

		[SerializeField]
		private Button _withdrawButton;

		[SerializeField]
		private Transform _withdrawPanel;

		[SerializeField]
		private Slider _skillToNextSlider;

		[SerializeField]
		private Slider _skillCastingSlider;

		[SerializeField]
		private Image _skillIcon;

		[SerializeField]
		private Image _skillReadyMark;

		[SerializeField]
		private Image _skillNotReadyMark;

		[SerializeField]
		private Image _skillStopMark;

		[SerializeField]
		private Image _skillBulletMark;

		[SerializeField]
		private Text _skillBulletLabel;

		[SerializeField]
		private Image _skillReadyButShowStackProcessMark;

		[SerializeField]
		private MaskableGraphic _skillAutoMark;

		[SerializeField]
		private Transform _skillAmountPanel;

		[SerializeField]
		private Text _skillAmountNum;

		[SerializeField]
		private Text _skillProgressLabel;

		[SerializeField]
		private Color _skillProgressLabelNormalColor;

		[SerializeField]
		private Color _skillProgressLabelStackColor;

		[SerializeField]
		private Toggle _skillRangeToggle;

		[SerializeField]
		private Transform _skillPanel;

		[SerializeField]
		private Follower2D _follower;
	}
}
