using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

// Built in Requirements
using Genna.Menus;
using Genna.Levels;
using Genna.Levels.Town;
using Genna.Levels.Town.Shops;
using Genna.Levels.Outside;
using Genna.GameObjects.GUI;
using Genna.GameObjects;
using Genna.GameObjects.Characters;
using Genna.GameObjects.Characters.Players;
using Genna.GameObjects.Characters.Players.Zanaj_Akari;
using Genna.Camera;

namespace Genna
{
    /// <summary>
    /// This is the main type for your game
    /// 
    /// Forrest
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public Camera2D camera;

        public GameMode gameMode;
        public MainMenu menu;
        public SettingsMenu settings;

        public DefaultLevel testLevel;
        public GennaTown gennaTown;
        public ItemShop itemShop;
        public NextLevel nextLevel;
        public List<Level> allLevels;
        public Zanaj zanaj;

        public MapTransition mpTransit;

        public Level currentLevel;

        public bool PlayerLoaded;

        public Tile tile;
        public SpriteFont spriteFont;

        KeyboardState keyState;
        KeyboardState prevKeyState;

        private UI gui;

        public GameTime theGameTime;

        public enum GameMode
        {
            Menu,
            Playing,
            Paused,
            Inventory
            //TO BE ADDED LATER AS NEEDED
        }

        public GameMode _GameMode
        {
            get
            {
                return gameMode;
            }
            set
            {
                gameMode = value;
            }
        }

        public Level CurrentLevel
        {
            get { return currentLevel; }
            set
            {
                currentLevel = value;

                if (currentLevel == testLevel)
                {
                    zanaj.X = GraphicsDevice.Viewport.Width / 2;
                    zanaj.Y = GraphicsDevice.Viewport.Height / 2;
                }

                if (currentLevel == gennaTown)
                {
                    zanaj.X = GraphicsDevice.Viewport.Width / 2;
                    zanaj.Y = GraphicsDevice.Viewport.Height / 2 - 88;
                    // Position is fine.
                }

                if (currentLevel == itemShop)
                {
                    zanaj.X = -200 + GraphicsDevice.Viewport.Width / 2;
                    zanaj.Y = -85 + GraphicsDevice.Viewport.Height / 2;

                }
            }

        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Contenqt";

            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferMultiSampling = false;
        }

        public Zanaj Zanaj
        {
            get { return zanaj; }
            set { zanaj = value; }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            menu = new MainMenu(this);
            gameMode = GameMode.Menu;
            allLevels = new List<Level>();
            camera = new Camera2D(GraphicsDevice.Viewport);
            
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            tile = new Tile();
            tile.Load(Content);

            spriteFont = Content.Load<SpriteFont>("SpriteFont1");
            //testTextBox = new TextBox("This is a test", spriteFont, 20, 20);


            gui = new UI(this, GraphicsDevice);

            menu.LoadContent(Content);

            zanaj = new Zanaj(this, 35, this.GraphicsDevice.Viewport.Height - 65);

            testLevel = new DefaultLevel(this);
            gennaTown = new GennaTown(this);
            itemShop = new ItemShop(this, GraphicsDevice);
            nextLevel = new NextLevel(this);

            gennaTown.MapLoader(tile, Content);
            itemShop.MapLoader(tile, Content);
            nextLevel.MapLoader(tile, Content);

            allLevels.Add(gennaTown);
            allLevels.Add(itemShop);
            allLevels.Add(nextLevel);

            testLevel.LoadContent(Content);

            currentLevel = testLevel;

            mpTransit = new MapTransition(this, allLevels);

            foreach (Level level in allLevels)
            {
                mpTransit.GetDoors(level.TileList);
            }

            //testTextBox.LoadContent(Content);

            gui.LoadContent(Content);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        public bool PressedOnlyOnce(Keys key)
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

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            theGameTime = gameTime;
            // Allows the game to exit
            keyState = Keyboard.GetState();
            currentLevel.CurrentKeyState = keyState;

            if (PressedOnlyOnce(Keys.OemTilde) || GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (gameMode == GameMode.Menu)
            {
                menu.Update(gameTime);
            }

            if (gameMode == GameMode.Playing)
            {
                currentLevel.Update(gameTime);
                mpTransit.Update();
                gui.Update();
            }
            if (gameMode == GameMode.Inventory)
            {
                currentLevel.zanaj._inventory.Update();
            }

            // TODO: Add your update logic here
            prevKeyState = keyState;

            camera.Update(gameTime, this);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            if (gameMode == GameMode.Menu)
            {
                spriteBatch.Begin();
                menu.Draw(spriteBatch);
                spriteBatch.End();
            }
            else if (gameMode == GameMode.Playing)
            {
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                                  null, null, null, null,
                                  camera.transform);
                currentLevel.Draw(spriteBatch, gameTime);
                //testTextBox.Draw(spriteBatch);
                spriteBatch.End();
                spriteBatch.Begin();
                gui.Draw(spriteBatch);
                spriteBatch.End();
            }
            else if (gameMode == GameMode.Inventory)
            {
                spriteBatch.Begin();
                currentLevel.zanaj._inventory.Draw(spriteBatch);
                spriteBatch.End();
            }

            /* spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
             * null, null, null, null,
             * camera.transform);
             * 
             * 
             * */

            base.Draw(gameTime);
        }
    }
}