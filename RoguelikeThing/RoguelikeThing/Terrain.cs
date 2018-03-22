using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        private Point mapSize;
        #endregion

        #region Accessors and Mutators
        public List<Tile> TileSet => tileSet;
        public Point MapSize => mapSize;


        #endregion

        public Terrain()
        { }

        public Terrain(int newLevelNumber)
        {
            Random rand = new Random();
            mapSize.X = rand.Next(5, 11);
            mapSize.Y = rand.Next(5, 11);
            this.CurrentLevel = newLevelNumber;
            this.tileSet = CreateTileSet();

            if (tileSet.Equals(null))
            {
                throw new Exception("Failed to create the level " + this.CurrentLevel + "tileset!");
            }

            // Make sure we don't populate the first level into the manager twice
            if(this.CurrentLevel > 1)
            {
                TerrainManager.MapList.Add(this.CurrentLevel, this);
            }

            // Set the current level number as this will only be called when a player is entering a level (in theory)
            TerrainManager.CurrentLevel = this.CurrentLevel;

        }

        /// <summary>
        /// Populates a list with Tiles
        /// </summary>
        /// <returns></returns>
        private List<Tile> CreateTileSet()
        {
            // Make sure the map and tile values are valid
            if ((MapSize.X < 1 || MapSize.Y < 1) || (GetTileSize().X < 1 || GetTileSize().Y < 1))
            {
                throw new Exception("MapSize or TileSize in Terrain::CreateTileSet is 0 or lower");
            }

            List<Tile> tempTile = new List<Tile>();

            // Calculate the maximum capacity of the Tile Set so we will know when it is done populating.
            int capacity = MapSize.X * MapSize.Y;
            
            System.Random random = new Random();                    // Randomizing floor tiles for fun
            List<GroundType> tempType = new List<GroundType>(3)     // This will need to be updated whenever we add new floor tiles
            {
                GroundType.Water,
                GroundType.Floor,
                GroundType.Dirt
            };    

            int j = 0, index;                                              // Iterator for mapSize.Y
            while (j < MapSize.Y)
            {
                for( int i = 0; i < MapSize.X; i++)                 // Iterator for mapSize.X
                {
                    Point location = new Point(i, j);
                    #region Temporary fix to prevent player from spawning on water at 0,0
                    index = random.Next(0, 3);
                    if(i == 0 && j == 0)
                    {
                        index = random.Next(1, 3);
                    }
                    #endregion
                    Tile tile = new Tile(location, 0,  tempType.ElementAt(index));
                    int orientation = random.Next(0, 3);
                    switch (orientation)
                    {
                        case 0:                 // Default orientation
                            break;
                        case 1:
                            tile.TileEffects = SpriteEffects.FlipHorizontally;
                            break;
                        case 2:
                            tile.TileEffects = SpriteEffects.FlipVertically;
                            break;
                        default:
                            System.Console.WriteLine("Problem assigning rotation to tile, using default value of orientation but you should fix this shit.");
                            break;

                    }
                    tempTile.Add(tile);
                }
                j++;
            }

            // Verify that the list is full as expected
            if (tempTile.Count == capacity)
            {
                return tempTile;
            }
            else
            {
                throw new Exception("Error in Terrain::CreateTileSet(): tempTile.Count does not match calculated capacity");
            }
        }

        /// <summary>
        /// Returns the Tile found at the passed grid position
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Tile GetTileAtGridPosition(Point position)
        {
            foreach(Tile tile in TileSet)
            {
                if (tile.GridPosition.Equals(position))
                {
                    return tile;
                }
            }
            return null;
        }
    }
}
