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

namespace Genna.GameObjects.Enemies.Mushinis
{
    class Mushini : Enemy
    {
        public Level currentLevel;
        public BugState bugState;
        public int frame;
        public double time = 0.0;
        public Texture2D[] spriteSheet;
        public Vector2 initPos;
        public int distance;

        public enum BugState
        {
            Left,
            Right
        }

        public Mushini(int pX, int pY, Game1 pGame, int pWidth = 96, int pHeight = 32, int pSpeed = 2, int pHealth = 200, int pDist = 400)
            : base(pX, pY, pWidth, pHeight, pSpeed, pHealth)
        {
            XSpeed = 2;
            YSpeed = 0;
            game = pGame;
            currentLevel = game.CurrentLevel;
            bugState = BugState.Right;
            spriteSheet = new Texture2D[9];
            distance = pDist;
            Health = pHealth;

            int i = 0;
            while (i < 9)
            {
                i++;
                spriteSheet[i - 1] = pGame.Content.Load<Texture2D>("Sprites/Enemies/Mushini/" + i);
            }

            frame = 0;

            initPos = new Vector2(pX, pY);
        }

        public override void Move()
        {
            if (currentLevel != null)
            {

                foreach(Tile tile in game.CurrentLevel.TileList)
                {
                    if (Rect.Intersects(tile.Rectangle))
                    {
                        Rect = pastRect;
                        XSpeed = -XSpeed;

                        if (bugState == BugState.Right)
                        {
                            bugState = BugState.Left;
                        }
                        else
                        {
                            bugState = BugState.Right;
                        }
                    }
                }
            }
            else
            {
                double pastTime = time;
                time = game.theGameTime.ElapsedGameTime.TotalMilliseconds;
                if (frame == 8 && time - pastTime >= 10000.0)
                {
                    Rect = pastRect;
                    XSpeed = -XSpeed;

                    if (bugState == BugState.Right)
                    {
                        bugState = BugState.Left;
                    }
                    else
                    {
                        bugState = BugState.Right;
                    }
                }
            }

            if (Rect.Intersects(Zanaj.getInstance().Rect))
            {
                Zanaj.getInstance().Health -= (int)((2.0f * ((1100.0f - (float)Zanaj.getInstance()._CombinedDurability) / 1100.0f)));
                Zanaj.getInstance().Rect = Zanaj.getInstance().pastRect;
                
                if(Zanaj.getInstance().X > this.X)
                {
                    Zanaj.getInstance().X++;
                }
                else if (Zanaj.getInstance().X < this.X)
                {
                    Zanaj.getInstance().X--;
                }

                if (Zanaj.getInstance().Y > this.Y)
                {
                    Zanaj.getInstance().Y++;
                }
                else if (Zanaj.getInstance().Y < this.Y)
                {
                    Zanaj.getInstance().Y--;
                }

                this.Rect = this.pastRect;
            }

            if (X > initPos.X + distance || X < initPos.X)
            {
                Rect = pastRect;
                XSpeed = -XSpeed;

                 if (bugState == BugState.Right)
                {
                    bugState = BugState.Left;
                }
                else
                {
                    bugState = BugState.Right;
                }
            }

            List<Bullet> toRemove = new List<Bullet>();
            List<Bullet> bullets = Zanaj.getInstance()._bullets;
            
            if(Zanaj.instance != null)
            {
                foreach(Bullet b in Zanaj.instance._bullets)
                {
                    if (Rect.Intersects(b.Rect))
                    {
                        this.Health-= b.BulletDamage;
                        toRemove.Add(b);
                    }
                }
            }   
            for (int i = 0; i < toRemove.Count; i++)
            {
                Zanaj.getInstance()._bullets.Remove(toRemove[i]);
            }

            base.Move();
        }

        public override void Update()
        {
            frame = (frame + 1) % 9;
            if (frame > 9)
                frame = 0;
            this.Move();
            
            if (Health < 0)
            {
                isDead = true;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if(bugState == BugState.Right)
                spriteBatch.Draw(spriteSheet[frame], Rect, Color.White);   
            else
                spriteBatch.Draw(spriteSheet[frame], Rect, null, Color.White, 0, Vector2.Zero, SpriteEffects.FlipHorizontally, 0);
        }
    }
}
