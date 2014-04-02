using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Genna.GameObjects;
using Genna.GameObjects.Characters.Players.Zanaj_Akari;

namespace Genna.Camera
{
    public class Camera2D
    {
        public GameObject focus;
        public Matrix transform; // draws camera on screen
        Viewport view; // where the camera is looking
        Vector2 center; // center of the camera (Zanaj)

        public Vector2 Center
        {
            get { return center; }
        }

        public GameObject Focus
        {
            get { return focus; }

            set
            {
                focus = (GameObject)value;
            }
        }

        public Camera2D(Viewport newView)
        {
            view = newView;
        }

        public void Update(GameTime gameTime, Game1 game)
        {
            if (focus == null)
                focus = Zanaj.instance;
            if (focus != null)
            {
                center = new Vector2(focus.X - focus.Game.GraphicsDevice.Viewport.Width / 2,
                                     focus.Y - focus.Game.GraphicsDevice.Viewport.Height / 2);
                transform = Matrix.CreateScale(new Vector3(1, 1, 0)) *
                    Matrix.CreateTranslation(new Vector3(-center.X, -center.Y, 0));
            }
        }
    }
}
