using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using Genna.Levels;
using Genna.GameObjects.Characters.Players.Zanaj_Akari;

namespace Genna.GameObjects.Characters.NPC
{
    public class Villager : NPC
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
        /// <param name="text">quote</param>
        public Villager(Game1 game1, string text)
        {
            fps = 10;
            timePerFrame = 2.0 / fps;
            XSpeed = 0;
            PoisonResist = 0;
            game = game1;
            spriteSheet = game1.Content.Load<Texture2D>("Sprites/Characters/NPCs/ShopKeeper/SpriteSheet");
            _frameCount = 4;
            Rect = new Rectangle(448, 288, 32, 64);
        }

        // Runs the animation and text
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
                game.Gui.TextBox.Text = "Hi there! You can use coal to heal or restore infirma resistance with \'H\' and \'M\' respectively.";
                game.Gui.TextBox.SpeakerImage = game.Gui.TextBox.ShopKeeperMugshot;
            }
            else
            {
                game.Gui.TextBox.Text = "";
                game.Gui.TextBox.SpeakerImage = null;
            }
        }

        // Draws each frame
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                        spriteSheet,					// - The texture to draw
                        new Rectangle(Rect.X,Rect.Y,Rect.Width, Rect.Height),
                        new Rectangle(					// - The "source" rectangle
                            (frame * 32) - 32,	//   - This rectangle specifies
                            0,		//	   where "inside" the texture
                            32,			//     to get pixels
                            64),			//   - We don't want to draw it all
                        Color.White);			    // - The color
        }
    }
}
