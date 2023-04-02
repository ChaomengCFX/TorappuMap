using Torappu.UI;
using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle
{
	public class DialogPlaybackTextView : MonoBehaviour
	{
		[SerializeField]
		private Text _name;

		[SerializeField]
		private Text _content;

		[SerializeField]
		private GameObject _current;

		[SerializeField]
		private GameObject _options;

		[SerializeField]
		private UIAtlasImage[] _optionsChosen;

		[SerializeField]
		private float _contentPadding;
	}
}
