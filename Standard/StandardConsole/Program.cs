using System;

using maqdel.Infra;

using log4net;

namespace maqdel.Infra
{
    internal class Program
    {
        private static maqdel.Infra.StandardConsole _standardConsole = new StandardConsole();

        static void Main(string[] args)
        {
            ILog _logger = LogManager.GetLogger(typeof(Program));

            _logger.Info("StandardConsole");

            _standardConsole = new StandardConsole();
            _standardConsole.StartConsole();
        }
    }
}
