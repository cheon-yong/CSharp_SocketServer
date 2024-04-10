using System.Net;
using System.Net.Sockets;
using System.Text;
using ServerCore;

namespace DummyClient
{
	internal class Program
	{
		static void Main(string[] args)
		{
			// www.naver.com -> ip?
			string host = Dns.GetHostName();
			IPHostEntry ipHost = Dns.GetHostEntry(host);
			IPAddress ipAddr = ipHost.AddressList[0];
			IPEndPoint endPoint = new IPEndPoint(ipAddr, 7777);

			Connector connector = new Connector();

			connector.Connect(endPoint, () => { return new ServerSession(); });

			while (true)
			{
				

				Thread.Sleep(100);
			}
		}
	}
}
