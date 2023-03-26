using Torappu.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle.UI
{
	public class UIUnitHUD : MonoBehaviour
	{
		[SerializeField]
		private UIFollower _follower;

		[SerializeField]
		private UITextSlider _hpSlider;

		[SerializeField]
		private UITextSlider _spSlider;

		[SerializeField]
		private UITextSlider _overloadSpTintSlider;

		[SerializeField]
		private UITextSlider _overloadSpSlider;

		[SerializeField]
		private UIFollowEpSlider _epSlider;

		[SerializeField]
		private UITextSlider _spCastSlider;

		[SerializeField]
		private UIBulletBar _bulletBar;

		[SerializeField]
		private UIBulletBar _overloadBulletSlider;

		[SerializeField]
		private RectTransform _manualSkillMark;

		[SerializeField]
		private RectTransform _manualSkillMarkEnhance;

		[SerializeField]
		private RectTransform _manualSkillSuspendable;

		[SerializeField]
		private RectTransform _autoSkillMark;

		[SerializeField]
		private RectTransform _groupSp;

		[SerializeField]
		private Text _skillCntLabel;

		[SerializeField]
		private Text _debugNameLabel;

		[SerializeField]
		private Transform _sliderGroup;

		[SerializeField]
		private Transform _popupTransform;

		[SerializeField]
		private Transform _pluginTransform;

		[SerializeField]
		private Transform _root;
	}
}
