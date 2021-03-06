using System;
using System.Threading;
using XNAGame.Client;
using XNAGame.util;

namespace XNAGame
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {




            

            ServerConn.InitConnectionFromServer serverConn = new ServerConn.InitConnectionFromServer();
            

            Thread thread = new Thread(new ThreadStart(() => serverConn.waitForConnection()));
            thread.Start();

            ClientConnectionInit.sendData(Constant.C2S_INITIALREQUEST);

            using (Game1 game = new Game1())
            {
                game.Run();

            }
        }
    }
#endif
}

