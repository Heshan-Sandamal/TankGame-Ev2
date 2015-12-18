using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using XNAGame.util;

namespace XNAGame.Client
{
    class ClientConnectionInit
    {
        static System.Net.Sockets.TcpClient clientSocket;     //create a TcpCLient socket to connect to server
        static NetworkStream stream = null;
        private static BinaryWriter writer;
        // private static NetworkStream clientStream;
        
        public static void Connect()
        {
            //connecting to server socket with port 6000
            clientSocket.Connect(IPAddress.Parse("127.0.0.1"), 6000);
            stream = clientSocket.GetStream();

            //joining message to server
            byte[] ba = Encoding.ASCII.GetBytes(Constant.C2S_INITIALREQUEST);

            for (int x = 0; x < ba.Length; x++)
            {
                Console.WriteLine(ba[x]);
            }

            stream.Write(ba, 0, ba.Length);        //send join# to server
            stream.Flush();
            stream.Close();          //close network stream

        }


        public static void sendData(String msg)
        {

            try
            {
                clientSocket = new TcpClient(); //the even number bux fix


                clientSocket.Connect(IPAddress.Parse("127.0.0.1"), 6000);

                //Console.WriteLine("This is broadcasting00");
                if (clientSocket.Connected)
                {
                    //To write to the socket
                    stream = clientSocket.GetStream();

                    //Create objects for writing across stream
                    writer = new BinaryWriter(stream);
                    Byte[] tempStr = Encoding.ASCII.GetBytes(msg);

                    //writing to the port                
                    writer.Write(tempStr);
                    //Console.WriteLine("This is broadcasting");
                    writer.Close();
                    stream.Close();

                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Write faild due to "+ e.Message);
                sendData(msg);
            }
            finally {
                
            }

        }



    }
}
