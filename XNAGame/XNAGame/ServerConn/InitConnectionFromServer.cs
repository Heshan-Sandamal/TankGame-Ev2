using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using XNAGame.util;
using XNAGame.PlayerDesc;
using System.Threading;

namespace XNAGame.ServerConn
{
    class InitConnectionFromServer
    {
        bool errorOcurred = false;
        Socket connection = null; //The socket that is listened to     
        TcpListener listener = null;
        private static TokenizerMain torkenizer;

        public InitConnectionFromServer()
        {

        }

        public void waitForConnection()
        {
            try
            {
                if(torkenizer==null){
                    torkenizer = new TokenizerMain();
                    Game1.setTorkenizerMain(torkenizer);

                }
                
                //Creating listening Socket
                this.listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7000);

                Console.WriteLine("waiting for server response");

                //Starts listening
                this.listener.Start();
                //Establish connection upon server request
                //int a = 0;
                
                    while (true)
                    {
                        //a++;
                        connection = listener.AcceptSocket();   //connection is connected socket

                        //Console.WriteLine("Connetion is established");

                        //Fetch the messages from the server
                        int asw = 0;
                        //create a network stream using connection
                        NetworkStream serverStream = new NetworkStream(connection);
                        List<Byte> inputStr = new List<byte>();

                        //fetch messages from  server
                        while (asw != -1)
                        {
                            asw = serverStream.ReadByte();
                            inputStr.Add((Byte)asw);
                        }

                        String messageFromServer = Encoding.UTF8.GetString(inputStr.ToArray());

                        //TokenizerMain torkenizer = new TokenizerMain();
                        //Console.Write("Response from server to join "+torkenizer.serverJoinReply(messageFromServer));
                        //Console.WriteLine(messageFromServer);

                        //Main torkenizer = new Main();
                        //Console.Write("Response from server to join "+torkenizer.serverJoinReply(messageFromServer));
                        Console.WriteLine(messageFromServer);

                        messageFromServer = messageFromServer.Substring(0, messageFromServer.Length - 1);

                        //Console.WriteLine("msg"+messageFromServer);
                        try
                        {
                            if (messageFromServer.StartsWith("L"))
                            {

                                Console.WriteLine("initialize");
                                torkenizer.LifePacks(messageFromServer);                //accept life packs
                            }
                            else if (messageFromServer.StartsWith("S"))
                            {
                                Console.WriteLine("accept & start the game");
                                torkenizer.JoinReply(messageFromServer);
                            }
                            else if (messageFromServer.StartsWith("C"))
                            {
                                Console.WriteLine("coins");
                                torkenizer.AquireCoins(messageFromServer);          //accept coins
                            }
                            else if (messageFromServer.StartsWith("I"))
                            {
                                
                                torkenizer.MapInitializer(messageFromServer);       //init map
                            }
                            else if (messageFromServer.StartsWith("G"))
                            {
                                torkenizer.UpdatePlayerStats(messageFromServer);
                                torkenizer.UpdateMap(messageFromServer);
                            }
                        }
                        catch (Exception ee)
                        {
                          //  Console.WriteLine(ee.Message);
                            //Console.WriteLine(ee.StackTrace);
                        }






                        serverStream.Close();       //close the netork stream


                    }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Communication (RECEIVING) Failed! \n " + e.Message);
                errorOcurred = true;
            }
            finally
            {
                if (connection != null)
                    if (connection.Connected)
                        connection.Close();
                if (errorOcurred)
                   this.waitForConnection();
            }
        }

    }
}
