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

namespace Genna.Menus
{
    public class SettingsMenu
    {
        private Game1 game;
        private Texture2D settings;

        public SettingsMenu(Game1 game)
        {
            this.game = game;
        }

        public void LoadContent()
        {
            settings = game.Content.Load<Texture2D>("Main_Menu/SettingsMenu");
        }

        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Q))
            {
                game._GameMode = Game1.GameMode.Menu;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(settings, new Rectangle(0, 0, 800, 600), Color.White);
        }
    }
}
