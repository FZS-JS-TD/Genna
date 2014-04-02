using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Input;

using Genna.Levels;
using Genna.Levels.Town;
using Genna.Levels.Town.Shops;
using Genna.GameObjects.Characters.Players.Zanaj_Akari;

namespace Genna.Levels
{
    public class MapTransition
    {
        #region Attributes

        private Game1 game;
        private List<Tile> upperDoors;
        private List<Tile> lowerDoors;
        private List<Level> allLevels;
        private Zanaj zanaj;

        #endregion

        #region Constructor

        public MapTransition(Game1 game, List<Level> allLevels)
        {
            upperDoors = new List<Tile>();
            lowerDoors = new List<Tile>();
            this.game = game;
            this.allLevels = allLevels;
            zanaj = game.gennaTown.zanaj;
            zanaj.X = (game.GraphicsDevice.Viewport.Width / 2);
            zanaj.Y = (game.GraphicsDevice.Viewport.Height / 2);
        }

        #endregion

        #region Doors

        public void GetDoors(List<Tile> tileList)
        {
            if (tileList != null)
            {
                foreach (Tile tile in tileList)
                {
                    if (tile.ID == 26)
                    {
                        upperDoors.Add(tile);
                    }
                    else if (tile.ID == 27)
                    {
                        lowerDoors.Add(tile);
                    }
                }
            }
        }

        public bool CollidingWithDoor()
        {
            if (upperDoors != null && lowerDoors != null)
            {
                foreach (Tile tile in upperDoors)
                {
                    if (zanaj.Rect.Intersects(tile.Rectangle))
                    {
                        foreach (Tile otherTile in lowerDoors)
                        {
                            if (zanaj.Rect.Intersects(otherTile.Rectangle))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        #endregion

        public void Update()
        {
            KeyboardState kState = new KeyboardState();
            KeyboardState prevKState = kState;
            kState = Keyboard.GetState();
            if (kState.IsKeyDown(Keys.Up) && prevKState.IsKeyUp(Keys.Up))
            {
                if (CollidingWithDoor())
                {
                    if (game.currentLevel == game.gennaTown)
                    {
                        foreach (Tile tile in upperDoors)
                        {
                            if (tile.X == 224 && tile.Y == 288 && zanaj.rect.Intersects(tile.Rectangle))
                            {
                                EnterNewMap(game.itemShop);
                            }
                            else if (tile.X == 544 && tile.Y == 288 && zanaj.rect.Intersects(tile.Rectangle))
                            {
                                //EnterNewMap();
                            }
                        }
                    }
                    else if (game.currentLevel == game.itemShop)
                    {
                        foreach (Tile tile in upperDoors)
                        {
                            if (tile.X == 96 && tile.Y == 224 && zanaj.rect.Intersects(tile.Rectangle))
                            {
                                EnterNewMap(game.gennaTown);
                            }
                        }
                    }
                }
            }

            if (game.CurrentLevel == game.gennaTown)
            {
                if (!game.CurrentLevel.Map.Contains(zanaj.rect))
                {
                    EnterNewMap(game.nextLevel);
                }
            }
        }

        public void EnterNewMap(Level nextLevel)
        {
            game.CurrentLevel = nextLevel;
            // Reset Zanaj's position and camera!
        }
    }
}