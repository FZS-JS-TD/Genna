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
    class RandomChest : Chest
    {
        Random rand = new Random();
        protected Zanaj zan;
        protected RandomItem ri;
        protected Texture2D image;
        protected int width;
        protected int height;
        protected int x;
        protected int y;
        protected bool isActive;

        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public int Width
        {
            get { return width; }
            set { width = value; }
        }
        public int Height
        {
            get { return height; }
            set { height = value; }
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

        public RandomChest(Zanaj pZan, bool pIsActive = true)
        {
            zan = pZan;
            ri = new RandomItem(zan);
            image = zan.Game.Content.Load<Texture2D>("Tiles/Chest1");
            width = 32;
            height = 32;
            isActive = pIsActive;
        }

        public ArrayList SpawnRandomChest()
        {
            //array list which holds all the chest's items
            ArrayList chestObjects = new ArrayList();

            //random roll to determine how many items in the chest
            int r1 = rand.Next(0, 100) + zan.Luck;
            int numItems;

            //depending on the random value plus Zanaj's luck, set
            //the numItems in the chest
            if (r1 > 0 && r1 < 41)
            {
                numItems = 1;
            }
            else if (r1 > 40 && r1 < 71)
            {
                numItems = 2;
            }
            else if (r1 > 70 && r1 < 91)
            {
                numItems = 3;
            }
            else if (r1 > 90 && r1 < 121)
            {
                numItems = 4;
            }
            else if (r1 > 120)
            {
                numItems = 5;
            }
            else
            {
                numItems = 0;
            }

            //for each item in the chest
            for (int i = 0; i < numItems; i++)
            {
                chestObjects.Add(ri.AddRandomItem());

                #region commented code
                /*
                int r2 = rand.Next(0, 100);

                int type;
                string id;

                #region coal
                //item is coal
                if (r2 <= 0)
                {
                    Coal c = new Coal(1, 0, "Coal", 1);
                    chestObjects.Add(c);
                }
                #endregion

                #region money
                //item is money
                else if (r2 > 0 && r2 < 41)
                {
                    //determines tier
                    int r3 = rand.Next(0, 100) + zan.Luck;
                    int tier;

                    type = 1;
                    id = "Money";

                    //determines amount
                    int r4;

                    //depending on tier, generate random amount of money, create
                    //Money class object and add to chestObjects list
                    if (r3 < 51)
                    {
                        tier = 1;
                        r4 = rand.Next(1, 300) + zan.Luck;

                        Money m = new Money(r4, tier, 1, id, r4);
                        chestObjects.Add(m);

                    }
                    else if (r3 > 50 && r3 < 86)
                    {
                        tier = 2;
                        r4 = rand.Next(301, 1000) + (2 * zan.Luck);

                        Money m = new Money(r4, tier, 1, id, r4);
                        chestObjects.Add(m);

                    }
                    else if (r3 > 85)
                    {
                        tier = 3;
                        r4 = rand.Next(1000, 5000) + (3 * zan.Luck);

                        Money m = new Money(r4, tier, 1, id, r4);
                        chestObjects.Add(m);
                    }
                }
                #endregion

                #region armor
                //item is armor
                else if (r2 > 40 && r2 < 66)
                {
                    //determines tier
                    int r3 = rand.Next(0, 100) + zan.Luck;
                    int tier;

                    //determines type
                    int r4 = rand.Next(0, 100);

                    #region tier 1 armor
                    //tier 1 armor
                    if (r3 < 61)
                    {
                        tier = 1;

                        //tier 1 boots
                        if (r4 < 34)
                        {
                            type = 2;
                            id = "Boots";
                            int value;

                            int durability = rand.Next(0, 30) + zan.Luck;

                            int plusSpeed;
                            int rPlusSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines plusSpeed
                            if (rPlusSpeed > 40 && rPlusSpeed < 81)
                            {
                                plusSpeed = rand.Next(1, 5);
                            }
                            else if (rPlusSpeed > 80)
                            {
                                plusSpeed = rand.Next(5, 10);
                            }
                            else
                            {
                                plusSpeed = 0;
                            }

                            if (durability > 20)
                            {
                                id = "Tough " + id;
                            }
                            if (rPlusSpeed > 80)
                            {
                                id = "Quick " + id;
                            }
                            if (durability > 24 && plusSpeed > 7)
                            {
                                id = "Jordan's Ed Hardy's";
                            }

                            value = durability + (plusSpeed * 2);

                            Boots b = new Boots(tier, type, id, value, durability, plusSpeed);

                            chestObjects.Add(b);
                        }

                        //tier 1 gloves
                        else if (r4 > 33 && r4 < 67)
                        {
                            type = 3;
                            id = "Gloves";
                            int value;

                            int durability = rand.Next(0, 30) + zan.Luck;

                            int plusLuck;
                            int rPlusLuck = rand.Next(0, 100) + zan.Luck;

                            //determines plusLuck
                            if (rPlusLuck > 40 && rPlusLuck < 81)
                            {
                                plusLuck = rand.Next(1, 5);
                            }
                            else if (rPlusLuck > 80)
                            {
                                plusLuck = rand.Next(6, 10);
                            }
                            else
                            {
                                plusLuck = 0;
                            }

                            if (durability > 20)
                            {
                                id = "Tough " + id;
                            }
                            if (rPlusLuck > 80)
                            {
                                id = "Lucky " + id;
                            }
                            if (durability > 24 && plusLuck > 7)
                            {
                                id = "Grandma's Knitted Mittens";
                            }

                            value = durability + (plusLuck * 2);

                            Gloves g = new Gloves(tier, type, id, value, durability, plusLuck);

                            chestObjects.Add(g);
                        }

                        //tier 1 breastplate
                        else if (r4 > 66)
                        {
                            type = 4;
                            id = "Breastplate";
                            int value;

                            int durability = rand.Next(0, 60) + zan.Luck;

                            int plusHealth;
                            int rPlusHealth = rand.Next(0, 100) + zan.Luck;

                            bool hasPoisonResist;
                            int plusPoisonResist;
                            int rPlusPoisonResist = rand.Next(0, 100) + zan.Luck;

                            //determines plusHealth
                            if (rPlusHealth > 40 && rPlusHealth < 81)
                            {
                                plusHealth = rand.Next(1, 30);
                            }
                            else if (rPlusHealth > 80)
                            {
                                plusHealth = rand.Next(31, 50);
                            }
                            else
                            {
                                plusHealth = 0;
                            }

                            //determines plusPoisonResist
                            if (rPlusPoisonResist > 40 && rPlusPoisonResist < 81)
                            {
                                plusPoisonResist = rand.Next(1, 30);
                                hasPoisonResist = true;
                            }
                            else if (rPlusPoisonResist > 80)
                            {
                                plusPoisonResist = rand.Next(31, 50);
                                hasPoisonResist = true;
                            }
                            else
                            {
                                plusPoisonResist = 0;
                                hasPoisonResist = false;
                            }

                            if (durability > 40)
                            {
                                id = "Tough " + id;
                            }
                            if (rPlusHealth > 80)
                            {
                                id = "Healthy " + id;
                            }
                            if (rPlusPoisonResist > 80)
                            {
                                id = id + " of Wellness";
                            }
                            if (durability > 45 && plusHealth > 40 && plusPoisonResist > 40)
                            {
                                id = "Turtle Shell";
                            }

                            value = durability + plusHealth + plusPoisonResist;

                            Breastplate br = new Breastplate(tier, type, id, value, durability, hasPoisonResist, plusPoisonResist, plusHealth);

                            chestObjects.Add(br);
                        }
                    }
                    #endregion

                    #region tier 2 armor
                    //tier 2 armor
                    else if (r3 > 50 && r3 < 86)
                    {
                        tier = 2;

                        //tier 2 boots
                        if (r4 < 34)
                        {
                            type = 2;
                            id = "Treads";
                            int value;

                            int durability = rand.Next(31, 100) + zan.Luck;

                            int plusSpeed;
                            int rPlusSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines plusSpeed
                            if (rPlusSpeed > 40 && rPlusSpeed < 81)
                            {
                                plusSpeed = rand.Next(11, 15);
                            }
                            else if (rPlusSpeed > 80)
                            {
                                plusSpeed = rand.Next(15, 20);
                            }
                            else
                            {
                                plusSpeed = 0;
                            }

                            if (durability > 80)
                            {
                                id = "Resilient " + id;
                            }
                            if (rPlusSpeed > 80)
                            {
                                id = "Fleet " + id;
                            }
                            if (durability > 85 && plusSpeed > 17)
                            {
                                id = " ";
                            }

                            value = 2 * (durability + (plusSpeed * 2));

                            Boots b = new Boots(tier, type, id, value, durability, plusSpeed);

                            chestObjects.Add(b);
                        }

                        //tier 2 gloves
                        else if (r4 > 33 && r4 < 67)
                        {
                            type = 3;
                            id = "Gauntlets";
                            int value;

                            int durability = rand.Next(31, 100) + zan.Luck;

                            int plusLuck;
                            int rPlusLuck = rand.Next(0, 100) + zan.Luck;

                            //determines plusLuck
                            if (rPlusLuck > 40 && rPlusLuck < 81)
                            {
                                plusLuck = rand.Next(10, 15);
                            }
                            else if (rPlusLuck > 80)
                            {
                                plusLuck = rand.Next(15, 20);
                            }
                            else
                            {
                                plusLuck = 0;
                            }

                            if (durability > 80)
                            {
                                id = "Resilient " + id;
                            }
                            if (rPlusLuck > 80)
                            {
                                id = "Fortunate " + id;
                            }
                            if (durability > 85 && plusLuck > 17)
                            {
                                id = " ";
                            }

                            value = 2 * (durability + (plusLuck * 2));

                            Gloves g = new Gloves(tier, type, id, value, durability, plusLuck);

                            chestObjects.Add(g);
                        }

                        //tier 2 breastplate
                        else if (r4 > 66)
                        {
                            type = 4;
                            id = "Mail";
                            int value;

                            int durability = rand.Next(61, 200) + zan.Luck;

                            int plusHealth;
                            int rPlusHealth = rand.Next(0, 100) + zan.Luck;

                            bool hasPoisonResist;
                            int plusPoisonResist;
                            int rPlusPoisonResist = rand.Next(0, 100) + zan.Luck;

                            //determines plusHealth
                            if (rPlusHealth > 40 && rPlusHealth < 81)
                            {
                                plusHealth = rand.Next(51, 100);
                            }
                            else if (rPlusHealth > 80)
                            {
                                plusHealth = rand.Next(101, 150);
                            }
                            else
                            {
                                plusHealth = 0;
                            }

                            //determines plusPoisonResist
                            if (rPlusPoisonResist > 40 && rPlusPoisonResist < 81)
                            {
                                plusPoisonResist = rand.Next(51, 100);
                                hasPoisonResist = true;
                            }
                            else if (rPlusPoisonResist > 80)
                            {
                                plusPoisonResist = rand.Next(101, 150);
                                hasPoisonResist = true;
                            }
                            else
                            {
                                plusPoisonResist = 0;
                                hasPoisonResist = false;
                            }

                            if (durability > 150)
                            {
                                id = "Resilient " + id;
                            }
                            if (rPlusHealth > 80)
                            {
                                id = id + " of Fitness";
                            }
                            if (rPlusPoisonResist > 80)
                            {
                                id = "Immune " + id;
                            }
                            if (durability > 170 && plusHealth > 120 && plusPoisonResist > 120)
                            {
                                id = " ";
                            }

                            value = 2 * (durability + plusHealth + plusPoisonResist);

                            Breastplate br = new Breastplate(tier, type, id, value, durability, hasPoisonResist, plusPoisonResist, plusHealth);

                            chestObjects.Add(br);
                        }
                    }
                    #endregion

                    #region tier 3 armor
                    //tier 3 armor
                    else if (r3 > 85)
                    {
                        tier = 3;

                        //tier 3 boots
                        if (r4 < 34)
                        {
                            type = 2;
                            id = "Sabatons";
                            int value;

                            int durability = rand.Next(101, 250) + zan.Luck;

                            int plusSpeed;
                            int rPlusSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines plusSpeed
                            if (rPlusSpeed > 40 && rPlusSpeed < 81)
                            {
                                plusSpeed = rand.Next(21, 25);
                            }
                            else if (rPlusSpeed > 80)
                            {
                                plusSpeed = rand.Next(26, 30);
                            }
                            else
                            {
                                plusSpeed = 0;
                            }

                            if (durability > 200)
                            {
                                id = "Resolute " + id;
                            }
                            if (rPlusSpeed > 80)
                            {
                                id = "Sprinter " + id;
                            }
                            if (durability > 220 && plusSpeed > 27)
                            {
                                id = " ";
                            }

                            value = 3 * (durability + (plusSpeed * 2));

                            Boots b = new Boots(tier, type, id, value, durability, plusSpeed);

                            chestObjects.Add(b);
                        }

                        //tier 3 gloves
                        else if (r4 > 33 && r4 < 67)
                        {
                            type = 3;
                            id = "Vambraces";
                            int value;

                            int durability = rand.Next(101, 250) + zan.Luck;

                            int plusLuck;
                            int rPlusLuck = rand.Next(0, 100) + zan.Luck;

                            //determines plusLuck
                            if (rPlusLuck > 40 && rPlusLuck < 81)
                            {
                                plusLuck = rand.Next(21, 25);
                            }
                            else if (rPlusLuck > 80)
                            {
                                plusLuck = rand.Next(26, 30);
                            }
                            else
                            {
                                plusLuck = 0;
                            }

                            if (durability > 200)
                            {
                                id = "Resolute " + id;
                            }
                            if (rPlusLuck > 80)
                            {
                                id = id + " of the Jackal";
                            }
                            if (durability > 220 && plusLuck > 27)
                            {
                                id = " ";
                            }

                            value = 3 * (durability + (plusLuck * 2));

                            Gloves g = new Gloves(tier, type, id, value, durability, plusLuck);

                            chestObjects.Add(g);
                        }

                        //tier 3 breastplate
                        else if (r4 > 66)
                        {
                            type = 4;
                            id = "Hauberk";
                            int value;

                            int durability = rand.Next(201, 500) + zan.Luck;

                            int plusHealth;
                            int rPlusHealth = rand.Next(0, 100) + zan.Luck;

                            bool hasPoisonResist;
                            int plusPoisonResist;
                            int rPlusPoisonResist = rand.Next(0, 100) + zan.Luck;

                            //determines plusHealth
                            if (rPlusHealth > 40 && rPlusHealth < 81)
                            {
                                plusHealth = rand.Next(151, 250);
                            }
                            else if (rPlusHealth > 80)
                            {
                                plusHealth = rand.Next(251, 350);
                            }
                            else
                            {
                                plusHealth = 0;
                            }

                            //determines plusPoisonResist
                            if (rPlusPoisonResist > 40 && rPlusPoisonResist < 81)
                            {
                                plusPoisonResist = rand.Next(151, 250);
                                hasPoisonResist = true;
                            }
                            else if (rPlusPoisonResist > 80)
                            {
                                plusPoisonResist = rand.Next(251, 350);
                                hasPoisonResist = true;
                            }
                            else
                            {
                                plusPoisonResist = 0;
                                hasPoisonResist = false;
                            }

                            if (durability > 400)
                            {
                                id = "Resolute " + id;
                            }
                            if (rPlusHealth > 80)
                            {
                                id = "Vigorous " + id;
                            }
                            if (rPlusPoisonResist > 80)
                            {
                                id = id + " of the Pure";
                            }
                            if (durability > 440 && plusHealth > 300 && plusPoisonResist > 300)
                            {
                                id = " ";
                            }

                            value = 3 * (durability + plusHealth + plusPoisonResist);

                            Breastplate br = new Breastplate(tier, type, id, value, durability, hasPoisonResist, plusPoisonResist, plusHealth);

                            chestObjects.Add(br);
                        }
                    }
                    #endregion
                }
                #endregion

                #region weapon
                //item is weapon
                else if (r2 > 65 && r2 < 91)
                {
                    //determines tier
                    int r3 = rand.Next(0, 100) + zan.Luck;
                    int tier;

                    //determines type
                    int r4 = rand.Next(0, 100);

                    #region tier 1 weapon
                    //tier 1 weapon
                    if (r3 < 51)
                    {
                        tier = 1;

                        //tier 1 pistol
                        if (r4 < 21)
                        {
                            type = 5;
                            id = "Pistol";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0,100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0,100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0,100) + zan.Luck;

                            //determines damage
                            if(rDamage < 70)
                            {
                                damage = rand.Next(5,15) + (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                damage = rand.Next(16,25) + (int)(0.5 * zan.Luck);
                            }

                            //determines fireRate
                            if(rFireRate < 80)
                            {
                                fireRate = rand.Next(31,90) - zan.Luck;
                            }
                            else
                            {
                                fireRate = rand.Next(20,40) - zan.Luck;
                            }

                            //determines bulletSpeed
                            if(rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(100,150) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(150,250) + zan.Luck;
                            }

                            if (damage > 20 && fireRate < 25 && bulletSpeed > 200)
                            {
                                id = "The Beagle";
                            }

                            value = (int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate);

                            Pistol ps = new Pistol(tier,type,id,value,damage,fireRate,1,1,bulletSpeed);

                            chestObjects.Add(ps);
                        }
                        
                        //tier 1 SMG
                        else if (r4 > 20 && r4 < 36)
                        {
                            type = 6;
                            id = "SMG";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(1, 8) + (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                damage = rand.Next(9, 15) + (int)(0.5 * zan.Luck);
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(15, 40) - (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(10, 20) - (int)(0.5 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(100, 150) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(150, 250) + zan.Luck;
                            }

                            value = (int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate);

                            SMG smg = new SMG(tier, type, id, value, damage, fireRate, 1, 1, bulletSpeed);

                            chestObjects.Add(smg);
                        }

                        //tier 1 shotgun
                        else if (r4 > 35 && r4 < 51)
                        {
                            type = 7;
                            id = "Shotgun";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            int spreadBullets;
                            int rSpreadBullets = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(5, 15) + (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                damage = rand.Next(16, 30) + (int)(0.5 * zan.Luck);
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(81, 120) - (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(60, 80) - (int)(0.5 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(100, 150) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(150, 250) + zan.Luck;
                            }

                            //determines spreadBullets
                            if (rSpreadBullets < 70)
                            {
                                spreadBullets = rand.Next(2, 10) + (int)(0.15 * zan.Luck);
                            }
                            else
                            {
                                spreadBullets = rand.Next(11, 15) + (int)(0.15 * zan.Luck);
                            }

                            value = (int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate) + (50 * spreadBullets);

                            Shotgun shg = new Shotgun(tier, type, id, value, damage, fireRate, spreadBullets, 1, bulletSpeed);

                            chestObjects.Add(shg);
                        }

                        //tier 1 assault rifle
                        else if (r4 > 50 && r4 < 63)
                        {
                            type = 8;
                            id = "Assault Rifle";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(15, 25) + (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                damage = rand.Next(26, 35) + (int)(0.5 * zan.Luck);
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(31, 60) - (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(15, 30) - (int)(0.5 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(100, 150) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(150, 250) + zan.Luck;
                            }

                            value = (int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate);

                            AssaultRifle ar = new AssaultRifle(tier, type, id, value, damage, fireRate, 1, 1, bulletSpeed);

                            chestObjects.Add(ar);
                        }

                        //tier 1 plasma blaster
                        else if (r4 > 62 && r4 < 75)
                        {
                            type = 9;
                            id = "Plasma Blaster";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(15, 25) + (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                damage = rand.Next(26, 35) + (int)(0.5 * zan.Luck);
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(31, 60) - (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(15, 30) - (int)(0.5 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(100, 150) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(150, 250) + zan.Luck;
                            }

                            value = (int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate);

                            PlasmaBlaster pb = new PlasmaBlaster(tier, type, id, value, damage, fireRate, 1, 3, bulletSpeed);

                            chestObjects.Add(pb);
                        }

                        //tier 1 sniper rifle
                        else if (r4 > 74 && r4 < 86)
                        {
                            type = 10;
                            id = "Sniper Rifle";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(35, 60) + zan.Luck;
                            }
                            else
                            {
                                damage = rand.Next(61, 120) + zan.Luck;
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(120, 180) - (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(90, 120) - (int)(0.5 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(200, 300) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(301, 400) + zan.Luck;
                            }

                            value = (int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate);

                            SniperRifle sr = new SniperRifle(tier, type, id, value, damage, fireRate, 1, 1, bulletSpeed);

                            chestObjects.Add(sr);
                        }

                        //tier 1 rocket launcher
                        else if (r4 > 85 && r4 < 96)
                        {
                            type = 11;
                            id = "Rocket Launcher";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(50, 100) + zan.Luck;
                            }
                            else
                            {
                                damage = rand.Next(101, 200) + zan.Luck;
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(220,300) - (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(180,220) - (int)(0.5 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(50, 100) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(100, 150) + zan.Luck;
                            }

                            value = (int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate);

                            RocketLauncher rl = new RocketLauncher(tier, type, id, value, damage, fireRate, 1, 3, bulletSpeed);

                            chestObjects.Add(rl);
                        }

                        //tier 1 raygun
                        else if (r4 > 95)
                        {
                            type = 12;
                            id = "Raygun";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(35, 60) + zan.Luck;
                            }
                            else
                            {
                                damage = rand.Next(61, 120) + zan.Luck;
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(120, 180) - (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(90, 120) - (int)(0.5 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(350, 450) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(451, 500) + zan.Luck;
                            }

                            if (damage > 75 && fireRate < 105 && bulletSpeed > 475)
                            {
                                id = "Needle";
                            }

                            value = (int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate);

                            Raygun rg = new Raygun(tier, type, id, value, damage, fireRate, 1, 2, bulletSpeed);

                            chestObjects.Add(rg);
                        }
                    }
                    #endregion

                    #region tier 2 weapon
                    //tier 2 weapon
                    else if (r3 > 50 && r3 < 86)
                    {
                        tier = 2;

                        //tier 2 pistol
                        if (r4 < 21)
                        {
                            type = 5;
                            id = "Pistol";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(26, 75) + zan.Luck;
                            }
                            else
                            {
                                damage = rand.Next(76, 100) + zan.Luck;
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(31, 90) - (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(18, 36) - (int)(0.5 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(125, 175) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(175, 275) + zan.Luck;
                            }

                            value = 2* ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                            Pistol ps = new Pistol(tier, type, id, value, damage, fireRate, 1, 1, bulletSpeed);

                            chestObjects.Add(ps);
                        }

                        //tier 2 SMG
                        else if (r4 > 20 && r4 < 36)
                        {
                            type = 6;
                            id = "SMG";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(16, 50) + (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                damage = rand.Next(51, 80) + (int)(0.5 * zan.Luck);
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(13, 35) - (int)(0.25 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(9, 18) - (int)(0.25 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(125, 175) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(175, 275) + zan.Luck;
                            }

                            if (damage > 70 && fireRate < 12 && bulletSpeed > 225)
                            {
                                id = "The Scalpel";
                            }

                            value = 2 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                            SMG smg = new SMG(tier, type, id, value, damage, fireRate, 1, 1, bulletSpeed);

                            chestObjects.Add(smg);
                        }

                        //tier 2 shotgun
                        else if (r4 > 35 && r4 < 51)
                        {
                            type = 7;
                            id = "Shotgun";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            int spreadBullets;
                            int rSpreadBullets = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(31, 80) + zan.Luck;
                            }
                            else
                            {
                                damage = rand.Next(81, 120) + zan.Luck;
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(75, 110) - (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(57, 74) - (int)(0.5 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(125, 175) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(175, 275) + zan.Luck;
                            }

                            //determines spreadBullets
                            if (rSpreadBullets < 70)
                            {
                                spreadBullets = rand.Next(4, 12) + (int)(0.15 * zan.Luck);
                            }
                            else
                            {
                                spreadBullets = rand.Next(13, 17) + (int)(0.15 * zan.Luck);
                            }

                            value = 2 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate) + (50 * spreadBullets));

                            Shotgun shg = new Shotgun(tier, type, id, value, damage, fireRate, spreadBullets, 1, bulletSpeed);

                            chestObjects.Add(shg);
                        }

                        //tier 2 assault rifle
                        else if (r4 > 50 && r4 < 63)
                        {
                            type = 8;
                            id = "Assault Rifle";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(35, 90) + zan.Luck;
                            }
                            else
                            {
                                damage = rand.Next(91, 120) + zan.Luck;
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(27, 55) - (int)(0.25 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(13, 27) - (int)(0.25 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(125, 175) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(175, 275) + zan.Luck;
                            }

                            if (damage > 105 && fireRate < 17 && bulletSpeed > 225)
                            {
                                id = "Old Dog";
                            }

                            value = 2 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                            AssaultRifle ar = new AssaultRifle(tier, type, id, value, damage, fireRate, 1, 1, bulletSpeed);

                            chestObjects.Add(ar);
                        }

                        //tier 2 plasma blaster
                        else if (r4 > 62 && r4 < 75)
                        {
                            type = 9;
                            id = "Plasma Blaster";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(35, 90) + zan.Luck;
                            }
                            else
                            {
                                damage = rand.Next(91, 120) + zan.Luck;
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(27, 55) - (int)(0.25 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(13, 27) - (int)(0.25 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(125, 175) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(175, 275) + zan.Luck;
                            }

                            value = 2 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                            PlasmaBlaster pb = new PlasmaBlaster(tier, type, id, value, damage, fireRate, 1, 3, bulletSpeed);

                            chestObjects.Add(pb);
                        }

                        //tier 2 sniper rifle
                        else if (r4 > 74 && r4 < 86)
                        {
                            type = 10;
                            id = "Sniper Rifle";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(121, 220) + (2 * zan.Luck);
                            }
                            else
                            {
                                damage = rand.Next(221, 375) + (2 * zan.Luck);
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(110, 170) - (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(80, 110) - (int)(0.5 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(225, 325) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(325, 425) + zan.Luck;
                            }

                            value = 2 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                            SniperRifle sr = new SniperRifle(tier, type, id, value, damage, fireRate, 1, 1, bulletSpeed);

                            chestObjects.Add(sr);
                        }

                        //tier 2 rocket launcher
                        else if (r4 > 85 && r4 < 96)
                        {
                            type = 11;
                            id = "Rocket Launcher";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(201, 400) + (2 * zan.Luck);
                            }
                            else
                            {
                                damage = rand.Next(401, 800) + (2 * zan.Luck);
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(210, 290) - (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(170, 210) - (int)(0.5 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(60, 110) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(110, 160) + zan.Luck;
                            }

                            value = 2 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                            RocketLauncher rl = new RocketLauncher(tier, type, id, value, damage, fireRate, 1, 3, bulletSpeed);

                            chestObjects.Add(rl);
                        }

                        //tier 2 raygun
                        else if (r4 > 95)
                        {
                            type = 12;
                            id = "Raygun";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(121, 220) + (2 * zan.Luck);
                            }
                            else
                            {
                                damage = rand.Next(221, 375) + (2 * zan.Luck);
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(110, 170) - (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(80, 110) - (int)(0.5 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(375, 475) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(476, 525) + zan.Luck;
                            }

                            value = 2 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                            Raygun rg = new Raygun(tier, type, id, value, damage, fireRate, 1, 2, bulletSpeed);

                            chestObjects.Add(rg);
                        }
                    }
                    #endregion

                    #region tier 3 weapon
                    //tier 3 weapon
                    else if (r3 > 85)
                    {
                        tier = 3;

                        //tier 3 pistol
                        if (r4 < 21)
                        {
                            type = 5;
                            id = "Pistol";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(101, 200) + (2 * zan.Luck);
                            }
                            else
                            {
                                damage = rand.Next(201, 300) + (2 * zan.Luck);
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(29, 86) - (int)(0.25 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(16, 29) - (int)(0.25 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(150, 200) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(200, 300) + zan.Luck;
                            }

                            value = 3 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                            Pistol ps = new Pistol(tier, type, id, value, damage, fireRate, 1, 1, bulletSpeed);

                            chestObjects.Add(ps);
                        }

                        //tier 3 SMG
                        else if (r4 > 20 && r4 < 36)
                        {
                            type = 6;
                            id = "SMG";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(81, 170) + zan.Luck;
                            }
                            else
                            {
                                damage = rand.Next(171, 270) + zan.Luck;
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(11, 33) - (int)(0.15 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(8, 16) - (int)(0.15 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(150, 200) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(200, 300) + zan.Luck;
                            }

                            value = 3 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                            SMG smg = new SMG(tier, type, id, value, damage, fireRate, 1, 1, bulletSpeed);

                            chestObjects.Add(smg);
                        }

                        //tier 3 shotgun
                        else if (r4 > 35 && r4 < 51)
                        {
                            type = 7;
                            id = "Shotgun";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            int spreadBullets;
                            int rSpreadBullets = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(121, 220) + (2 * zan.Luck);
                            }
                            else
                            {
                                damage = rand.Next(221, 375) + (2 * zan.Luck);
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(70, 100) - (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(50, 70) - (int)(0.5 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(150, 200) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(200, 300) + zan.Luck;
                            }

                            //determines spreadBullets
                            if (rSpreadBullets < 70)
                            {
                                spreadBullets = rand.Next(7, 14) + (int)(0.15 * zan.Luck);
                            }
                            else
                            {
                                spreadBullets = rand.Next(14, 20) + (int)(0.15 * zan.Luck);
                            }

                            value = 3 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate) + (50 * spreadBullets));

                            Shotgun shg = new Shotgun(tier, type, id, value, damage, fireRate, spreadBullets, 1, bulletSpeed);

                            chestObjects.Add(shg);
                        }

                        //tier 3 assault rifle
                        else if (r4 > 50 && r4 < 63)
                        {
                            type = 8;
                            id = "Assault Rifle";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(121, 225) + (2 * zan.Luck);
                            }
                            else
                            {
                                damage = rand.Next(226, 400) + (2 * zan.Luck);
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(25, 50) - (int)(0.15 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(11, 25) - (int)(0.15 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(150, 200) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(200, 300) + zan.Luck;
                            }

                            value = 3 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                            AssaultRifle ar = new AssaultRifle(tier, type, id, value, damage, fireRate, 1, 1, bulletSpeed);

                            chestObjects.Add(ar);
                        }

                        //tier 3 plasma blaster
                        else if (r4 > 62 && r4 < 75)
                        {
                            type = 9;
                            id = "Plasma Blaster";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(121, 225) + (2 * zan.Luck);
                            }
                            else
                            {
                                damage = rand.Next(226, 400) + (2 * zan.Luck);
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(25, 50) - (int)(0.15 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(11, 25) - (int)(0.15 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(150, 200) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(200, 300) + zan.Luck;
                            }

                            if (damage > 330 && fireRate < 17 && bulletSpeed > 240)
                            {
                                id = "Thor's Hammer";
                            }

                            value = 3 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                            PlasmaBlaster pb = new PlasmaBlaster(tier, type, id, value, damage, fireRate, 1, 3, bulletSpeed);

                            chestObjects.Add(pb);
                        }

                        //tier 3 sniper rifle
                        else if (r4 > 74 && r4 < 86)
                        {
                            type = 10;
                            id = "Sniper Rifle";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(376, 550) + (4 * zan.Luck);
                            }
                            else
                            {
                                damage = rand.Next(551, 1000) + (4 * zan.Luck);
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(100, 160) - (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(70, 100) - (int)(0.5 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(275, 375) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(375, 475) + zan.Luck;
                            }

                            if (damage > 900 && fireRate < 85 && bulletSpeed > 425)
                            {
                                id = "Star-Hunter";
                            }

                            value = 3 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                            SniperRifle sr = new SniperRifle(tier, type, id, value, damage, fireRate, 1, 1, bulletSpeed);

                            chestObjects.Add(sr);
                        }

                        //tier 3 rocket launcher
                        else if (r4 > 85 && r4 < 96)
                        {
                            type = 11;
                            id = "Rocket Launcher";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(801, 1200) + (4 * zan.Luck);
                            }
                            else
                            {
                                damage = rand.Next(1201, 1800) + (4 * zan.Luck);
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(200, 280) - (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(160, 200) - (int)(0.5 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(75, 125) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(125, 175) + zan.Luck;
                            }

                            value = 3 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                            RocketLauncher rl = new RocketLauncher(tier, type, id, value, damage, fireRate, 1, 3, bulletSpeed);

                            chestObjects.Add(rl);
                        }

                        //tier 3 raygun
                        else if (r4 > 95)
                        {
                            type = 12;
                            id = "Raygun";
                            int value;

                            int damage;
                            int rDamage = rand.Next(0, 100) + zan.Luck;

                            int fireRate;
                            int rFireRate = rand.Next(0, 100) + zan.Luck;

                            int bulletSpeed;
                            int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                            //determines damage
                            if (rDamage < 70)
                            {
                                damage = rand.Next(376, 550) + (4 * zan.Luck);
                            }
                            else
                            {
                                damage = rand.Next(551, 1000) + (4 * zan.Luck);
                            }

                            //determines fireRate
                            if (rFireRate < 80)
                            {
                                fireRate = rand.Next(100, 160) - (int)(0.5 * zan.Luck);
                            }
                            else
                            {
                                fireRate = rand.Next(70, 100) - (int)(0.5 * zan.Luck);
                            }

                            //determines bulletSpeed
                            if (rBulletSpeed < 70)
                            {
                                bulletSpeed = rand.Next(425, 525) + zan.Luck;
                            }
                            else
                            {
                                bulletSpeed = rand.Next(526, 575) + zan.Luck;
                            }

                            value = 3 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                            Raygun rg = new Raygun(tier, type, id, value, damage, fireRate, 1, 2, bulletSpeed);

                            chestObjects.Add(rg);
                        }
                    }
                    #endregion
                }
                #endregion

                #region tier 3 armor
                //item is tier 3 armor
                else if (r2 > 90 && r2 < 96)
                {
                    int tier = 3;

                    //determines type
                    int r4 = rand.Next(0, 100);

                    //tier 3 boots
                    if (r4 < 34)
                    {
                        type = 2;
                        id = "Boots";
                        int value;

                        int durability = rand.Next(101, 250) + zan.Luck;

                        int plusSpeed;
                        int rPlusSpeed = rand.Next(0, 100) + zan.Luck;

                        //determines plusSpeed
                        if (rPlusSpeed > 40 && rPlusSpeed < 81)
                        {
                            plusSpeed = rand.Next(21, 25);
                        }
                        else if (rPlusSpeed > 80)
                        {
                            plusSpeed = rand.Next(26, 30);
                        }
                        else
                        {
                            plusSpeed = 0;
                        }

                        value = 3 * (durability + (plusSpeed * 2));

                        Boots b = new Boots(tier, type, id, value, durability, plusSpeed);

                        chestObjects.Add(b);
                    }

                    //tier 3 gloves
                    else if (r4 > 33 && r4 < 67)
                    {
                        type = 3;
                        id = "Gloves";
                        int value;

                        int durability = rand.Next(101, 250) + zan.Luck;

                        int plusLuck;
                        int rPlusLuck = rand.Next(0, 100) + zan.Luck;

                        //determines plusLuck
                        if (rPlusLuck > 40 && rPlusLuck < 81)
                        {
                            plusLuck = rand.Next(21, 25);
                        }
                        else if (rPlusLuck > 80)
                        {
                            plusLuck = rand.Next(26, 30);
                        }
                        else
                        {
                            plusLuck = 0;
                        }

                        value = 3 * (durability + (plusLuck * 2));

                        Gloves g = new Gloves(tier, type, id, value, durability, plusLuck);

                        chestObjects.Add(g);
                    }

                    //tier 3 breastplate
                    else if (r4 > 66)
                    {
                        type = 4;
                        id = "Breastplate";
                        int value;

                        int durability = rand.Next(201, 500) + zan.Luck;

                        int plusHealth;
                        int rPlusHealth = rand.Next(0, 100) + zan.Luck;

                        bool hasPoisonResist;
                        int plusPoisonResist;
                        int rPlusPoisonResist = rand.Next(0, 100) + zan.Luck;

                        //determines plusHealth
                        if (rPlusHealth > 40 && rPlusHealth < 81)
                        {
                            plusHealth = rand.Next(151, 250);
                        }
                        else if (rPlusHealth > 80)
                        {
                            plusHealth = rand.Next(251, 350);
                        }
                        else
                        {
                            plusHealth = 0;
                        }

                        //determines plusPoisonResist
                        if (rPlusPoisonResist > 40 && rPlusPoisonResist < 81)
                        {
                            plusPoisonResist = rand.Next(151, 250);
                            hasPoisonResist = true;
                        }
                        else if (rPlusPoisonResist > 80)
                        {
                            plusPoisonResist = rand.Next(251, 350);
                            hasPoisonResist = true;
                        }
                        else
                        {
                            plusPoisonResist = 0;
                            hasPoisonResist = false;
                        }

                        value = 3 * (durability + plusHealth + plusPoisonResist);

                        Breastplate br = new Breastplate(tier, type, id, value, durability, hasPoisonResist, plusPoisonResist, plusHealth);

                        chestObjects.Add(br);
                    }
                }
                #endregion

                #region tier 3 weapon
                //item is tier 3 weapon
                else if (r2 > 95 && r2 < 101)
                {

                    int tier = 3;

                    //determines type
                    int r4 = rand.Next(0, 100);
                    

                    //tier 3 pistol
                    if (r4 < 21)
                    {
                        type = 5;
                        id = "Pistol";
                        int value;

                        int damage;
                        int rDamage = rand.Next(0, 100) + zan.Luck;

                        int fireRate;
                        int rFireRate = rand.Next(0, 100) + zan.Luck;

                        int bulletSpeed;
                        int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                        //determines damage
                        if (rDamage < 70)
                        {
                            damage = rand.Next(101, 200) + (2 * zan.Luck);
                        }
                        else
                        {
                            damage = rand.Next(201, 300) + (2 * zan.Luck);
                        }

                        //determines fireRate
                        if (rFireRate < 80)
                        {
                            fireRate = rand.Next(29, 86) - (int)(0.25 * zan.Luck);
                        }
                        else
                        {
                            fireRate = rand.Next(16, 29) - (int)(0.25 * zan.Luck);
                        }

                        //determines bulletSpeed
                        if (rBulletSpeed < 70)
                        {
                            bulletSpeed = rand.Next(150, 200) + zan.Luck;
                        }
                        else
                        {
                            bulletSpeed = rand.Next(200, 300) + zan.Luck;
                        }

                        value = 3 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                        Pistol ps = new Pistol(tier, type, id, value, damage, fireRate, 1, 1, bulletSpeed);

                        chestObjects.Add(ps);
                    }

                    //tier 3 SMG
                    else if (r4 > 20 && r4 < 36)
                    {
                        type = 6;
                        id = "SMG";
                        int value;

                        int damage;
                        int rDamage = rand.Next(0, 100) + zan.Luck;

                        int fireRate;
                        int rFireRate = rand.Next(0, 100) + zan.Luck;

                        int bulletSpeed;
                        int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                        //determines damage
                        if (rDamage < 70)
                        {
                            damage = rand.Next(81, 170) + zan.Luck;
                        }
                        else
                        {
                            damage = rand.Next(171, 270) + zan.Luck;
                        }

                        //determines fireRate
                        if (rFireRate < 80)
                        {
                            fireRate = rand.Next(11, 33) - (int)(0.15 * zan.Luck);
                        }
                        else
                        {
                            fireRate = rand.Next(8, 16) - (int)(0.15 * zan.Luck);
                        }

                        //determines bulletSpeed
                        if (rBulletSpeed < 70)
                        {
                            bulletSpeed = rand.Next(150, 200) + zan.Luck;
                        }
                        else
                        {
                            bulletSpeed = rand.Next(200, 300) + zan.Luck;
                        }

                        value = 3 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                        SMG smg = new SMG(tier, type, id, value, damage, fireRate, 1, 1, bulletSpeed);

                        chestObjects.Add(smg);
                    }

                    //tier 3 shotgun
                    else if (r4 > 35 && r4 < 51)
                    {
                        type = 7;
                        id = "Shotgun";
                        int value;

                        int damage;
                        int rDamage = rand.Next(0, 100) + zan.Luck;

                        int fireRate;
                        int rFireRate = rand.Next(0, 100) + zan.Luck;

                        int bulletSpeed;
                        int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                        int spreadBullets;
                        int rSpreadBullets = rand.Next(0, 100) + zan.Luck;

                        //determines damage
                        if (rDamage < 70)
                        {
                            damage = rand.Next(121, 220) + (2 * zan.Luck);
                        }
                        else
                        {
                            damage = rand.Next(221, 375) + (2 * zan.Luck);
                        }

                        //determines fireRate
                        if (rFireRate < 80)
                        {
                            fireRate = rand.Next(70, 100) - (int)(0.5 * zan.Luck);
                        }
                        else
                        {
                            fireRate = rand.Next(50, 70) - (int)(0.5 * zan.Luck);
                        }

                        //determines bulletSpeed
                        if (rBulletSpeed < 70)
                        {
                            bulletSpeed = rand.Next(150, 200) + zan.Luck;
                        }
                        else
                        {
                            bulletSpeed = rand.Next(200, 300) + zan.Luck;
                        }

                        //determines spreadBullets
                        if (rSpreadBullets < 70)
                        {
                            spreadBullets = rand.Next(7, 14) + (int)(0.15 * zan.Luck);
                        }
                        else
                        {
                            spreadBullets = rand.Next(14, 20) + (int)(0.15 * zan.Luck);
                        }

                        value = 3 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate) + (50 * spreadBullets));

                        Shotgun shg = new Shotgun(tier, type, id, value, damage, fireRate, spreadBullets, 1, bulletSpeed);

                        chestObjects.Add(shg);
                    }

                    //tier 3 assault rifle
                    else if (r4 > 50 && r4 < 63)
                    {
                        type = 8;
                        id = "Assault Rifle";
                        int value;

                        int damage;
                        int rDamage = rand.Next(0, 100) + zan.Luck;

                        int fireRate;
                        int rFireRate = rand.Next(0, 100) + zan.Luck;

                        int bulletSpeed;
                        int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                        //determines damage
                        if (rDamage < 70)
                        {
                            damage = rand.Next(121, 225) + (2 * zan.Luck);
                        }
                        else
                        {
                            damage = rand.Next(226, 400) + (2 * zan.Luck);
                        }

                        //determines fireRate
                        if (rFireRate < 80)
                        {
                            fireRate = rand.Next(25, 50) - (int)(0.15 * zan.Luck);
                        }
                        else
                        {
                            fireRate = rand.Next(11, 25) - (int)(0.15 * zan.Luck);
                        }

                        //determines bulletSpeed
                        if (rBulletSpeed < 70)
                        {
                            bulletSpeed = rand.Next(150, 200) + zan.Luck;
                        }
                        else
                        {
                            bulletSpeed = rand.Next(200, 300) + zan.Luck;
                        }

                        value = 3 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                        AssaultRifle ar = new AssaultRifle(tier, type, id, value, damage, fireRate, 1, 1, bulletSpeed);

                        chestObjects.Add(ar);
                    }

                    //tier 3 plasma blaster
                    else if (r4 > 62 && r4 < 75)
                    {
                        type = 9;
                        id = "Plasma Blaster";
                        int value;

                        int damage;
                        int rDamage = rand.Next(0, 100) + zan.Luck;

                        int fireRate;
                        int rFireRate = rand.Next(0, 100) + zan.Luck;

                        int bulletSpeed;
                        int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                        //determines damage
                        if (rDamage < 70)
                        {
                            damage = rand.Next(121, 225) + (2 * zan.Luck);
                        }
                        else
                        {
                            damage = rand.Next(226, 400) + (2 * zan.Luck);
                        }

                        //determines fireRate
                        if (rFireRate < 80)
                        {
                            fireRate = rand.Next(25, 50) - (int)(0.15 * zan.Luck);
                        }
                        else
                        {
                            fireRate = rand.Next(11, 25) - (int)(0.15 * zan.Luck);
                        }

                        //determines bulletSpeed
                        if (rBulletSpeed < 70)
                        {
                            bulletSpeed = rand.Next(150, 200) + zan.Luck;
                        }
                        else
                        {
                            bulletSpeed = rand.Next(200, 300) + zan.Luck;
                        }

                        value = 3 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                        PlasmaBlaster pb = new PlasmaBlaster(tier, type, id, value, damage, fireRate, 1, 3, bulletSpeed);

                        chestObjects.Add(pb);
                    }

                    //tier 3 sniper rifle
                    else if (r4 > 74 && r4 < 86)
                    {
                        type = 10;
                        id = "Sniper Rifle";
                        int value;

                        int damage;
                        int rDamage = rand.Next(0, 100) + zan.Luck;

                        int fireRate;
                        int rFireRate = rand.Next(0, 100) + zan.Luck;

                        int bulletSpeed;
                        int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                        //determines damage
                        if (rDamage < 70)
                        {
                            damage = rand.Next(376, 550) + (4 * zan.Luck);
                        }
                        else
                        {
                            damage = rand.Next(551, 1000) + (4 * zan.Luck);
                        }

                        //determines fireRate
                        if (rFireRate < 80)
                        {
                            fireRate = rand.Next(100, 160) - (int)(0.5 * zan.Luck);
                        }
                        else
                        {
                            fireRate = rand.Next(70, 100) - (int)(0.5 * zan.Luck);
                        }

                        //determines bulletSpeed
                        if (rBulletSpeed < 70)
                        {
                            bulletSpeed = rand.Next(275, 375) + zan.Luck;
                        }
                        else
                        {
                            bulletSpeed = rand.Next(375, 475) + zan.Luck;
                        }

                        value = 3 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                        SniperRifle sr = new SniperRifle(tier, type, id, value, damage, fireRate, 1, 1, bulletSpeed);

                        chestObjects.Add(sr);
                    }

                    //tier 3 rocket launcher
                    else if (r4 > 85 && r4 < 96)
                    {
                        type = 11;
                        id = "Rocket Launcher";
                        int value;

                        int damage;
                        int rDamage = rand.Next(0, 100) + zan.Luck;

                        int fireRate;
                        int rFireRate = rand.Next(0, 100) + zan.Luck;

                        int bulletSpeed;
                        int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                        //determines damage
                        if (rDamage < 70)
                        {
                            damage = rand.Next(801, 1200) + (4 * zan.Luck);
                        }
                        else
                        {
                            damage = rand.Next(1201, 1800) + (4 * zan.Luck);
                        }

                        //determines fireRate
                        if (rFireRate < 80)
                        {
                            fireRate = rand.Next(200, 280) - (int)(0.5 * zan.Luck);
                        }
                        else
                        {
                            fireRate = rand.Next(160, 200) - (int)(0.5 * zan.Luck);
                        }

                        //determines bulletSpeed
                        if (rBulletSpeed < 70)
                        {
                            bulletSpeed = rand.Next(75, 125) + zan.Luck;
                        }
                        else
                        {
                            bulletSpeed = rand.Next(125, 175) + zan.Luck;
                        }

                        if (damage > 900 && fireRate < 85 && bulletSpeed > 425)
                        {
                            id = "Star-Hunter";
                        }

                        value = 3 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                        RocketLauncher rl = new RocketLauncher(tier, type, id, value, damage, fireRate, 1, 3, bulletSpeed);

                        chestObjects.Add(rl);
                    }

                    //tier 3 raygun
                    else if (r4 > 95)
                    {
                        type = 12;
                        id = "Raygun";
                        int value;

                        int damage;
                        int rDamage = rand.Next(0, 100) + zan.Luck;

                        int fireRate;
                        int rFireRate = rand.Next(0, 100) + zan.Luck;

                        int bulletSpeed;
                        int rBulletSpeed = rand.Next(0, 100) + zan.Luck;

                        //determines damage
                        if (rDamage < 70)
                        {
                            damage = rand.Next(376, 550) + (4 * zan.Luck);
                        }
                        else
                        {
                            damage = rand.Next(551, 1000) + (4 * zan.Luck);
                        }

                        //determines fireRate
                        if (rFireRate < 80)
                        {
                            fireRate = rand.Next(100, 160) - (int)(0.5 * zan.Luck);
                        }
                        else
                        {
                            fireRate = rand.Next(70, 100) - (int)(0.5 * zan.Luck);
                        }

                        //determines bulletSpeed
                        if (rBulletSpeed < 70)
                        {
                            bulletSpeed = rand.Next(425, 525) + zan.Luck;
                        }
                        else
                        {
                            bulletSpeed = rand.Next(526, 575) + zan.Luck;
                        }

                        value = 3 * ((int)(2 * damage) + (int)(0.5 * bulletSpeed) + (300 - fireRate));

                        Raygun rg = new Raygun(tier, type, id, value, damage, fireRate, 1, 2, bulletSpeed);

                        chestObjects.Add(rg);
                    }
                }
                #endregion */
                #endregion
            }

            return chestObjects;
        }
    }
}
