using UnityEngine;
using UnityEngine.UI;

namespace Torappu.UI
{
	public class UISkillTagView : MonoBehaviour
	{
		[SerializeField]
		private SkillTagType _tagType;

		[SerializeField]
		private Image _colorTarget;

		[SerializeField]
		private Text _contentTarget;
	}
}
