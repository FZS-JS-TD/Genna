using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Genna.GameObjects.GUI
{
    class GUI
    {
        private string directory = "GUI/";
        private Color color = Color.White;

        private Vector2 gearCenter;
        private float rotationAngle = 0;
        private int playerHealth = 100;
        private Vector2 gearScreenPos;
        private Vector2 barCenter1;
        private Vector2 barPosition1;
        private Vector2 barPosition2;
        private int resistance = 0;



        #region Texture Attributes
        private Texture2D healthGearTexture;
        private Texture2D panelTexture;
        private Texture2D resistanceTubeTexture;
        private Texture2D resistanceBarGreenTexture;
        private Texture2D resistanceBarBlackTexture;
        private Texture2D menuBoxTexture;
        private Texture2D mainMenuButtonTexture;
        private Texture2D settingsButtonTexture;
        private Texture2D quitButtonTexture;
        #endregion

        #region File Name Attributes
        private string healthGearFile = "Gear";
        private string panelFile = "Panel";
        private string resistanceTubeFile = "Tubes";
        private string resistanceBarGreenFile = "GreenBar";
        private string resistanceBarBlackFile = "BlackBar";
        private string menuBoxFile = "MenuBox";
        private string mainMenuButtonFile = "MainMenu";
        private string settingsButtonFile = "Settings";
        private string quitButtonFile = "SaveAndQuit";
        #endregion

        #region Rectangle Attributes
        private Rectangle healthGearRect;
        private Rectangle panelRect;
        private Rectangle resistanceTubeRect;
        private Rectangle resistanceBarGreenRect1;
        private Rectangle resistanceBarBlackRect1;
        private Rectangle resistanceBarGreenRect2;
        private Rectangle resistanceBarBlackRect2;
        private Rectangle menuBoxRect;
        private Rectangle mainMenuButtonRect;
        private Rectangle settingsButtonRect;
        private Rectangle quitButtonRect;
        #endregion

        #region Constructor
        public GUI(GraphicsDevice graphics)
        {
            panelRect = new Rectangle(0, graphics.Viewport.Height - 112, graphics.Viewport.Width, 112);
            healthGearRect = new Rectangle(-27, graphics.Viewport.Height - 206, 236, 235);
            resistanceTubeRect = new Rectangle(135, graphics.Viewport.Height - 90, 156, 96);
            resistanceBarGreenRect1 = new Rectangle(151, 542, 100, 23);
            resistanceBarGreenRect2 = new Rectangle(160, 572, 100, 23);
            resistanceBarBlackRect1 = new Rectangle(135, graphics.Viewport.Height - 90, 120, 25);
            menuBoxRect = new Rectangle(0, graphics.Viewport.Height - 90, 138, 89);
            mainMenuButtonRect = new Rectangle(7, graphics.Viewport.Height - 82, 127, 27);
            settingsButtonRect = new Rectangle(7, graphics.Viewport.Height - 56, 127, 27);
            quitButtonRect = new Rectangle(7, graphics.Viewport.Height - 30, 127, 27);
            gearCenter.X = healthGearRect.Width / 2;
            gearCenter.Y = healthGearRect.Height / 2;
            gearScreenPos.X = gearCenter.X - 27;
            gearScreenPos.Y = graphics.Viewport.Height - gearCenter.Y + 27;
            barCenter1 = Vector2.Zero;
            barPosition1.X = barCenter1.X + 150;
            barPosition1.Y = 540;
            barPosition2.X = barCenter1.X + 159;
            barPosition2.Y = 570;
        }
        #endregion

        #region Methods
        public void LoadContent(ContentManager Content)
        {
            healthGearTexture = Content.Load<Texture2D>(directory + healthGearFile);
            panelTexture = Content.Load<Texture2D>(directory + panelFile);
            resistanceTubeTexture = Content.Load<Texture2D>(directory + resistanceTubeFile);
            resistanceBarGreenTexture = Content.Load<Texture2D>(directory + resistanceBarGreenFile);
            resistanceBarBlackTexture = Content.Load<Texture2D>(directory + resistanceBarBlackFile);
            menuBoxTexture = Content.Load<Texture2D>(directory + menuBoxFile);
            mainMenuButtonTexture = Content.Load<Texture2D>(directory + mainMenuButtonFile);
            settingsButtonTexture = Content.Load<Texture2D>(directory + settingsButtonFile);
            quitButtonTexture = Content.Load<Texture2D>(directory + quitButtonFile);
        }

        public void Update()
        {
            rotationAngle = (playerHealth - 100) * (MathHelper.Pi / 180);
            Resistance();
            KeyboardState keyState = Keyboard.GetState();
            if (keyState.IsKeyDown(Keys.U))
            {
                resistance++;
            }
            else if (keyState.IsKeyDown(Keys.J))
            {
                resistance--;
            }

            if (keyState.IsKeyDown(Keys.O))
            {
                playerHealth++;
            }
            else if (keyState.IsKeyDown(Keys.P))
            {
                playerHealth--;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthGearTexture, gearScreenPos, null, color, rotationAngle, gearCenter, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(panelTexture, panelRect, color);
            spriteBatch.Draw(resistanceBarBlackTexture, barPosition1, null, color, (-15 * (MathHelper.Pi / 180)), barCenter1, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(resistanceBarBlackTexture, barPosition2, null, color, (-15 * (MathHelper.Pi / 180)), barCenter1, 1.0f, SpriteEffects.None, 0f);
            spriteBatch.Draw(resistanceBarGreenTexture, resistanceBarGreenRect2, null, color, (-15 * (MathHelper.Pi / 180)), barCenter1, SpriteEffects.None, 0f);
            spriteBatch.Draw(resistanceBarGreenTexture, resistanceBarGreenRect1, null, color, (-15 * (MathHelper.Pi / 180)), barCenter1, SpriteEffects.None, 0f);
            spriteBatch.Draw(resistanceTubeTexture, resistanceTubeRect, color);
            spriteBatch.Draw(menuBoxTexture, menuBoxRect, color);
            spriteBatch.Draw(mainMenuButtonTexture, mainMenuButtonRect, color);
            spriteBatch.Draw(settingsButtonTexture, settingsButtonRect, color);
            spriteBatch.Draw(quitButtonTexture, quitButtonRect, color);
        }

        public void Resistance(/*int resistance*/)
        {
            if (resistance > 100)
            {
                resistanceBarGreenRect1.Width = 100;
                resistanceBarGreenRect2.Width = 100;
                resistance = 100;
            }
            else if (resistance > 50 && resistance <= 100)
            {
                resistanceBarGreenRect1.Width = 100;
                resistanceBarGreenRect2.Width = (resistance * 2) - 100;
            }
            else if (resistance <= 50 && resistance >= 0)
            {
                resistanceBarGreenRect1.Width = resistance * 2;
                resistanceBarGreenRect2.Width = 0;
            }
            else
            {
                resistanceBarGreenRect2.Width = 0;
                resistanceBarGreenRect1.Width = 0;
                resistance = 0;
            }
        }
        #endregion
    }
}