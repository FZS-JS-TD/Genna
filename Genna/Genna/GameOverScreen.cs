using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Graphics;

namespace Genna
{
    public class GameOverScreen
    {
        Game1 game;
        public GameOverScreen(Game1 pGame)
        {
            game = pGame;
        }

        public void Update()
        {
            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q))
            {
                game.Renew();
            }
            Game1.gameOverScreen = game.Content.Load<Texture2D>("gos");
        }

        public void Draw(Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(game.Content.Load<Texture2D>("gos"), new Microsoft.Xna.Framework.Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), Microsoft.Xna.Framework.Color.White);
            spriteBatch.End();
        }
    }
}
