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

using Genna.GameObjects.Characters;
using Genna.GameObjects.Characters.Players;
using Genna.Items;
using Genna.Items.Moneys;
using Genna.Items.Armors;
using Genna.Items.Weapons;
using Genna;
using Genna.Levels;
using Genna.LoadSave;

namespace Genna.GameObjects.Characters.Players.Zanaj_Akari
{ 
    /// <summary>
    /// Main character's class, 
    /// 
    /// Jordan, Forrest
    /// </summary>
    public class Zanaj : Player
    {
        public ZanState _state;
        public int shootTimer;

        // Texture and drawing
        Texture2D spriteSheet;	// The single image with all of the animation frames
        
        // Animation
        int frame;				// The current animation frame
        double timeCounter;		// The amount of time that has passed
        double fps;				// The speed of the animation
        double timePerFrame;	// The amount of time (in fractional seconds) per frame
        int _frameCount;

        // Constants for "source" rectangle (inside the image)
        const int BACK_WALK_FRAME_COUNT = 9;
        const int FACE_WALK_FRAME_COUNT = 9;
        const int LEFT_WALK_FRAME_COUNT = 9;
        const int RIGHT_WALK_FRAME_COUNT = 9;
        const int BACK_IDLE_FRAME_COUNT = 9;
        const int FACE_IDLE_FRAME_COUNT = 9;
        const int LEFT_IDLE_FRAME_COUNT = 9;
        const int RIGHT_IDLE_FRAME_COUNT = 9;
        
        public List<Bullet> _bullets;
        public Level currentLevel;
        private const int BASE_SPEED = 2;
        private const int BASE_HEALTH = 100;
        protected int maxHealth;
        protected int maxSpeed;
        public bool Jumping;
        public static bool isDead = false;

        protected Weapon _eqWeapon;
        protected Breastplate _eqBreastplate;
        public Vector2 _eqBreastplateIndex;
        protected Gloves _eqGloves;
        protected Boots _eqBoots;

        public Inventory _inventory;
        public static Zanaj instance = null;

        public static FileHandler fileHand = new FileHandler();

        public int _CombinedDurability
        {
            get { return (_eqBoots.Durability + _eqGloves.Durability + _eqBreastplate.Durability); }
        }
        public Weapon _EqWeapon
        {
            get { return _eqWeapon; }
            set { _eqWeapon = value; }
        }
        public Breastplate _EqBreastplate
        {
            get { return _eqBreastplate; }
            set { _eqBreastplate = value; }
        }
        public Gloves _EqGloves
        {
            get { return _eqGloves; }
            set { _eqGloves = value; }
        }
        public Boots _EqBoots
        {
            get { return _eqBoots; }
            set { _eqBoots = value; }
        }
        public int MaxHealth
        {
            get { return maxHealth; }
            set { maxHealth = value; }
        }
        public int MaxSpeed
        {
            get { return maxSpeed; }
            set { maxSpeed = value; }
        }

        public enum ZanState
        {
            IdleBack,
            IdleFace,
            IdleLeft,
            IdleRight,
            MoveBack,
            MoveFace,
            MoveLeft,
            MoveRight
        }

        public static Zanaj getInstance(Game1 game1 = null, int x = int.MaxValue, int y = int.MaxValue)
        {
            if (instance == null || Zanaj.isDead)
            {
                if (!(game1 == null && x == int.MaxValue && y == int.MaxValue))
                    instance = new Zanaj(game1, x, y);
                else
                {
                    instance = new Zanaj(game1, x, y);
                    Console.WriteLine("Error");
                    Console.ReadLine();
                }

                isDead = false;
            }
            return instance;
        }

        public Zanaj()
            :base(null, 0, 0, 0, 0)
        {
            // Initialize
            fps = 10.0;
            timePerFrame = 1.0 / fps;
            _state = ZanState.IdleRight;
            XSpeed = 0;
            Health = BASE_HEALTH;
            PoisonResist = 0;
            Luck = 0;
            _bullets = new List<Bullet>();
        }

        /// <summary>
        /// Moves, animates, and makes a stepping sound
        /// </summary>
        /// <param name="dir"></param>

        int newZan = 0;

        public new virtual void Move(Vector2 vel)
        {
            base.Move(vel);
        }

        public Zanaj(Game1 game1, int x, int y)
            : base (game1, x, y, 32, 64)
        {
            // Initialize
            fps = 10;
            timePerFrame = 1.0 / fps;
            _state = ZanState.IdleRight;
            XSpeed = 0;
            Health = BASE_HEALTH;
            PoisonResist = 0;
            Luck = 0;
            game = game1;
            IsZanaj = true;
            _bullets = new List<Bullet>();
            spriteSheet = game1.Content.Load<Texture2D>("Sprites/Characters/Zanaj/ZanajSprite");
            _eqWeapon = new Weapon(game.Content.Load<Texture2D>("Weapons/pistol"), 1, 5, "Peashooter", 50, 30, 20, 1, 1, 200);
            _eqBoots = new Boots(null, 1, 2, "Original Boots", 20, 0, 10);
            _eqBreastplate = new Breastplate(null, 1, 4, "Original Breastplate", 30, 0, false, 0, 0);
            _eqGloves = new Gloves(null, 1, 3, "Original Gloves", 20, 0, 0);
            _inventory = new Inventory(this);
            newZan = 1;
        }

        public void Shoot()
        {

            if (_state == ZanState.IdleRight || _state == ZanState.MoveRight)
            {
                if (_eqWeapon.Type == 7)
                {
                    for (int i = 0; i < 2 * _eqWeapon.SpreadBullets; i++)
                    {
                        _bullets.Add(new Bullet(X + (Width / 2), Y + (Height / 2), ((_eqWeapon.BulletSpeed / 20) + XSpeed),
                            0, (_eqWeapon.BulletSize / 5), game, 1, _eqWeapon.Damage, ((i - ((float)_eqWeapon.SpreadBullets)) / 3)));
                    }
                }
                else
                {
                    _bullets.Add(new Bullet(X + (Width / 2), Y + (Height / 2), ((_eqWeapon.BulletSpeed / 20) + XSpeed), 0, _eqWeapon.BulletSize, game, 1, _eqWeapon.Damage));
                }
            }
            else if (_state == ZanState.IdleLeft || _state == ZanState.MoveLeft)
            {
                if (_eqWeapon.Type == 7)
                {
                    for (int i = 0; i < 2 * _eqWeapon.SpreadBullets; i++)
                    {
                        _bullets.Add(new Bullet(X + (Width / 2), Y + (Height / 2), ((_eqWeapon.BulletSpeed / 20) - XSpeed),
                            0, (_eqWeapon.BulletSize / 5), game, 3, _eqWeapon.Damage, ((i - ((float)_eqWeapon.SpreadBullets)) / 3)));
                    }
                }
                else
                {
                    _bullets.Add(new Bullet(X + (Width / 2), Y + (Height / 2), ((_eqWeapon.BulletSpeed / 20) - XSpeed), 0, _eqWeapon.BulletSize, game, 3, _eqWeapon.Damage));
                }
            }
        }

        public void ChangeState(ZanState animState)
        {
            _state = animState;
            frame = 1;

            if (animState == ZanState.IdleBack)
            {
                fps = 2;
                timePerFrame = 1.0 / fps;
                _frameCount = 2;
            }

            if (animState == ZanState.IdleFace)
            {
                fps = 2;
                timePerFrame = 1.0 / fps;
                _frameCount = 2;
            }

            if (animState == ZanState.IdleLeft)
            {
                fps = 4;
                timePerFrame = 1.0 / fps;
                _frameCount = 4;
            }

            if (animState == ZanState.IdleRight)
            {
                fps = 4;
                timePerFrame = 1.0 / fps;
                _frameCount = 4;
            }

            if (animState == ZanState.MoveBack)
            {
                fps = 9;
                timePerFrame = 1.0 / fps;
                _frameCount = 9;      
            }                         
                                      
            if (animState == ZanState.MoveFace)
            {
                fps = 4;
                timePerFrame = 1.0 / fps;
                _frameCount = 4;      
            }                         
                                      
            if (animState == ZanState.MoveLeft)
            {
                fps = 8;
                timePerFrame = 1.0 / fps;
                _frameCount = 8;      
            }                         
                                      
            if (animState == ZanState.MoveRight)
            {
                fps = 8;
                timePerFrame = 1.0 / fps;
                _frameCount = 8;
            }
        }

        private void ProcessInput(Level level)
        {
            if (level != null)
            {
                switch (_state)
                {
                    case ZanState.IdleRight:
                        #region IdleRight
                            if (level.CurrentKeyState.IsKeyDown(Keys.Left))
                            {   
                                ChangeState(ZanState.IdleLeft);
                            }
                            else if (level.CurrentKeyState.IsKeyDown(Keys.Right))
                            {
                                ChangeState(ZanState.MoveRight);
                            }
                            else if (level.CurrentKeyState.IsKeyDown(Keys.Up))
                            {
                                ChangeState(ZanState.IdleBack);
                            }
                            else if (level.CurrentKeyState.IsKeyDown(Keys.Down))
                            {
                                ChangeState(ZanState.IdleFace);
                            }

                            XSpeed = 0;
                            break;
                            #endregion

                    case ZanState.MoveRight:
                        #region MoveRight
                            if(!(level.CurrentKeyState.IsKeyDown(Keys.Right)))
                            {
                                ChangeState(ZanState.IdleRight);
                            }

                            XSpeed = BASE_SPEED + (_eqBoots.PlusSpeed / 4);
                            break;
                            #endregion
                        
                    case ZanState.IdleLeft:
                        #region IdleLeft
                            if (level.CurrentKeyState.IsKeyDown(Keys.Left))
                            {
                                ChangeState(ZanState.MoveLeft);
                            }
                            else if (level.CurrentKeyState.IsKeyDown(Keys.Right))
                            {
                                ChangeState(ZanState.IdleRight);
                            }
                            else if (level.CurrentKeyState.IsKeyDown(Keys.Up))
                            {
                                ChangeState(ZanState.IdleBack);
                            }
                            else if (level.CurrentKeyState.IsKeyDown(Keys.Down))
                            {
                                ChangeState(ZanState.IdleFace);
                            }

                            XSpeed = 0;
                            break;
                            #endregion

                    case ZanState.MoveLeft:
                        #region MoveLeft
                            if(!(level.CurrentKeyState.IsKeyDown(Keys.Left)))
                            {
                                ChangeState(ZanState.IdleLeft);
                            }

                            XSpeed = -BASE_SPEED - (_eqBoots.PlusSpeed / 4);
                            break;
                            #endregion

                    case ZanState.IdleBack:
                        #region IdleBack
                            if (level.CurrentKeyState.IsKeyDown(Keys.Left))
                            {
                                ChangeState(ZanState.IdleLeft);
                            }
                            else if (level.CurrentKeyState.IsKeyDown(Keys.Right))
                            {
                                ChangeState(ZanState.IdleRight);
                            }
                            else if (level.CurrentKeyState.IsKeyDown(Keys.Up))
                            {
                                ChangeState(ZanState.MoveBack);
                            }
                            else if (level.CurrentKeyState.IsKeyDown(Keys.Down))
                            {
                                ChangeState(ZanState.IdleFace);
                            }

                            XSpeed = 0;
                            break;
                            #endregion

                    case ZanState.MoveBack:
                        #region MoveBack
                            if(!(level.CurrentKeyState.IsKeyDown(Keys.Up)))
                            {
                                ChangeState(ZanState.IdleBack);
                            }

                            XSpeed = 0;
                            break;
                            #endregion

                    case ZanState.IdleFace:
                        #region IdleFace
                        if (level.CurrentKeyState.IsKeyDown(Keys.Left))
                        {
                            ChangeState(ZanState.IdleLeft);
                        }
                        else if (level.CurrentKeyState.IsKeyDown(Keys.Right))
                        {
                            ChangeState(ZanState.IdleRight);
                        }
                        else if (level.CurrentKeyState.IsKeyDown(Keys.Up))
                        {
                            ChangeState(ZanState.IdleBack);
                        }
                        else if (level.CurrentKeyState.IsKeyDown(Keys.Down))
                        {
                            ChangeState(ZanState.MoveFace);
                        }

                        XSpeed = 0;
                        break;
                        #endregion

                    case ZanState.MoveFace:
                        #region MoveFace
                        if (!(level.CurrentKeyState.IsKeyDown(Keys.Down)))
                        {
                            ChangeState(ZanState.IdleFace);
                        }
                        XSpeed = 0;
                        break;
                        #endregion
                }
                
                if (level.CurrentKeyState.IsKeyDown(Keys.Space) && !(Jumping))
                {
                    YSpeed = -17;
                    Jumping = true;
                }

                if ((level.CurrentKeyState.IsKeyDown(Keys.F) || level.CurrentKeyState.IsKeyDown(Keys.Z)) && shootTimer >= _eqWeapon.FireRate)
                {
                    Shoot();
                    shootTimer = 0;
                }
                else
                {
                    shootTimer++;
                }

                if (level.PressedOnlyOnce(Keys.Tab))
                {
                    fileHand.Save(game, 1);
                }
            }

            if (level.PressedOnlyOnce(Keys.I))
            {
                game._GameMode = Game1.GameMode.Inventory;
            }
        }

        public void Update(Level level)
        {
            if (newZan == 1)
            {
                newZan = 0;
                Level.zanaj = this;
                game.Zanaj = this;
                Game1.zanaj = this;
                game.camera.Focus = this;
            }
            
            currentLevel = level;

            ProcessInput(level);

            bool collide = false;

            if(currentLevel != game.testLevel)
            {
                collide = (CheckTileCollisions(level) == 0);
            }

            if ((Jumping) || (collide && !Jumping))
            {
                if (YSpeed <= 16)
                {
                    YSpeed += 1;

                    if (!currentLevel.CurrentKeyState.IsKeyDown(Keys.Space) && YSpeed < 0)
                    {
                        YSpeed += 3;
                    }
                }
                else if (YSpeed > 16 && !Jumping)
                {
                    YSpeed = 0;
                    Jumping = false;
                }
            }

            if (YSpeed > 16)
            {
                YSpeed = 16;
            }

            checkEquipped();

            Move(Velocity);

            if (currentLevel != game.testLevel)
            {
                CheckTileCollisions(level);
            }

            foreach (Bullet b in _bullets)
            {
                b.Move(4);
            }

            currentLevel = level;
            
            // Handle animation timing
            // - Add to the time counter
            // - Check if we have enough "time" to advance the frame
            timeCounter += game.theGameTime.ElapsedGameTime.TotalSeconds;

            if (timeCounter >= timePerFrame)
            {
                frame += 1;						// Adjust the frame

                if (frame > _frameCount)	// Check the bounds
                {
                    frame = 1;					// Back to 1 (since 0 is the "standing" frame)
                }
                
                timeCounter -= timePerFrame;	// Remove the time we "used"

                if (Jumping)
                {
                    if (frame > 3 && frame <= 4)
                    {
                        frame = 3;
                    }
                    else if (frame == 3)
                    {
                        frame = 2;
                    }

                    if (frame > 7 && frame <= 8)
                    {
                        frame = 7;
                    }
                    else if (frame == 5)
                    {
                        frame = 6;
                    }
                    else if (frame != 5 && frame == 7)
                    {
                        frame = 5;
                    }
                }   
            }

            if (Health <= 0)
            {
                isDead = true;
            }

            if (isDead)
            {
                Game1.gameMode = Game1.GameMode.GameOver;
                
            }
        }

        public bool IsColliding(Tile t)
        {
            if (Rect.Intersects(t.Rectangle))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void checkEquipped()
        {
            luck = _eqGloves.PlusLuck;
            maxHealth = BASE_HEALTH + _eqBreastplate.PlusHealth;
            PoisonResist = _eqBreastplate.PlusPoisonResist;
            maxSpeed = BASE_SPEED + _eqBoots.PlusSpeed;
        }

        public int CheckTileCollisions(Level level)
        {
            int collisions = 0;
            foreach (Tile t in level.TileList)
            {
                if (IsColliding(t) && t.Collides)
                {
                    if (new Rectangle(X, Y + 24, Width / 4, 16).Intersects(t.Rectangle) && (XSpeed < 0))
                    {
                        Console.WriteLine("1: {0},{1}", t.Rectangle.X, X);
                        XSpeed = 0;

                        while (IsColliding(t))
                        {
                            X++;
                        }
                    }
                    else if (new Rectangle(X + 3 * Width / 4, Y + 24, Width / 4, 16).Intersects(t.Rectangle) && (XSpeed > 0))
                    {
                        Console.WriteLine("2: {0},{1}", t.Rectangle.X, X);
                        XSpeed = 0; 
                        
                        while (IsColliding(t))
                        {
                            X--;
                        }
                    }

                    if (new Rectangle(X + Width / 4, Y + 40, Width / 2, 24).Intersects(t.Rectangle))
                    {
                        Console.WriteLine("COLLIDE!");
                        YSpeed = 0;
                        Y = PastY;

                        collisions++;

                        if (Jumping)
                        {
                            Jumping = false;
                        }

                        while (IsColliding(t))
                        {
                            Y--;
                        }
                    }

                    if (new Rectangle(X + Width / 4, Y, Width / 2, 24).Intersects(t.Rectangle))
                    {
                        YSpeed = 0;
                        Y = PastY;

                        collisions++;

                        while (IsColliding(t))
                        {
                            Y++;
                        }
                    }
                }

                if (IsColliding(t) && t.ID == 43)
                {
                    Health -= 2;
                }
            }

            return collisions;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet b in _bullets)
            {
                if (b.Active)
                {
                    spriteBatch.Draw(b.Image, new Rectangle(b.X - 5, b.Y - 5, b.Width + 10, b.Height + 10), Color.Yellow);
                    spriteBatch.Draw(b.Image, new Rectangle(b.X - 4, b.Y - 4, b.Width + 8, b.Height + 8), Color.Orange);
                    spriteBatch.Draw(b.Image, new Rectangle(b.X - 3, b.Y - 3, b.Width + 6, b.Height + 6), Color.Red);
                }
            }

            switch (_state)
            {
                case ZanState.MoveRight:
                    spriteBatch.Draw(
                        spriteSheet,					// - The texture to draw
                        Rect, 
                        new Rectangle(					// - The "source" rectangle
                            (frame * 32) - 32,	//   - This rectangle specifies
                            2 * 64,		//	   where "inside" the texture
                            32,			//     to get pixels
                            64),			//   - We don't want to draw it all
                        Color.White);			    // - The color
                    break;

                case ZanState.MoveLeft:
                    spriteBatch.Draw(
                        spriteSheet,					// - The texture to draw
                        Rect,
                        new Rectangle(					// - The "source" rectangle
                            (frame * 32) - 32,	//   - This rectangle specifies
                            1 * 64,		//	   where "inside" the texture
                            32,			//     to get pixels
                            64),			//   - We don't want to draw it all
                        Color.White); 			    // - The color
                    break;

                case ZanState.MoveBack:
                    spriteBatch.Draw(
                        spriteSheet,					// - The texture to draw
                        Rect,
                        new Rectangle(					// - The "source" rectangle
                            (frame * 32) - 32,	//   - This rectangle specifies
                            0 * 64,		//	   where "inside" the texture
                            32,			//     to get pixels
                            64),			//   - We don't want to draw it all
                        Color.White); 			    // - The color
                    break;

                case ZanState.MoveFace:
                    spriteBatch.Draw(
                        spriteSheet,					// - The texture to draw
                        Rect,
                        new Rectangle(					// - The "source" rectangle
                            (frame * 32) - 32,	//   - This rectangle specifies
                            3 * 64,		//	   where "inside" the texture
                            32,			//     to get pixels
                            64),			//   - We don't want to draw it all
                        Color.White); 			    // - The color
                    break;

                case ZanState.IdleRight:
                    spriteBatch.Draw(
                        spriteSheet,					// - The texture to draw
                        Rect,
                        new Rectangle(					// - The "source" rectangle
                            (frame * 32) - 32,	                        //   - This rectangle specifies
                            4 * 64,		//	   where "inside" the texture
                            32,			//     to get pixels
                            64),			//   - We don't want to draw it all
                            Color.White);               // - The color
                    break;

                case ZanState.IdleLeft:
                    spriteBatch.Draw(
                        spriteSheet,					// - The texture to draw
                        Rect,
                        new Rectangle(					// - The "source" rectangle
                            (frame * 32) + (3 * 32),	                        //   - This rectangle specifies
                            3 * 64,		//	   where "inside" the texture
                            32,			//     to get pixels
                            64),			//   - We don't want to draw it all
                            Color.White);
                    break;

                case ZanState.IdleBack:
                    spriteBatch.Draw(
                        spriteSheet,					// - The texture to draw
                        Rect,
                        new Rectangle(					// - The "source" rectangle
                            (frame * 32) + (5 * 32),	                        //   - This rectangle specifies
                            4 * 64,		//	   where "inside" the texture
                            32,			//     to get pixels
                            64),			//   - We don't want to draw it all
                            Color.White);               // - The color
                    break;

                case ZanState.IdleFace:
                    spriteBatch.Draw(
                        spriteSheet,					// - The texture to draw
                        Rect,
                        new Rectangle(					// - The "source" rectangle
                            (frame * 32) + (3 * 32),	                        //   - This rectangle specifies
                            4 * 64,		//	   where "inside" the texture
                            32,			//     to get pixels
                            64),			//   - We don't want to draw it all
                            Color.White);
                    break;
            }
        }
    }
}