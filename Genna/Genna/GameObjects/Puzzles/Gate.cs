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

using Genna.Levels;

namespace Genna.GameObjects.Puzzles
{
    public class Gate
    {
        private Game1 game;
        private int width;
        private int length; 
        private List<Tile> tileList; // the list of each tile in the gate

        public List<Tile> TileList
        {
            get { return tileList; }
        }


        /// <summary>
        /// Creates a gate made of tiles
        /// </summary>
        /// <param name="game">Game1 object</param>
        /// <param name="x">starting x pos</param>
        /// <param name="y">starting y pos</param>
        /// <param name="width">number of tiles horizontally</param>
        /// <param name="length">number of tiles vertically</param>
        public Gate(Game1 game, int x, int y, int width, int length)
        {
            this.game = game;
            this.width = width;
            this.length = length;
            tileList = new List<Tile>();

            for (int i = 0; i < width; i++)
            {
                Tile tile = new Tile();
                tile.X = x + (i * 32);
                tile.Y = y;
                tile.Rectangle = new Rectangle(x + (i * 32), y, 32, 32);
                tile.Texture = game.tile.Platform1;
                tileList.Add(tile);
                for (int j = 0; j < length; j++)
                {
                    Tile otherTile = new Tile();
                    tile.X = x + (i * 32);
                    tile.Y = y + (j * 32);
                    otherTile.Rectangle = new Rectangle(x + (i * 32), y + (j * 32), 32, 32);
                    otherTile.Texture = game.tile.Platform1;
                    tileList.Add(otherTile);
                }
            }
        }
    }
}
