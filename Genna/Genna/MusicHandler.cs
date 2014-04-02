using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

using Genna.GameObjects.Characters.Players.Zanaj_Akari;

namespace Genna
{
    public class MusicHandler
    {
        IList<Song> songs;
        int index;
 
        public IList<Song> Songs
        {
            get { return songs; }
            set { songs = value; }
        }

        Game1 game;

        public int Current
        {
            get
            {
                return index;
            }
            set
            {
                songs.Clear();
                index = value;
                songs.Add(allMusic[index]);
            }
        }

        public static Song _title;
        public static Song _shop;
        public static Song _altShop;
        public static Song _infirma;
        public static Song _town; // previously happy
        public static Song _cave;
        public static Song _boss;

        public static SoundEffect _step;

        public IList<Song> allMusic;

        public MusicHandler(Game1 pGame)
        {
            game = pGame;
            MusicHandler._step = game.Content.Load<SoundEffect>("Sounds/SFX/gearRotate");

            MusicHandler._title = game.Content.Load<Song>(@"Sounds/Music/Genna's Gleam");
            MusicHandler._town = game.Content.Load<Song>(@"Sounds/Music/light");
            MusicHandler._shop = game.Content.Load<Song>(@"Sounds/Music/shop");
            MusicHandler._altShop = game.Content.Load<Song>("Sounds/alt");
            MusicHandler._infirma = game.Content.Load<Song>("Sounds/Music/enterTheInfirma");
            MusicHandler._cave = game.Content.Load<Song>("Sounds/set");
            MusicHandler._boss = game.Content.Load<Song>("Sounds/think");

            songs = new List<Song>();
            allMusic = new List<Song>();

            addSong(MusicHandler._title);
            addSong(MusicHandler._town);
            addSong(MusicHandler._shop);
            addSong(MusicHandler._altShop);
            addSong(MusicHandler._infirma);
            addSong(MusicHandler._cave);
            addSong(MusicHandler._boss);
            
            index = 0;
 
            MediaPlayer.IsVisualizationEnabled = true;
 
            MediaPlayer.ActiveSongChanged += new EventHandler<EventArgs>(MediaPlayer_ActiveSongChanged);
            MediaPlayer.MediaStateChanged += new EventHandler<EventArgs>(MediaPlayer_MediaStateChanged);
        }

        void addSong(Song song)
        {
            songs.Add(song);
            allMusic.Add(song);
        }

        public void SetSong(int songNum = int.MinValue, Levels.Level level = null)
        {
            if (Game1.gameMode == Game1.GameMode.Menu)
            {
                Current = 0;
            }
            else if (game._GameMode == Game1.GameMode.Playing && Zanaj.getInstance().currentLevel != null)
            {
                if (level == null)
                    level = Levels.Level.game.CurrentLevel;
                if (level == null)
                    level = Levels.Level.game.allLevels[0];
                if (Zanaj.instance.currentLevel != null && !Zanaj.isDead)
                    level = Zanaj.instance.currentLevel;
                if (level == game.allLevels[0])
                {
                    Current = 1;
                }
                else if (level == game.allLevels[1])
                {
                    Current = 2;
                }
                else if (level == game.allLevels[2])
                {
                    Current = 3;
                }
                else if (level == game.allLevels[3])
                {
                    Current = 4;
                }
                else if (level == game.allLevels[4])
                {
                    Current = 5;
                }
                else if (level == game.allLevels[5])
                {
                    Current = 6;
                }
            }

            Play();
        }

        void MediaPlayer_ActiveSongChanged(object sender, EventArgs e)
        {

        }

        void MediaPlayer_MediaStateChanged(object sender, EventArgs e)
        {
            SetSong();
        }

        public void Play()
        {
            if(allMusic.Count != 0)
                songs.Add(allMusic[index]);
            if (MediaPlayer.GameHasControl)
            {
                MediaPlayer.IsRepeating = false;
 
                if (MediaPlayer.State == MediaState.Stopped)
                {
                    if(Songs.Count > 0)
                    {
                        MediaPlayer.Play(songs[0]);
                        songs.RemoveAt(0);
                    }
                }
            }
        }

        public void Stop()
        {
            songs.Clear();
 
            if (MediaPlayer.GameHasControl)
            {
                MediaPlayer.Stop();
            }
        }
    }
}