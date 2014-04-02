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
using Genna.GameObjects.Characters.Players.Zanaj_Akari;

namespace Genna.Levels.Town
{
    //Forrest
    public class GennaTown : Level
    {
        private Game1 game;
        private KeyboardState prevKeyState;
        private KeyboardState keyState;


        public GennaTown(Game1 game)
        {
            LoadMap("GennaTown.txt");
            BackgroundRect = new Rectangle(-game.GraphicsDevice.Viewport.Width / 2, -game.GraphicsDevice.Viewport.Height / 2, game.GraphicsDevice.Viewport.Width * 2, game.GraphicsDevice.Viewport.Height * 2);
            this.game = game;
            Name = "GennaTown";

            zanaj = game.Zanaj;

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

            Rectangle rectL = MapEdgeLRect;
            Rectangle rectR = MapEdgeRRect;

            rectL.X = -400;
            rectR.X = Map.Width - 400;

            MapEdgeLRect = rectL;
            MapEdgeRRect = rectR;

            zanaj.Update(this);

            Console.WriteLine("Zanaj: {0},{1}    World: {2},{3}\nSpeed: {4},{5}     IsJumping?: {6}", zanaj.X, zanaj.Y, X, Y, zanaj.XSpeed, zanaj.YSpeed, zanaj.Jumping);
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

        public override void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            spritebatch.Draw(BackgroundTexture, BackgroundRect, Color.White);
            foreach (Tile tile in TileList)
            {
                spritebatch.Draw(tile.Texture, tile.Rectangle, Color.White);
            }

            zanaj.Draw(spritebatch);

            spritebatch.Draw(MapEdgeR, MapEdgeRRect, Color.White);
            spritebatch.Draw(MapEdgeL, MapEdgeLRect, Color.White);
        }
    }
}