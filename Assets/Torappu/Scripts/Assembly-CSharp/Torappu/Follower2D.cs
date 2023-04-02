using UnityEngine;

namespace Torappu
{
	public class Follower2D : MonoBehaviour
	{
		[SerializeField]
		private Vector2 _moveThreshold;

		[SerializeField]
		private bool _inactiveWhenUnFollow;

		[SerializeField]
		private Transform _targetOnStart;
	}
}
