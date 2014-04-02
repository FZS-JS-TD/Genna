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

namespace Genna.GameObjects
{
    //Forrest
    public abstract class GameObject
    {
        public Rectangle pastRect;
        public Rectangle rect;
        private int xSpeed;
        private int health;
        private int poisonResist;
        protected Game1 game;
        private Vector2 velocity;
        private int ySpeed;
        public bool IsZanaj = false;

        public GameObject(Game1 theGame)
        {
            Game = theGame;
        }

        public GameObject(int pX, int pY, int pWidth, int pHeight, int pSpeed, int pHealth = 0, Game1 pGame = null, int pPoisonResist = 0)
        {
            rect = new Rectangle(pX, pY, pWidth, pHeight);
            pastRect = rect;
            health = pHealth;
            poisonResist = pPoisonResist;
            game = pGame;
            xSpeed = pSpeed;
        }

        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }

            set
            {
                velocity = value;
            }
        }
        
        public int X
        {
            get
            {
                return rect.X;
            }

            set
            {
                pastRect.X = rect.X;
                rect.X = value;
            }
        }

        public int Y
        {
            get
            {
                return rect.Y;
            }

            set
            {
                pastRect.Y = rect.Y;
                rect.Y = value;
            }
        }

        public int Width
        {
            get
            {
                return rect.Width;
            }

            set
            {
                pastRect.Width = rect.Width;
                rect.Width = value;
            }
        }

        public int Height
        {
            get
            {
                return rect.Height;
            }

            set
            {
                pastRect.Height = rect.Height;
                rect.Height = value;
            }
        }

        public Rectangle Rect
        {
            get
            {
                return rect;
            }

            set
            {
                pastRect = rect;
                rect = value;
            }
        }

        public int PastX
        {
            get { return pastRect.X; }
        }

        public int PastY
        {
            get { return pastRect.Y; }
        }

        public int XSpeed
        {
            get { return xSpeed; }
            
            set 
            {
                velocity.X = value;
                xSpeed = value; 
            }
        }

        public int YSpeed
        {
            get { return ySpeed; }

            set
            {
                velocity.Y = value;
                ySpeed = value;
            }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int PoisonResist
        {
            get { return poisonResist; }
            set { poisonResist = value; }
        }

        public Game1 Game
        {
            get { return game; }
            set { game = value; }
        }

        public void Move(int x, int y)
        {
            X += x;
            Y += y;
        }

        public virtual void Move(Vector2 vel)
        {
            Move((int) vel.X, (int) vel.Y);

            if (!IsZanaj)
            {
                Screenwrap();
            }
            else
            {
                Zanaj zan = (Zanaj)this;
                zan.currentLevel.X += (int)vel.X;
                zan.currentLevel.Y += (int)vel.Y;

                zan.currentLevel.ScreenWrapWorld(zan.Game, zan);
            }
        }

        public void Screenwrap(int tempX = 0, int tempY = 0)
        {
            if (tempX == 0)
            {
                tempX = Game.currentLevel.X + game.GraphicsDevice.Viewport.Width;
            }
            else
            {
                tempX = Game.GraphicsDevice.Viewport.Width + Width;
            }

            if (tempY == 0)
            {
                tempY = game.currentLevel.Y + game.GraphicsDevice.Viewport.Height;
            }
            else
            {
                tempY = game.GraphicsDevice.Viewport.Height;
            }

            if (rect.X > tempX)
            {
                try
                {
                    rect.X = -rect.Width + game.currentLevel.X;
                }
                catch (Exception e)
                {
                    rect.X = -rect.Width;
                }
            }

            if (rect.X < -rect.Width)
            {
                rect.X = game.GraphicsDevice.Viewport.Width + Width;
            }

            if (rect.Y > tempY)
            {
                rect.Y = -rect.Height;
            }

            if (rect.Y < -rect.Height)
            {
                rect.Y = tempY;
            }
        }
    }
}
