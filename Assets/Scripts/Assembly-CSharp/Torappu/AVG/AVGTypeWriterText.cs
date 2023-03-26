using UnityEngine;

namespace Torappu.AVG
{
	public class AVGTypeWriterText : MonoBehaviour
	{
		[SerializeField]
		private float _typeWriterDelay;

		[SerializeField]
		private bool _ignoreTimeScale;

		[SerializeField]
		private bool _onMiddle;

		[SerializeField]
		private float _maxWidth;
	}
}
