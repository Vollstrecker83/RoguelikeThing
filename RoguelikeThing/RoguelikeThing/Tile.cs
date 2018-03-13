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
    /// This class will cover tiles for the generated map tileset
    /// </summary>
    public class Tile:Terrain
    {
        #region Private Member Variables
        private bool showDebugNumber;
        private GroundType floorType;
        #endregion

        #region Accessors and Mutators
        public bool ShowDebugNumber { get => showDebugNumber; set => showDebugNumber = value; }
        public GroundType FloorType { get => floorType; set => floorType = value; }
        #endregion

        // Constructor, it will be passed the grid location that the tile will be set to.
        public Tile(Terrain map, Point passedGridLocation, int passedObject,  GroundType passedGroundType)
        {
            this.GridPosition = passedGridLocation;
            this.FloorType = passedGroundType;
            this.DrawPosition = new Point(this.GridPosition.X * map.TileSize.X, this.GridPosition.Y * map.TileSize.Y);
            this.DrawRectangle = new Rectangle(this.DrawPosition, map.TileSize);
        }
    }
}
