using UnityEngine;

namespace Torappu.UI
{
	public class EasyInstancePool : MonoBehaviour
	{
		[SerializeField]
		private int _testItemsCnt;

		[SerializeField]
		private Object _instancePrefab;

		[SerializeField]
		private bool _autoFillIndex;

		[SerializeField]
		private bool _autoAdjustPos;

		[SerializeField]
		private bool _setZAxisToZero;

		[SerializeField]
		private bool _clearOnDisable;

		[SerializeField]
		private Vector3 _slotsBeginPos;

		[SerializeField]
		private Vector2 _slotsOffsetPos;

		[SerializeField]
		private int _slotsMaxPerLineOrRow;

		[SerializeField]
		private bool _horizontal;
	}
}
