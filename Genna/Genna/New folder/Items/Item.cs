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
using Genna.GameObjects;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Genna.Items
{
    //Jordan
    public abstract class Item : GameObject
    {
        protected int tier;
        protected int type;
        protected string id;
        protected int _value;
        protected Zanaj zan;
        protected Texture2D image;
        protected bool isActive;
        protected bool equipped;
        protected bool legendary;

        public virtual int Durability
        {
            get;
            set;
        }
        public virtual int Damage
        {
            get;
            set;
        }
        public virtual int FireRate
        {
            get;
            set;
        }
        public virtual int SpreadBullets
        {
            get;
            set;
        }
        public virtual int BulletSize
        {
            get;
            set;
        }
        public virtual int BulletSpeed
        {
            get;
            set;
        }
        public virtual int PlusLuck
        {
            get;
            set;
        }
        public virtual int PlusPoisonResist
        {
            get;
            set;
        }
        public virtual int PlusHealth
        {
            get;
            set;
        }
        public virtual int PlusSpeed
        {
            get;
            set;
        }
        public bool Legendary
        {
            get { return legendary; }
            set { legendary = value; }
        }
        public bool Equipped
        {
            get { return equipped; }
            set { equipped = value; }
        }
        public int Tier
        {
            get { return tier; }
            set { tier = value; }
        }
        public int Type
        {
            get { return type; }
            set { type = value; }
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
        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        public int Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public Item(Texture2D pImage, int pTier, int pType, string pId, int pValue,int pX = 0, int pY = 0, int pWidth = 50, int pHeight = 50, bool pIsActive = true) : base(pX,pY,pWidth,pHeight,0)
        {
            image = pImage;
            tier = pTier;
            type = pType;
            id = pId;
            _value = pValue;
            X = pX;
            Y = pY;
            Width = pWidth;
            Height = pHeight;
            isActive = pIsActive;
            equipped = false;
            legendary = false;
        }

        public override string ToString()
        {
            return id + ": Type " + type + " Tier " + tier + " Value " + _value + "\n      ";
        }
    }
}
