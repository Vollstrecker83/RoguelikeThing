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

        public Player(Point passedTileSize)
        {
            this.GridPosition = new Point(0, 0);
            this.ObjectSize = this.UpdateObjectSize();
            this.DrawPosition = this.UpdateDrawPosition();
            this.DrawOffset = this.GenerateDrawOffset();
            this.DrawRectangle = this.UpdateDrawRectangle(this.GridPosition);
            this.IsCollider = true;
            this.IsActive = true;
            this.currentExperience = 0;
        }


    }
}
