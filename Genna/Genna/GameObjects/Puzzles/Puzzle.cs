using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;

using Genna.Levels;
using Genna.GameObjects.Puzzles.Levers;

namespace Genna.GameObjects.Puzzles
{
    public class Puzzle
    {
        private Game1 game;
        private Level level;

        private KeyboardState kbState;
        private KeyboardState prevKBState;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game">Game1 object</param>
        /// <param name="level">current level</param>
        public Puzzle(Game1 game, Level level)
        {
            this.game = game;
            this.level = level;
        }

        // Handles gates with levers
        public void GateControl(SpriteBatch spriteBatch)
        {
            Update(level);
            if (level.Name == "GennaTown")
            {
                Lever lever1 = level.LeverList[0];
                Lever lever2 = level.LeverList[1];
                Gate gate1 = level.GateList[0];
                Gate gate2 = level.GateList[1];
                if (lever1.SwitchedOn && lever2.SwitchedOn)
                {
                    foreach (Tile tile in gate1.TileList)
                    {
                        tile.Collides = false;

                        if (level.TileList.Contains(tile))
                        {
                            level.TileList.Remove(tile);
                        }
                    }
                    foreach (Tile tile in gate2.TileList)
                    {
                        tile.Collides = false;

                        if (level.TileList.Contains(tile))
                        {
                            level.TileList.Remove(tile);
                        }
                    }
                }
                else if (!lever1.SwitchedOn && lever2.SwitchedOn)
                {
                    foreach (Tile tile in gate1.TileList)
                    {
                        tile.Collides = true;

                        if (!level.TileList.Contains(tile))
                        {
                            level.TileList.Add(tile);
                        }
                    }
                    foreach (Tile tile in gate2.TileList)
                    {
                        tile.Collides = true;

                        if (!level.TileList.Contains(tile))
                        {
                            level.TileList.Add(tile);
                        }
                    }
                }
                else
                {
                    foreach (Tile tile in gate1.TileList)
                    {
                        tile.Collides = false;

                        if (level.TileList.Contains(tile))
                        {
                            level.TileList.Remove(tile);
                        }
                    }
                    foreach (Tile tile in gate2.TileList)
                    {
                        tile.Collides = true;

                        if (!level.TileList.Contains(tile))
                        {
                            level.TileList.Add(tile);
                        }
                    }
                }

                lever1.Draw(spriteBatch);
                lever2.Draw(spriteBatch);
            }
            else if (level.Name == "Bridge")
            {
                Lever lever1 = level.LeverList[0];
                Lever lever2 = level.LeverList[1];
                Lever lever3 = level.LeverList[2];
                Gate gate1 = level.GateList[0];
                if (lever1.SwitchedOn && lever2.SwitchedOn && lever3.SwitchedOn)
                {
                    foreach (Tile tile in gate1.TileList)
                    {
                        tile.Collides = false;
                        if (level.TileList.Contains(tile))
                        {
                            level.TileList.Remove(tile);
                        }
                    }
                }
                else
                {
                    foreach (Tile tile in gate1.TileList)
                    {
                        tile.Collides = true;
                        if (!level.TileList.Contains(tile))
                        {
                            level.TileList.Add(tile);
                        }
                    }
                }
                lever1.Draw(spriteBatch);
                lever2.Draw(spriteBatch);
                lever3.Draw(spriteBatch);
            }
        }

        // Updates the levers and gates
        public void Update(Level level)
        {
            kbState = Keyboard.GetState();
            foreach (Lever existingLever in level.LeverList)
            {
                if (Game1.zanaj.rect.Intersects(existingLever.Rect))
                {
                    if (kbState.IsKeyDown(Keys.E) && prevKBState.IsKeyUp(Keys.E))
                    {
                        if (existingLever.SwitchedOn)
                        {
                            existingLever.SwitchedOn = false;
                        }
                        else
                        {
                            existingLever.SwitchedOn = true;
                        }
                    }
                }

            }
            prevKBState = kbState;
        }
    }
}
