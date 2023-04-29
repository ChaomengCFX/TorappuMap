#if UNITY_EDITOR
// Author: ChaomengCFX
// Created: 2023/04/29

using System.Collections.Generic;
using Torappu;
using Torappu.Battle;
using UnityEngine;

namespace CustomMap.TileInformation
{
    public class EmptyCustomTileInfo : CustomTileInfo
    {
        public EmptyCustomTileInfo()
        {
            TileData = new TileData { tileKey = "tile_empty", buildableType = BuildableType.NONE, passableMask = MotionMask.NONE };
        }

        // 需要序列化的字段
        [SerializeField]
        protected TileData m_tileData;

        public override bool IsNull => true;

        public override TileData TileData
        {
            get
            {
                return m_tileData;
            }
            protected set
            {
                m_tileData = value;
            }
        }

        public override Tile TilePrefab => null;

        public override GameObject TileGraphicPrefab => null;

        public override TileDirection Direction { get { return TileDirection.Up; } protected set { } }

        public override TileType SpecialTileType { get { return TileType.None; } protected set { } }

        public override bool HasGraphic => false;

        public override IModifyInterface GetModifyInterface() => null;

        public override bool TryGetGUIIcon(out Texture2D texture)
        {
            texture = null;
            return false;
        }

        public override void Instantiate(int row, int col, MapBuildView view, List<Tile> tileList, List<TileGraphic> tileGraphicList)
        {
            GameObject tileObj = new GameObject(string.Format("({0},{1})#tile_empty", row, col));
            tileObj.transform.SetParent(view.tileRoot);
            tileObj.transform.localPosition = new Vector3(col, row, 0f);

            Tile tile = tileObj.AddComponent<Tile>();
            tile._data = TileData;
            tile._tileKey = tile._data.tileKey;

            SpecialTileTypeHandler handler;
            if (TryGetSpecialTileTypeHandler(out handler))
            {
                handler.Handle(tile);
            }

            tileList.Add(tile);
        }

        public override bool IsFrom(CustomTile tile) => false;
    }
}
#endif