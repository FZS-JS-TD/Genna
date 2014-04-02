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

namespace Genna.GameObjects.Characters
{
    //Forrest
    abstract public class Character: GameObject
    {
        public Character(Game1 game1, int x, int y, int width, int height)
            :base(x, y, width, height, 0, 100, game1, 0)
        {
        }
    }
}