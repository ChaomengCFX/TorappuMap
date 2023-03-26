using UnityEngine;

namespace Torappu.Battle.UI
{
	public abstract class UIPopup : MonoBehaviour
	{
		[SerializeField]
		private Vector2 _randomRange;

		[SerializeField]
		private float _lifeTime;
	}
}
