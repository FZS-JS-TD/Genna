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
using Genna.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Genna.GameObjects.Enemies.Boss
{
    public class Boss : Enemy
    {
        public static Texture2D bossText;

        int timer;

        // Creates the boss
        public Boss(int x, int y, int width = 128, int height = 128, int speed = 0, int health = 10000)
            : base(x, y, width, height, speed, height)
        {
            Rect = new Rectangle(396, 224, 128, 128);
        }

        public void Load()
        {
        }

        // Overtime, damage the player, and take damage from bullets
        public override void Update()
        {
            timer++;
            if (timer > 30)
            {

                Zanaj.getInstance().Health--;
                timer = 0;
            }

            foreach (Bullet bull in Zanaj.getInstance()._bullets)
            {
                if (bull.Rect.Intersects(Rect))
                {
                    Health -= bull.BulletDamage;
                }
            }

            if (Health < 0)
            {
                Game1.gameMode = Game1.GameMode.GameOver;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(bossText, Rect, Color.White);
        }
    }
}
