using UnityEngine;

namespace Torappu.Battle
{
	public class BattleController : SingletonMonoBehaviour<BattleController>
	{
		[SerializeField]
		private Map _map;

		[SerializeField]
		private Scheduler _scheduler;

		[SerializeField]
		private BattleFactory _factory;

		[SerializeField]
		private GridRangeDrawer _gridRangeDrawer;

		[SerializeField]
		private Transform _dragPlane;

		[SerializeField]
		private Transform[] _predefinedLocations;
	}
}
