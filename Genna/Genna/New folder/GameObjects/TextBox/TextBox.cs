using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Genna.GameObjects.TextBox
{
    public class TextBox
    {
        private Rectangle textBox;
        private SpriteFont spriteFont;
        private string text;
        private Texture2D textBoxImage;
        private Vector2 textLocation;

        private int x;
        private int y;

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public TextBox(string text, SpriteFont font, int x, int y)
        {
            this.text = text;
            spriteFont = font;
            this.x = x - 7; // 7 is subtracted to center it with the text
            this.y = y;
            Vector2 dimensions = spriteFont.MeasureString(text);
            int width = Convert.ToInt32(dimensions.X) + 14; // 14 is added to increase the textbox size
            int height = Convert.ToInt32(dimensions.Y);
            textBox = new Rectangle(this.x, this.y, width, height);
            textLocation = new Vector2(x, this.y);
        }

        public void LoadContent(ContentManager content)
        {
            textBoxImage = content.Load<Texture2D>("Tiles/TextBox");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textBoxImage, textBox, Color.White);
            spriteBatch.DrawString(spriteFont, text, textLocation, Color.White);
        }
    }
}
