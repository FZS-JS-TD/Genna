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

namespace Genna.Levels.Outside
{
    public class NextLevel : Level
    {
        private Game1 game;
        private KeyboardState prevKeyState;
        private KeyboardState keyState;

        public NextLevel(Game1 game)
        {
            LoadMap("NextLevel.txt");
            Name = "NextLevel";
            this.game = game;

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
                keyState = value;
            }
        }

        public override bool PressedOnlyOnce(Keys key)
        {
            return false;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            if (Background != null && Background != "")
            {
                spritebatch.Draw(BackgroundTexture, BackgroundRect, Color.White);
            }
            foreach (Tile tile in TileList)
            {
                Rectangle rect = tile.Rectangle;
                rect.X -= X;
                rect.Y += Y;
                spritebatch.Draw(tile.Texture, rect, Color.White);
            }

            zanaj.Draw(spritebatch);
        }
    }
}