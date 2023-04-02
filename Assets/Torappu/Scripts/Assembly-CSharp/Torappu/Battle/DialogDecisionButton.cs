using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle
{
	public class DialogDecisionButton : MonoBehaviour
	{
		[SerializeField]
		private Text _optionText;

		[SerializeField]
		private Button _optionButton;

		[SerializeField]
		private float _fadeTime;

		[SerializeField]
		private Ease _easeType;

		[SerializeField]
		private Graphic[] _fade;
	}
}
