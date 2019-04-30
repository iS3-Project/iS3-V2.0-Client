using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using iS3;
namespace iS3.Unity.EXE
{
    public class TcpServer
    {
        //私有成员
        private static byte[] result = new byte[4000000];
        private int myProt = 500;   //端口  
        static Socket serverSocket;
        static Socket clientSocket;

        Thread myThread;
        static Thread receiveThread;

        public static EventHandler<string> messageHandler;
        //属性

        public int port { get; set; }
        //方法



        internal void StartServer()
        {
            //服务器IP地址  
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, myProt));  //绑定IP地址：端口  
            serverSocket.Listen(10);    //设定最多10个排队连接请求  

            Debug.WriteLine("启动监听{0}成功", serverSocket.LocalEndPoint.ToString());

            //通过Clientsoket发送数据  
            myThread = new Thread(ListenClientConnect);
            myThread.Start();

        }


        internal void QuitServer()
        {
            try
            {
                serverSocket.Close();
                clientSocket.Close();
                myThread.Abort();
                receiveThread.Abort();
            }
            catch
            {
                myThread.Abort();
                receiveThread.Abort();
            }

        }


        internal void SendMessage(string msg)
        {
            try
            {
                Debug.WriteLine("send------>" + msg);
                clientSocket.Send(Encoding.UTF8.GetBytes(msg));
            }
            catch { }

        }

        /// <summary>  
        /// 监听客户端连接  
        /// </summary>  
        private static void ListenClientConnect()
        {
            while (true)
            {
                try
                {
                    clientSocket = serverSocket.Accept();
                    //clientSocket.ReceiveTimeout = 1000;
                   // clientSocket.ReceiveBufferSize = 4196304;
                    clientSocket.Send(Encoding.UTF8.GetBytes("Server Say Hello"));
                    receiveThread = new Thread(ReceiveMessage);
                    receiveThread.Start(clientSocket);
                }
                catch (Exception)
                {

                }
            }
        }
        /// <summary>  
        /// 接收消息  
        /// </summary>  
        /// <param name="clientSocket"></param>  
        /// string
        /// 
        private static byte[] oldresult = new byte[4000000];
        static int oldNumber;
        private static void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            string Str = "";
            int receiveNumber = 0;
            while (true)
            {
                try
                {
                    Str = "";
                    //通过clientSocket接收数据  
                    receiveNumber = myClientSocket.Receive(result);
                    if ((result[0] == 64) && (result[1] == 64) && (result[2] == 64) && (result[3] == 64))
                    {
                        if ((result[receiveNumber - 1] == 37) && (result[receiveNumber - 2] == 37) && (result[receiveNumber - 3] == 37) && (result[receiveNumber - 4] == 37))
                        {
                            Str = Encoding.UTF8.GetString(result, 0, receiveNumber);
                            Str = Str.Substring(4, Str.Length - 8);
                            oldNumber = 0;
                            oldresult=new byte[4000000];
                        }
                        else
                        {
                            for (int i = 0; i < receiveNumber; i++)
                            {
                                oldresult[i] = result[i];
                            }
                            oldNumber = receiveNumber;
                        }
                    }
                    else
                    {
                        if ((result[receiveNumber - 1] == 37) && (result[receiveNumber - 2] == 37) && (result[receiveNumber - 3] == 37) && (result[receiveNumber - 4] == 37))
                        {
                            for (int i = 0; i < receiveNumber; i++)
                            {
                                oldresult[i + oldNumber] = result[i];
                            }
                            Str = Encoding.UTF8.GetString(oldresult, 0, oldNumber + receiveNumber);
                            Str = Str.Substring(4, Str.Length - 8);
                            oldNumber = 0;
                            oldresult = new byte[4000000];
                        }
                        else
                        {
                            for (int i = 0; i < receiveNumber; i++)
                            {
                                oldresult[i + oldNumber] = result[i];
                            }
                            oldNumber += receiveNumber;
                        }
                    }
                    //Debug.WriteLine("接收客户端{0}消息{1}", myClientSocket.RemoteEndPoint.ToString(), Encoding.UTF8.GetString(result, 0, receiveNumber));
                   // Str = Encoding.UTF8.GetString(result, 0, receiveNumber);
                    //if (receiveNumber>=1024)
                    //{
                    //    continue;
                    //}
                    if (Str == "") continue;
                    if (Str == "_Focus")
                    {
                        UnityPanel.main.ActivateUnityWindow();

                    }
                    else
                    {
                      Debug.WriteLine(Str);
                    }
                    if (messageHandler != null)
                    {
                        //MessageArgs arg = new MessageArgs();
                        //arg.message = Str;
                        //messageHandler(null, arg);
                        //Debug.WriteLine(Str);
                    }
                    Str = "";
                    receiveNumber = 0;
                }
                catch (Exception ex)
                {
                    try
                    {
                        Debug.WriteLine(ex.Message);
                        myClientSocket.Shutdown(SocketShutdown.Both);
                        myClientSocket.Close();
                        break;
                    }
                    catch (Exception)
                    {
                    }

                }
            }
        }
    }
}
