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

namespace Genna.Items.Weapons
{
    class Raygun : Weapon
    {
        public Raygun(int pTier, int pType, string pId, Zanaj pZan, 
            int pWeaponType, int pDamage, int pFireRate, int pSpreadBullets, int pBulletSize, int pBulletSpeed) 
            : base(pTier, pType, pId, pZan,pWeaponType,pDamage,pFireRate,pSpreadBullets,pBulletSize,pBulletSpeed)
        {
        }

        public void CheckEquipped()
        {
        }
    }
}
