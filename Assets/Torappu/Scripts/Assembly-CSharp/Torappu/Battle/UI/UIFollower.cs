using UnityEngine;

namespace Torappu.Battle.UI
{
	public class UIFollower : MonoBehaviour
	{
		[SerializeField]
		private Vector2 _moveThreshold;

		[SerializeField]
		private bool _inactiveWhenUnFollow;

		[SerializeField]
		private bool _targetInGameSpace;
	}
}
