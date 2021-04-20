using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Ruoran.Roguelike.Rand;

namespace Ruoran.Roguelike.Dungeon
{
    public static class TileChooser
    {
        public static List<DefaultWeightInfo> CrossTileWeightList { get; set; } = new List<DefaultWeightInfo>();
        public static List<DefaultWeightInfo> ObstacleTileWeightList { get; set; } = new List<DefaultWeightInfo>();
        public static List<DefaultWeightInfo> RoadTileWeightList { get; set; } = new List<DefaultWeightInfo>();
        public static List<DefaultWeightInfo> RoomTileWeightList { get; set; } = new List<DefaultWeightInfo>();
        public static List<DefaultWeightInfo> WallTileWeightList { get; set; } = new List<DefaultWeightInfo>();

        // 贴图缓存，不清空
        public static Dictionary<string, Tile> TileBuffer { get; set; } = new Dictionary<string, Tile>();

        public static void Reset()
        {
            CrossTileWeightList = new List<DefaultWeightInfo>();
            ObstacleTileWeightList = new List<DefaultWeightInfo>();
            RoadTileWeightList = new List<DefaultWeightInfo>();
            RoomTileWeightList = new List<DefaultWeightInfo>();
            WallTileWeightList = new List<DefaultWeightInfo>();
        }

        public static Tile RandTile(string type)
        {
            DefaultWeightInfo weightInfo;

            if (type == "Cross")
            {
                weightInfo = WeightListUtils.HitWeightListByBinary(CrossTileWeightList);
            }
            else if (type == "Obstacle")
            {

                weightInfo = WeightListUtils.HitWeightListByBinary(ObstacleTileWeightList);
            }
            else if (type == "Road")
            {

                weightInfo = WeightListUtils.HitWeightListByBinary(RoadTileWeightList);
            }
            else if (type == "Room")
            {

                weightInfo = WeightListUtils.HitWeightListByBinary(RoomTileWeightList);
            }
            else if (type == "Wall")
            {

                weightInfo = WeightListUtils.HitWeightListByBinary(WallTileWeightList);
            }
            else
            {
                weightInfo = WeightListUtils.HitWeightListByBinary(ObstacleTileWeightList);
            }

            if (TileBuffer.ContainsKey(weightInfo.Type))
            {
                return TileBuffer[weightInfo.Type];
            }
            else
            {
                var tileSprite = Resources.Load<Sprite>($"Sprites/{weightInfo.Type}");
                var tile = ScriptableObject.CreateInstance("Tile") as Tile;

                tile.sprite = tileSprite;
                TileBuffer.Add(weightInfo.Type, tile);

                return tile;
            }

        }
    }
}