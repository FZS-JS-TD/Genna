using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genna.Items;
using Genna.GameObjects.Chests;
using Genna.GameObjects.Chests.RandomChest;
using Genna.GameObjects.Chests.SpecialChest;
using Genna.Items.Moneys;
using Genna.Items.Armors;
using Genna.Items.Weapons;
using Genna.GameObjects.Characters;
using Genna.GameObjects.Characters.Players;
using Genna.GameObjects.Characters.Players.Zanaj_Akari;

namespace Genna.Items.Armors
{
    class Breastplate : Armor
    {
        protected int plusPoisonResist;
        protected int plusHealth;

        public int PlusPoisonResist
        {
            get { return plusPoisonResist; }
            set { plusPoisonResist = value; }
        }
        public int PlusHealth
        {
            get { return plusHealth; }
            set { plusHealth = value; }
        }

        public Breastplate(int pTier, int pType, string pId, Zanaj pZan, int pArmorType, int pDurability, int pPlusPoisonResist, int pPlusHealth) 
            : base(pTier,pType, pId, pZan, pArmorType,pDurability)
        {
            plusPoisonResist = pPlusPoisonResist;
            plusHealth = pPlusHealth;
        }

        public void CheckEquipped()
        {
            if (equipped == true)
            {
                zan.PoisonResist += plusPoisonResist;
                zan.Health += plusHealth;
            }
        }
    }
}
