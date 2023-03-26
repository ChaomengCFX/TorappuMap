using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle.UI
{
	public class UIAutoBattlePanel : MonoBehaviour
	{
		[SerializeField]
		private Transform[] _autoBattleStates;

		[SerializeField]
		private Image _topMask;

		[SerializeField]
		private Image _bottomMask;

		[SerializeField]
		private Material _normalTopMaterial;

		[SerializeField]
		private Material _normalBottomMaterial;

		[SerializeField]
		private Color _errorColor;

		[SerializeField]
		private Sprite _errorMask;

		[SerializeField]
		private float _normalMaskHeight;

		[SerializeField]
		private float _errorMaskHeight;

		[SerializeField]
		private Animator[] _buttonAnimation;

		[SerializeField]
		private float _interval;
	}
}
