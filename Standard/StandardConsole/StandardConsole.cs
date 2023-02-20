using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Reflection;

using log4net;

using maqdel.Infra;
using maqdel.Infra.Diagnostics;
//using maqdel.Infra.tempo;

namespace maqdel.Infra
{
    public class StandardConsole{

        private readonly ILog _logger = LogManager.GetLogger(typeof(StandardConsole));

        private void MainMenu() {
            //Console.Clear();
            //maqdel.Infra.ConsoleHelper.Cls(ConsoleColor.Green, ConsoleColor.Black);
            ConsoleHelper.Cls(ConsoleColor.Green, ConsoleColor.Black);
            Console.WriteLine("Standard Console");
            Console.WriteLine("");
            //Console.WriteLine("Q - Play game (" + _cols.ToString() + "x" + _rows.ToString() + ")");{}
            ///Console.WriteLine("W - Change");
            //Console.WriteLine("E - ");
            Console.WriteLine("");
            Console.WriteLine("Press a key to execute, or ESC to end");
        }

        public void StartConsole()
        {
            _logger.Info("StartConsole");
            try
            {
            ConsoleKeyInfo cki;
            MainMenu();
            do
            {
                cki = Console.ReadKey();
                Console.Write(" - You pressed ");
                Console.WriteLine(cki.Key.ToString());

                switch (cki.Key)
                {
                    case ConsoleKey.Q:
                        //PlayGame();
                        break;
                    case ConsoleKey.W:
                        Console.Clear();
                        Console.WriteLine("2");
                        Console.WriteLine("");
                        Console.WriteLine("Press any key");
                        Console.ReadLine();
                        break;
                    case ConsoleKey.E:
                        Console.Clear();
                        Console.WriteLine("3");
                        Console.WriteLine("");
                        Console.WriteLine("Press any key");
                        Console.ReadLine();
                        break;
                    default:
                        //Menu();
                        break;
                }
                MainMenu();

            } while (cki.Key != ConsoleKey.Escape);
            }
            catch (Exception ex)
            {
                _logger.Error("StartConsole, Exception:", ex);
            }
        }


    }
}