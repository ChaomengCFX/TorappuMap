using UnityEngine;

namespace Torappu.Audio
{
	public class AudioClickPlayer : MonoBehaviour
	{
		private enum SoundType
		{
			Default = 0,
			Internal = 1,
			Custom = 2,
			Mute = 3,
			Building = 4
		}

		[SerializeField]
		private SoundType _soundType;

		[SerializeField]
		private UiInternalSoundType _internalType;

		[SerializeField]
		private UiBuildingSoundType _buildingSoundType;

		[SerializeField]
		private string _signal;

		[SerializeField]
		private string _subsignal;
	}
}
