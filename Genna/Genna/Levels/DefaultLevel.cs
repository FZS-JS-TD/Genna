using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Genna.Items;
using Genna.GameObjects.Chests;
using Genna.GameObjects.Chests.RandomChest;
using Genna.GameObjects.Chests.SpecialChest;
using Genna.Items.Moneys;
using Genna.Items.Armors;
using Genna.Items.Weapons;
using Genna.GameObjects;
using Genna.GameObjects.Enemies;
using Genna.GameObjects.Characters;
using Genna.GameObjects.Characters.Players;
using Genna.GameObjects.Characters.Players.Zanaj_Akari;

namespace Genna.Levels
{
    //Forrest
    public class DefaultLevel : Level
    {
        public DefaultLevel (Game1 sentGame)
        {
            game = sentGame;
            Level.game = sentGame;
            zanaj = sentGame.Zanaj; 
            zanaj.X = sentGame.GraphicsDevice.Viewport.Width / 2;
            zanaj.Y = sentGame.GraphicsDevice.Viewport.Height / 2;
            rndChestItemLists = new List<List<Item>>();
            chestItems = new List<Item>();
            
            ri = new RandomItem(zanaj);
            rndChests = new List<RandomChest>();

            for (int k = 0; k < 10; k++)
            {
                rndChests.Add(new RandomChest(zanaj));
                rndChests[k].X = (100 * k);
                rndChests[k].Y = zanaj.Y + 32;
            }

            spriteFont = game.Content.Load<SpriteFont>("Fonts/itemOverload");

            _backgroundImage = game.Content.Load<Texture2D>(@"Main_Menu\Genna Symbol");

            // TODO: Add your initialization logic here
            particles = new List<Particle>();

            Random rand = new Random();

            int i = 0;
            while (i <300)
            {
                int xSpd = rand.Next(5) - 3;
                int ySpd = rand.Next(5) - 3;

                Particle p = new Particle(xSpd, ySpd, sentGame);
                p.Max = 0;
                p.Min = 0;
                p.Rect = new Rectangle(rand.Next(sentGame.GraphicsDevice.Viewport.Width), rand.Next(sentGame.GraphicsDevice.Viewport.Height), 48, 48);

                particles.Add(p);
                i++;
            }
        }

        public void LoadContent(ContentManager content)
        {
            enemies = new List<Enemy>();
            enemies.Add(new GameObjects.Enemies.Mushinis.Mushini(zanaj.X - 500, zanaj.Y + (zanaj.Height / 2), game));
            // TODO: use this.Content to load your game content here
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (Zanaj.getInstance().Y > game.GraphicsDevice.Viewport.Height / 2)
            {
                Zanaj.getInstance().Y = game.GraphicsDevice.Viewport.Height / 2;
                Zanaj.getInstance().YSpeed = 0;
                Zanaj.getInstance().Jumping = false;
            }

            foreach (Particle p in particles)
            {
                p.Move();
                p.Screenwrap(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            }

            zanaj.Update(this);

            List<Enemy> toKill = new List<Enemy>();
            foreach (Enemy enemy in enemies)
            {
                enemy.Update();

                if (enemy.isDead)
                {
                    toKill.Add(enemy);
                }
            }

            foreach (Enemy e in toKill)
            {
                enemies.Remove(e);
                rndChests.Add(new RandomChest(Zanaj.getInstance(), e.X, e.Y));
            }

            Console.WriteLine("Zanaj: {0},{1}\nZanSpeed: {2},{3}    Jumping?: {4}", zanaj.X, zanaj.Y, zanaj.XSpeed, zanaj.YSpeed, zanaj.Jumping);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_backgroundImage,
                             new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height),
                             Color.White);
            foreach (RandomChest rc in rndChests)
            {
                if (rc.IsActive)
                {
                    spriteBatch.Draw(rc.Image, rc.Rect, Color.White);
                }
            }

            foreach (Item i in chestItems)
            {
                if (i.IsActive)
                {
                    spriteBatch.Draw(i.Image, i.rect, Color.White);
                }
            }

            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            
            zanaj.Draw(spriteBatch);

            foreach (RandomChest rc in rndChests)
            {
                if (PressedOnlyOnce(Keys.Enter))
                {
                    if (zanaj.rect.Intersects(rc.Rect) && rc.IsActive == true)
                    {
                        rc.IsOpen = true;
                        rc.IsActive = false;
                        int prevChestItemCount = chestItems.Count;

                        for (int i = 0; i < rc.NumChestItems; i++)
                        {
                            chestItems.Add(ri.AddRandomItem());
                        }

                        for (int i = 0; i < chestItems.Count - prevChestItemCount; i++)
                        {
                            chestItems[i + prevChestItemCount].Rect = new Rectangle((rc.X - 50 + (50 * i)), (rc.Y), 50, 50);
                        }

                        rndChestItemLists.Add(chestItems);
                    }
                }

                if (rc.Rect.Intersects(zanaj.rect) && rc.IsActive && rc.IsOpen == false)
                {
                    spriteBatch.DrawString(spriteFont, "Press 'Enter' to Open Chest", new Vector2(rc.X, (rc.Y - 35)), Color.White);
                }
            }

            foreach (Item i in chestItems)
            {
                Rectangle itemRect = i.Rect;

                if (itemRect.Intersects(zanaj.rect) && i.IsActive)
                {
                    bool b = zanaj._inventory.AddItem(i);

                    if (!b)
                    {
                        spriteBatch.DrawString(spriteFont, "Too Many Items of This Type", new Vector2(itemRect.X, itemRect.Y), Color.White);
                    }
                    else
                    {
                        i.IsActive = false;
                        //chestItems.Remove(i);
                    }
                }
            }
        }
    }
}
