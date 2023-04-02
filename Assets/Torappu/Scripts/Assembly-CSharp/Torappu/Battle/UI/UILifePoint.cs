using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle.UI
{
	public class UILifePoint : MonoBehaviour
	{
		[SerializeField]
		private Text _lifePointLabel;

		[SerializeField]
		private float _tweenTime;

		[SerializeField]
		private Image[] _images;
	}
}
