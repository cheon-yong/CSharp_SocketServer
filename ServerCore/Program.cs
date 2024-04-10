using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ServerCore
{
	class Program
	{
		static Listener _listener = new Listener();

		static void OnAcceptHandler(Socket clientSocket)
		{
			Session session = new Session();
			session.Start(clientSocket);

			byte[] sendBuff = Encoding.UTF8.GetBytes("Welcome to MMO Server");
			session.Send(sendBuff);

			Thread.Sleep(1000);

			session.Disconnect();
		}
		
		static void Main(string[] args)
		{
			// DNS (Domain Name)
			// www.rookiss.com -> 123.123.123.12
			string host = Dns.GetHostName();
			IPHostEntry ipHost = Dns.GetHostEntry(host);
			IPAddress ipAddr = ipHost.AddressList[0];
			IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

			_listener.Init(endPoint, OnAcceptHandler);
            Console.WriteLine("Listening...");

            while (true)
			{

			}
        }
	}
}
