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
    class Boots : Armor
    {
        protected int plusSpeed;

        public int PlusSpeed
        {
            get { return plusSpeed; }
            set { plusSpeed = value; }
        }

        public Boots(int pTier, int pType, string pId, Zanaj pZan, int pArmorType, int pDurability, int pPlusSpeed) 
            : base(pTier,pType, pId, pZan, pArmorType,pDurability)
        {
            plusSpeed = pPlusSpeed;
        }

        public void AddStats()
        {
            if (equipped == true)
            {
                zan.Speed += plusSpeed;
            }
        }
    }
}
