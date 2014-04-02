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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Genna.Items.Armors
{
    //Jordan
    public class Armor : Item
    {
        protected int durability;

        public override int Durability
        {
            get { return durability; }
            set { durability = value; }
        }

        public Armor(Texture2D pImage, int pTier, int pType, string pId, int pValue, int pDurability)
            : base(pImage, pTier, pType, pId, pValue)
        {
            durability = pDurability;
            equipped = false;
            legendary = false;
        }

        public override string ToString()
        {
            return base.ToString() + " Equipped " + equipped + " Durability " + durability;

        }
    }
}
