#if UNITY_EDITOR
// Author: ChaomengCFX
// Created: 2023/04/29

using UnityEngine;

namespace CustomMap.TileInformation
{
    /// <summary>
    /// 自定义Tile组件
    /// </summary>
    public abstract class CustomTile : MonoBehaviour
    {
        public abstract string Description { get; }
        public abstract CustomTileInfo CreateCustomTileInfo();
    }
}
#endif