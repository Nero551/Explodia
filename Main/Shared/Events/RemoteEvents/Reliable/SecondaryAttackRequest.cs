using System;
using Godot;

namespace RemoteEvents;

public class SecondaryAttackRequest : RemoteEvent
{

	public override int Flag => (int)ENetPacketPeer.FlagReliable;

	public override byte[] Encode()
	{
		base.Encode();
		//* Writing Here
		return CreateBytesArray();
	}

	public override void Decode()
	{
		base.Decode();

		//* Reading Here
	}
}


