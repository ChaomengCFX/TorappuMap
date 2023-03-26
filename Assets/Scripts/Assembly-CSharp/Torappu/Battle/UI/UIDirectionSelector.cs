using Torappu.UI;
using UnityEngine;

namespace Torappu.Battle.UI
{
	public class UIDirectionSelector : MonoBehaviour
	{
		[SerializeField]
		private UIFollower _follower;

		[SerializeField]
		private UIDirectionArrow[] _arrows;

		[SerializeField]
		private TwoStateFadeSwitcher _cancelHintPanel;
	}
}
