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

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Genna.GameObjects
{
    public class Bullet : GameObject
    {
        protected bool active;
        protected int dir;
        protected Texture2D image;
        protected int speed;

        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }

        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        public Bullet(int pX, int pY, int pSpeed, int pHealth, int pSize, Game1 pGame, int pDir)
            : base(pX, pY, (pSize * 5), (pSize * 5) , pSpeed, pHealth)
        {
            active = true;
            dir = pDir;
            speed = pSpeed;
            this.Rect = new Rectangle(pX, pY, pSize * 5, pSize * 5);
            image = pGame.Content.Load<Texture2D>("Main_Menu/particle");
            SoundEffect pewPewPew = pGame.Content.Load<SoundEffect>("Sounds/SFX/pew");
            pewPewPew.Play();
        }

        public void Move(int Dir)
        {
            if (Dir != 4)
            {
                dir = Dir;
            }

            if (dir == 0)
            {
                Move(0, -speed);
            }
            else if (dir == 1)
            {
                Move(speed, 0);
            }
            else if (dir == 2)
            {
                Move(0, speed);
            }
            else if (dir == 3)
            {
                Move(-speed, 0);
            }
        }
    }
}
