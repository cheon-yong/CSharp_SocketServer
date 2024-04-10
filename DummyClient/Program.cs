﻿using System.Net;
using System.Net.Sockets;
using System.Text;

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

			// 휴대폰 설정
			Socket socket = new Socket(endPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

			// 입장 문의
			socket.Connect(endPoint);

            Console.WriteLine($"Connected To {socket.RemoteEndPoint.ToString()}");

			// 보낸다
			byte[] sendBuff = Encoding.UTF8.GetBytes("Hello World!");
			int sendBytes = socket.Send(sendBuff);

			// 받는다
			byte[] recvBuffer = new byte[1024];
			int recvBytes = socket.Receive(recvBuffer);
			string recvData = Encoding.UTF8.GetString(recvBuffer, 0, recvBytes);
            Console.WriteLine($"[From Server] {recvData}");

			// 나간다
			socket.Shutdown(SocketShutdown.Both);
			socket.Close();
        }
	}
}