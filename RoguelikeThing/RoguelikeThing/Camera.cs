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
    /// This class will implement and control the in-game camera
    /// </summary>
    public sealed class Camera
    {

        #region Private Member Variables
        private static Camera camera = new Camera();
        private const float zoomUpperLimit = 1.5f;
        private const float zoomLowerLimit = 0.5f;
        private static float zoom;
        private static Matrix transform;
        private static Vector2 position;
        private static int worldWidth;
        private static int worldHeight;
        private static bool isLocked;
        private static GameObject lockedTarget;
        private static Point cursorLocation;
        #endregion

        #region Accessors/Mutators
        public static bool IsLocked { get { return isLocked; } set { isLocked = value; } } 
        public static GameObject LockedTarget { get { return lockedTarget; } set { lockedTarget = value; } }

        public static float Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                if (zoom < zoomLowerLimit)
                    zoom = zoomLowerLimit;
                if (zoom > zoomUpperLimit)
                    zoom = zoomUpperLimit;
            }
        }
        public static Vector2 Position
        {
            get { return position; }

            // Making sure we don't move the camera beyond the boundaries of the viewport
            set
            {
                float leftBarrier = MainGame.GetViewport.Width* 0.5f / zoom;
                float rightBarrier = worldWidth - MainGame.GetViewport.Width * 0.5f / zoom;
                float topBarrier = worldHeight - MainGame.GetViewport.Height * 0.5f / zoom;
                float bottomBarrier = MainGame.GetViewport.Height * 0.5f / zoom;

                position = value;

                if (position.X < leftBarrier)
                    position.X = leftBarrier;
                if (position.X > rightBarrier)
                    position.X = rightBarrier;
                if (position.Y > topBarrier)
                    position.Y = topBarrier;
                if (position.Y < bottomBarrier)
                    position.Y = bottomBarrier;
            }
        }
        #endregion

        Camera()
        {
            zoom = 1.0f;
            position = Vector2.Zero;
            isLocked = false;
            cursorLocation = Point.Zero;
        }

        public static void Move(Vector2 amount)
        {
            // If the camera is locked to a target, ignore any movement commands
            if (isLocked)
                return;

            position += amount;
        }

        public static Matrix GetTransformation()
        {
            transform =
                Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                Matrix.CreateRotationZ(0) *
                Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(MainGame.GetViewport.Width * 0.5f, MainGame.GetViewport.Height * 0.5f, 0));

            return transform;
        }

        public static void GiveTime(GameTime gameTime)
        {
            if(isLocked)
            {
                Position = lockedTarget.DrawPosition.ToVector2();
                return;
            }
            else
            {
                // Camera movement logic goes here
                cursorLocation = InputController.MousePosition;
                Vector2 moveAmount = Position;
                int rightScroll = (MainGame.GetViewport.Width / 6) * 5;
                int leftScroll = MainGame.GetViewport.Width / 6;
                int upScroll = MainGame.GetViewport.Height / 6;
                int downScroll = (MainGame.GetViewport.Height / 6) * 5;

                if(cursorLocation.X >= rightScroll )
                {
                    moveAmount.X += MainGame.Map.GetTileSize().X;
                    Move(moveAmount);
                }

                if (cursorLocation.X <= leftScroll)
                {
                    moveAmount.X -= MainGame.Map.GetTileSize().X;
                    Move(moveAmount);
                }

                if(cursorLocation.Y <= upScroll)
                {
                    moveAmount.Y -= MainGame.Map.GetTileSize().Y;
                    Move(moveAmount);
                }

                if(cursorLocation.Y >= downScroll)
                {
                    moveAmount.Y += MainGame.Map.GetTileSize().Y;
                    Move(moveAmount);
                }
            }
        }
    }
}
