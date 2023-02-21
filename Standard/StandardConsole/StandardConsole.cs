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
            var options = new List<string>();
            options.Add("Option 1");
            options.Add("Option 2");
            options.Add("Option 3");
            options.Add("Option 4");
            options.Add("Option 5");
            ConsoleHelper.WriteWindow(1,1,50,"Standard Console 2.1",options,"Press a key to execute, or ESC to end");
        }

        public void StartConsole()
        {
            _logger.Info("StartConsole");
            try
            {

                var xx = maqdel.Infra.IO.IOHelper.GetPathFiles(@"c:\");
                Console.WriteLine("Count:" + xx.Count());
                /*
                foreach(var x in xx){
                    Console.WriteLine(x);
                }
                */


                /*
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
                */
            }
            catch (Exception ex)
            {
                _logger.Error("StartConsole, Exception:", ex);
            }
        }


    }
}