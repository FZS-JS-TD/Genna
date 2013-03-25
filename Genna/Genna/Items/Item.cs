using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genna.GameObjects.Chests;
using Genna.GameObjects.Chests.RandomChest;
using Genna.GameObjects.Chests.SpecialChest;
using Genna.Items.Moneys;
using Genna.Items.Armors;
using Genna.Items.Weapons;
using Genna.GameObjects.Characters;
using Genna.GameObjects.Characters.Players;
using Genna.GameObjects.Characters.Players.Zanaj_Akari;

namespace Genna.Items
{
    // Jordan
    class Item
    {
        protected int tier;
        protected int type;
        protected string id;
        protected Zanaj zan;

        public int Tier
        {
            get { return tier; }
            set { tier = value; }
        }
        public int Type
        {
            get{ return type; }
            set{ type = value; }
        }
        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        public Zanaj Zan
        {
            get { return zan; }
            set { zan = value; }
        }

        public Item(int pTier, int pType, string pId, Zanaj pZan)
        {
            tier = pTier;
            type = pType;
            id = pId;
            zan = pZan;

            /*
            //if type is coal
            if (pType == 0)
            {
                //pass in for coal
            }
            
            //if type is money
            else if (pType == 1)
            {
                //if tier is 1
                if (pTier == 1)
                {
                    //pass in for tier 1 money
                }
                else if (pTier == 2)
                {
                    //pass in for tier 2 money
                }
                else if (pTier == 3)
                {
                    //pass in for tier 3 money
                }
            }

            //if type is armor
            else if (pType == 2)
            {
                //if tier is 1
                if (pTier == 1)
                {
                    //pass in for tier 1 armor
                }
                else if (pTier == 2)
                {
                    //pass in for tier 2 armor
                }
                else if (pTier == 3)
                {
                    //pass in for tier 3 armor
                }
            }

            //if type is weapon
            else if (pType == 3)
            {
                //if tier is 1
                if (pTier == 1)
                {
                    //pass in for tier 1 weapon
                }
                else if (pTier == 2)
                {
                    //pass in for tier 2 weapon
                }
                else if (pTier == 3)
                {
                    //pass in for tier 3 weapon
                }
            }
            */
        }
    }
}
