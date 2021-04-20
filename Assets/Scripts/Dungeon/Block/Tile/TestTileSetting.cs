using Ruoran.Roguelike.Rand;

namespace Ruoran.Roguelike.Dungeon
{
    public static class TestTileSetting
    {
        public static void Import()
        {
            TileChooser.Reset();

            WeightListUtils.Add("Black", 10, TileChooser.ObstacleTileWeightList);
            WeightListUtils.Add("Black", 10, TileChooser.WallTileWeightList);

            WeightListUtils.Add("White", 10, TileChooser.RoadTileWeightList);
            WeightListUtils.Add("White", 10, TileChooser.CrossTileWeightList);
            WeightListUtils.Add("White", 10, TileChooser.RoomTileWeightList);
        }
    }
}