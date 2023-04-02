using DG.Tweening;
using UnityEngine;

namespace Torappu
{
	public abstract class BasicTween<ValueType> : MonoBehaviour
	{
		[SerializeField]
		private ValueType _from;

		[SerializeField]
		private ValueType _to;

		[SerializeField]
		private bool _startByFrom;

		[SerializeField]
		private float _duration;

		[SerializeField]
		private float _delay;

		[SerializeField]
		private Ease _easeType;

		[SerializeField]
		private int _loop;

		[SerializeField]
		private LoopType _loopType;

		[SerializeField]
		private bool _playWhenStart;

		[SerializeField]
		private bool _ignoreTimeScale;
	}
}
