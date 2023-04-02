using System.Collections.Generic;
using UnityEngine;

namespace Torappu.Battle.UI
{
	public class UICharacterTabGroup : MonoBehaviour
	{
		public enum TabInfomationStyleEnum
		{
			SKILL = 0,
			SUBPROFESSION_TRAIT = 1,
			TRAIT = 2,
			TALENT = 3,
			LEGIONMODE_BUFF = 4,
			ENUM = 5
		}

		[SerializeField]
		private Transform _root;

		[SerializeField]
		private RectTransform _rectRoot;

		[SerializeField]
		private Transform _infoTabRoot;

		[SerializeField]
		private Transform _infoTabDetailPanelRoot;

		[SerializeField]
		private List<float> _widthWhenShow;
	}
}
