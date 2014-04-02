using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Genna.Camera
{
    public class Camera2D
    {
        public Matrix transform; // draws camera on screen
        Viewport view; // where the camera is looking
        Vector2 center; // center of the camera (Zanaj)

        public Camera2D(Viewport newView)
        {
            view = newView;
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            center = new Vector2(game.zanaj.X + (game.zanaj.Width / 2) - game.GraphicsDevice.Viewport.Width / 2, game.zanaj.Y + (game.zanaj.Height / 2) - game.GraphicsDevice.Viewport.Height / 2);
            transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
        }
    }
}