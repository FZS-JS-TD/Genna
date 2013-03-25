using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genna.GameObjects.Characters;
using Genna.GameObjects.Characters.Players;
using Genna.Items;
using Genna.Items.Moneys;
using Genna.Items.Armors;
using Genna.Items.Weapons;

namespace Genna.GameObjects.Characters.Players.Zanaj_Akari
{
    class Zanaj : Players.Player
    {
        protected int speed;
        protected int luck;
        protected int health;
        protected int poisonResist;

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public int Luck
        {
            get { return luck; }
            set { luck = value; }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int PoisonResist
        {
            get { return poisonResist; }
            set { poisonResist = value; }
        }
    }
}
