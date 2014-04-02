using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Genna.GameObjects.Characters.Players.Zanaj_Akari;
using Genna.Items;
using Genna.Items.Armors;
using Genna.Items.Weapons;


namespace Genna.LoadSave
{
    public class FileHandler
    {
        StreamWriter output;

        public FileHandler()
        {

        }

        // Saves the player health and inventory
        public void Save(Game1 game, int slot)
        {
            if (output == null)
            {
                output = new StreamWriter("../../../../GennaContent/Saves/Slot" + slot + ".txt");
                output.WriteLine(Zanaj.getInstance().Health);
                output.WriteLine(Zanaj.getInstance()._inventory.MoneyAmount);
                for (int i = 0; i < Zanaj.getInstance()._inventory.InvList.Count; i++)
                {
                    output.Write(Zanaj.getInstance()._inventory.InvList[i].Tier + ",");
                    output.Write(Zanaj.getInstance()._inventory.InvList[i].Type + ",");
                    output.Write(Zanaj.getInstance()._inventory.InvList[i].Value + ",");
                    output.Write(Zanaj.getInstance()._inventory.InvList[i].Legendary + ",");
                    output.Write(Zanaj.getInstance()._inventory.InvList[i].Id + ",");
                    if (Zanaj.getInstance()._inventory.InvList[i].Type == 2)
                    {
                        output.Write(Zanaj.getInstance()._inventory.InvList[i].PlusSpeed + ",");
                        output.Write(Zanaj.getInstance()._inventory.InvList[i].Durability);
                    }
                    else if (Zanaj.getInstance()._inventory.InvList[i].Type == 3)
                    {
                        output.Write(Zanaj.getInstance()._inventory.InvList[i].PlusLuck + ",");
                        output.Write(Zanaj.getInstance()._inventory.InvList[i].Durability);
                    }
                    else if (Zanaj.getInstance()._inventory.InvList[i].Type == 4)
                    {
                        output.Write(Zanaj.getInstance()._inventory.InvList[i].PlusHealth + ",");
                        output.Write(Zanaj.getInstance()._inventory.InvList[i].PlusPoisonResist + ",");
                        output.Write(Zanaj.getInstance()._inventory.InvList[i].Durability);
                    }
                    else if (Zanaj.getInstance()._inventory.InvList[i].Type > 4)
                    {
                        output.Write(Zanaj.getInstance()._inventory.InvList[i].Damage + ",");
                        output.Write(Zanaj.getInstance()._inventory.InvList[i].FireRate + ",");
                        output.Write(Zanaj.getInstance()._inventory.InvList[i].BulletSize + ",");
                        output.Write(Zanaj.getInstance()._inventory.InvList[i].BulletSpeed + ",");
                        output.Write(Zanaj.getInstance()._inventory.InvList[i].SpreadBullets);
                    }

                    output.WriteLine("");
                }

                output.Close();
                output = null;
            }
        }

        // Loads the player health and inventory
        public void Load(Game1 game, int slot)
        {
            Zanaj.isDead = false;
            Zanaj.instance = new Zanaj(game, 0, 0);
            Game1.zanaj = Zanaj.getInstance();
            StreamReader input = new StreamReader("../../../../GennaContent/Saves/Slot" + slot + ".txt");
            List<Item> itemList = new List<Item>();

            game.Initialize(1);
            game.CurrentLevel = game.gennaTown;

            Game1.gameMode = Game1.GameMode.Playing;

            Zanaj.getInstance().Health = Convert.ToInt32(input.ReadLine());
            Zanaj.getInstance()._inventory.MoneyAmount = Convert.ToInt32(input.ReadLine());

            string file = "";
            while (file != null)
            {
                file = input.ReadLine();
                if (file != null)
                {
                    string[] parts = file.Split(',');
                    int tier = Convert.ToInt32(parts[0]);
                    int type = Convert.ToInt32(parts[1]);
                    int value = Convert.ToInt32(parts[2]);
                    bool Legendary = Convert.ToBoolean(parts[3]);
                    string id = parts[4];

                    if (type == 2)
                    {
                        int plusSpeed = Convert.ToInt32(parts[5]);
                        int durability = Convert.ToInt32(parts[6]);
                        Texture2D image = game.Content.Load<Texture2D>("Armors/boots");
                        itemList.Add(new Boots(image, tier, type, id, value, durability, plusSpeed));
                    }
                    else if (type == 3)
                    {
                        int plusLuck = Convert.ToInt32(parts[5]);
                        int durability = Convert.ToInt32(parts[6]);
                        Texture2D image = game.Content.Load<Texture2D>("Armors/gloves");
                        itemList.Add(new Gloves(image, tier, type, id, value, durability, plusLuck));
                    }
                    else if (type == 4)
                    {
                        int plusHealth = Convert.ToInt32(parts[5]);
                        int plusPosionResist = Convert.ToInt32(parts[6]);
                        int durability = Convert.ToInt32(parts[7]);
                        Texture2D image = game.Content.Load<Texture2D>("Armors/breastplate");
                        itemList.Add(new Breastplate(image, tier, type, id, value, durability, true, plusPosionResist, plusHealth));
                    }
                    else if (type > 4)
                    {
                        int damage = Convert.ToInt32(parts[5]);
                        int fireRate = Convert.ToInt32(parts[6]);
                        int bulletSize = Convert.ToInt32(parts[7]);
                        int bulletSpeed = Convert.ToInt32(parts[8]);
                        int spreadBullets = Convert.ToInt32(parts[9]);
                        if (type == 5)
                        {
                            Texture2D image = game.Content.Load<Texture2D>("Weapons/pistol");
                            itemList.Add(new Pistol(image, tier, type, id, value, damage, fireRate, spreadBullets, bulletSize, bulletSpeed));
                        }
                        else if (type == 6)
                        {
                            Texture2D image = game.Content.Load<Texture2D>("Weapons/SMG");
                            itemList.Add(new SMG(image, tier, type, id, value, damage, fireRate, spreadBullets, bulletSize, bulletSpeed));
                        }
                        else if (type == 7)
                        {
                            Texture2D image = game.Content.Load<Texture2D>("Weapons/shotgun");
                            itemList.Add(new Shotgun(image, tier, type, id, value, damage, fireRate, spreadBullets, bulletSize, bulletSpeed));
                        }
                        else if (type == 8)
                        {
                            Texture2D image = game.Content.Load<Texture2D>("Weapons/assaultRifle");
                            itemList.Add(new AssaultRifle(image, tier, type, id, value, damage, fireRate, spreadBullets, bulletSize, bulletSpeed));
                        }
                        else if (type == 9)
                        {
                            Texture2D image = game.Content.Load<Texture2D>("Weapons/Plasma_Shot");
                            itemList.Add(new PlasmaBlaster(image, tier, type, id, value, damage, fireRate, spreadBullets, bulletSize, bulletSpeed));
                        }
                        else if (type == 10)
                        {
                            Texture2D image = game.Content.Load<Texture2D>("Weapons/sniperRifle");
                            itemList.Add(new SniperRifle(image, tier, type, id, value, damage, fireRate, spreadBullets, bulletSize, bulletSpeed));
                        }
                        else if (type == 11)
                        {
                            Texture2D image = game.Content.Load<Texture2D>("Weapons/rocketLauncher");
                            itemList.Add(new RocketLauncher(image, tier, type, id, value, damage, fireRate, spreadBullets, bulletSize, bulletSpeed));
                        }
                        else if (type == 12)
                        {
                            Texture2D image = game.Content.Load<Texture2D>("Weapons/raygun");
                            itemList.Add(new Raygun(image, tier, type, id, value, damage, fireRate, spreadBullets, bulletSize, bulletSpeed));
                        }
                    }
                }
            }
            foreach (Item item in itemList)
            {
                Zanaj.getInstance()._inventory.AddItem(item);
            }
        }
    }
}
