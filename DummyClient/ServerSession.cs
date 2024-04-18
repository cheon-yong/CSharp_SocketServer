using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DummyClient
{
	public class ServerSession : Session
	{
		public override void OnConnected(EndPoint endPoint)
		{
			Console.WriteLine($"OnConnected : {endPoint}");

			
		}

		public override void OnDisconnected(EndPoint endPoint)
		{
		}

		public override int OnRecv(ArraySegment<byte> buffer)
		{
			return 0;
		}

		public override void OnSend(int numOfBytes)
		{
		}
	}
}
