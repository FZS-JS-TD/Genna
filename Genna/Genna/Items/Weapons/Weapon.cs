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

namespace Genna.Items.Weapons
{
    //Jordan
    public class Weapon : Item
    {
        protected int damage;
        protected int fireRate;
        protected int spreadBullets;
        protected int bulletSize;
        protected int bulletSpeed;

        public override int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        public override int FireRate
        {
            get { return fireRate; }
            set { fireRate = value; }
        }
        public override int SpreadBullets
        {
            get { return spreadBullets; }
            set { spreadBullets = value; }
        }
        public override int BulletSize
        {
            get { return bulletSize; }
            set { bulletSize = value; }
        }
        public override int BulletSpeed
        {
            get { return bulletSpeed; }
            set { bulletSpeed = value; }
        }

        public Weapon(Texture2D pImage, int pTier, int pType, string pId, int pValue,
            int pDamage, int pFireRate, int pSpreadBullets, int pBulletSize, int pBulletSpeed)
            : base(pImage, pTier, pType, pId, pValue)
        {
            damage = pDamage;
            fireRate = pFireRate;
            spreadBullets = pSpreadBullets;
            bulletSize = pBulletSize;
            bulletSpeed = pBulletSpeed;
            equipped = false;
            legendary = false;
        }

        public Weapon(Weapon w)
            : base(w.image, w.tier, w.type, w.id + "+", w._value)
        {
            damage = w.damage;
            fireRate = w.fireRate;
            spreadBullets = w.spreadBullets;
            bulletSize = w.bulletSize;
            bulletSpeed = w.bulletSpeed;
            equipped = false;
            legendary = w.legendary;
        }

        public override string ToString()
        {
            return base.ToString() + " Equipped " + equipped + " Damage " + damage + " FireRate " + fireRate + " BulletSize " + bulletSize
                + " BulletSpeed " + bulletSpeed;
        }
    }
}
