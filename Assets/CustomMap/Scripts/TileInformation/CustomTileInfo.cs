#if UNITY_EDITOR
// Author: ChaomengCFX
// Created: 2023/04/28

using System.Collections.Generic;
using Torappu;
using Torappu.Battle;
using UnityEngine;

namespace CustomMap.TileInformation
{
    /// <summary>
    /// 自定义Tile的信息基类（用于TileMap绘制）
    /// </summary>
    public abstract class CustomTileInfo
    {
        /// <summary>
        /// TileData信息
        /// </summary>
        public abstract TileData TileData { get; protected set; }

        /// <summary>
        /// Tile预制体
        /// </summary>
        public abstract Tile TilePrefab { get; }

        /// <summary>
        /// TileMesh预制体
        /// </summary>
        public abstract GameObject TileGraphicPrefab { get; }

        /// <summary>
        /// Tile方向
        /// </summary>
        public abstract TileDirection Direction { get; protected set; }

        /// <summary>
        /// Tile特殊类型
        /// </summary>
        public abstract TileType SpecialTileType { get; protected set; }

        /// <summary>
        /// 此Tile是否有Mesh
        /// </summary>
        public abstract bool HasGraphic { get; }

        /// <summary>
        /// 判断TileInfo是否为空
        /// </summary>
        public virtual bool IsNull => !TilePrefab;

        /// <summary>
        /// 是否由CustomTile实例产生
        /// </summary>
        /// <param name="tile"></param>
        /// <returns></returns>
        public abstract bool IsFrom(CustomTile tile);

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="direction">Tile方向</param>
        public virtual void SetUp(TileDirection direction, TileType tileType)
        {
            Direction = direction;
            SpecialTileType = tileType;
        }

        /// <summary>
        /// 获得自定义的TileMap图标
        /// </summary>
        /// <param name="texture">Icon的2D贴图</param>
        /// <returns>是否有自定义的TileMap图标</returns>
        public abstract bool TryGetGUIIcon(out Texture2D texture);

        /// <summary>
        /// 获得字段修改接口
        /// </summary>
        /// <returns></returns>
        public abstract IModifyInterface GetModifyInterface();

        /// <summary>
        /// 实例化该Tile
        /// </summary>
        /// <param name="row">行</param>
        /// <param name="col">列</param>
        /// <param name="view">MapBuildView对象</param>
        /// <param name="tileList">tile列表</param>
        /// <param name="tileGraphicList">tileGraphic列表</param>
        public virtual void Instantiate(int row, int col, MapBuildView view, List<Tile> tileList, List<TileGraphic> tileGraphicList)
        {
            if (IsNull) return;

            Tile tile = Object.Instantiate(TilePrefab, view.tileRoot);
            tile.transform.localPosition += new Vector3(col, row, 0f);
            string pos = string.Format("({0},{1})#", row, col);
            tile.gameObject.name = pos + tile.gameObject.name;

            if (HasGraphic)
            {
                GameObject graphic = Object.Instantiate(TileGraphicPrefab, view.meshRoot);

                graphic.transform.localPosition += new Vector3(col, row, 0f);
                switch (Direction)
                {
                    case TileDirection.Random:
                        graphic.transform.localEulerAngles += new Vector3(Random.Range(0, 4) * 90f, 0f, 0f);
                        break;
                    case TileDirection.Up:
                        break;
                    case TileDirection.Down:
                        graphic.transform.localEulerAngles += new Vector3(180f, 0f, 0f);
                        break;
                    case TileDirection.Left:
                        graphic.transform.localEulerAngles += new Vector3(270f, 0f, 0f);
                        break;
                    case TileDirection.Right:
                        graphic.transform.localEulerAngles += new Vector3(90f, 0f, 0f);
                        break;
                }

                TileGraphic[] tileGraphics = graphic.GetComponentsInChildren<TileGraphic>(true);
                TileGraphic mainTileGraphic = null;

                foreach (TileGraphic tileGraphic in tileGraphics)
                {
                    tileGraphic.gameObject.name = pos + tileGraphic.gameObject.name;
                    tileGraphic._tile = tile;
                    tileGraphic._gridPos = new GridPosition { row = row, col = col };
                    tile._allGraphicList.Add(tileGraphic);
                    tileGraphicList.Add(tileGraphic);

                    MainTileGraphic isMain = tileGraphic.gameObject.GetComponent<MainTileGraphic>();
                    if (isMain)
                    {
                        mainTileGraphic = tileGraphic;
                        Object.DestroyImmediate(isMain);
                    }
                }

                if (!mainTileGraphic)
                    mainTileGraphic = tileGraphicList[0];

                tile._graphic = mainTileGraphic;
            }

            tile._data = TileData;

            SpecialTileTypeHandler handler;
            if (TryGetSpecialTileTypeHandler(out handler))
            {
                handler.Handle(tile);
            }

            tileList.Add(tile);
        }

        protected virtual bool TryGetSpecialTileTypeHandler(out SpecialTileTypeHandler handler)
        {
            handler = null;
            switch (SpecialTileType)
            {
                case TileType.Start:
                    handler = new StartEndHandler("tile_start");
                    return true;
                case TileType.End:
                    handler = new StartEndHandler("tile_end");
                    return true;
                case TileType.FlyStart:
                    handler = new StartEndHandler("tile_flystart");
                    return true;
            }
            return false;
        }

        public interface IModifyInterface { }

        public enum TileDirection
        {
            Up,
            Down,
            Left,
            Right,
            Random
        }

        public enum TileType
        {
            None,
            Start,
            End,
            FlyStart
        }
    }
}
#endif