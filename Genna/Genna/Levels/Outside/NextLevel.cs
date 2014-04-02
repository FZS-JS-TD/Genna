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

            zanaj = Genna.GameObjects.Characters.Players.Zanaj_Akari.Zanaj.getInstance();

            MapEdgeRRect = new Rectangle(Map.Width - 400, 0, 800, 800);
            MapEdgeLRect = new Rectangle(-400, 0, 800, 800);
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
                PrevKeyState = keyState;
                keyState = value;
            }
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

        public override void Update(GameTime gameTime)
        {
            CurrentKeyState = Keyboard.GetState();

            Rectangle rectL = MapEdgeLRect;
            Rectangle rectR = MapEdgeRRect;

            rectL.X = -400;
            rectR.X = Map.Width - 400;

            MapEdgeLRect = rectL;
            MapEdgeRRect = rectR;

            zanaj.Update(this);

            Console.WriteLine("Zanaj: {0},{1}\nSpeed: {2},{3}     IsJumping?: {4}", zanaj.X, zanaj.Y, zanaj.XSpeed, zanaj.YSpeed, zanaj.Jumping);
        }

        public void Pause()
        {
            game._GameMode = Game1.GameMode.Menu;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Background != null && Background != "")
            {
                spriteBatch.Draw(BackgroundTexture, BackgroundRect, Color.White);
            }
            foreach (Tile tile in TileList)
            {
                spriteBatch.Draw(tile.Texture, tile.Rectangle, Color.White);
            }

            zanaj.Draw(spriteBatch);

            spriteBatch.Draw(MapEdgeR, MapEdgeRRect, Color.White);
            spriteBatch.Draw(MapEdgeL, MapEdgeLRect, Color.White);
        }
    }
}
