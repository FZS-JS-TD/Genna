using System;
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
using Genna.GameObjects.Enemies.Mushinis;
using Genna.GameObjects.Characters;
using Genna.GameObjects.Characters.Players.Zanaj_Akari;
using Genna.GameObjects.Puzzles;
using Genna.GameObjects.Puzzles.Levers;
using Genna.GameObjects.Enemies.Boss;

namespace Genna.Levels
{
    public class BossLevel : Level
    {
        private Boss boss;

        public BossLevel(Game1 game)
        {
            LoadMap("BossLevel.txt");
            Name = "BossLevel";
            rndChests = new List<RandomChest>();
            chestItems = new List<Item>();
            enemies = new List<GameObjects.Enemies.Enemy>();
            BackgroundRect = new Rectangle((int)game.camera.Center.X, (int)game.camera.Center.Y, 800, 600);
            MapEdgeRRect = new Rectangle(Map.Width - 400, 0, 800, 800);
            MapEdgeLRect = new Rectangle(-400, 0, 800, 800);
            boss = new Boss(390, 288);
        }

        public void Load()
        {
            boss.Load();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Rectangle backRect = BackgroundRect;
            backRect.X = (int)game.camera.Center.X;
            backRect.Y = (int)game.camera.Center.Y;
            BackgroundRect = backRect;

            Rectangle rectL = MapEdgeLRect;
            Rectangle rectR = MapEdgeRRect;

            rectL.Y = (int)game.camera.Center.Y - 30;
            rectR.Y = (int)game.camera.Center.Y - 30;

            MapEdgeLRect = rectL;
            MapEdgeRRect = rectR;

            boss.Update();

            Zanaj.getInstance().Update(this);

            Console.WriteLine("Zanaj: {0},{1}\nSpeed: {2},{3}     IsJumping?: {4}", zanaj.X, zanaj.Y, zanaj.XSpeed, zanaj.YSpeed, zanaj.Jumping);
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);

            spriteBatch.Draw(BackgroundTexture, BackgroundRect, Color.White);

            foreach(Tile tile in TileList)
            {
                spriteBatch.Draw(tile.Texture, tile.Rectangle, Color.White);
            }

            boss.Draw(spriteBatch);

            zanaj.Draw(spriteBatch);

             spriteBatch.Draw(MapEdgeR, MapEdgeRRect, Color.White);
            spriteBatch.Draw(MapEdgeL, MapEdgeLRect, Color.White);
        }
    }
}
