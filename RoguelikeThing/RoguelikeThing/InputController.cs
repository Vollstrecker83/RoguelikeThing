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
    public sealed class InputController:Game1
    {
        static InputController inputController = new InputController();
        public static InputController GetInputController { get { return inputController; } }

        public KeyboardState KeyboardState { get { return keyboardState; } }
        public MouseState MouseState { get { return mouseState; } }

        KeyboardState keyboardState;
        MouseState mouseState;

        private InputController()
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();
        }

        public override void GiveTime(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            mouseState = Mouse.GetState();

            if(keyboardState.IsKeyDown(Keys.Space))
            {
                if(Camera.GetCamera.IsLocked)
                {
                    Camera.GetCamera.IsLocked = false;
                }
                else
                {
                    Camera.GetCamera.IsLocked = true;
                    Camera.GetCamera.LockedTarget = Player;
                }
            }

            ProcessPlayerMovement(gameTime);
        }

        public void ProcessPlayerMovement(GameTime gameTime)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - Player.LastUpdate >= Player.MovementTimeLimit && !Player.MoveAllowed)
                Player.MoveAllowed = true;

            if (!Player.MoveAllowed)
                return;

            Point newGridPosition = Player.GridPosition;

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

            if (newGridPosition != Player.GridPosition)
            {
                // Movement has been detected, verify that we can actually move into this tile
                if (Player.CanMoveIntoTile(Player.CurrentLevel, newGridPosition))
                {
                    Player.DrawRectangle = Player.UpdateDrawRectangle(newGridPosition, TerrainManager.GetMapSize(Player.CurrentLevel));
                    Player.LastUpdate = gameTime.TotalGameTime.TotalMilliseconds;
                    Player.MoveAllowed = false;
                }

                else
                    return;     // Illegal move, exit
            }
        }
    }
}
