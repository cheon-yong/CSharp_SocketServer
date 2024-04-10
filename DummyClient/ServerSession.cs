using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DummyClient
{
	class Packet
	{
		public ushort size;
		public ushort packetId;
	}

	class PlayerInfoReq : Packet
	{
		public long playerId;
	}

	class PlayerInfoOk : Packet
	{
		public int hp;
		public int attack;
	}

	public enum PacketID
	{
		PlayerInfoReq = 1,
		PlayerInfoOk = 2,
	}

	public class ServerSession : Session
	{
		public override void OnConnected(EndPoint endPoint)
		{
			Console.WriteLine($"OnConnected : {endPoint}");

			PlayerInfoReq packet = new PlayerInfoReq() { size = 4, packetId = (ushort)PacketID.PlayerInfoReq, playerId = 1001 };

			// 보낸다
			for (int i = 0; i < 5; i++)
			{
				ArraySegment<byte> s = new ArraySegment<byte>(new byte[1024]);

				ushort size = 0;
				bool success = true;

				size += 2;
				success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + size, s.Count - size), packet.packetId);
				size += 2;
				success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset + size, s.Count - size), packet.playerId);
				size += 8;
				success &= BitConverter.TryWriteBytes(new Span<byte>(s.Array, s.Offset, s.Count), size);

				if (success)
					Send(s);
			}
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
