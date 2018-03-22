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

        public Player():base(TerrainManager.CurrentLevel)
        {
            // Make sure we have a valid map to load into
            if (TerrainManager.MapList.Equals(null))
            {
                throw new Exception("Map passed to Player constructor is empty!");
            }

            this.CurrentLevel = TerrainManager.CurrentLevel;

            this.GridPosition = new Point(0, 0);
            this.ObjectSize = this.ScaleObjectToTileSize();
            this.DrawPosition = this.UpdateDrawPosition();
            this.DrawOffset = this.GenerateDrawOffset();
            this.DrawRectangle = this.UpdateDrawRectangle(this.GridPosition, TerrainManager.GetCurrentMap(this.CurrentLevel).MapSize);
            this.IsCollider = true;
            this.IsDormant = false;
            this.currentExperience = 0;
            this.movementTimeLimit = 500;
            this.moveAllowed = true;
        }
    }
}
