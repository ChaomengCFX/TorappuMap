using UnityEngine;

namespace Torappu.Battle.UI
{
	public class UIBattleSystemMenuPanel : MonoBehaviour
	{
		public abstract class StyleController : MonoBehaviour
		{
		}

		[SerializeField]
		private float _fadeInTime;

		[SerializeField]
		private StyleController[] _menuStyles;

		[SerializeField]
		private GameObject _debugPanel;
	}
}
