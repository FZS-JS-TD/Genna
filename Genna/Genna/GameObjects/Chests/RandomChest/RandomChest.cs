using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genna.GameObjects.Chests;
using Genna.Items;
using Genna.GameObjects.Chests.SpecialChest;
using Genna.Items.Moneys;
using Genna.Items.Armors;
using Genna.Items.Weapons;
using Genna.GameObjects.Characters;
using Genna.GameObjects.Characters.Players;
using Genna.GameObjects.Characters.Players.Zanaj_Akari;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Genna.GameObjects.Chests.RandomChest
{
    //Jordan
    public class RandomChest : Chest
    {
        Random rand = new Random();
        protected Zanaj zan;
        protected RandomItem ri;
        protected Texture2D image;
        protected bool isActive;
        protected int numChestItems;
        protected List<Item> chestObjects;

        public List<Item> ChestObjects
        {
            get { return chestObjects; }
            set { chestObjects = value; }
        }
        public int NumChestItems
        {
            get { return numChestItems; }
            set { numChestItems = value; }
        }
        public int X
        {
            get { return rect.X; }
            set { rect.X = value; }
        }
        public int Y
        {
            get { return rect.Y; }
            set { rect.Y = value; }
        }
        public int Width
        {
            get { return rect.Width; }
            set { rect.Width = value; }
        }
        public int Height
        {
            get { return rect.Height; }
            set { rect.Height = value; }
        }
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        public Texture2D Image
        {
            get { return image; }
            set { image = value; }
        }

        public RandomChest(Zanaj pZan, int pX, int pY, bool pIsActive = true)
        {
            Rect = new Rectangle(pX, pY, 32, 32);

            zan = pZan;
            ri = new RandomItem(zan);
            image = Zanaj.getInstance().Game.Content.Load<Texture2D>("Tiles/Chest1");
            isActive = pIsActive;
            isOpen = false;
            chestObjects = new List<Item>();

            //random roll to determine how many items in the chest
            int r1 = rand.Next(0, 100) + zan.Luck;

            //depending on the random value plus Zanaj's luck, set
            //the numItems in the chest
            if (r1 > 0 && r1 < 41)
            {
                numChestItems = 1;
            }
            else if (r1 > 40 && r1 < 71)
            {
                numChestItems = 2;
            }
            else if (r1 > 70 && r1 < 91)
            {
                numChestItems = 3;
            }
            else if (r1 > 90 && r1 < 121)
            {
                numChestItems = 4;
            }
            else if (r1 > 120)
            {
                numChestItems = 5;
            }
            else
            {
                numChestItems = 0;
            }

            //for each item in the chest
            for (int i = 0; i < numChestItems; i++)
            {
                chestObjects.Add(ri.AddRandomItem());
            }
        }

        public RandomChest(Zanaj pZan, bool pIsActive = true)
        {
            rect = new Rectangle();
            zan = pZan;
            ri = new RandomItem(zan);
            image = Zanaj.getInstance().Game.Content.Load<Texture2D>("Tiles/Chest1");
            Width = 32;
            Height = 32;
            isActive = pIsActive;
            isOpen = false;
            chestObjects = new List<Item>();

            //random roll to determine how many items in the chest
            int r1 = rand.Next(0, 100) + zan.Luck;

            //depending on the random value plus Zanaj's luck, set
            //the numItems in the chest
            if (r1 > 0 && r1 < 41)
            {
                numChestItems = 1;
            }
            else if (r1 > 40 && r1 < 71)
            {
                numChestItems = 2;
            }
            else if (r1 > 70 && r1 < 91)
            {
                numChestItems = 3;
            }
            else if (r1 > 90 && r1 < 121)
            {
                numChestItems = 4;
            }
            else if (r1 > 120)
            {
                numChestItems = 5;
            }
            else
            {
                numChestItems = 0;
            }

            //for each item in the chest
            for (int i = 0; i < numChestItems; i++)
            {
                chestObjects.Add(ri.AddRandomItem());
            }
        }
    }
}
