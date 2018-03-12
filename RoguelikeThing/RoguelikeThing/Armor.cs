using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeThing
{
    public class Armor:Item
    {
        public enum ArmorClass { None = 1, Light, Medium, Heavy, Shield };
        #region Private Member Variables
        private float mitigation;
        private float blockChance;
        private int maxDEXBonus;
        private int maxSPDBonus;
        private int minSTRRequirement;
        #endregion

        #region Accessors/Mutators
        public float Mitigation => mitigation;
        public float BlockChance => blockChance;
        public int MaxDEXBonus => maxDEXBonus;
        public int MaxSPDBonus => maxSPDBonus;
        public int MinSTRRequirement => minSTRRequirement;
        #endregion
    }
}
