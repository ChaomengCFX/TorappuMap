using UnityEngine;

namespace Torappu.I18N
{
	public class LocalizeTextUIConfig : MonoBehaviour
	{
		public enum PlaceholderType
		{
			PlaceholderByCode = 0,
			ManualPlaceholder = 1,
			ManualNonPlaceholder = 2
		}

		public PlaceholderType type;

		public string pathRecord;
	}
}
