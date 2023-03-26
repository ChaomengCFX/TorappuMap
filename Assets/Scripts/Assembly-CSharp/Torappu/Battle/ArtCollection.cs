using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle
{
	public class ArtCollection : SingletonMonoBehaviour<ArtCollection>
	{
		[SerializeField]
		private Sprite _defaultIcon;

		[SerializeField]
		private Image _defaultIllust;
	}
}
