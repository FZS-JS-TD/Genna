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

using Genna.GameObjects.Characters.Players.Zanaj_Akari;

namespace Genna.Levels.InfirmaLevel
{
    public class InfirmaLevel : Level
    {
        public InfirmaLevel(Game1 game)
        {
            LoadMap("InfirmaLevel.txt");
            Name = "InfirmaLevel";
        }

        public override void Update(GameTime gameTime)
        {
            CurrentKeyState = Keyboard.GetState();

            Zanaj.getInstance().Update(this);

            base.Update(gameTime);
            //Zanaj.getInstance().Health -= (

            Console.WriteLine("Zanaj: {0},{1}\nSpeed: {2},{3}     IsJumping?: {4}", zanaj.X, zanaj.Y, zanaj.XSpeed, zanaj.YSpeed, zanaj.Jumping);
        }

        public override void Draw(SpriteBatch spritebatch, GameTime gameTime)
        {
            spritebatch.Draw(BackgroundTexture, BackgroundRect, Color.White);
            foreach (Tile tile in TileList)
            {
                spritebatch.Draw(tile.Texture, tile.Rectangle, Color.White);
            }            

            zanaj.Draw(spritebatch);
        }
    }
}
