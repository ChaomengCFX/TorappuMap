using UnityEngine;

namespace Torappu
{
	public class FpsController : MonoBehaviour
	{
		public enum FpsMode
		{
			BATTLE = 0,
			UI = 1,
			BUILDING = 2,
			UNLOCK = 3,
			E_NUM = 4
		}

		[SerializeField]
		private bool _lockTargetFps;

		[SerializeField]
		private bool _makeNeverSleep;

		[SerializeField]
		private FpsMode _targetFpsMode;

		[SerializeField]
		private float _periodToProfile;

		[SerializeField]
		private int _fpsFontSize;

		[SerializeField]
		private Color _fpsFontColor;

		[SerializeField]
		private Rect _fpsRect;
	}
}
