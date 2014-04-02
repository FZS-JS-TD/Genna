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

using Genna.Items;
using Genna.GameObjects.Chests;
using Genna.GameObjects.Chests.RandomChest;
using Genna.GameObjects.Chests.SpecialChest;
using Genna.Items.Moneys;
using Genna.Items.Armors;
using Genna.Items.Weapons;
using Genna.GameObjects;
using Genna.GameObjects.Enemies;
using Genna.GameObjects.Characters;
using Genna.GameObjects.Characters.Players;
using Genna.GameObjects.Characters.Players.Zanaj_Akari;
using Genna.GameObjects.Puzzles.Levers;
using Genna.GameObjects.Puzzles;

namespace Genna.Levels
{
    //Forrest
    public class Level
    {
        public static Zanaj zanaj;
        public static Game1 game;

        protected KeyboardState prevKeyState;
        protected KeyboardState keyState;

        private List<Tile> tileList;
        public List<Enemy> enemies;

        public Rectangle map;

        private Texture2D mapEdgeR;
        private Texture2D mapEdgeL;
        private Rectangle mapEdgeRRect;
        private Rectangle mapEdgeLRect;

        protected List<Particle> particles;
        public static Texture2D _particleImg;
        protected static Texture2D _backgroundImage;

        protected List<RandomChest> rndChests;
        protected List<List<Item>> rndChestItemLists;
        protected List<Item> chestItems;
        protected RandomItem ri;

        private string name;

        private string background;
        private Texture2D backgroundTexture;
        private Rectangle backgroundRect;
        private List<Lever> leverList = new List<Lever>();
        private List<Gate> gateList = new List<Gate>();

        public SpriteFont spriteFont;

        public List<Gate> GateList
        {
            get { return gateList; }
        }

        public List<Lever> LeverList
        {
            get { return leverList; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public Rectangle Map
        {
            get { return map; }
            set { map = value; }
        }

        public List<Tile> TileList
        {
            get { return tileList; }
            set { tileList = value; }
        }

        public string Background
        {
            get { return background; }
            set { background = value; }
        }

        public Texture2D BackgroundTexture
        {
            get { return backgroundTexture; }
            set { backgroundTexture = value; }
        }

        public Rectangle BackgroundRect
        {
            get { return backgroundRect; }
            set { backgroundRect = value; }
        }

        public Texture2D MapEdgeR
        {
            get { return mapEdgeR; }
            set { mapEdgeR = value; }
        }

        public Texture2D MapEdgeL
        {
            get { return mapEdgeL; }
            set { mapEdgeL = value; }
        }

        public Rectangle MapEdgeRRect
        {
            get { return mapEdgeRRect; }
            set { mapEdgeRRect = value; }
        }

        public Rectangle MapEdgeLRect
        {
            get { return mapEdgeLRect; }
            set { mapEdgeLRect = value; }
        }

        public bool PressedOnlyOnce(Keys key)
        {
            if (prevKeyState.IsKeyUp(key) && keyState.IsKeyDown(key))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public KeyboardState PrevKeyState
        {
            get
            {
                return prevKeyState;
            }
            set
            {
                prevKeyState = value;
            }
        }

        public KeyboardState CurrentKeyState
        {
            get
            {
                return keyState;
            }
            set
            {
                prevKeyState = keyState;
                keyState = value;
            }
        }

        public virtual void Pause()
        {
            game._GameMode = Game1.GameMode.Menu;
        }

        public virtual void ScreenWrapWorld(Game1 gGame, Zanaj zan = null)
        {
        }

        public virtual void LoadMap(string mapName)
        {
            TileList = new List<Tile>();
            StreamReader input = new StreamReader("../../../../GennaContent/MapFiles/" + mapName);

            string file = input.ReadLine();

            string[] parts = file.Split(',');
            int mapWidth = Convert.ToInt32(parts[0]);
            int mapHeight = Convert.ToInt32(parts[1]);
            Map = new Rectangle(0, 0, mapWidth, mapHeight);

            Background = input.ReadLine();

            file = "";
            while (file != null)
            {
                file = input.ReadLine();

                if (file != null)
                {
                    parts = file.Split(',');
                    int x = Convert.ToInt32(parts[0]); // tile x position
                    int y = Convert.ToInt32(parts[1]); // tile y position
                    int width = Convert.ToInt32(parts[2]); // tile width
                    int height = Convert.ToInt32(parts[3]); // tile height
                    int id = Convert.ToInt32(parts[4]); // tile id
                    bool collision = Convert.ToBoolean(parts[5]); // tile collision detection
                    Tile tile = new Tile();
                    tile.Rectangle = new Rectangle(x, y, width, height);
                    tile.ID = id;
                    tile.Collides = collision;
                    tile.X = x;
                    tile.Y = y;
                    TileList.Add(tile);
                }
            }
            input.Close();
        }

        public virtual void Update(GameTime gameTime)
        {
            CurrentKeyState = Keyboard.GetState();

            if (PressedOnlyOnce(Keys.Q))
            {
                Pause();
            }

            List<Enemy> toKill = new List<Enemy>();
            foreach (Enemy enemy in enemies)
            {
                enemy.Update();

                if (enemy.isDead)
                {
                    toKill.Add(enemy);
                }
            }

            foreach (Enemy e in toKill)
            {
                enemies.Remove(e);
                rndChests.Add(new RandomChest(Zanaj.getInstance(), e.X, e.Y));
            }

            
        }


        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (RandomChest rc in rndChests)
            {
                if (rc.IsActive)
                {
                    spriteBatch.Draw(rc.Image, rc.Rect, Color.White);
                }
            }

            foreach (Item i in chestItems)
            {
                if (i.IsActive)
                {
                    spriteBatch.Draw(i.Image, i.rect, Color.White);
                }
            }

            foreach (Enemy enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (RandomChest rc in rndChests)
            {
                if (PressedOnlyOnce(Keys.Enter))
                {
                    if (zanaj.rect.Intersects(rc.Rect) && rc.IsActive == true)
                    {
                        rc.IsOpen = true;
                        rc.IsActive = false;
                        int prevChestItemCount = chestItems.Count;

                        for (int i = 0; i < rc.NumChestItems; i++)
                        {
                            chestItems.Add(ri.AddRandomItem());
                        }

                        for (int i = 0; i < chestItems.Count - prevChestItemCount; i++)
                        {
                            chestItems[i + prevChestItemCount].Rect = new Rectangle((rc.X - 50 + (50 * i)), (rc.Y), 50, 50);
                        }

                        rndChestItemLists.Add(chestItems);
                    }
                }

                if (rc.Rect.Intersects(zanaj.rect) && rc.IsActive && rc.IsOpen == false)
                {
                    spriteBatch.DrawString(spriteFont, "Press 'Enter' to Open Chest", new Vector2(rc.X, (rc.Y - 35)), Color.White);
                }
            }

            foreach (Item i in chestItems)
            {
                Rectangle itemRect = i.Rect;

                if (itemRect.Intersects(zanaj.rect) && i.IsActive)
                {
                    bool b = zanaj._inventory.AddItem(i);

                    if (!b)
                    {
                        spriteBatch.DrawString(spriteFont, "Too Many Items of This Type", new Vector2(itemRect.X, itemRect.Y), Color.White);
                    }
                    else
                    {
                        i.IsActive = false;
                        //chestItems.Remove(i);
                    }
                }
            }

        }

        public virtual void MapLoader(Tile tile, ContentManager Content)
        {
            if (background != null && background != "")
            {
                BackgroundTexture = Content.Load<Texture2D>("Backgrounds/" + Background);
            }
            mapEdgeR = Content.Load<Texture2D>("Tiles/MapEdgeR");
            mapEdgeL = Content.Load<Texture2D>("Tiles/MapEdgeL");
            if (TileList != null)
            {
                foreach (Tile existingTile in TileList)
                {
                    switch (existingTile.ID)
                    {
                        case 0:
                            existingTile.Texture = tile.Brick1;
                            break;
                        case 1:
                            existingTile.Texture = tile.Brick2;
                            break;
                        case 2:
                            existingTile.Texture = tile.ChestArmored;
                            break;
                        case 3:
                            existingTile.Texture = tile.ChestEnchanted;
                            break;
                        case 4:
                            existingTile.Texture = tile.ChestNormal;
                            break;
                        case 5:
                            existingTile.Texture = tile.Concrete;
                            break;
                        case 6:
                            existingTile.Texture = tile.ConcreteStriped;
                            break;
                        case 7:
                            existingTile.Texture = tile.Dirt1;
                            break;
                        case 8:
                            existingTile.Texture = tile.Dirt2;
                            break;
                        case 9:
                            existingTile.Texture = tile.DirtPoisoned;
                            break;
                        case 10:
                            existingTile.Texture = tile.GateBars;
                            break;
                        case 11:
                            existingTile.Texture = tile.GateHorizontalBar;
                            break;
                        case 12:
                            existingTile.Texture = tile.GateTop;
                            break;
                        case 13:
                            existingTile.Texture = tile.GateTopEnd;
                            break;
                        case 14:
                            existingTile.Texture = tile.PipeLTurn;
                            break;
                        case 15:
                            existingTile.Texture = tile.PipeStraight1;
                            break;
                        case 16:
                            existingTile.Texture = tile.PipeStraight2;
                            break;
                        case 17:
                            existingTile.Texture = tile.Platform1;
                            break;
                        case 18:
                            existingTile.Texture = tile.Rock1;
                            break;
                        case 19:
                            existingTile.Texture = tile.Rock2;
                            break;
                        case 20:
                            existingTile.Texture = tile.Rock3;
                            break;
                        case 21:
                            existingTile.Texture = tile.RoofingSlope;
                            break;
                        case 22:
                            existingTile.Texture = tile.RoofingSlopeReversed;
                            break;
                        case 23:
                            existingTile.Texture = tile.RoofingTile;
                            break;
                        case 24:
                            existingTile.Texture = tile.Window;
                            break;
                        case 25:
                            existingTile.Texture = tile.Wood;
                            break;
                        case 26:
                            existingTile.Texture = tile.DoorTop;
                            break;
                        case 27:
                            existingTile.Texture = tile.DoorBottom;
                            break;
                        case 28:
                            existingTile.Texture = tile.Clock;
                            break;
                        case 29:
                            existingTile.Texture = tile.EmptyShelveLeft;
                            break;
                        case 30:
                            existingTile.Texture = tile.EmptyShelve;
                            break;
                        case 31:
                            existingTile.Texture = tile.EmptyShelveRight;
                            break;
                        case 32:
                            existingTile.Texture = tile.PotionShelveLeft;
                            break;
                        case 33:
                            existingTile.Texture = tile.PotionShelve;
                            break;
                        case 34:
                            existingTile.Texture = tile.PotionShelveRight;
                            break;
                        case 35:
                            existingTile.Texture = tile.WoodFloor;
                            break;
                        case 36:
                            existingTile.Texture = tile.Counter;
                            break;
                        case 37:
                            existingTile.Texture = tile.ClinicFloor;
                            break;
                        case 38:
                            existingTile.Texture = tile.ClinicTile;
                            break;
                        case 39:
                            existingTile.Texture = tile.Clock2;
                            break;
                        case 40:
                            existingTile.Texture = tile.Counter2;
                            break;
                        case 41:
                            existingTile.Texture = tile.FlowerVase; ;
                            break;
                        case 42:
                            existingTile.Texture = tile.MedKit;
                            break;
                        case 43:
                            existingTile.Texture = tile.Spikes;
                            break;
                    }
                }
            }
        }
    }
}   