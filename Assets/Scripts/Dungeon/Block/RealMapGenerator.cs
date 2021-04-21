using UnityEngine;
using UnityEngine.Tilemaps;

namespace Ruoran.Roguelike.Dungeon
{
    public static class RealMapGenerator
    {
        public static void PrintMap(BlockInfo[,] BlockMap)
        {
            var Floor = GameObject.Find("Grid/Floor").GetComponent<Tilemap>();
            var Wall = GameObject.Find("Grid/Wall").GetComponent<Tilemap>();

            for (int i = 0; i < BlockMap.GetLength(0); i++)
            {
                for (int j = 0; j < BlockMap.GetLength(1); j++)
                {
                    var info = BlockMap[i, j];
                    if (info.CanPass == false)
                    {
                        Wall.SetTile(new Vector3Int(i, j, 0), TileChooser.RandTile("Obstacle"));
                    }
                    else Floor.SetTile(new Vector3Int(i, j, 0), TileChooser.RandTile("Cross"));
                }
            }
        }
    }
}
