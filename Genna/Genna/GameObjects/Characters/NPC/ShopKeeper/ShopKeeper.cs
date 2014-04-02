using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using Genna.Levels;
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

namespace Genna.GameObjects.Characters.NPC.ShopKeeper
{
    public class ShopKeeper : NPC
    {
        // Texture and drawing
        private Texture2D spriteSheet;	// The single image with all of the animation frames

        // Animation
        private int frame;				// The current animation frame
        private double timeCounter;		// The amount of time that has passed
        private double fps;				// The speed of the animation
        private double timePerFrame;	// The amount of time (in fractional seconds) per frame
        private int _frameCount;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game1">Game1 object</param>
        public ShopKeeper(Game1 game1)
        {
            fps = 10;
            timePerFrame = 2.0 / fps;
            XSpeed = 0;
            PoisonResist = 0;
            game = game1;
            spriteSheet = game1.Content.Load<Texture2D>("Sprites/Characters/NPCs/ShopKeeper/SpriteSheet");
            _frameCount = 4;
            Rect = new Rectangle(448, 224, 32, 64);
        }

        // Runs animation and text
        public void Update()
        {
            // Handle animation timing
            // - Add to the time counter
            // - Check if we have enough "time" to advance the frame
            timeCounter += game.theGameTime.ElapsedGameTime.TotalSeconds;

            if (timeCounter >= timePerFrame)
            {
                frame += 1;						// Adjust the frame

                if (frame > _frameCount)	// Check the bounds
                {
                    frame = 1;					
                }

                timeCounter -= timePerFrame;	// Remove the time we "used"
            }

            if (Rect.Intersects(Level.zanaj.Rect))
            {
                game.Gui.TextBox.Text = "Hello traveler! Press \"E\" to gain a better weapon than the one you have equipped (" +
                    (Zanaj.getInstance()._EqWeapon.Value + (int)(0.5 * Zanaj.getInstance()._EqWeapon.Value)) + " gold)";
                game.Gui.TextBox.SpeakerImage = game.Gui.TextBox.ShopKeeperMugshot;
            }
            else
            {
                game.Gui.TextBox.Text = "";
                game.Gui.TextBox.SpeakerImage = null;
            }

            bool space = false;

            for (int y = 0; y < 12; y++)
            {
                if (Zanaj.getInstance()._inventory.InvDict.ContainsKey(new Vector2(y, 0)) == false)
                {
                    space = true;
                }
            }

            if (Rect.Intersects(Level.zanaj.Rect) && Game.CurrentLevel.PressedOnlyOnce(Keys.E)
                && Zanaj.getInstance()._inventory.MoneyAmount > ((Zanaj.getInstance()._EqWeapon.Value + (int)(0.5 * Zanaj.getInstance()._EqWeapon.Value)) - 1)
                && space == true)
            {
                Random rand = new Random();
                Weapon wep = new Weapon(Zanaj.getInstance()._EqWeapon);
                int statTypeRoll = rand.Next(0, 100) + Zanaj.getInstance().Luck;

                if((wep.Type > 4 && wep.Type < 7) || (wep.Type > 7 && wep.Type < 13))
                {
                    if(statTypeRoll > -1 && statTypeRoll < 34)
                    {
                        wep.Damage += (int)(wep.Damage * 0.1);
                    }
                    else if (statTypeRoll > 33 && statTypeRoll < 67)
                    {
                        wep.BulletSpeed += (int)(wep.BulletSpeed * 0.1);
                    }
                    else if (statTypeRoll > 66 && statTypeRoll < 100)
                    {
                        if (wep.FireRate > 7)
                        {
                            wep.FireRate -= (int)(wep.FireRate * 0.1);
                        }
                        else if (wep.FireRate < 8)
                        {
                            wep.Damage += (int)(wep.Damage * 0.1);
                            wep.BulletSpeed += (int)(wep.BulletSpeed * 0.1);
                        }
                    }
                    else if (statTypeRoll > 99)
                    {
                        wep.Damage += (int)(wep.Damage * 0.1);
                        wep.BulletSpeed += (int)(wep.BulletSpeed * 0.1);

                        if (wep.FireRate > 7)
                        {
                            wep.FireRate -= (int)(wep.FireRate * 0.1);
                        }
                        else if (wep.FireRate < 8)
                        {
                            wep.Damage += (int)(wep.Damage * 0.1);
                            wep.BulletSpeed += (int)(wep.BulletSpeed * 0.1);
                        }
                    }
                }
                else if(wep.Type == 7)
                {
                    if (statTypeRoll > -1 && statTypeRoll < 26)
                    {
                        wep.Damage += (int)(wep.Damage * 0.1);
                    }
                    else if (statTypeRoll > 25 && statTypeRoll < 51)
                    {
                        wep.BulletSpeed += (int)(wep.BulletSpeed * 0.1);
                    }
                    else if (statTypeRoll > 50 && statTypeRoll < 76)
                    {
                        if (wep.FireRate > 7)
                        {
                            wep.FireRate -= (int)(wep.FireRate * 0.1);
                        }
                        else if (wep.FireRate < 8)
                        {
                            wep.Damage += (int)(wep.Damage * 0.1);
                            wep.BulletSpeed += (int)(wep.BulletSpeed * 0.1);
                        }
                    }
                    else if (statTypeRoll > 75 && statTypeRoll < 100)
                    {
                        wep.SpreadBullets++;
                    }
                    else if (statTypeRoll > 99)
                    {
                        wep.Damage += (int)(wep.Damage * 0.1);
                        wep.BulletSpeed += (int)(wep.BulletSpeed * 0.1);
                        wep.SpreadBullets++;

                        if (wep.FireRate > 7)
                        {
                            wep.FireRate -= (int)(wep.FireRate * 0.1);
                        }
                        else if (wep.FireRate < 8)
                        {
                            wep.Damage += (int)(wep.Damage * 0.1);
                            wep.BulletSpeed += (int)(wep.BulletSpeed * 0.1);
                        }
                    }
                }

                wep.Value = Zanaj.getInstance()._EqWeapon.Value + (int)(0.5 * Zanaj.getInstance()._EqWeapon.Value);
                Zanaj.getInstance()._inventory.AddItem(wep);
                Zanaj.getInstance()._inventory.MoneyAmount -= wep.Value;
            }
        }

        // Draws each frame
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                        spriteSheet,					// - The texture to draw
                        new Rectangle(Rect.X,Rect.Y,Rect.Width, Rect.Height/2),
                        new Rectangle(					// - The "source" rectangle
                            (frame * 32) - 32,	//   - This rectangle specifies
                            0,		//	   where "inside" the texture
                            32,			//     to get pixels
                            32),			//   - We don't want to draw it all
                        Color.White);			    // - The color
        }
    }
}
