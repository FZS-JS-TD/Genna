using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Genna.GameObjects.GUI
{
    public class TextBox
    {
        private Rectangle textBox;
        private SpriteFont spriteFont;
        private string text;
        private Texture2D textBoxImage;
        private Texture2D speakerImage;
        private Rectangle speakerRect;
        private Vector2 textLocation;
        private Rectangle speakerBorder;
        private Rectangle textBorder;
        private Texture2D shopKeeperMugshot;
        private Texture2D doctorMugshot;

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

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public Texture2D ShopKeeperMugshot
        {
            get { return shopKeeperMugshot; }
        }

        public Texture2D DoctorMugshot
        {
            get { return doctorMugshot; }
        }

        public string ParseText(string text)
        {
            this.text = text;

            string line = string.Empty;
            string returnString = string.Empty;
            string[] wordArray = text.Split(' ');

            foreach (string word in wordArray)
            {
                if (spriteFont.MeasureString(line + word).Length() > textBox.Width - speakerRect.Width - 30)
                {
                    returnString += line + "\n";
                    line = string.Empty;
                }
                line = line + word + ' ';
            }

            return returnString + line;
        }

        public Rectangle SpeakerRect
        {
            get { return speakerRect; }
            set { speakerRect = value; }
        }

        public Texture2D SpeakerImage
        {
            get { return speakerImage; }
            set { speakerImage = value; }
        }

        public TextBox(SpriteFont font, int x, int y)
        {
            this.x = x;
            this.y = y;
            spriteFont = font;
            textBox = new Rectangle(x, y, 480, 250);
            speakerRect = new Rectangle(x + 15, y + 15, 64, 64);
            textLocation = new Vector2(x + speakerRect.Width + 30, y + 15);
            speakerBorder = new Rectangle(x + 7, y + 7, 78, 78);
            textBorder = new Rectangle((int)textLocation.X - 6, (int)textLocation.Y - 8, textBox.Width - speakerBorder.Width - 14, y - 7);
        }

        public void LoadContent(ContentManager content)
        {
            textBoxImage = content.Load<Texture2D>("Tiles/TextBox");
            shopKeeperMugshot = content.Load<Texture2D>("Tiles/ShopKeeperBox");
            doctorMugshot = content.Load<Texture2D>("Tiles/DoctorBox");
        }

        public void Update()
        {
            textBox.Y = y;
            speakerBorder.Y = y + 7;
            textBorder.Y = y + 7;
            speakerRect.Y = y + 15;
            textLocation.Y = y + 15;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(textBoxImage, textBox, Color.White);
            spriteBatch.Draw(textBoxImage, speakerBorder, Color.Black);
            spriteBatch.Draw(textBoxImage, textBorder, Color.Black);
            if (text != null)
            {
                spriteBatch.DrawString(spriteFont, ParseText(text), textLocation, Color.White);
            }
            if (speakerImage != null)
            {
                spriteBatch.Draw(speakerImage, speakerRect, Color.White);
            }
            
        }
    }
}
