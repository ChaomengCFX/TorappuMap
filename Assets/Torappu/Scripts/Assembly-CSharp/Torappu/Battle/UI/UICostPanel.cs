using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle.UI
{
	public class UICostPanel : MonoBehaviour
	{
		[SerializeField]
		private Text _costLabel;

		[SerializeField]
		private Image _panelImage;

		[SerializeField]
		private Slider _costSlider;

		[SerializeField]
		private float _tweenTime;

		[SerializeField]
		private Color _tweenColor;
	}
}
