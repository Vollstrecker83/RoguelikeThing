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
        SpriteEffects tileEffects;
        TerrainObject tObject;
        #endregion

        #region Accessors and Mutators
        public bool ShowDebugNumber { get { return showDebugNumber; } set { showDebugNumber = value; } }
        public GroundType FloorType { get { return floorType; } set { floorType = value; } }
        public SpriteEffects TileEffects { get { return tileEffects; } set { tileEffects = value; } }
        #endregion

        // Constructor, it will be passed the grid location that the tile will be set to.
        public Tile(Point passedGridLocation, int passedObject,  GroundType passedGroundType)
        {
            tObject = null;
            this.GridPosition = passedGridLocation;
            this.FloorType = passedGroundType;
            this.DrawPosition = new Point(this.GridPosition.X * GetTileSize().X, this.GridPosition.Y * GetTileSize().Y);
            this.DrawRectangle = new Rectangle(this.DrawPosition, GetTileSize());
            this.tileEffects = SpriteEffects.None;
            if (this.FloorType == GroundType.Water)
            {
                this.IsCollider = true;
            }
        }
    }
}
