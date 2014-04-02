using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

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
using Genna.GameObjects.Enemies.Mushinis;
using Genna.GameObjects.Characters;
using Genna.GameObjects.Characters.Players.Zanaj_Akari;
using Genna.GameObjects.Puzzles;
using Genna.GameObjects.Puzzles.Levers;

namespace Genna.Levels.Outside
{
    public class Cave : Level
    {
        private int timer;

        Texture2D particleImg;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game1 object</param>
        public Cave(Game1 game)
        {
            LoadMap("Cave.txt");
            Name = "Cave";
            Level.game = game;

            particles = new List<Particle>();
            particleImg = game.Content.Load<Texture2D>("Main_Menu/particle");

            Random rand = new Random();

            int i = 0;
            while (i < 1500)
            {
                int xSpd = rand.Next(25) - 10;
                int ySpd = rand.Next(10) - 5;

                int dimensions = 96 + rand.Next(97);

                Particle p = new Particle(xSpd, ySpd, game);
                p.Rect = new Rectangle(rand.Next(game.GraphicsDevice.Viewport.Width), rand.Next(game.GraphicsDevice.Viewport.Height), dimensions, dimensions);

                p.Color = new Color(18 + rand.Next(10), 3 + rand.Next(5), 25 + rand.Next(20));

                particles.Add(p);
                i++;
            }

            BackgroundRect = new Rectangle((int)game.camera.Center.X - game.GraphicsDevice.Viewport.Width / 2, (int)game.camera.Center.Y - game.GraphicsDevice.Viewport.Height / 2, game.GraphicsDevice.Viewport.Width * 2, game.GraphicsDevice.Viewport.Height * 2);

            enemies = new List<GameObjects.Enemies.Enemy>();
            enemies.Add(new Mushini(930, 40, game));
            enemies.Add(new Mushini(900, 140, game));
            enemies.Add(new Mushini(680, 460, game));
            enemies.Add(new Mushini(600, 90, game));
            enemies.Add(new Mushini(160, 720, game));
            enemies.Add(new Mushini(300, 720, game));
            enemies.Add(new Mushini(450, 720, game));
            enemies.Add(new Mushini(1200, 650, game));

            rndChests = new List<GameObjects.Chests.RandomChest.RandomChest>();

            rndChestItemLists = new List<List<Item>>();
            chestItems = new List<Item>();

            spriteFont = game.Content.Load<SpriteFont>("Fonts/itemOverload");

            ri = new RandomItem(Zanaj.getInstance());

            MapEdgeRRect = new Rectangle(Map.Width - 400, 0, 800, 800);
            MapEdgeLRect = new Rectangle(-400, 0, 800, 800);
        }

        // Updates everything
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Rectangle backRect = BackgroundRect;
            backRect.X = (int)game.camera.Center.X - game.GraphicsDevice.Viewport.Width / 2;
            backRect.Y = (int)game.camera.Center.Y - game.GraphicsDevice.Viewport.Height / 2;
            BackgroundRect = backRect;

            Rectangle rectL = MapEdgeLRect;
            Rectangle rectR = MapEdgeRRect;

            rectL.Y = (int)game.camera.Center.Y - 30;
            rectR.Y = (int)game.camera.Center.Y - 30;

            MapEdgeLRect = rectL;
            MapEdgeRRect = rectR;

            zanaj.Update(this);

            foreach (Particle p in particles)
            {
                p.Move();
                p.Screenwrap(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            }

            timer++;

            if (timer >= 60)
            {
                Zanaj.getInstance()._EqBreastplate.PlusPoisonResist--;
                Vector2 v = Zanaj.getInstance()._eqBreastplateIndex;
                if (Zanaj.getInstance()._inventory.InvSlot[(int)v.X, (int)v.Y] != null)
                {
                    Zanaj.getInstance()._inventory.InvSlot[(int)v.X, (int)v.Y].PlusPoisonResist--;
                }
                timer = 0;
            }

            Console.WriteLine("Zanaj: {0},{1}\nSpeed: {2},{3}     IsJumping?: {4}", zanaj.X, zanaj.Y, zanaj.XSpeed, zanaj.YSpeed, zanaj.Jumping);
        }

        // Draws everything
        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(BackgroundTexture, BackgroundRect, Color.White);

            foreach (Tile tile in TileList)
            {
                spriteBatch.Draw(tile.Texture, tile.Rectangle, Color.White);
            }

            base.Draw(spriteBatch, gameTime);

            foreach (Particle p in particles)
            {
                spriteBatch.Draw(particleImg, p.Rect, p.Color/*new Color(1.0f, 0.5f, 0.25f)*/);
            }

            zanaj.Draw(spriteBatch);

            spriteBatch.Draw(MapEdgeR, MapEdgeRRect, Color.White);
            spriteBatch.Draw(MapEdgeL, MapEdgeLRect, Color.White);
        }
    }
}
