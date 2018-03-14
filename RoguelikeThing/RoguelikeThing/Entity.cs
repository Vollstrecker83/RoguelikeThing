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
        public int BaseHealth => baseHealth;
        public int BaseMana => baseMana;
        public int CurrentHealth { get => currentHealth; set => currentHealth = value; }
        public int CurrentMana { get => currentMana; set => currentMana = value; }
        public int BaseSTR => baseSTR;
        public int BaseDEX => baseDEX;
        public int BaseCON => baseCON;
        public int BaseMND => baseMND;
        public int BaseSPD => baseSPD;
        public int CurrentSTR { get => currentSTR; set => currentSTR = value; }
        public int CurrentDEX { get => currentDEX; set => currentDEX = value; }
        public int CurrentCON { get => currentCON; set => currentCON = value; }
        public int CurrentMND { get => currentMND; set => currentMND = value; }
        public int CurrentSPD { get => currentSPD; set => currentSPD = value; }
        public int BaseLightningResist => baseLightningResist;
        public int BaseFireResist => baseFireResist;
        public int BaseFrostResist => baseFrostResist;
        public int BasePoisonResist => basePoisonResist;
        public int CurrentLightningResist { get => currentLightningResist; set => currentLightningResist = value; }
        public int CurrentFireResist { get => currentFireResist; set => currentFireResist = value; }
        public int CurrentFrostResist { get => currentFrostResist; set => currentFrostResist = value; }
        public int CurrentPoisonResist { get => currentPoisonResist; set => currentPoisonResist = value; }
        public int Level { get => level; set => level = value; }
        public float BaseCrit => baseCrit;
        public float CurrentCrit { get => currentCrit; set => currentCrit = value; }
        public int VisionRadius { get => visionRadius; set => visionRadius = value; }
        public int BaseArmor => baseArmor;
        public int BaseDodge => baseDodge;
        public int CurrentArmor { get => currentArmor; set => currentArmor = value; }
        public int CurrentDodge { get => currentDodge; set => currentDodge = value; }
        public Weapon EquippedWeapon { get => equippedWeapon; set => equippedWeapon = value; }
        public Armor EquippedHead { get => equippedHead; set => equippedHead = value; }
        public Armor EquippedChest { get => equippedChest; set => equippedChest = value; }
        public Armor EquippedLegs { get => equippedLegs; set => equippedLegs = value; }
        public Armor EquippedHands { get => equippedHands; set => equippedHands = value; }
        public Armor EquippedFeet { get => equippedFeet; set => equippedFeet = value; }
        internal Armor EquippedShield { get => equippedShield; set => equippedShield = value; }
        public int BaseToHit => baseToHit;
        public int CurrentToHit { get => currentToHit; set => currentToHit = value; }
        public int HealthScaling { get => healthScaling; set => healthScaling = value; }
        public int ManaScaling { get => manaScaling; set => manaScaling = value; }
        public bool IsDead { get => isDead; set => isDead = value; }
        #endregion

        /// <summary>
        /// Verifies that the entity is attempting to move into a valid tile
        /// </summary>
        /// <param name="map"></param>
        /// <param name="attemptedMovePosition"></param>
        /// <returns></returns>
        public bool CanMoveIntoTile(Terrain map, Point attemptedMovePosition)
        {
            // Makes sure we stay within the map boundaries
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
