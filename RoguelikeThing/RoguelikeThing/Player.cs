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

        public Player()
        {
            this.ObjectPosition = new Vector2(0, 0);
        }
    }
}
