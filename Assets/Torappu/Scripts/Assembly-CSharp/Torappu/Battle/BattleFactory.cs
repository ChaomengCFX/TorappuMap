using UnityEngine;

namespace Torappu.Battle
{
	public class BattleFactory : MonoBehaviour
	{
		[SerializeField]
		private PreviewCursor _walkCursor;

		[SerializeField]
		private PreviewCursor _flyCursor;

		[SerializeField]
		private Transform _enemyFolder;

		[SerializeField]
		private Transform _characterFolder;

		[SerializeField]
		private Transform _miscFolder;

		[SerializeField]
		private Transform _projectileFolder;

		[SerializeField]
		private Transform _effectFolder;

		[SerializeField]
		private Transform _mapWidgetFolder;

		[SerializeField]
		private Transform _cameraFolder;
	}
}
