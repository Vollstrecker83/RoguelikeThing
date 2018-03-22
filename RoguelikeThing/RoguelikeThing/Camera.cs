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
    public sealed class Camera:Game1
    {
        #region Private Member Variables
        static Camera camera = new Camera();
        private Viewport viewport;
        private const float zoomUpperLimit = 1.5f;
        private const float zoomLowerLimit = 0.5f;
        private float zoom;
        private Matrix transform;
        private Vector2 position;
        private int viewportWidth;
        private int viewportHeight;
        private int worldWidth;
        private int worldHeight;
        #endregion

        #region Accessors/Mutators
        public static Camera GetCamera => camera;

        public float Zoom
        {
            get => zoom;
            set
            {
                zoom = value;
                if (zoom < zoomLowerLimit)
                    zoom = zoomLowerLimit;
                if (zoom > zoomUpperLimit)
                    zoom = zoomUpperLimit;
            }
        }
        public Vector2 Position
        {
            get { return position; }
            set
            {
                float leftBarrier = (float)viewportWidth * 0.5f / zoom;
                float rightBarrier = worldWidth - (float)viewportWidth * 0.5f / zoom;
                float topBarrier = worldHeight - (float)viewportHeight * 0.5f / zoom;
                float bottomBarrier = (float)viewportHeight * 0.5f / zoom;

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

        private Camera()
        {
            viewport = GetViewport;
            zoom = 1.0f;
            position = Vector2.Zero;
            viewportWidth = viewport.Width;
            viewportHeight = viewport.Height;
        }

        public void Move(Vector2 amount)
        {
            position += amount;
        }

        public Matrix GetTransformation()
        {
            transform =
                Matrix.CreateTranslation(new Vector3(-position.X, -position.Y, 0)) *
                Matrix.CreateRotationZ(0) *
                Matrix.CreateScale(new Vector3(zoom, zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(viewportWidth * 0.5f, viewportHeight * 0.5f, 0));

            return transform;
        }
    }
}
