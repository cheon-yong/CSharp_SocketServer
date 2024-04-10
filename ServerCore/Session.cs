using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerCore
{
	class Session
	{
		Socket _socket;
		int _disconnected = 0;
		// IP
		// Recv Buffer
		// Account
		// Player

		public void Start(Socket socket)
		{
			_socket = socket;

			// -----------------------
			SocketAsyncEventArgs recvArgs = new SocketAsyncEventArgs();
			recvArgs.Completed += OnRecvCompleted;
			recvArgs.UserToken = this;
			recvArgs.SetBuffer(new byte[1024], 0, 1024);

			RegisterRecv(recvArgs);
		}

		public void Send(byte[] sendBuff)
		{
			_socket.Send(sendBuff);
		}


		public void Disconnect()
		{
			if (Interlocked.Exchange(ref _disconnected, 1) == 1)
				return;

			_socket.Shutdown(SocketShutdown.Both);
			_socket.Close();
			_socket.Dispose();
		}

		#region 네트워크 통신
		void RegisterRecv(SocketAsyncEventArgs args)
		{
			bool pending = _socket.ReceiveAsync(args);
            if (pending == false)
            {
				OnRecvCompleted(null, args);
			}
        }

		void OnRecvCompleted(object sender, SocketAsyncEventArgs args)
		{
			if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
			{
				// TODO
				string recvData = Encoding.UTF8.GetString(args.Buffer, args.Offset, args.BytesTransferred);
                Console.WriteLine($"[From Client] {recvData}");
				RegisterRecv(args);
            }
			else
			{
				// TODO Disconnect
			}
		}
		#endregion
	}
}
