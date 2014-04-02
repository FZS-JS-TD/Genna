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

using Genna.Levels;

namespace Genna.Levels.Town.Shops
{
    public class ItemShop : Level
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">The Game1 Object</param>
        /// <param name="graphics">The GraphicsDevice</param>
        public ItemShop(Game1 game, GraphicsDevice graphics)
        {
            LoadMap("ItemShop.txt");
            Level.game = game;
            Name = "ItemShop";
            zanaj = Game1.zanaj;
        }

        // Updates everything (the player and npc)
        public override void Update(GameTime gameTime)
        {
            CurrentKeyState = Keyboard.GetState();

            game.shopKeeper.Update();

            zanaj.Update(this);
            Console.WriteLine("Zanaj: {0},{1}\nSpeed: {2},{3}     IsJumping?: {4}", zanaj.X, zanaj.Y, zanaj.XSpeed, zanaj.YSpeed, zanaj.Jumping);

            if (zanaj.Y > 1000)
            {
                zanaj.Y = 221;
            }
        }

        // Draws each tile and then the npc and then the player
        public override void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            foreach (Tile tile in TileList)
            {
                spritebatch.Draw(tile.Texture, tile.Rectangle, Color.White);
            }

            game.shopKeeper.Draw(spritebatch);

            Game1.zanaj.Draw(spritebatch);
        }
    }
}
