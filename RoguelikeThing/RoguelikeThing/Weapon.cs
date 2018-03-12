using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeThing
{
    public class Weapon:Item
    {
        enum DamageType { Slashing = 1, Piercing, Crushing }; // For future use
        enum WieldType { OneHanded = 1, TwoHanded };
        enum WeaponType { Melee = 1, Ranged };
        enum BonusDamageType { Lightning = 1, Fire, Frost, Poison, Physical };

        #region Private Member Variables
        private int damage;
        private int bonusDamage;
        #endregion
    }
}
