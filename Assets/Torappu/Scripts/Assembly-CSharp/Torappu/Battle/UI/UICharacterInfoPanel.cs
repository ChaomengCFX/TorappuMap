using UnityEngine;

namespace Torappu.Battle.UI
{
	public class UICharacterInfoPanel : MonoBehaviour
	{
		[SerializeField]
		private float m_tweenTime;

		[SerializeField]
		private BattleIllustration _illustHandler;

		[SerializeField]
		private UICharacterInfoSubPanel _statusSubPanel;

		[SerializeField]
		private UICharacterInfoSubPanel _tabGroupSubPanel;
	}
}
