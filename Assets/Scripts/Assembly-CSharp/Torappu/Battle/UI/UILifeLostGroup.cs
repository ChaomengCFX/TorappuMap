using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle.UI
{
	public class UILifeLostGroup : MonoBehaviour
	{
		[SerializeField]
		private GameObject _enemyLostLabel;

		[SerializeField]
		private Text _enemyLostPoint;

		[SerializeField]
		private GameObject _othersLostLabel;

		[SerializeField]
		private Text _othersLostPoint;

		[SerializeField]
		private AnimationClip _moveDownAnimation;
	}
}
