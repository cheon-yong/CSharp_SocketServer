using Google.Protobuf;
using Google.Protobuf.Protocol;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class PacketHandler
{
	public static void S_TestHandler(PacketSession session, IMessage packet)
	{
		S_Test testPacket = (S_Test)packet;
		// TODO


	}

	public static void S_HelloWorldHandler(PacketSession session, IMessage packet)
	{

	}
}
