#if UNITY_EDITOR
// Author: ChaomengCFX
// Created: 2023/04/29

using Torappu.Battle;
using UnityEngine;

namespace CustomMap.TileInformation
{
    /// <summary>
    /// Tile和TileGraphic的组合
    /// </summary>
    public class TileDataPair : MonoBehaviour
    {
        public Tile tile;
        public GameObject tileGraphic;
    }
}
#endif