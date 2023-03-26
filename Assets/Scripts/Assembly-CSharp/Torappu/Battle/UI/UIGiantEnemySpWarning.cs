using UnityEngine;
using UnityEngine.UI;

namespace Torappu.Battle.UI
{
	public class UIGiantEnemySpWarning : MonoBehaviour
	{
		[SerializeField]
		private BattleUIConst.GiantBossInfoType _giantBossInfoType;

		[SerializeField]
		private Image _spWarningGlow;

		[SerializeField]
		private Image _spWarningIconYellow;

		[SerializeField]
		private Image _spWarningIconYellow2;

		[SerializeField]
		private Image _spWarningIconRed;

		[SerializeField]
		private Image _spWarningIconRed2;

		[SerializeField]
		private Image _spWarningLine;

		[SerializeField]
		private Image _spWarningLine2;

		[SerializeField]
		private Image _spWarningClaw;
	}
}
