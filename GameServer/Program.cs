using ServerCore;
using System.Net;

namespace GameServer
{
	internal class Program
	{
		static Listener _listener = new Listener();

		static void Main(string[] args)
		{
			// DNS (Domain Name)
			// www.rookiss.com -> 123.123.123.12
			string host = Dns.GetHostName();
			IPHostEntry ipHost = Dns.GetHostEntry(host);
			IPAddress ipAddr = ipHost.AddressList[0];
			IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

			_listener.Init(endPoint, () => { return new ClientSession(); });
			Console.WriteLine("Listening...");

			while (true)
			{

			}
		}
	}
}
