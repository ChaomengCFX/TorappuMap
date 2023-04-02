using System;
using UnityEngine;

namespace Torappu.ObjectPool
{
	public class PoolManager : SingletonMonoBehaviour<PoolManager>
	{
		[Serializable]
		public struct ObjectConfig
		{
			public enum SourceType
			{
				RESOURCE = 0,
				PROTOTYPE = 1
			}

			public SourceType sourceType;

			public string assetName;

			public GameObject prototype;

			public GameObjectPool.Options poolOptions;
		}

		[SerializeField]
		private bool _usePoolManager;

		[SerializeField]
		private bool _keepInNextScene;

		[SerializeField]
		private bool _autoAddMissingPool;

		[SerializeField]
		private ObjectConfig[] _scenePools;
	}
}
