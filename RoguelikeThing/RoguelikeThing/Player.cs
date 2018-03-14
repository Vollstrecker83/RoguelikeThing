using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace RoguelikeThing
{
    /// <summary>
    /// The player object and associated data/methods
    /// </summary>
    public class Player:Entity
    {
        #region Private Member Variables
        private int currentExperience;
        #endregion

        #region Accessors/Mutators
        public int CurrentExperience { get => currentExperience; set => currentExperience = value; }
        #endregion

        public Player(Point passedTileSize, Point sizeOfMap)
        {
            // Null-protect the passed value
            if (passedTileSize.Equals(null) || 
                (passedTileSize.X < 0 || passedTileSize.Y < 0))
            {
                throw new Exception("Passed value in Player(Point) is either null or less than zero");
            }

            this.GridPosition = new Point(0, 0);
            this.ObjectSize = this.ScaleObjectToTileSize();
            this.DrawPosition = this.UpdateDrawPosition();
            this.DrawOffset = this.GenerateDrawOffset();
            this.DrawRectangle = this.UpdateDrawRectangle(this.GridPosition, sizeOfMap);
            this.IsCollider = true;
            this.IsDormant = false;
            this.currentExperience = 0;
        }

        public bool ProcessPlayerMovement(Point newGridPosition, KeyboardState state, Terrain map)
        {
            if (state.IsKeyDown(Keys.Up))
            {
                newGridPosition.Y -= 1;
            }
            else if (state.IsKeyDown(Keys.Down))
            {
                newGridPosition.Y += 1;
            }
            else if (state.IsKeyDown(Keys.Left))
            {
                newGridPosition.X -= 1;
            }
            else if (state.IsKeyDown(Keys.Right))
            {
                newGridPosition.X += 1;
            }
            else
            {
                // No movement input detected, return control back to Update()
                return false;
            }

            // Verify that we can actually move into the tile we're attempting to
            if (this.CanMoveIntoTile(map, newGridPosition))
            {
                this.DrawRectangle = this.UpdateDrawRectangle(newGridPosition, map.MapSize);
                return true;
            }
            return false;
        }


    }
}
