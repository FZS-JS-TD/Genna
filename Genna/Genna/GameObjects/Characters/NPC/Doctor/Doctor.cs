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

namespace Genna.GameObjects.Characters.NPC.Doctor
{
    public class Doctor : NPC
    {
        // Texture and drawing
        private Texture2D spriteSheet;	// The single image with all of the animation frames

        // Animation
        private int frame;				// The current animation frame
        private double timeCounter;		// The amount of time that has passed
        private double fps;				// The speed of the animation
        private double timePerFrame;	// The amount of time (in fractional seconds) per frame
        private int _frameCount;
        private bool b;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game1">Game1 object</param>
        public Doctor(Game1 game1)
        {
            fps = 10;
            timePerFrame = 2.0 / fps;
            XSpeed = 0;
            PoisonResist = 0;
            game = game1;
            spriteSheet = game1.Content.Load<Texture2D>("Sprites/Characters/NPCs/Doctor/SpriteSheet");
            _frameCount = 4;
            Rect = new Rectangle(514, 224, 32, 64);
            b = false;
        }

        // Runs the animations and textbox
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

            if (Rect.Intersects(Level.zanaj.Rect) && Zanaj.getInstance().Health == Zanaj.getInstance().MaxHealth)
            {
                game.Gui.TextBox.Text = "Welcome to the GGC!";
                game.Gui.TextBox.SpeakerImage = game.Gui.TextBox.DoctorMugshot;
                b = false;
            }
            else if (Rect.Intersects(Level.zanaj.Rect) && Zanaj.getInstance().Health < Zanaj.getInstance().MaxHealth)
            {
                game.Gui.TextBox.SpeakerImage = game.Gui.TextBox.DoctorMugshot;
                if (!b)
                {
                    game.Gui.TextBox.Text = "Hello sir!  In need of some surgery?  Press \"E\" to be healed! (100 gold)";
                }
                if (Rect.Intersects(Level.zanaj.Rect) && Game.CurrentLevel.PressedOnlyOnce(Keys.E))
                {
                    if (Zanaj.getInstance()._inventory.MoneyAmount > 99)
                    {
                        game.Gui.TextBox.Text = "Thank you for visiting the Gennatown General Clinic!";
                        Zanaj.getInstance().Health = Zanaj.getInstance().MaxHealth;
                        Zanaj.getInstance()._inventory.MoneyAmount -= 100;
                    }
                    else
                    {
                        game.Gui.TextBox.Text = "You don't have enough money!!! >:(";
                        b = true;
                    }

                }
            }
            
            else
            {
                game.Gui.TextBox.Text = "";
                game.Gui.TextBox.SpeakerImage = null;
                b = false;
            }

            if (game.CurrentLevel.CurrentKeyState.IsKeyDown(Keys.E) && game.CurrentLevel.PrevKeyState.IsKeyUp(Keys.E))
            {
                // Reset zanaj's health?
            }
        }

        // Draws each frame
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                        spriteSheet,					// - The texture to draw
                        new Rectangle(Rect.X, Rect.Y, Rect.Width, Rect.Height / 2),
                        new Rectangle(					// - The "source" rectangle
                            (frame * 32) - 32,	//   - This rectangle specifies
                            0,		//	   where "inside" the texture
                            32,			//     to get pixels
                            32),			//   - We don't want to draw it all
                        Color.White);			    // - The color
        }
    }
}
