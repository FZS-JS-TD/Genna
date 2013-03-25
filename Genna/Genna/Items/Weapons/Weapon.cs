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
    class Weapon : Item
    {
        protected int weaponType;
        protected int damage;
        protected int fireRate;
        protected int spreadBullets;
        protected int bulletSize;
        protected int bulletSpeed;
        protected bool equipped;

        public int WeaponType
        {
            get { return weaponType; }
            set { weaponType = value; }
        }
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        public int FireRate
        {
            get { return fireRate; }
            set { fireRate = value; }
        }
        public int SpreadBullets
        {
            get { return spreadBullets; }
            set { spreadBullets = value; }
        }
        public int BulletSize
        {
            get { return bulletSize; }
            set { bulletSize = value; }
        }
        public int BulletSpeed
        {
            get { return bulletSpeed; }
            set { bulletSpeed = value; }
        }
        public bool Equipped
        {
            get { return equipped; }
            set { equipped = value; }
        }

        public Weapon(int pTier, int pType, string pId, Zanaj pZan, 
            int pWeaponType, int pDamage, int pFireRate, int pSpreadBullets, int pBulletSize, int pBulletSpeed) 
            : base(pTier, pType, pId, pZan)
        {
            weaponType = pWeaponType;
            damage = pDamage;
            fireRate = pFireRate;
            spreadBullets = pSpreadBullets;
            bulletSize = pBulletSize;
            bulletSpeed = pBulletSpeed;
            equipped = false;
        }
    }
}
