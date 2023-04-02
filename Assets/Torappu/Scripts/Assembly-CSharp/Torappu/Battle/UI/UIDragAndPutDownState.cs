using UnityEngine;

namespace Torappu.Battle.UI
{
	public class UIDragAndPutDownState : UIStateNode
	{
		[SerializeField]
		private UIDirectionSelector _directionSelector;

		[SerializeField]
		private float _tileLocateRadius;

		[SerializeField]
		private Vector2 _touchOffset;
	}
}
