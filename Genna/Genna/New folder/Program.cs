using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Genna.GameObjects.Chests;
using Genna.Items;
using Genna.GameObjects.Chests.RandomChest;
using Genna.GameObjects.Chests.SpecialChest;
using Genna.Items.Moneys;
using Genna.Items.Armors;
using Genna.Items.Weapons;
using Genna.GameObjects.Characters;
using Genna.GameObjects.Characters.Players;
using Genna.GameObjects.Characters.Players.Zanaj_Akari;

namespace Genna
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// 
        /// Jordan
        /// </summary>
        static void Main(string[] args)
        {
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);

                if (cki.Key == ConsoleKey.Spacebar)
                {
                    RandomItem ri = new RandomItem();

                    Item i = ri.AddRandomItem();
                    Console.WriteLine(i.ToString());
                    Console.WriteLine();
                }

                if (cki.Key == ConsoleKey.Q)
                {
                    break;
                }
            }

            using (Game1 game = new Game1())
            {
                game.Run();
            }
        }
    }
#endif
}

