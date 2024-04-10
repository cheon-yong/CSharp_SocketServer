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
		bool _pending = false;


		object _lock = new object();
		Queue<byte[]> _sendQueue = new Queue<byte[]>();
		SocketAsyncEventArgs _sendArgs = new SocketAsyncEventArgs();
		SocketAsyncEventArgs _recvArgs = new SocketAsyncEventArgs();
		// IP
		// Recv Buffer
		// Account
		// Player

		public void Start(Socket socket)
		{
			_socket = socket;

			// -----------------------
			_recvArgs.Completed += OnRecvCompleted;
			_recvArgs.UserToken = this;
			_recvArgs.SetBuffer(new byte[1024], 0, 1024);

			_sendArgs.Completed += OnSendCompleted;

			RegisterRecv();
		}

		public void Send(byte[] sendBuff)
		{
			lock (_lock)
			{
				_sendQueue.Enqueue(sendBuff);

				if (_pending == false)
					RegisterSend();
			}
		}


		#region 네트워크 통신
		void RegisterSend()
		{
			_pending = true;

			byte[] buff = _sendQueue.Dequeue();
			_sendArgs.SetBuffer(buff, 0, buff.Length);

			bool pending = _socket.SendAsync(_sendArgs);
			if (pending)
				OnSendCompleted(null, _sendArgs);
		}

		void OnSendCompleted(object sender, SocketAsyncEventArgs args)
		{
            lock (_lock)
            {
				if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
				{
					if (_sendQueue.Count > 0)
						RegisterSend();
					else
						_pending = false;
				}
				else
				{
					Disconnect();
				}
			}
            
		}


		void RegisterRecv()
		{
			bool pending = _socket.ReceiveAsync(_recvArgs);
            if (pending == false)
            {
				OnRecvCompleted(null, _recvArgs);
			}
        }

		void OnRecvCompleted(object sender, SocketAsyncEventArgs args)
		{
			if (args.BytesTransferred > 0 && args.SocketError == SocketError.Success)
			{
				// TODO
				string recvData = Encoding.UTF8.GetString(args.Buffer, args.Offset, args.BytesTransferred);
                Console.WriteLine($"[From Client] {recvData}");
				RegisterRecv();
            }
			else
			{
				// TODO Disconnect
			}
		}
		public void Disconnect()
		{
			if (Interlocked.Exchange(ref _disconnected, 1) == 1)
				return;

			_socket.Shutdown(SocketShutdown.Both);
			_socket.Close();
			_socket.Dispose();
		}
		#endregion
	}
}
