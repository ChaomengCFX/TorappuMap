using UnityEngine;

namespace Torappu.Battle.UI
{
	public class UIToastController : MonoBehaviour
	{
		public abstract class UIToastSubPanel : MonoBehaviour
		{
		}

		[SerializeField]
		private float _tweenDuration;

		[SerializeField]
		private UIToastSubPanel _infoPanel;

		[SerializeField]
		private UIToastSubPanel _enemyPanel;

		[SerializeField]
		private bool _ignoreTimeScale;
	}
}
