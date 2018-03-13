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
    public abstract class GameObject:Game1
    {
        #region Graphics Data
        private Texture2D objectTexture;
        private Point objectPosition, objectSize, drawPosition, drawOffset;
        private Rectangle drawRectangle;
        #endregion

        #region Other Data
        private bool isActive;              // A flag to check if the object is still active
        private bool isCollider;            // A flag to check if the object can be navigated through
        private bool isVisible;             // A flag for invisibility
        private bool isInLOS;               // A flag for Line of Sight detection
        private Point gridPosition;         // So we know what tile the object is on
        #endregion

        #region Accessors/Mutators
        public Texture2D ObjectTexture { get => objectTexture; set => objectTexture = value; }
        public Point ObjectPosition { get => objectPosition; set => objectPosition = value; }
        public Point ObjectSize { get => objectSize; set => objectSize = value; }
        public bool IsActive { get => isActive; set => isActive = value; }
        public bool IsCollider { get => isCollider; set => isCollider = value; }
        public bool IsVisible { get => isVisible; set => isVisible = value; }
        public bool IsInLOS { get => isInLOS; set => isInLOS = value; }
        public Point GridPosition { get => gridPosition; set => gridPosition = value; }
        public Rectangle DrawRectangle { get => drawRectangle; set => drawRectangle = value; }
        public Point DrawPosition { get => drawPosition; set => drawPosition = value; }
        public Point DrawOffset { get => drawOffset; set => drawOffset = value; }
        #endregion

        public Rectangle UpdateDrawRectangle(Point passedGridPosition)
        {
            this.GridPosition = passedGridPosition;
            this.DrawPosition = UpdateDrawPosition();
            return new Rectangle(this.DrawPosition, this.ObjectSize);
        }

        public Point UpdateDrawPosition()
        {
            return new Point((this.GridPosition.X * Game1.TileSize.X) + this.DrawOffset.X, (this.GridPosition.Y * Game1.TileSize.Y) + this.DrawOffset.Y);
        }

        public Point GenerateDrawOffset()
        {
            return new Point((Game1.TileSize.X - this.ObjectSize.X) / 2, (Game1.TileSize.Y - this.ObjectSize.Y) / 2);
        }

        public Point UpdateObjectSize()
        {
            return new Point(Convert.ToInt32(Game1.TileSize.X * 0.75), Convert.ToInt32(Game1.TileSize.Y * 0.75));
        }
    }
}
