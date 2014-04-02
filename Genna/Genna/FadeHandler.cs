using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;

using Genna;
using Genna.Levels;

namespace Genna
{
    class FadeHandler
    {
        void fade()
        {/*
            int count = 0;
            bool[,] used = new bool[40, 30];

            for (int i = 0; i < 40; i++)
            {
                for (int ii = 0; ii < 30; ii++)
                {
                    used[i, ii] = false;
                }
            }

            Queue<Vector2> fade = new Queue<Vector2>();
            Stack<Vector2> toBeFade = new Stack<Vector2>();
            Stack<Vector2> toBe_toBe = new Stack<Vector2>();

            Texture2D fadeImg = .Load<Texture2D>("fade");

            fade.Enqueue(new Vector2(0, 0));

            do
            {
                spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend);

                for (int i = 0; i < 40; i++)
                {
                    for (int ii = 0; ii < 30; ii++)
                    {
                        if (used[i, ii])
                        {
                            Vector2 loc = new Vector2(i, ii);
                            spriteBatch.Draw(fadeImg, new Rectangle((int)loc.X * 20, (int)loc.Y * 20, 20, 20), Color.White);
                        }
                    }
                }
                spriteBatch.End();

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                while (fade.Count > 0)
                {
                    Vector2 loc = fade.Dequeue();
                    used[(int)loc.X, (int)loc.Y] = true;
                    spriteBatch.Draw(fadeImg, new Rectangle((int)loc.X * 20, (int)loc.Y * 20, 20, 20), Color.White);
                    count++;

                    if (loc.X > 0)
                    {
                        if (!used[(int)loc.X - 1, (int)loc.Y])
                        {
                            used[(int)loc.X - 1, (int)loc.Y] = true;
                            toBeFade.Push(new Vector2(loc.X - 1, loc.Y));
                        }
                    }
                    if (loc.X < 39)
                    {
                        if (!used[(int)loc.X + 1, (int)loc.Y])
                        {
                            used[(int)loc.X + 1, (int)loc.Y] = true;
                            toBeFade.Push(new Vector2(loc.X + 1, loc.Y));
                        }
                    }

                    if (loc.Y > 0)
                    {
                        if (!used[(int)loc.X, (int)loc.Y - 1])
                        {
                            used[(int)loc.X, (int)loc.Y - 1] = true;
                            toBeFade.Push(new Vector2(loc.X, loc.Y - 1));
                        }
                    }
                    if (loc.Y < 29)
                    {
                        if (!used[(int)loc.X, (int)loc.Y + 1])
                        {
                            used[(int)loc.X, (int)loc.Y + 1] = true;
                            toBeFade.Push(new Vector2(loc.X, loc.Y + 1));
                        }
                    }
                    base.Draw(gameTime);

                    System.Threading.Thread.Sleep(1);
                }
                spriteBatch.End();

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                while (toBeFade.Count > 0)
                {
                    Vector2 loc = toBeFade.Pop();
                    //add tiles

                    if (loc.X > 0)
                    {
                        if (!used[(int)loc.X - 1, (int)loc.Y])
                        {
                            used[(int)loc.X - 1, (int)loc.Y] = true;
                            toBe_toBe.Push(new Vector2(loc.X - 1, loc.Y));
                        }
                    }
                    if (loc.X < 39)
                    {
                        if (!used[(int)loc.X + 1, (int)loc.Y])
                        {
                            used[(int)loc.X + 1, (int)loc.Y] = true;
                            toBe_toBe.Push(new Vector2(loc.X + 1, loc.Y));
                        }
                    }

                    if (loc.Y > 0)
                    {
                        if (!used[(int)loc.X, (int)loc.Y - 1])
                        {
                            used[(int)loc.X, (int)loc.Y - 1] = true;
                            toBe_toBe.Push(new Vector2(loc.X, loc.Y - 1));
                        }
                    }
                    if (loc.Y < 29)
                    {
                        if (!used[(int)loc.X, (int)loc.Y + 1])
                        {
                            used[(int)loc.X, (int)loc.Y + 1] = true;
                            toBe_toBe.Push(new Vector2(loc.X, loc.Y + 1));
                        }
                    }

                    fade.Enqueue(loc);

                    //draw toBeFade    
                    spriteBatch.Draw(fadeImg, new Rectangle((int)tile.X * 20, (int)tile.Y * 20, 20, 20), Color.White * 0.7f);

                    base.Draw(gameTime);
                }
                spriteBatch.End();

                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);
                while (toBe_toBe.Count > 0)
                {
                    Vector2 loc = toBe_toBe.Pop();
                    spriteBatch.Draw(fadeImg, new Rectangle((int)loc.X * 20, (int)loc.Y * 20, 20, 20), Color.White * 0.4f);
                    toBeFade.Push(loc);
                }
                spriteBatch.End();

            }
            while (count < 1200);
       */
        }
    }
}
