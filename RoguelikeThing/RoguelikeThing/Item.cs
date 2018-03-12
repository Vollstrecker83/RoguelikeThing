using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeThing
{
    public class Item:GameObject
    {
        // TODO: Items will need a way to add stat bonuses/resists/etc
        #region Private Member Variables
        private int weight;
        private int durability;
        private bool canBeDamaged;          // Mostly so we can have a flag to set if an item doesn't take durability damage
        private int durabilityLossPerUse;   // Some items may use different amounts of durability per use
        #endregion

        #region Accessors and Mutators
        public int Weight => weight;
        public int Durability => durability;
        public bool CanBeDamaged => canBeDamaged;
        public int DurabilityLossPerUse => durabilityLossPerUse;
        #endregion
    }
}
