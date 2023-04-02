using System.Collections.Generic;
using Torappu.Battle.Opera;
using UnityEngine;

namespace Torappu.Battle
{
	public class BattleLauncher : SingletonMonoBehaviour<BattleLauncher>
	{
		[SerializeField]
		private string _levelId;

		[SerializeField]
		private TextAsset _levelJson;

		[SerializeField]
		private TextAsset _squadJson;

		[SerializeField]
		private TextAsset _runeJson;

		[SerializeField]
		private OperaConfig _operaConfig;

		[SerializeField]
		private TextAsset _relicJson;

		[SerializeField]
		private LevelData.Difficulty _difficulty;

		[SerializeField]
		private List<CharacterInst> _slots;

		[SerializeField]
		private bool _forceReimportOnStart;

		[SerializeField]
		private bool _includeDefaultGraphic;

		[SerializeField]
		private bool _forceUseSquadFile;

		[SerializeField]
		private bool _autoReplay;

		[SerializeField]
		private TextAsset _logJson;

		[SerializeField]
		private bool _roguelikeDevLocal;

		[SerializeField]
		private bool _multiplayerDevLocal;

		[SerializeField]
		private PlayerSide _playerSide;

		[SerializeField]
		private bool _enableAnother;

		[SerializeField]
		private PlayerSide _playerSideAnother;

		[SerializeField]
		private TextAsset _squadAnotherJson;

		[SerializeField]
		private List<CharacterInst> _slotsAnother;
	}
}
