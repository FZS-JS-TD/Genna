﻿using System;
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

        List<Particle> particles;
        Texture2D particleImg;

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

            // TODO: Add your initialization logic here
            particles = new List<Particle>();

            Random rand = new Random();

            int i = 0;
            while (i < 750)
            {
                int xSpd = rand.Next(11) - 3;
                int ySpd = rand.Next(11) - 3;

                int dimensions = 96 + rand.Next(97);

                Particle p = new Particle(xSpd, ySpd, sentGame);
                p.Rect = new Rectangle(rand.Next(sentGame.GraphicsDevice.Viewport.Width), rand.Next(sentGame.GraphicsDevice.Viewport.Height), dimensions, dimensions);

                p.Color = new Color(18 + rand.Next(10), 3 + rand.Next(5), 25 + rand.Next(20));

                particles.Add(p);
                i++;
            }
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

            particleImg = game.Content.Load<Texture2D>("Main_Menu/particle");
            // TODO: use this.Content to load your game content here
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

            foreach (Particle p in particles)
            {
                p.Move();
                p.Screenwrap(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            }
            // TODO: Add your update logic here

            if (PressedOnlyOnce(Keys.Escape))
            {
                this.SoundOff();
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
                    this.SoundOff();
                    game._GameMode = Game1.GameMode.Playing;
                    game.CurrentLevel = game.gennaTown;
                }
                else if (_menuMode == MenuMode.NewGame)
                {
                    this.SoundOff();
                    game._GameMode = Game1.GameMode.Playing;
                    game.CurrentLevel = game.testLevel;
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
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_menuImage, new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height), Color.White);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);

            foreach (Particle p in particles)
            {
                spriteBatch.Draw(particleImg, p.Rect, p.Color/*new Color(1.0f, 0.5f, 0.25f)*/);
            }
        }
    }
}