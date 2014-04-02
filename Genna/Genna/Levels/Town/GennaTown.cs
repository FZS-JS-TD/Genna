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
using Genna.GameObjects.Puzzles;
using Genna.GameObjects.Puzzles.Levers;

namespace Genna.Levels.Town
{
    //Forrest
    public class GennaTown : Level
    {
        private Puzzle puzzle;
        private Lever lever1;
        private Lever lever2;
        private Gate gate1;
        private Gate gate2;

        public GennaTown(Game1 game)
        {
            LoadMap("GennaTown.txt");
            BackgroundRect = new Rectangle((int)game.camera.Center.X - game.GraphicsDevice.Viewport.Width / 2, (int)game.camera.Center.Y - game.GraphicsDevice.Viewport.Height / 2, game.GraphicsDevice.Viewport.Width * 2, game.GraphicsDevice.Viewport.Height * 2);
            Level.game = game;
            Name = "GennaTown";

            lever1 = new Lever(game, 1408, 96);
            lever2 = new Lever(game, 1728, 128);

            lever1.Load();
            lever2.Load();

            LeverList.Add(lever1);
            LeverList.Add(lever2);

            gate1 = new Gate(game, 1664, 32, 1, 5);
            gate2 = new Gate(game, 1760, 224, 1, 4);

            GateList.Add(gate1);
            GateList.Add(gate2);

            puzzle = new Puzzle(game, this);

            zanaj = game.Zanaj;

            MapEdgeRRect = new Rectangle(Map.Width - 400, 0, 800, 800);
            MapEdgeLRect = new Rectangle(- 400, 0, 800, 800);
        }

        public override void Update(GameTime gameTime)
        {
            if (game.levelChange)
                game.music.Current = 1;
    
            CurrentKeyState = Keyboard.GetState();

            game.villager.Update();

            Rectangle backRect = BackgroundRect;
            backRect.X = (int)game.camera.Center.X - game.GraphicsDevice.Viewport.Width / 2;
            backRect.Y = (int)game.camera.Center.Y - game.GraphicsDevice.Viewport.Height / 2;
            BackgroundRect = backRect;

            Rectangle rectL = MapEdgeLRect;
            Rectangle rectR = MapEdgeRRect;

            rectL.Y = (int)game.camera.Center.Y - 30;
            rectR.Y = (int)game.camera.Center.Y - 30;

            MapEdgeLRect = rectL;
            MapEdgeRRect = rectR;

            zanaj.Update(this);

            Console.WriteLine("Zanaj: {0},{1}\nSpeed: {2},{3}     IsJumping?: {4}", zanaj.X, zanaj.Y, zanaj.XSpeed, zanaj.YSpeed, zanaj.Jumping);
        }

        public override void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            spritebatch.Draw(BackgroundTexture, BackgroundRect, Color.White);
            foreach (Tile tile in TileList)
            {
                spritebatch.Draw(tile.Texture, tile.Rectangle, Color.White);
            }

            puzzle.GateControl(spritebatch);

            game.villager.Draw(spritebatch);

            zanaj.Draw(spritebatch);

            spritebatch.Draw(MapEdgeR, MapEdgeRRect, Color.White);
            spritebatch.Draw(MapEdgeL, MapEdgeLRect, Color.White);
        }
    }
}