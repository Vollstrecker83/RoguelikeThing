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
        int movementTimeLimit;
        bool moveAllowed;
        #endregion

        #region Accessors/Mutators
        public int CurrentExperience { get => currentExperience; set => currentExperience = value; }
        public int MovementTimeLimit { get => movementTimeLimit; set => movementTimeLimit = value; }
        public bool MoveAllowed { get => moveAllowed; set => moveAllowed = value; }
        #endregion

        public Player():base(TerrainManager.GetTerrainManager.CurrentLevel)
        {
            // Make sure we have a valid map to load into
            if (TerrainManager.GetTerrainManager.MapList.Equals(null))
            {
                throw new Exception("Map passed to Player constructor is empty!");
            }

            this.CurrentLevel = TerrainManager.GetTerrainManager.CurrentLevel;

            this.GridPosition = new Point(0, 0);
            this.ObjectSize = this.ScaleObjectToTileSize();
            this.DrawPosition = this.UpdateDrawPosition();
            this.DrawOffset = this.GenerateDrawOffset();
            this.DrawRectangle = this.UpdateDrawRectangle(this.GridPosition, TerrainManager.GetTerrainManager.GetCurrentMap(this.CurrentLevel).MapSize);
            this.IsCollider = true;
            this.IsDormant = false;
            this.currentExperience = 0;
            this.movementTimeLimit = 500;
            this.moveAllowed = true;
        }

        private void ProcessPlayerMovement( GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - this.LastUpdate >= this.movementTimeLimit && !moveAllowed)
                moveAllowed = true;

            if (!moveAllowed)
                return;

            Point newGridPosition = this.GridPosition;

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
                newGridPosition.Y -= 1;
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                newGridPosition.Y += 1;
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                newGridPosition.X -= 1;
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
                newGridPosition.X += 1;
            else                
                return;     // No movement input detected, exit early

            if(newGridPosition != this.GridPosition)
            {
                // Movement has been detected, verify that we can actually move into this tile
                if (this.CanMoveIntoTile(this.CurrentLevel, newGridPosition))
                {
                    this.DrawRectangle = this.UpdateDrawRectangle(newGridPosition, GetMapSize(this.CurrentLevel));
                    this.LastUpdate = gameTime.TotalGameTime.TotalMilliseconds;
                    this.moveAllowed = false;
                }

                else
                    return;     // Illegal move, exit
            }
        }

        public void GiveTime(GameTime gameTime)
        {
            ProcessPlayerMovement(gameTime);
                
        }
    }
}
