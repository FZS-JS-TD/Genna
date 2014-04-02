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
        Game1 game;
        private KeyboardState prevKeyState;
        private KeyboardState keyState;

        public ItemShop(Game1 game, GraphicsDevice graphics)
        {
            LoadMap("ItemShop.txt");
            this.game = game;
            Name = "ItemShop";
            zanaj = game.gennaTown.zanaj;
        }

        public override KeyboardState PrevKeyState
        {
            get
            {
                return prevKeyState;
            }
            set
            {
                prevKeyState = value;
            }
        }

        public override KeyboardState CurrentKeyState
        {
            get
            {
                return keyState;
            }
            set
            {
                prevKeyState = keyState;
                keyState = value;
            }
        }

        public override void Update(GameTime gameTime)
        {
            CurrentKeyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.D))
            {
                X++;
            }
            if (keyState.IsKeyDown(Keys.W))
            {
                Y--;
            }
            if (keyState.IsKeyDown(Keys.A))
            {
                X--;
            }
            if (keyState.IsKeyDown(Keys.S))
            {
                Y++;
            }

            zanaj.Update(this);
            Console.WriteLine("Zanaj: {0},{1}    World: {2},{3}\nSpeed: {4},{5}     IsJumping?: {6}", zanaj.X, zanaj.Y, X, Y, zanaj.XSpeed, zanaj.YSpeed, zanaj.Jumping);

            map.Y = Y;
            map.X = X;
        }

        public override void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            foreach (Tile tile in TileList)
            {
                spritebatch.Draw(tile.Texture, tile.Rectangle, Color.White);
            }

            zanaj.Draw(spritebatch);
        }

        public override bool PressedOnlyOnce(Keys key)
        {
            if (prevKeyState.IsKeyUp(key) && keyState.IsKeyDown(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Pause()
        {
            game._GameMode = Game1.GameMode.Menu;
        }


    }
}