#if UNITY_EDITOR
// Author: ChaomengCFX
// Created: 2023/04/30

using System;
using System.Collections.Generic;
using UnityEngine;
using Torappu;
using Torappu.Battle;
using Sirenix.OdinInspector;
using System.Linq;

namespace CustomMap.TileInformation
{
    [DisallowMultipleComponent]
    public class MultiCustomTile : CustomTile
    {
        public Dictionary<string, TileDataPair> dataPairs = new Dictionary<string, TileDataPair>();
        public bool random = false;
        [ShowIf("@!random")]
        public string defaultKey;
        public string description;
        public Texture2D icon;

        public override string Description => description;

        public override CustomTileInfo CreateCustomTileInfo()
        {
            return new MultiCustomTileInfo(this);
        }

        protected class MultiCustomTileInfo : CustomTileInfo
        {
            public MultiCustomTileInfo(MultiCustomTile tile)
            {
                m_tile = tile;
                if (tile.random)
                {
                    string[] keys = tile.dataPairs.Keys.ToArray();
                    m_selectedKey = keys[UnityEngine.Random.Range(0, keys.Length)];
                }
                else
                {
                    m_selectedKey = tile.defaultKey;
                }
                TileData _data = m_tile.dataPairs[m_selectedKey].tile._data;
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

            // 需要序列化的字段
            [SerializeField]
            protected MultiCustomTile m_tile;
            [SerializeField]
            protected TileData m_tileData;
            [SerializeField]
            protected TileDirection m_direction;
            [SerializeField]
            protected TileType m_specialTileType;
            [SerializeField]
            protected string m_selectedKey;

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

            public override Tile TilePrefab => m_tile.dataPairs[m_selectedKey].tile;

            public override GameObject TileGraphicPrefab => m_tile.dataPairs[m_selectedKey].tileGraphic;

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
                texture = m_tile.icon;
                return texture;
            }

            public override bool IsFrom(CustomTile tile) => tile == m_tile;

            private void SelectTile(string key)
            {
                m_selectedKey = key;
                TileData = m_tile.dataPairs[key].tile._data;
            }

            private class ModifyInterface : IModifyInterface
            {
                public ModifyInterface(MultiCustomTileInfo tileInfo)
                {
                    m_tileInfo = tileInfo;
                    foreach (string key in m_tileInfo.m_tile.dataPairs.Keys)
                    {
                        m_items.Add(new TileInfoItem(key, this));
                    }
                    m_items.Find(x => x.key == m_tileInfo.m_selectedKey).OnSelected();
                }

                private MultiCustomTileInfo m_tileInfo;

                [SerializeField, TableList]
                private List<TileInfoItem> m_items = new List<TileInfoItem>();

                [Serializable]
                private class TileInfoItem
                {
                    public TileInfoItem(string key, ModifyInterface @interface)
                    {
                        this.key = key;
                        m_interface = @interface;
                    }

                    private Color m_guiColor = Color.white;

                    [ReadOnly, GUIColor("$m_guiColor")]
                    public string key;

                    private ModifyInterface m_interface;

                    private void OnDeSelect()
                    {
                        m_guiColor = Color.white;
                    }

                    [Button("选中", ButtonSizes.Small), GUIColor("$m_guiColor")]
                    public void OnSelected()
                    {
                        m_interface.m_tileInfo.SelectTile(key);
                        m_guiColor = Color.green;
                        m_interface.m_items.ForEach(item => { if (item != this) item.OnDeSelect(); });
                    }
                }

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