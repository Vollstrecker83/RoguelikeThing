using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace RoguelikeThing
{
    /// <summary>
    /// This class should handle the background map
    /// Should also handle map grid functionality
    /// </summary>
    public class Terrain : GameObject
    {
        public enum GroundType { Dirt = 1, Floor, Water };

        #region Private Member Variables

        private List<Tile> tileSet;
        private Point mapSize, tileSize;
        #endregion

        #region Accessors and Mutators
        public List<Tile> TileSet { get => tileSet; set => tileSet = value; }
        public Point MapSize { get => mapSize; set => mapSize = value; }
        public Point TileSize { get => tileSize; set => tileSize = value; }
        #endregion

        public Terrain()
        { }

        public Terrain(Point passedMapSize, Point passedTileSize)
        {
            tileSet = new List<Tile>();
            mapSize = passedMapSize;
            tileSize = passedTileSize;

            if (!CreateTileSet())
            {
                System.Console.ForegroundColor = System.ConsoleColor.Red;
                System.Console.WriteLine("Failed to create the Tileset, shit's fucked yo.");
                System.Console.ResetColor();
                System.Environment.Exit(1);
            }
                
        }

        private bool CreateTileSet()
        {
            // Make sure the map and tile values are valid
            if ((MapSize.X < 1 || MapSize.Y < 1) || (TileSize.X < 1 || TileSize.Y < 1))
                return false;

            // Calculate the maximum capacity of the Tile Set so we will know when it is done populating.
            int capacity = MapSize.X * MapSize.Y;
            
            System.Random random = new Random();                    // Randomizing floor tiles for fun
            List<GroundType> tempType = new List<GroundType>(3);    // This will need to be updated whenever we add new floor tiles
            tempType.Add(GroundType.Water);
            tempType.Add(GroundType.Floor);
            tempType.Add(GroundType.Dirt);

            int j = 0;
            while (tileSet.Count() < capacity)
            {
                for( int i = 0; i < MapSize.X; i++)
                {
                    Point location = new Point(i, j);
                    int index = random.Next(0, 3);
                    Tile tile = new Tile(this, location, 0,  tempType.ElementAt(index));
                    tileSet.Add(tile);
                }
                j++;
            }
            return true;
        }
    }
}
