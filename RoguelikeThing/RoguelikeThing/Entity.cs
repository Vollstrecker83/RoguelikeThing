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
    /// Organization class to hold data for Player and Monster subtypes
    /// </summary>
    public abstract class Entity:GameObject
    {
        public enum ClassType { Warrior = 1, Ranger, Mage };
        public enum StatusEffect
        {
            LightlyEncumbered = 1, ModeratelyEncumbered, HeavilyEncumbered, Rooted, Stunned, Slowed,
            Silenced, Blinded, Deafened, Poisoned, Bleeding, Sleeping
        };
        public enum EquipmentSlot { Head = 1, Chest, Legs, Hands, Feet, Weapon, Shield };
        public enum EntityStat { STR = 1, DEX, CON, MND, SPD };

        #region Private Member Variables
        private Texture2D corpseTexture;
        private int baseHealth, baseMana;
        private int currentHealth, currentMana;
        private int baseSTR, baseDEX, baseCON, baseMND, baseSPD;
        private int currentSTR, currentDEX, currentCON, currentMND, currentSPD;
        private int baseLightningResist, baseFireResist, baseFrostResist, basePoisonResist;
        private int currentLightningResist, currentFireResist, currentFrostResist, currentPoisonResist;
        private int level;
        private float baseCrit, currentCrit;
        private int baseToHit, currentToHit;
        private int visionRadius;
        private int baseArmor;
        private int baseDodge;
        private int currentArmor;
        private int currentDodge;
        private Weapon equippedWeapon;
        private Armor equippedHead, equippedChest, equippedLegs, equippedHands, equippedFeet, equippedShield;
        private int healthScaling, manaScaling;                     // Looks like we'll do a Die-roll for this eventually
        private bool isDead;
        #endregion

        #region Accessors and Mutators
        public int BaseHealth { get { return baseHealth; } }
        public int BaseMana { get { return baseMana; } }
        public int CurrentHealth 
        { 
            get { return currentHealth; } 
            set 
            { 
                currentHealth = value;
                if ( currentHealth < 0 )
                {
                    currentHealth = 0;
                }
            } 
        
        }
        public int CurrentMana 
        { 
            get { return currentMana; } 
            set 
            { 
                currentMana = value; 

                if ( currentMana < 0 )
                {
                    currentMana = 0;
                }
            } 
        }
        public int BaseSTR { get { return baseSTR; } }
        public int BaseDEX { get { return baseDEX; } }
        public int BaseCON { get { return baseCON; } }
        public int BaseMND { get { return baseMND; } }
        public int BaseSPD { get { return baseSPD; } }
        public int CurrentSTR 
        { 
            get { return currentSTR; } 
            set 
            { 
                currentSTR = value;

                if ( currentSTR < 0 )
                {
                    currentSTR = 0;
                }
            }
        }
        public int CurrentDEX
        { 
            get { return currentDEX; } 
            set 
            { 
                currentDEX = value;

                if ( currentDEX < 0 )
                {
                    currentDEX = 0;
                }
            }
        }
        public int CurrentCON
        { 
            get { return currentCON; } 
            set 
            { 
                currentCON = value;

                if ( currentCON < 0 )
                {
                    currentCON = 0;
                }
            }
        }
        public int CurrentMND
        { 
            get { return currentMND; } 
            set 
            { 
                currentMND = value;

                if ( currentMND < 0 )
                {
                    currentMND = 0;
                }
            }
        }
        public int CurrentSPD
        { 
            get { return currentSPD; } 
            set 
            { 
                currentSPD = value;

                if ( currentSPD < 0 )
                {
                    currentSPD = 0;
                }
            }
        }
        public int BaseLightningResist { get { return baseLightningResist; } }
        public int BaseFireResist { get { return baseFireResist; } }
        public int BaseFrostResist { get { return baseFrostResist; } }
        public int BasePoisonResist { get { return basePoisonResist; } }
        public int CurrentLightningResist
        { 
            get { return currentLightningResist; } 
            set { currentLightningResist = value; }
        }
        public int CurrentFireResist
        { 
            get { return currentFireResist; } 
            set { currentFireResist = value; }
        }
        public int CurrentFrostResist 
        { 
            get { return currentFrostResist; } 
            set { currentFrostResist = value; }
        }
        public int CurrentPoisonResist 
        { 
            get { return currentPoisonResist; }
            set { currentPoisonResist = value; }
        }
        public int Level 
        { 
            get { return level; }
            set { level = value; }
        }
        public float BaseCrit { get { return baseCrit; } }
        public float CurrentCrit 
        { 
            get { return currentCrit; }
            set 
            { 
                currentCrit = value;

                if( currentCrit < 0.0f )
                {
                    currentCrit = 0.0f;
                }
            }
        }
        public int VisionRadius 
        { 
            get { return visionRadius;  }
            set 
            {
                visionRadius = value; 

                if( visionRadius < 0 )
                {
                    visionRadius = 0;
                }
            }
        }
        public int BaseArmor { get { return baseArmor; } }
        public int BaseDodge { get { return baseDodge; } }
        public int CurrentArmor 
        { 
            get { return currentArmor; }
            set { currentArmor = value; }
        }
        public int CurrentDodge 
        { 
            get { return currentDodge; }
            set { currentDodge = value; }
        }
        public Weapon EquippedWeapon 
        { 
            get { return equippedWeapon; }
            set { equippedWeapon = value; }
        }
        public Armor EquippedHead 
        { 
            get { return equippedHead; }
            set { equippedHead = value; }
        }
        public Armor EquippedChest 
        { 
            get { return equippedChest; }
            set { equippedChest = value; }
        }
        public Armor EquippedLegs 
        { 
            get { return equippedLegs; }
            set { equippedLegs = value; }
        }
        public Armor EquippedHands 
        { 
            get { return equippedHands; }
            set { equippedHands = value; }
        }
        public Armor EquippedFeet 
        { 
            get { return equippedFeet; }
            set { equippedFeet = value; }
        }
        public Armor EquippedShield 
        { 
            get { return equippedShield; }
            set { equippedShield = value; }
        }
        public int BaseToHit { get { return baseToHit; } }
        public int CurrentToHit 
        { 
            get { return currentToHit; }
            set 
            {
                currentToHit = value; 

                if( currentToHit < 0 )
                {
                    currentToHit = 0;
                }
            }
        }
        public int HealthScaling 
        { 
            get { return healthScaling; }
            set { healthScaling = value; }
        }
        public int ManaScaling 
        { 
            get { return manaScaling; }
            set { manaScaling = value; }
        }
        public bool IsDead 
        { 
            get { return isDead; }
            set { isDead = value; }
        }
        #endregion

        public Entity(int level)
        { }

        /// <summary>
        /// Verifies that the entity is attempting to move into a valid tile
        /// </summary>
        /// <param name="map"></param>
        /// <param name="attemptedMovePosition"></param>
        /// <returns></returns>
        public bool CanMoveIntoTile(int level, Point attemptedMovePosition)
        {
            Terrain map = TerrainManager.GetCurrentMap(level);

            // Make sure we stay within the map boundaries
            if ((attemptedMovePosition.X >= map.MapSize.X || attemptedMovePosition.X < 0) ||
                (attemptedMovePosition.Y >= map.MapSize.Y || attemptedMovePosition.Y < 0))
            {
                return false;
            }

            // Checks if the tile has a collider
            if (map.GetTileAtGridPosition(attemptedMovePosition).IsCollider)
            {
                return false;
            }

            return true;
        }
    }
}
