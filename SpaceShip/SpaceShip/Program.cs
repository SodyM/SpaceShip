using System;

namespace SpaceShip
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (SpaceShipGame game = new SpaceShipGame())
            {
                game.Run();
            }
        }
    }
#endif
}

