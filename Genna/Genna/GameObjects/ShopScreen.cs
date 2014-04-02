using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Genna.Items;
using Genna.Items.Moneys;
using Genna.Items.Armors;
using Genna.Items.Weapons;
using Genna.GameObjects.Characters.Players.Zanaj_Akari;

namespace Genna.GameObjects
{
    public class ShopScreen
    {
        private Game1 game;
        private Texture2D menu;
        private Texture2D cursor;
        private Rectangle cursorRect;
        private Vector2 menuPos;
        private SpriteFont font;
        private SpriteFont itemNameFont;
        private Item[,] inventorySlot;
        private Dictionary<Vector2, Item> inventoryDict;

        public ShopScreen(Game1 game)
        {
            this.game = game;
            menuPos = new Vector2(0, 0);
            inventoryDict = Game1.zanaj._inventory.InvDict;
            inventorySlot = new Item[12, 6];
            cursorRect = new Rectangle(0, 0, 50, 50);
        }

        public void Load()
        {
            menu = game.Content.Load<Texture2D>("Inventory/ShopMenu");
            font = game.Content.Load<SpriteFont>("Fonts/itemOverload");
            cursor = game.Content.Load<Texture2D>("Inventory/Cursor");
            itemNameFont = game.Content.Load<SpriteFont>("Fonts/itemName");
        }

        public Item GetItemAtIndex(int x, int y)
        {
            Vector2 v = new Vector2(x, y);

            if (inventoryDict.ContainsKey(v))
            {
                return inventoryDict[v];
            }

            return null;
        }

        public void AddItem(Item item)
        {
            if (item.Type > 1 && item.Type < 5)
            {
                if (item.Type == 2)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        if (!inventoryDict.ContainsKey(new Vector2(i, 3)))
                        {
                            inventorySlot[i, 3] = (Boots)item;
                            inventoryDict[new Vector2(i, 3)] = (Boots)item;
                        }
                    }
                }
                else if (item.Type == 3)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        if (!inventoryDict.ContainsKey(new Vector2(i, 2)))
                        {
                            inventorySlot[i, 2] = (Gloves)item;
                            inventoryDict[new Vector2(i, 2)] = (Gloves)item;
                        }
                    }
                }
                else if (item.Type == 4)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        if (!inventoryDict.ContainsKey(new Vector2(i, 1)))
                        {
                            inventorySlot[i, 1] = (Breastplate)item;
                            inventoryDict[new Vector2(i, 1)] = (Breastplate)item;
                        }
                    }
                }
            }
            else if (item.Type > 4 && item.Type < 13)
            {
                for (int i = 0; i < 12; i++)
                {
                    if (!inventoryDict.ContainsKey(new Vector2(i, 0)))
                    {
                        inventorySlot[i, 0] = (Weapon)item;
                        inventoryDict[new Vector2(i, 0)] = (Weapon)item;
                    }
                }
            }
        }

        public void RemoveItem(int x, int y)
        {
            Vector2 v = new Vector2(x, y);

            if (inventoryDict.ContainsKey(v))
            {
                inventoryDict.Remove(v);
                inventorySlot[x, y] = null;
            }
        }

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(menu, menuPos, Color.White);

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Item itemAtIndex = GetItemAtIndex(i, j);

                    if (itemAtIndex != null)
                    {
                        if (itemAtIndex.Legendary)
                        {
                            spriteBatch.Draw(itemAtIndex.Image, new Rectangle((i * 50), (j * 50), 50, 50), Color.Tomato);
                        }
                        else
                        {
                            spriteBatch.Draw(itemAtIndex.Image, new Rectangle((i * 50), (j * 50), 50, 50), Color.White);
                        }
                    }
                }
            }
        }
    }
}
