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
    class Gloves : Armor
    {
        protected int plusLuck;

        public int PlusLuck
        {
            get { return plusLuck; }
            set { plusLuck = value; }
        }

        public Gloves(int pTier, int pType, string pId, Zanaj pZan, int pArmorType, int pDurability, int pPlusLuck) 
            : base(pTier,pType, pId, pZan, pArmorType,pDurability)
        {
            plusLuck = pPlusLuck;
        }

        public void CheckEquipped()
        {
            if (equipped == true)
            {
                zan.Luck += plusLuck;
            }
        }
    }
}
