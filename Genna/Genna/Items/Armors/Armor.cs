using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genna.Items;
using Genna.GameObjects.Chests;
using Genna.GameObjects.Chests.RandomChest;
using Genna.GameObjects.Chests.SpecialChest;
using Genna.Items.Moneys;
using Genna.Items.Weapons;
using Genna.GameObjects.Characters;
using Genna.GameObjects.Characters.Players;
using Genna.GameObjects.Characters.Players.Zanaj_Akari;

namespace Genna.Items.Armors
{
    class Armor : Item
    {
        protected int armorType;
        protected int durability;
        protected bool equipped;

        public int ArmorType
        {
            get { return armorType; }
            set { armorType = value; }
        }
        public int Durability
        {
            get { return durability; }
            set { durability = value; }
        }
        public bool Equipped
        {
            get { return equipped; }
            set { equipped = value; }
        }

        public Armor(int pTier, int pType, string pId, Zanaj pZan, int pArmorType, int pDurability) 
            : base(pTier, pType, pId, pZan)
        {
            armorType = pArmorType;
            durability = pDurability;
            equipped = false;
        }
    }
}
