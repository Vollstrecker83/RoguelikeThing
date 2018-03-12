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
        private Vector2 objectPosition;
        private Vector2 objectSize;

        public Texture2D ObjectTexture { get => objectTexture; set => objectTexture = value; }
        public Vector2 ObjectPosition { get => objectPosition; set => objectPosition = value; }
        public Vector2 ObjectSize { get => objectSize; set => objectSize = value; }
        #endregion

        #region Other Data
        private bool isActive;              // A flag to check if the object is still active
        #endregion

        /// <summary>
        /// Default Constructor
        /// </summary>
        public GameObject()
        {
            
        }


    }
}
