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
    public class Bridge : Level
    {
        // Attributes
        private Puzzle puzzle;
        private Gate gate;
        private Lever lever1;
        private Lever lever2;
        private Lever lever3;

        /// <summary>
        /// Costructor
        /// </summary>
        /// <param name="game">The Game1 Object</param>
        public Bridge(Game1 game)
        {
            LoadMap("ConstructionBridge.txt");
            Name = "Bridge";
            Level.game = game;

            BackgroundRect = new Rectangle((int)game.camera.Center.X - game.GraphicsDevice.Viewport.Width / 2, (int)game.camera.Center.Y - game.GraphicsDevice.Viewport.Height / 2, game.GraphicsDevice.Viewport.Width * 2, game.GraphicsDevice.Viewport.Height * 2);

            zanaj = Game1.zanaj;

            MapEdgeRRect = new Rectangle(Map.Width - 400, 0, 800, 800);
            MapEdgeLRect = new Rectangle(-400, 0, 800, 800);

            gate = new Gate(game, 1728, 384, 1, 11);
            GateList.Add(gate);
            lever1 = new Lever(game, 600, 256);
            lever2 = new Lever(game, 900, 480);
            lever3 = new Lever(game, 1024, 256);
            lever1.Load();
            lever2.Load();
            lever3.Load();
            LeverList.Add(lever1);
            LeverList.Add(lever2);
            LeverList.Add(lever3);
            puzzle = new Puzzle(game, this);

            enemies = new List<GameObjects.Enemies.Enemy>();
            enemies.Add(new Mushini(850, 610, game));
            enemies.Add(new Mushini(1125, 490, game));
            enemies.Add(new Mushini(1350, 530, game));
            enemies.Add(new Mushini(1500, 700, game));

            rndChests = new List<GameObjects.Chests.RandomChest.RandomChest>();

            rndChestItemLists = new List<List<Item>>();
            chestItems = new List<Item>();

            spriteFont = game.Content.Load<SpriteFont>("Fonts/itemOverload");

            ri = new RandomItem(Zanaj.getInstance());
        }

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

            Console.WriteLine("Zanaj: {0},{1}\nSpeed: {2},{3}     IsJumping?: {4}", zanaj.X, zanaj.Y, zanaj.XSpeed, zanaj.YSpeed, zanaj.Jumping);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(BackgroundTexture, BackgroundRect, Color.White);
           
            foreach (Tile tile in TileList)
            {
                spriteBatch.Draw(tile.Texture, tile.Rectangle, Color.White);
            }
            
            puzzle.GateControl(spriteBatch);

            base.Draw(spriteBatch, gameTime);

            zanaj.Draw(spriteBatch);

            spriteBatch.Draw(MapEdgeR, MapEdgeRRect, Color.White);
            spriteBatch.Draw(MapEdgeL, MapEdgeLRect, Color.White);
        }
    }
}
