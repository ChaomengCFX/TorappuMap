#if UNITY_EDITOR
// Author: ChaomengCFX
// Created: 2023/04/29

using UnityEngine;
using Torappu;
using Torappu.Battle;
using Sirenix.OdinInspector;

namespace CustomMap.TileInformation
{
    [DisallowMultipleComponent]
    public class MonoCustomTile : CustomTile
    {
        public TileDataPair dataPair;
        public string description;

        public override string Description => description;

        public override CustomTileInfo CreateCustomTileInfo()
        {
            return new MonoCustomTileInfo(this);
        }

        protected class MonoCustomTileInfo : CustomTileInfo
        {
            public MonoCustomTileInfo(MonoCustomTile tile)
            {
                m_tile = tile;
                TileData _data = m_tile.dataPair.tile._data;
                if (_data != null)
                {
                    TileData = new TileData
                    {
                        tileKey = _data.tileKey,
                        heightType = _data.heightType,
                        buildableType = _data.buildableType,
                        passableMask = _data.passableMask,
                        playerSideMask = _data.playerSideMask,
                        blackboard = _data.blackboard.Count > 0 ? _data.blackboard : null,
                        effects = _data.effects.Length > 0 ? _data.effects : null
                    };
                }
            }

            // 需要序列化的字段
            [SerializeField]
            protected MonoCustomTile m_tile;
            [SerializeField]
            protected TileData m_tileData;
            [SerializeField]
            protected TileDirection m_direction;
            [SerializeField]
            protected TileType m_specialTileType;

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

            public override Tile TilePrefab => m_tile.dataPair.tile;

            public override GameObject TileGraphicPrefab => m_tile.dataPair.tileGraphic;
            [SerializeField]

            public override TileDirection Direction
            {
                get
                {
                    return m_direction;
                }
                protected set
                {
                    m_direction = value;
                }
            }

            public override TileType SpecialTileType
            {
                get
                {
                    return m_specialTileType;
                }
                protected set
                {
                    m_specialTileType = value;
                }
            }

            public override bool HasGraphic => TileGraphicPrefab;

            public override IModifyInterface GetModifyInterface() => new ModifyInterface(this);

            public override bool TryGetGUIIcon(out Texture2D texture)
            {
                texture = null;
                return false;
            }

            public override bool IsFrom(CustomTile tile) => tile == m_tile;

            private class ModifyInterface : IModifyInterface
            {
                public ModifyInterface(MonoCustomTileInfo tileInfo)
                {
                    m_tileInfo = tileInfo;
                }

                private MonoCustomTileInfo m_tileInfo;

                [ShowInInspector, EnumToggleButtons, LabelText("Tile方向")]
                private TileDirection Direction
                {
                    get
                    {
                        return m_tileInfo.Direction;
                    }
                    set
                    {
                        m_tileInfo.Direction = value;
                    }
                }

                [ShowInInspector, EnumToggleButtons, LabelText("Tile特殊类型")]
                private TileType SpecialTileType
                {
                    get
                    {
                        return m_tileInfo.SpecialTileType;
                    }
                    set
                    {
                        m_tileInfo.SpecialTileType = value;
                    }
                }

                [ShowInInspector, EnumToggleButtons]
                private TileData TileData
                {
                    get
                    {
                        return m_tileInfo.TileData;
                    }
                    set
                    {
                        m_tileInfo.TileData = value;
                    }
                }
            }
        }
    }
}
#endif