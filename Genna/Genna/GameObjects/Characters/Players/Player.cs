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

namespace Genna.GameObjects.Characters.Players
{
    //Forrest
    abstract public class Player : Character
    {
        protected int luck;

        public int Luck
        {
            get { return luck; }
            set { luck = value; }
        }

        public Player(Game1 game1, int x, int y, int width, int height)
            :base(game1, x, y, width, height)
        {
        }
    }
}
