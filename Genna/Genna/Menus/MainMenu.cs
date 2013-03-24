using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Genna.Menus
{
    public class MainMenu
    {
        Game1 game;
        Texture2D _menuImage;
        KeyboardState prevKeyState;
        KeyboardState keyState;
        Song titleTheme;
        bool songStart = false;
        SoundEffect gearTurn;

        private List<Texture2D> menuImages;

        public MenuMode _menuMode;

        public enum MenuMode
        {
            Continue,
            NewGame,
            LoadGame,
            Default,
            Settings,
            Quit
        }

        public MainMenu(Game1 sentGame)
        {
            _menuMode = MenuMode.Default;
            menuImages = new List<Texture2D>();
            game = sentGame;
        }

        public void LoadContent(ContentManager content)
        {
            menuImages.Add(content.Load<Texture2D>("Main_Menu/MenuStates/continue"));
            menuImages.Add(content.Load<Texture2D>("Main_Menu/MenuStates/newGame"));
            menuImages.Add(content.Load<Texture2D>("Main_Menu/MenuStates/load"));
            menuImages.Add(content.Load<Texture2D>("Main_Menu/MenuStates/settings"));
            menuImages.Add(content.Load<Texture2D>("Main_Menu/MenuStates/quit"));
            menuImages.Add(content.Load<Texture2D>("Main_Menu/MenuStates/default"));

            gearTurn = content.Load<SoundEffect>("Sounds/SFX/gearRotate");
            
            _menuImage = menuImages[5];

            titleTheme = content.Load<Song>(@"Sounds/Music/Genna's Gleam");
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

        public void Update(GameTime gameTime)
        {
            prevKeyState = keyState;
            keyState = Keyboard.GetState();

            if (!songStart)
            {
                MediaPlayer.Play(titleTheme);
                songStart = true;
                MediaPlayer.IsRepeating = true;
            }

            #region If Enter is Pressed
            if (PressedOnlyOnce(Keys.Enter))
            {
                if (_menuMode == MenuMode.Default)
                {
                    //MakeErrorrySound
                }
                else if (_menuMode == MenuMode.Continue)
                {
                    //Not Yet Implemented
                }
                else if (_menuMode == MenuMode.NewGame)
                {
                    game._GameMode = Game1.GameMode.Playing;
                    this.SoundOff();
                }
                else if (_menuMode == MenuMode.LoadGame)
                {
                    //Not Yet Implemented
                }
                else if (_menuMode == MenuMode.Settings)
                {
                    //Not Yet Implemented
                }
                else if (_menuMode == MenuMode.Quit)
                {
                    game.Exit();
                }
            }
            #endregion

            #region Up Scrolling
            else if (PressedOnlyOnce(Keys.S) || PressedOnlyOnce(Keys.Down) || PressedOnlyOnce(Keys.D) || PressedOnlyOnce(Keys.Right))
            {
                if (_menuMode == MenuMode.Default)
                {
                    _menuMode = MenuMode.Continue;
                    _menuImage = menuImages[0];
                    gearTurn.Play();
                }
                else if (_menuMode == MenuMode.Continue)
                {
                    _menuMode = MenuMode.NewGame;
                    _menuImage = menuImages[1];
                    gearTurn.Play();

                }
                else if (_menuMode == MenuMode.NewGame)
                {
                    _menuMode = MenuMode.LoadGame;
                    _menuImage = menuImages[2];
                    gearTurn.Play();
                }
                else if (_menuMode == MenuMode.LoadGame)
                {
                    _menuMode = MenuMode.Settings;
                    _menuImage = menuImages[3];
                    gearTurn.Play();
                }
                else if (_menuMode == MenuMode.Settings)
                {
                    _menuMode = MenuMode.Quit;
                    _menuImage = menuImages[4];
                }
                else if (_menuMode == MenuMode.Quit)
                {
                    _menuMode = MenuMode.Continue;
                    _menuImage = menuImages[0];
                    gearTurn.Play();
                }
            }
#endregion

            #region Down Scrolling
            else if (PressedOnlyOnce(Keys.W) || PressedOnlyOnce(Keys.Up) || PressedOnlyOnce(Keys.A) || PressedOnlyOnce(Keys.Left))
            {
                if (_menuMode == MenuMode.Default)
                {
                    _menuMode = MenuMode.Quit;
                    _menuImage = menuImages[4];
                    gearTurn.Play();
                }
                else if (_menuMode == MenuMode.Continue)
                {
                    _menuMode = MenuMode.Quit;
                    _menuImage = menuImages[4];
                    gearTurn.Play();
                }
                else if (_menuMode == MenuMode.NewGame)
                {
                    _menuMode = MenuMode.Continue;
                    _menuImage = menuImages[0];
                    gearTurn.Play();
                }
                else if (_menuMode == MenuMode.LoadGame)
                {
                    _menuMode = MenuMode.NewGame;
                    _menuImage = menuImages[1];
                    gearTurn.Play();
                }
                else if (_menuMode == MenuMode.Settings)
                {
                    _menuMode = MenuMode.LoadGame;
                    _menuImage = menuImages[2];
                    gearTurn.Play();
                }
                else if (_menuMode == MenuMode.Quit)
                {
                    _menuMode = MenuMode.Settings;
                    _menuImage = menuImages[3];
                }
            }
            #endregion
        }

        public void SoundOff()
        {
            MediaPlayer.IsRepeating = false;
            MediaPlayer.Stop();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_menuImage, new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height) , Color.White);
        }
    }
}
