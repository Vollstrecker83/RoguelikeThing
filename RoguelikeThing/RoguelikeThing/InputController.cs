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
    /// This should handle all input for the game.
    /// </summary>
    public sealed class InputController
    {
        
        private static InputController inputController = new InputController();

        public static Point MousePosition { get { return Mouse.GetState().Position; } }

        private InputController()
        {
            
        }

        public static void GiveTime(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                if(Camera.IsLocked)
                {
                    Camera.IsLocked = false;
                }
                else
                {
                    Camera.IsLocked = true;
                    Camera.LockedTarget = MainGame.GetPlayer;
                }
            }

            ProcessPlayerMovement(gameTime);
        }

        public static void ProcessPlayerMovement(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - MainGame.GetPlayer.LastUpdate >= MainGame.GetPlayer.MovementTimeLimit && !MainGame.GetPlayer.MoveAllowed)
                MainGame.GetPlayer.MoveAllowed = true;

            if (!MainGame.GetPlayer.MoveAllowed)
                return;

            Point newGridPosition = MainGame.GetPlayer.GridPosition;

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

            if (newGridPosition != MainGame.GetPlayer.GridPosition)
            {
                // Movement has been detected, verify that we can actually move into this tile
                if (MainGame.GetPlayer.CanMoveIntoTile(MainGame.GetPlayer.CurrentLevel, newGridPosition))
                {
                    MainGame.GetPlayer.DrawRectangle = MainGame.GetPlayer.UpdateDrawRectangle(newGridPosition, TerrainManager.GetMapSize(MainGame.GetPlayer.CurrentLevel));
                    MainGame.GetPlayer.LastUpdate = gameTime.TotalGameTime.TotalMilliseconds;
                    MainGame.GetPlayer.MoveAllowed = false;
                }

                else
                    return;     // Illegal move, exit
            }
        }
    }
}
