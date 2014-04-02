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
using Genna.GameObjects.Characters.NPC.ShopKeeper;
using Genna.GameObjects.Characters.NPC.Doctor;
using Genna.GameObjects.Characters.NPC;
using Genna.Camera;
using Genna.GameObjects.Enemies.Boss;

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

        public MusicHandler music;

        public static GameMode gameMode;
        public MainMenu menu;
        public SettingsMenu settings;

        public DefaultLevel testLevel;
        public GennaTown gennaTown;
        public ItemShop itemShop;
        public Clinic clinic;
        public Bridge nextLevel;
        public Cave cave;
        public BossLevel bossLevel;
        public List<Level> allLevels;
        GameOverScreen GOS;

        public static Zanaj zanaj;
        public ShopKeeper shopKeeper;
        public Doctor doctor;
        public Villager villager;

        public MapTransition mpTransit;

        private Level currentLevel;

        public bool PlayerLoaded;

        public Tile tile;
        public SpriteFont spriteFont;

        public static Texture2D gameOverScreen;

        KeyboardState keyState;
        KeyboardState prevKeyState;

        private UI gui;

        public GameTime theGameTime;

        public void Renew()
        {
            gameMode = GameMode.Menu;
            Zanaj.instance = null;
        }

        public enum GameMode
        {
            Menu,
            Playing,
            Paused,
            Inventory,
            GameOver, 
            Settings
            //TO BE ADDED LATER AS NEEDED
        }

        private GameMode prevMode;

        public GameMode _GameMode
        {
            get
            {
                return gameMode;
            }
            set
            {
                prevMode = gameMode;
                gameMode = value;
            }
        }

        public UI Gui
        {
            get { return gui; }
        }

        public bool levelChange;

        public Level CurrentLevel
        {
            get { return currentLevel; }
            set 
            {
                levelChange = true;
                bool fromItemShop = false;
                bool fromClinic = false;

                currentLevel = value;
                
                if (currentLevel == testLevel)
                {
                    zanaj.X = GraphicsDevice.Viewport.Width / 2;
                    zanaj.Y = GraphicsDevice.Viewport.Height / 2;
                }

                if (currentLevel == gennaTown)
                {
                    if (fromItemShop)
                    {
                        zanaj.X = 256;
                        zanaj.Y = 264;
                    }
                    else if (fromClinic)
                    {
                        
                    }
                    else
                    {
                        zanaj.X = GraphicsDevice.Viewport.Width / 2;
                        zanaj.Y = GraphicsDevice.Viewport.Height / 2 - 88;
                    }
                    fromItemShop = false;
                    fromClinic = false;
                    // Position is fine.
                }

                if (currentLevel == itemShop)
                {
                    fromItemShop = true;
                    zanaj.X = -338 + GraphicsDevice.Viewport.Width / 2;
                    zanaj.Y = -85 + GraphicsDevice.Viewport.Height / 2;
                }

                if (currentLevel == clinic)
                {
                    fromClinic = true;
                    zanaj.X = 145;
                    zanaj.Y = 224;
                }

                if (currentLevel == nextLevel)
                {
                    zanaj.X = 64;
                    zanaj.Y = 224;
                }

                if (currentLevel == cave)
                {
                    zanaj.X = 64;
                    zanaj.Y = 192;
                }

                if (currentLevel == bossLevel)
                {
                    zanaj.X = 64;
                    zanaj.Y = 400;
                }

                music.SetSong(int.MinValue, CurrentLevel);
            }
        }

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

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
            music = new MusicHandler(this);
            menu = new MainMenu(this);
            gameMode = GameMode.Menu;
            allLevels = new List<Level>();
            camera = new Camera2D(GraphicsDevice.Viewport);
            zanaj = Zanaj.getInstance(this, 35, this.GraphicsDevice.Viewport.Height - 65);
            shopKeeper = new ShopKeeper(this);
            doctor = new Doctor(this);
            zanaj = Zanaj.getInstance(this);
            villager = new Villager(this, "ROFL");
            settings = new SettingsMenu(this);
            gameOverScreen = Content.Load<Texture2D>("gos");
            GOS = new GameOverScreen(this);
            base.Initialize();
        }

        public void Initialize(int i)
        {
            // TODO: Add your initialization logic here
            menu = new MainMenu(this);
            gameMode = GameMode.Menu;
            camera = new Camera2D(GraphicsDevice.Viewport);
            shopKeeper = new ShopKeeper(this);
            doctor = new Doctor(this);
            villager = new Villager(this, "ROFL");
            settings = new SettingsMenu(this);
            gameOverScreen = Content.Load<Texture2D>("gos");
            GOS = new GameOverScreen(this);
            LoadContent();
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
            gameOverScreen = Content.Load<Texture2D>("Main_Menu/GameOver/GameOverScreen");

            gui = new UI(this, GraphicsDevice);

            menu.LoadContent(Content);

            testLevel = new DefaultLevel(this);
            gennaTown = new GennaTown(this);
            itemShop = new ItemShop(this, GraphicsDevice);
            clinic = new Clinic(this, GraphicsDevice);
            nextLevel = new Bridge(this);
            cave = new Cave(this);
            bossLevel = new BossLevel(this);
            bossLevel.Load();

            gennaTown.MapLoader(tile, Content);
            itemShop.MapLoader(tile, Content);
            clinic.MapLoader(tile, Content);
            nextLevel.MapLoader(tile, Content);
            cave.MapLoader(tile, Content);
            bossLevel.MapLoader(tile, Content);
            

            allLevels.Add(gennaTown);
            allLevels.Add(itemShop);
            allLevels.Add(clinic);
            allLevels.Add(nextLevel);
            allLevels.Add(cave);
            allLevels.Add(bossLevel);

            testLevel.LoadContent(Content);

            settings.LoadContent();

            currentLevel = testLevel;
            
            mpTransit = new MapTransition(this, allLevels);

            foreach (Level level in allLevels)
            {
                mpTransit.GetDoors(level.TileList);
            }

            gui.LoadContent(Content);

            Level._particleImg = Content.Load<Texture2D>("Main_Menu/particle");

            Boss.bossText = Content.Load<Texture2D>("Sprites/Enemies/Boss/Boss");
            // TODO: use this.Content to load your game content here
            levelChange = false;
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

            if (PressedOnlyOnce(Keys.OemTilde)||GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (PressedOnlyOnce(Keys.Escape) || GamePad.GetState(PlayerIndex.One).Buttons.Start == ButtonState.Pressed)
                gameMode = GameMode.Menu;

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
               Level.zanaj._inventory.Update();
            }
            if (gameMode == GameMode.GameOver)
            {
                GOS.Update();
            }
            if (gameMode == GameMode.Settings)
            {
                settings.Update();
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
            //GraphicsDevice.Clear(Color.Black);

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
                Level.zanaj._inventory.Draw(spriteBatch);
                spriteBatch.End();
            }
            else if (gameMode == GameMode.Settings)
            {
                spriteBatch.Begin();
                settings.Draw(spriteBatch);
                spriteBatch.End();
            }
            else if (gameMode == GameMode.GameOver)
            {
                GOS.Draw(spriteBatch);
            }

            if (levelChange)
            {
                //fade out

                music.Stop();
                music.SetSong();
                levelChange = false;
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
