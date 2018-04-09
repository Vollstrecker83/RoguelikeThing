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
        public float Mitigation { get { return mitigation; } }
        public float BlockChance { get { return blockChance; } }
        public int MaxDEXBonus { get { return maxDEXBonus; } }
        public int MaxSPDBonus { get { return maxSPDBonus; } }
        public int MinSTRRequirement { get { return minSTRRequirement; } }
        #endregion
    }
}
