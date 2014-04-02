using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genna.Items;
using Genna.GameObjects.Chests;
using Genna.GameObjects.Chests.RandomChest;
using Genna.GameObjects.Chests.SpecialChest;
using Genna.Items.Moneys;
using Genna.Items.Armors;
using Genna.Items.Weapons;
using Genna.GameObjects.Characters;
using Genna.GameObjects.Characters.Players;
using Genna.GameObjects.Characters.Players.Zanaj_Akari;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Genna.GameObjects
{
    //Jordan and Forrest
    public class Inventory
    {
        KeyboardState keyState = Keyboard.GetState();
        KeyboardState prevKeyState = Keyboard.GetState();

        SpriteFont _font;
        SpriteFont _cursorItemNameFont;
        Game1 game;

        Texture2D _cursorImg;
        Rectangle _cursorRect = new Rectangle(0, 0, 50, 50);

        protected Dictionary<Vector2, Item> _invDict;

        protected Texture2D baseImage;

        protected int moneyAmount;

        protected Zanaj zan = new Zanaj();

        protected Rectangle[,] _itemRects;

        protected Item[,] _invSlot;

        public KeyboardState KeyState
        {
            get { return keyState; }
            set
            {
                prevKeyState = keyState;
                keyState = value;
            }
        }

        public Inventory(Zanaj pZan)
        {
            zan = pZan;
            game = zan.Game;
            _itemRects = new Rectangle[12, 6];
            _invSlot = new Item[12, 6];
            _invDict = new Dictionary<Vector2, Item>();
            baseImage = zan.Game.Content.Load<Texture2D>("Inventory/Inventory1");
            _cursorImg = zan.Game.Content.Load<Texture2D>("Inventory/Cursor");
            _font = game.Content.Load<SpriteFont>("Fonts/itemOverload");
            _cursorItemNameFont = game.Content.Load<SpriteFont>("Fonts/itemName");
        }

        /// <summary>
        /// returns a specific item at a particular index for the inventory
        /// </summary>
        /// <param name="x">x index of inventory</param>
        /// <param name="y">y index of inventory</param>
        /// <returns></returns>
        public Item GetItemAtIndex(int x, int y)
        {
            Vector2 v = new Vector2(x, y);

            if (_invDict.ContainsKey(v))
            {
                return _invDict[v];
            }

            return null;
        }

        /// <summary>
        /// adds an item to the inventory
        /// </summary>
        /// <param name="pItem">item to be added</param>
        public bool AddItem(Item pItem)
        {
            //armor
            if (pItem.Type > 1 && pItem.Type < 5)
            {
                //boots
                if (pItem.Type == 2)
                {
                    for (int y = 0; y < 12; y++)
                    {
                        if (_invDict.ContainsKey(new Vector2(y, 3)) == false)
                        {
                            _invSlot[y, 3] = (Boots)pItem;
                            _invDict[new Vector2(y, 3)] = (Boots)pItem;
                            return true;
                        }
                        else if (y == 11 && _invSlot[y, 3] == null)
                        {
                            //to be edited later with prompt for user to
                            //replace item with another that exists
                            return false;
                        }
                    }
                }
                //gloves
                else if (pItem.Type == 3)
                {
                    for (int y = 0; y < 12; y++)
                    {
                        if (_invDict.ContainsKey(new Vector2(y, 2)) == false)
                        {
                            _invSlot[y, 2] = (Gloves)pItem;
                            _invDict[new Vector2(y, 2)] = (Gloves)pItem;
                            return true;
                        }
                        else if (y == 11 && _invSlot[y, 2] == null)
                        {
                            return false;
                        }
                    }
                }

                //breastplate
                else if (pItem.Type == 4)
                {
                    for (int y = 0; y < 12; y++)
                    {
                        if (_invDict.ContainsKey(new Vector2(y, 1)) == false)
                        {
                            _invSlot[y, 1] = (Breastplate)pItem;
                            _invDict[new Vector2(y, 1)] = (Breastplate)pItem;
                            return true;
                        }
                        else if (y == 11 && _invSlot[y, 1] == null)
                        {
                            return false;
                        }
                    }
                }
            }

            //weapon
            else if (pItem.Type > 4 && pItem.Type < 13)
            {
                for (int y = 0; y < 12; y++)
                {
                    if (_invDict.ContainsKey(new Vector2(y, 0)) == false)
                    {
                        _invSlot[y, 0] = (Weapon)pItem;
                        _invDict[new Vector2(y, 0)] = (Weapon)pItem;
                        return true;
                    }
                    else if (y == 11 && _invSlot[y, 0] == null)
                    {
                        return false;
                    }
                }
            }

            //coal
            else if (pItem.Type == 0)
            {
                for (int y = 0; y < 12; y++)
                {
                    if (_invDict.ContainsKey(new Vector2(y, 4)) == false)
                    {
                        _invSlot[y, 4] = (Coal)pItem;
                        _invDict[new Vector2(y, 4)] = (Coal)pItem;
                        return true;
                    }
                    else if (_invDict.ContainsKey(new Vector2(y, 5)) == false)
                    {
                        _invSlot[y, 5] = (Coal)pItem;
                        _invDict[new Vector2(y, 5)] = (Coal)pItem;
                        return true;
                    }
                    else if (y == 11 && _invSlot[y, 4] == null && _invSlot[y, 4] == null)
                    {
                        return false;
                    }
                }
            }

            //money
            else if (pItem.Type == 1)
            {
                moneyAmount += pItem.Value;
                return true;
            }

            return false;
        }

        public void RemoveItem(int x, int y)
        {
            Vector2 v = new Vector2(x, y);

            if (_invDict.ContainsKey(v))
            {
                _invDict.Remove(v);
                _invSlot[x, y] = null;
            }
        }

        public void Equip(int x, int y)
        {
            Vector2 v = new Vector2(x, y);

            if (_invDict.ContainsKey(v))
            {
                if (y == 0)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        Vector2 v2 = new Vector2(i, y);

                        if (_invDict.ContainsKey(v2))
                        {
                            _invDict[v2].Equipped = false;
                        }
                    }

                    zan._EqWeapon = (Weapon)_invDict[v];
                    _invDict[v].Equipped = true;
                }
                else if (y == 1)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        Vector2 v2 = new Vector2(i, y);

                        if (_invDict.ContainsKey(v2))
                        {
                            _invDict[v2].Equipped = false;
                        }
                    }

                    zan._EqBreastplate = (Breastplate)_invDict[v];
                    _invDict[v].Equipped = true;
                }
                else if (y == 2)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        Vector2 v2 = new Vector2(i, y);

                        if (_invDict.ContainsKey(v2))
                        {
                            _invDict[v2].Equipped = false;
                        }
                    }

                    zan._EqGloves = (Gloves)_invDict[v];
                    _invDict[v].Equipped = true;
                }
                else if (y == 3)
                {
                    for (int i = 0; i < 12; i++)
                    {
                        Vector2 v2 = new Vector2(i, y);

                        if (_invDict.ContainsKey(v2))
                        {
                            _invDict[v2].Equipped = false;
                        }
                    }

                    zan._EqBoots = (Boots)_invDict[v];
                    _invDict[v].Equipped = true;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(baseImage,
                new Rectangle(0, 0, zan.Game.GraphicsDevice.Viewport.Width, zan.Game.GraphicsDevice.Viewport.Height), Color.White);

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    Item itemAtIndex = GetItemAtIndex(i, j);

                    if (itemAtIndex != null)
                    {
                        if (itemAtIndex.Legendary == true)
                        {
                            if (itemAtIndex.Equipped == false)
                            {
                                spriteBatch.Draw(itemAtIndex.Image, new Rectangle((i * 50), (j * 50), 50, 50), Color.Tomato);
                            }
                            if (itemAtIndex.Equipped == true)
                            {
                                spriteBatch.Draw(itemAtIndex.Image, new Rectangle((i * 50), (j * 50), 50, 50), Color.Green);
                            }
                        }
                        else if (itemAtIndex.Legendary == false)
                        {
                            if (itemAtIndex.Equipped == false)
                            {
                                spriteBatch.Draw(itemAtIndex.Image, new Rectangle((i * 50), (j * 50), 50, 50), Color.White);
                            }
                            if (itemAtIndex.Equipped == true)
                            {
                                spriteBatch.Draw(itemAtIndex.Image, new Rectangle((i * 50), (j * 50), 50, 50), Color.Green);
                            }
                        }
                    }
                }
            }

            spriteBatch.Draw(_cursorImg, _cursorRect, Color.White);
            spriteBatch.DrawString(_cursorItemNameFont, "Character\n  Stats:", new Vector2(625, 215), Color.White);
            spriteBatch.DrawString(_font, "Max Health: " + zan.MaxHealth, new Vector2(610, 325), Color.White);
            spriteBatch.DrawString(_font, "Pois. Resist: " + zan.PoisonResist, new Vector2(610, 385), Color.White);
            spriteBatch.DrawString(_font, "Speed: " + zan.MaxSpeed, new Vector2(610, 445), Color.White);
            spriteBatch.DrawString(_font, "Luck: " + zan.Luck, new Vector2(610, 505), Color.White);
            spriteBatch.DrawString(_font, "Gold: " + moneyAmount, new Vector2(610, 560), Color.White);

            Item cursorItemAtIndex = GetItemAtIndex((_cursorRect.X / 50), (_cursorRect.Y / 50));
            if (cursorItemAtIndex != null)
            {
                spriteBatch.Draw(cursorItemAtIndex.Image, new Rectangle(0, 300, 250, 250), Color.White);

                spriteBatch.DrawString(_cursorItemNameFont, cursorItemAtIndex.Id, new Vector2(260, 310), Color.White);
                spriteBatch.DrawString(_font, "Value: " + cursorItemAtIndex.Value, new Vector2(260, 560), Color.White);
                spriteBatch.DrawString(_font, "Tier: " + cursorItemAtIndex.Tier, new Vector2(260, 380), Color.White);

                if (cursorItemAtIndex.Type > 1 && cursorItemAtIndex.Type < 5)
                {
                    spriteBatch.DrawString(_font, "Durability: " + cursorItemAtIndex.Durability, new Vector2(260, 415), Color.White);

                    if (cursorItemAtIndex.Type == 2)
                    {
                        spriteBatch.DrawString(_font, "Plus Speed: " + cursorItemAtIndex.PlusSpeed, new Vector2(260, 450), Color.White);
                    }
                    else if (cursorItemAtIndex.Type == 3)
                    {
                        spriteBatch.DrawString(_font, "Plus Luck: " + cursorItemAtIndex.PlusLuck, new Vector2(260, 450), Color.White);
                    }
                    else if (cursorItemAtIndex.Type == 4)
                    {
                        spriteBatch.DrawString(_font, "Plus Health: " + cursorItemAtIndex.PlusHealth, new Vector2(260, 450), Color.White);
                        spriteBatch.DrawString(_font, "Plus Poison Resist: " + cursorItemAtIndex.PlusPoisonResist, new Vector2(260, 485), Color.White);
                    }
                }
                else if (cursorItemAtIndex.Type > 4 && cursorItemAtIndex.Type < 13)
                {
                    spriteBatch.DrawString(_font, "Damage: " + cursorItemAtIndex.Damage, new Vector2(260, 415), Color.White);
                    spriteBatch.DrawString(_font, "Fire Rate Interval: " + cursorItemAtIndex.FireRate, new Vector2(260, 450), Color.White);
                    spriteBatch.DrawString(_font, "Bullet Speed: " + cursorItemAtIndex.BulletSpeed, new Vector2(260, 485), Color.White);

                    if (cursorItemAtIndex.Type == 7)
                    {
                        spriteBatch.DrawString(_font, "Spread Bullets: " + cursorItemAtIndex.SpreadBullets, new Vector2(260, 520), Color.White);
                    }
                }

                if (cursorItemAtIndex.Legendary == true)
                {
                    spriteBatch.DrawString(_font, "LEGENDARY", new Vector2(70, 560), Color.White);
                }
            }
        }

        private bool PressedOnlyOnce(Keys key)
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

        public void Update()
        {
            KeyState = Keyboard.GetState();

            if (PressedOnlyOnce(Keys.R))
            {
                RemoveItem((_cursorRect.X / 50), (_cursorRect.Y / 50));
            }

            if (PressedOnlyOnce(Keys.E))
            {
                Equip((_cursorRect.X / 50), (_cursorRect.Y / 50));
            }

            if (PressedOnlyOnce(Keys.Right))
            {
                if (_cursorRect.X == 550)
                {
                    _cursorRect.X = 0;
                }
                else
                {
                    _cursorRect.X += 50;
                }
            }
            else if (PressedOnlyOnce(Keys.Left))
            {
                if (_cursorRect.X == 0)
                {
                    _cursorRect.X = 550;
                }
                else
                {
                    _cursorRect.X -= 50;
                }
            }
            else if (PressedOnlyOnce(Keys.Up))
            {
                if (_cursorRect.Y == 0)
                {
                    _cursorRect.Y = 250;
                }
                else
                {
                    _cursorRect.Y -= 50;
                }
            }
            else if (PressedOnlyOnce(Keys.Down))
            {
                if (_cursorRect.Y == 250)
                {
                    _cursorRect.Y = 0;
                }
                else
                {
                    _cursorRect.Y += 50;
                }
            }

            else if (PressedOnlyOnce(Keys.I) || PressedOnlyOnce(Keys.Escape))
            {
                this.zan.Game._GameMode = Game1.GameMode.Playing;
            }

            zan.checkEquipped();
        }

        public void UseItem()
        {
            //to be edited later?
        }
    }
}