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

using Genna.Items;
using Genna.GameObjects.Chests;
using Genna.GameObjects.Chests.RandomChest;
using Genna.GameObjects.Chests.SpecialChest;
using Genna.Items.Moneys;
using Genna.Items.Armors;
using Genna.Items.Weapons;
using Genna.GameObjects.Characters;
using Genna.GameObjects.Characters.Players;
using Genna.GameObjects.Characters.Players.Zanaj_Akari;

namespace Genna.Levels
{
    //Forrest
    public class DefaultLevel : Level
    {
        Game1 game;
        private KeyboardState prevKeyState;
        private KeyboardState keyState;

        List<Item> testWeps;
        List<RandomChest> rndChests;

        Song _backgroundSong;
        Song _townSong;
        Song _shopSong;
        Song _infirmaSong;
        Song _happySong;

        Rectangle _currentBox;

        Rectangle _townBox;
        Rectangle _shopBox;
        Rectangle _infirmaBox;

        RandomItem ri;
        RandomChest rc1;
        RandomChest rc2;
        RandomChest rc3;

        bool _songStart = false;
        SoundEffect _step;

        List<Particle> particles;
        Texture2D _particleImg;
        Texture2D _backgroundImage;

        SpriteFont spriteFont;

        public SongMode _songMode;

        public enum SongMode
        {
            Town,
            Shop,
            Infirma,
            Happy
        }

        public void Pause()
        {
            game._GameMode = Game1.GameMode.Menu;
        }

        public override KeyboardState PrevKeyState
        {
            get
            {
                return prevKeyState;
            }
            set
            {
                prevKeyState = value;
            }
        }

        public override KeyboardState CurrentKeyState
        {
            get
            {
                return keyState;
            }
            set
            {
                prevKeyState = value;
            }
        }

        public DefaultLevel(Game1 sentGame)
        {
            _songMode = SongMode.Town;
            game = sentGame;
            zanaj = sentGame.Zanaj;
            zanaj.X = sentGame.GraphicsDevice.Viewport.Width / 2;
            zanaj.Y = sentGame.GraphicsDevice.Viewport.Height / 2;

            testWeps = new List<Item>();
            ri = new RandomItem(zanaj);
            rndChests = new List<RandomChest>();

            for (int k = 0; k < 3; k++)
            {
                rndChests.Add(new RandomChest(zanaj));
                rndChests[k].X = (50 * k);
                rndChests[k].Y = zanaj.Y;
            }

            for (int j = 0; j < 14; j++)
            {
                testWeps.Add(ri.AddRandomItem());
                testWeps[j].X = 50 * j;
                testWeps[j].Y = zanaj.Y;
            }

            spriteFont = game.Content.Load<SpriteFont>("Fonts/itemOverload");

            _townBox = new Rectangle(0, 0, 150, game.GraphicsDevice.Viewport.Height);
            _shopBox = new Rectangle(283, 0, 250, game.GraphicsDevice.Viewport.Height);
            _infirmaBox = new Rectangle(565, 0, game.GraphicsDevice.Viewport.Width - 564, game.GraphicsDevice.Viewport.Height);

            _backgroundImage = game.Content.Load<Texture2D>(@"Main_Menu\Genna Symbol");

            _currentBox = _townBox;
            // TODO: Add your initialization logic here
            particles = new List<Particle>();

            Random rand = new Random();

            int i = 0;
            while (i < 300)
            {
                int xSpd = rand.Next(5) - 3;
                int ySpd = rand.Next(5) - 3;

                Particle p = new Particle(xSpd, ySpd, sentGame);
                p.Max = 0;
                p.Min = 0;
                p.Rect = new Rectangle(rand.Next(sentGame.GraphicsDevice.Viewport.Width), rand.Next(sentGame.GraphicsDevice.Viewport.Height), 48, 48);

                particles.Add(p);
                i++;
            }
        }

        public void LoadContent(ContentManager content)
        {
            _step = content.Load<SoundEffect>("Sounds/SFX/gearRotate");

            _townSong = content.Load<Song>(@"Sounds/Music/gennaTown");
            _shopSong = content.Load<Song>(@"Sounds/Music/shop");
            _infirmaSong = content.Load<Song>(@"Sounds/Music/enterTheInfirma");
            _happySong = content.Load<Song>(@"Sounds/Music/light");
            _particleImg = game.Content.Load<Texture2D>("Main_Menu/particle");
            // TODO: use this.Content to load your game content here
        }

        public override bool PressedOnlyOnce(Keys key)
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

        public override void Update(GameTime gameTime)
        {
            prevKeyState = keyState;
            keyState = Keyboard.GetState();

            if (!_songStart)
            {
                if (_backgroundSong == null)
                {
                    _backgroundSong = _townSong;
                    _songMode = SongMode.Town;
                }

                SoundOn(_backgroundSong);
            }

            if (_townBox.Intersects(zanaj.Rect) && _songMode != SongMode.Town)
            {
                SoundOff();
                _backgroundSong = _townSong;
                _songMode = SongMode.Town;
            }

            if (_shopBox.Intersects(zanaj.Rect) && _songMode != SongMode.Shop)
            {
                SoundOff();
                _backgroundSong = _shopSong;
                _songMode = SongMode.Shop;
            }

            if (_infirmaBox.Intersects(zanaj.Rect) && _songMode != SongMode.Infirma)
            {
                SoundOff();
                _backgroundSong = _infirmaSong;
                _songMode = SongMode.Infirma;
            }

            if (_songMode != SongMode.Happy && !(_townBox.Intersects(zanaj.Rect)) && !(_infirmaBox.Intersects(zanaj.Rect)) && !(_shopBox.Intersects(zanaj.Rect)))
            {
                SoundOff();
                _backgroundSong = _happySong;
                _songMode = SongMode.Happy;
            }

            foreach (Particle p in particles)
            {
                p.Move();
                p.Screenwrap(game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height);
            }

            zanaj.Update(this);

            if (PressedOnlyOnce(Keys.Q))
            {
                Pause();
            }

            Console.WriteLine("Zanaj: {0},{1}    World: {2},{3}\nZanSpeed: {4},{5}    Jumping?: {6}", zanaj.X, zanaj.Y, X, Y, zanaj.XSpeed, zanaj.YSpeed, zanaj.Jumping);
        }

        public void SoundOff()
        {
            MediaPlayer.IsRepeating = false;
            _songStart = false;
            MediaPlayer.Stop();
        }

        public void SoundOn(Song song)
        {
            MediaPlayer.Play(song);
            _songStart = true;
            MediaPlayer.IsRepeating = true;
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(_backgroundImage,
                             new Rectangle(0, 0, game.GraphicsDevice.Viewport.Width, game.GraphicsDevice.Viewport.Height),
                             Color.White);

            foreach (RandomChest rc in rndChests)
            {
                RandomChest chest = rc;
                Rectangle chestRect = new Rectangle(rc.X, rc.Y, rc.Width, rc.Height);

                if (chestRect.Intersects(zanaj.rect) && chest.IsActive)
                {
                    spriteBatch.DrawString(spriteFont, "Press 'O' to Open Chest", new Vector2(chestRect.X, chestRect.Y), Color.White);
                }

                if (chest.IsActive)
                {
                    spriteBatch.Draw(chest.Image, new Rectangle(chestRect.X, chestRect.Y, 32, 32), Color.White);
                }
            }

            foreach (Item w in testWeps)
            {
                Item testWep = w;
                Rectangle testWepRect = testWep.Rect;

                if (testWepRect.Intersects(zanaj.rect) && testWep.IsActive)
                {
                    bool b = zanaj._inventory.AddItem(testWep);

                    if (!b)
                    {
                        spriteBatch.DrawString(spriteFont, "Too Many Items of This Type", new Vector2(testWepRect.X, testWepRect.Y), Color.White);
                    }
                    else
                    {
                        testWep.IsActive = false;
                    }
                }

                if (testWep.IsActive)
                {
                    spriteBatch.Draw(testWep.Image, new Rectangle(testWep.Rect.X, testWep.Rect.Y, 50, 50), Color.White);
                }
            }

            zanaj.Draw(spriteBatch);
        }
    }
}