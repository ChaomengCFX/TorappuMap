using UnityEngine;

namespace Torappu.Battle
{
	public class DeathArea : MonoBehaviour
	{
		[SerializeField]
		private BoxCollider2D _up;

		[SerializeField]
		private BoxCollider2D _down;

		[SerializeField]
		private BoxCollider2D _left;

		[SerializeField]
		private BoxCollider2D _right;
	}
}
