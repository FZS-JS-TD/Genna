using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genna.GameObjects.Chests;
using Genna.Items;
using Genna.GameObjects.Chests.RandomChest;
using Genna.GameObjects.Chests.SpecialChest;
using Genna.Items.Moneys;
using Genna.Items.Armors;
using Genna.Items.Weapons;
using Genna.GameObjects.Characters;
using Genna.GameObjects.Characters.Players;
using Genna.GameObjects.Characters.Players.Zanaj_Akari;

namespace Genna.GameObjects.Enemies
{
    public abstract class Enemy : GameObject
    {
        public bool isDead;

        public Enemy(int pX, int pY, int pWidth, int pHeight, int pSpeed, int pHealth)
            : base(pX, pY, pWidth, pHeight, pSpeed = 0, pHealth = 32)
        {
            isDead = false;
        }

        public abstract void Update();

        public abstract void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch);
    }
}
