using DG.Tweening;
using UnityEngine;

namespace Torappu.Battle
{
	public class CameraController : SingletonMonoBehaviour<CameraController>
	{
		[SerializeField]
		private Camera _camera;

		[SerializeField]
		private Camera _fakeCamera;

		[SerializeField]
		private Camera _uiPerspectiveCamera;

		[SerializeField]
		private Transform _offset;

		[SerializeField]
		private float _moveTime;

		[SerializeField]
		private Ease _easeType;

		[SerializeField]
		private Transform[] _placeholders;

		[SerializeField]
		private Vector2 _fromResolution;

		[SerializeField]
		private Vector2 _toResolution;

		[SerializeField]
		private Vector3 _fromLocalPosition;

		[SerializeField]
		private Vector3 _toLocalPosition;
	}
}
