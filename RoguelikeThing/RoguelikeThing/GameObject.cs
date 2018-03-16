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
    /// Base class for any object that needs to be drawn in the scene
    /// </summary>
    public abstract class GameObject
    {
        #region Graphics Data
        private Texture2D objectTexture;
        private Point objectPosition, objectSize, drawPosition, drawOffset;
        private Rectangle drawRectangle;
        #endregion

        #region Other Data
        private bool isDormant;             // A flag to check if the object is still active
        private bool isCollider;            // A flag to check if the object can be navigated through
        private bool isVisible;             // A flag for invisibility
        private bool isInLOS;               // A flag for Line of Sight detection
        private Point gridPosition;         // So we know what tile the object is on
        private double lastUpdate;
        private int currentLevel;
        #endregion

        #region Accessors/Mutators
        public Texture2D ObjectTexture { get => objectTexture; set => objectTexture = value; }
        public Point ObjectPosition { get => objectPosition; set => objectPosition = value; }
        public Point ObjectSize { get => objectSize; set => objectSize = value; }
        public bool IsDormant { get => isDormant; set => isDormant = value; }
        public bool IsCollider { get => isCollider; set => isCollider = value; }
        public bool IsVisible { get => isVisible; set => isVisible = value; }
        public bool IsInLOS { get => isInLOS; set => isInLOS = value; }
        public Point GridPosition { get => gridPosition; set => gridPosition = value; }
        public Rectangle DrawRectangle { get => drawRectangle; set => drawRectangle = value; }
        public Point DrawPosition { get => drawPosition; set => drawPosition = value; }
        public Point DrawOffset { get => drawOffset; set => drawOffset = value; }
        public double LastUpdate { get => lastUpdate; set => lastUpdate = value; }
        public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
        #endregion

        /// <summary>
        /// Updates the DrawRectangle with the new position of the GameObject for rendering
        /// </summary>
        /// <param name="passedGridPosition"></param>
        /// <returns></returns>
        public Rectangle UpdateDrawRectangle(Point passedGridPosition, Point sizeOfMap)
        {
            // Null-protect the passed value
            if (passedGridPosition.Equals(null) ||
                (passedGridPosition.X < 0 || passedGridPosition.Y < 0))
            {
                throw new Exception("Passed Point in UpdateDrawRectangle is either null or less than zero");
            }

            if (passedGridPosition.X > sizeOfMap.X || passedGridPosition.Y > sizeOfMap.Y)
            {
                throw new Exception("Passed Point in UpdateDrawRectangle is larger than the map size");
            }

            this.GridPosition = passedGridPosition;
            this.DrawPosition = UpdateDrawPosition();
            return new Rectangle(this.DrawPosition, this.ObjectSize);
        }

        /// <summary>
        /// Updates the position of the GameObject's origin (0,0) for feeding into UpdateDrawRectangle
        /// </summary>
        /// <returns></returns>
        public Point UpdateDrawPosition()
        {
            return new Point((this.GridPosition.X * GetTileSize().X) + this.DrawOffset.X, 
                (this.GridPosition.Y * GetTileSize().Y) + this.DrawOffset.Y);
        }

        /// <summary>
        /// Generates the number of pixels offset from the Tile origin to ensure that the GameObject is centered in the Tile
        /// Used for GameObjects that are smaller in size than the TileSize
        /// </summary>
        /// <returns></returns>
        public Point GenerateDrawOffset()
        {
            return new Point((GetTileSize().X - this.ObjectSize.X) / 2, (GetTileSize().Y - this.ObjectSize.Y) / 2);
        }

        /// <summary>
        /// Scales the GameObject size to whatever the current GetTileSize() is
        /// </summary>
        /// <returns></returns>
        public Point ScaleObjectToTileSize()
        {
            return new Point(Convert.ToInt32(GetTileSize().X * 0.75), Convert.ToInt32(GetTileSize().Y * 0.75));
        }

        /// <summary>
        /// Used to scale a GameObject size on the fly using a Vector2 to hold X and Y percentages
        /// NOTE: Will not scale an object past tile size or below 25% tile size
        /// </summary>
        /// <param name="percentage"></param>
        /// <returns></returns>
        public Point UpdateObjectSize(Vector2 percentage)
        {
            Point newSize;

            // Null-protect the passed value
            if (percentage.Equals(null) || 
                (percentage.X.Equals(0.0f) || percentage.Y.Equals(0.0f)))
            {
                throw new Exception("Yo, you can't pass null or a zero value to UpdateObjectSize(Vector2)");
            }

            newSize = new Point(Convert.ToInt32(this.ObjectSize.X * percentage.X), Convert.ToInt32(this.ObjectSize.Y * percentage.Y));

            if (newSize.X > GetTileSize().X || newSize.Y > GetTileSize().Y)
            {
                newSize = GetTileSize();
            }

            if (newSize.X < Convert.ToInt32((GetTileSize().X / 4)) || (newSize.Y < Convert.ToInt32(GetTileSize().X / 4)))
            {
                newSize.X = Convert.ToInt32(GetTileSize().X / 4);
                newSize.Y = Convert.ToInt32(GetTileSize().Y / 4);
            }

            return newSize;
        }

        /// <summary>
        /// Used to scale a GameObject size on the fly using a single float for X and Y values for uniform scaling
        /// NOTE: Will not scale an object past tile size or below 25% tile size
        /// </summary>
        /// <param name="percentage"></param>
        /// <returns></returns>
        public Point UpdateObjectSize(float percentage)
        {
            Point newSize;

            // Null-protect the passed value
            if (percentage.Equals(null) || percentage.Equals(0.0f))
            {
                throw new Exception("Yo, you can't pass null or a zero value to UpdateObjectSize(float)");
            }

            newSize = new Point(Convert.ToInt32(this.ObjectSize.X * percentage), Convert.ToInt32(this.ObjectSize.Y * percentage));

            if (newSize.X > GetTileSize().X || newSize.Y > GetTileSize().Y)
            {
                newSize = GetTileSize();
            }

            if (newSize.X < Convert.ToInt32((GetTileSize().X / 4)) || (newSize.Y < Convert.ToInt32(GetTileSize().X / 4)))
            {
                newSize.X = Convert.ToInt32(GetTileSize().X / 4);
                newSize.Y = Convert.ToInt32(GetTileSize().Y / 4);
            }

            return newSize;
        }

        public Point GetTileSize()
        {
            return TerrainManager.GetTerrainManager.TileSize;
        }

        public Point GetMapSize(int level)
        {
            return TerrainManager.GetTerrainManager.GetMapSize(level);
        }
    }
}
