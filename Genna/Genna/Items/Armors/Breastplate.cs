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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Genna.Items.Armors
{
    //Jordan
    public class Breastplate : Armor
    {
        protected bool hasPoisonResist;
        protected int plusPoisonResist;
        protected int plusHealth;

        public bool HasPoisonResist
        {
            get { return hasPoisonResist; }
            set { hasPoisonResist = value; }
        }
        public override int PlusPoisonResist
        {
            get { return plusPoisonResist; }
            set { plusPoisonResist = value; }
        }
        public override int PlusHealth
        {
            get { return plusHealth; }
            set { plusHealth = value; }
        }

        public Breastplate(Texture2D pImage, int pTier, int pType, string pId, int pValue, int pDurability, bool pHasPoisonResist, int pPlusPoisonResist, int pPlusHealth)
            : base(pImage, pTier, pType, pId, pValue, pDurability)
        {
            hasPoisonResist = pHasPoisonResist;
            plusPoisonResist = pPlusPoisonResist;
            plusHealth = pPlusHealth;
        }

        public void AddStats()
        {
        }

        public override string ToString()
        {
            return base.ToString() + " PlusHealth " + plusHealth + " PlusPoisonResist " + plusPoisonResist;
        }
    }
}
