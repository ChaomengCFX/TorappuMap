using UnityEngine;

namespace Torappu.Battle
{
	public class PreviewCursor : BObject
	{
		[SerializeField]
		private float _height;

		[SerializeField]
		private float _moveSpeed;

		[SerializeField]
		private bool _faceToDirection;

		[SerializeField]
		private int _times;

		[SerializeField]
		private Transform _bodyTransform;

		[SerializeField]
		private float _delayToRecycle;
	}
}
