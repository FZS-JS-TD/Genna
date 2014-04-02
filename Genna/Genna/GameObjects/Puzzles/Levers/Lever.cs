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

namespace Genna.GameObjects.Puzzles.Levers
{
    public class Lever
    {
        private Game1 game;
        private bool switchedOn;
        private Texture2D leverOnImage;
        private Texture2D leverOffImage;
        private Rectangle leverRect;
        private int x;
        private int y;

        public Rectangle Rect
        {
            get { return leverRect; }            
        }

        public bool SwitchedOn
        {
            get { return switchedOn; }
            set { switchedOn = value; }
        }

        public Lever(Game1 game, int x, int y)
        {
            this.game = game;
            this.x = x;
            this.y = y;
            leverRect = new Rectangle(x, y, 32, 32);
            switchedOn = false;
        }

        public void Load()
        {
            leverOffImage = game.Content.Load<Texture2D>("Tiles/LeverOff");
            leverOnImage = game.Content.Load<Texture2D>("Tiles/LeverOn");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (switchedOn)
            {
                spriteBatch.Draw(leverOnImage, leverRect, Color.White);
            }
            else
            {
                spriteBatch.Draw(leverOffImage, leverRect, Color.White);
            }
        }

    }
}
