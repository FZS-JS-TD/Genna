using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;

using Genna.GameObjects;

namespace Genna
{
    //Forrest
    public class Particle : GameObject
    {
        private Color color;
        private int minSpeed = -3;
        private int maxSpeed = 5;

        public Particle(int x, int y, Game1 game1 = null)
            : base(game1)
        {
            XSpeed = x;
            YSpeed = y;
            Game = game1;
        }

        public int Max
        {
            get
            {
                return maxSpeed;
            }

            set
            {
                maxSpeed = value;
            }
        }

        public int Min
        {
            get
            {
                return minSpeed;
            }

            set
            {
                minSpeed = value;
            }
        }

        public void ShiftSpeed()
        {
            if (Max != 0 && Min != 0)
            {
                Random rand = new Random();

                if (rand.Next(100) > 95)
                {
                    if (rand.Next(100) > 85)
                    {
                        XSpeed++;
                    }

                    if (rand.Next(100) > 95)
                    {
                        XSpeed--;
                    }

                    if (rand.Next(100) > 85)
                    {
                        YSpeed++;
                    }

                    if (rand.Next(100) > 95)
                    {
                        YSpeed--;
                    }
                }

                if (XSpeed > Max)
                {
                    XSpeed--;
                }

                if (XSpeed < Min)
                {
                    XSpeed++;
                }

                if (YSpeed > Max)
                {
                    YSpeed--;
                }

                if (YSpeed < Min)
                {
                    YSpeed++;
                }
            }
        }

        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        public void ScreenWrap(int x, int y)
        {
            if (rect.X > x)
            {
                rect.X = -rect.Width;
            }

            if (rect.X < -rect.Width)
            {
                rect.X = x + rect.Width;
            }

            if (rect.Y > y)
            {
                rect.Y = -rect.Height;
            }

            if (rect.Y < -rect.Height)
            {
                rect.Y = y;
            }
        }

        public override void Move()
        {
            ShiftSpeed();

            rect.X += XSpeed;
            rect.Y += YSpeed;
        }
    }
}