using System;
using System.Collections.Generic;
using Godot;

public static class Client
{
    public static ENetConnection Connection;
    public static Entities.Player Player;
    public static int PeerId;

    public static void Start()
    {
        EventService.Subscribe<RemoteEvents.CreatePlayer>((evnt) => PlayersService.CreatePlayer(evnt.UserId));
        EventService.Subscribe<RemoteEvents.RemovePlayer>((evnt) => PlayersService.RemovePlayer(evnt.UserId));

        EventService.Subscribe<RemoteEvents.SetupClient>(OnSetupClient);
        if (NetworkService.IsClient())
        {
            Connection = new ENetConnection();
            Error error = Connection.CreateHost(1);
            if (error != default)
            {
                GD.Print($"Client Failed To Start: {error}");
                Connection = null;
                return;
            }
            Connection.ConnectToHost(Server.IP, Server.Port);
            GD.Print("Client Started");
        }
    }

    public static void Process(double delta)
    {
        HandlePackets();
    }

    static void HandlePackets()
    {
        var packetEvent = Connection.Service();
        ENetConnection.EventType eventType = packetEvent[0].As<ENetConnection.EventType>();
        var peer = packetEvent[1].As<ENetPacketPeer>();

        switch (eventType)
        {
            case ENetConnection.EventType.Error:
                GD.PushWarning("Packet Resulted in Unknown Error!");
                break;
            case ENetConnection.EventType.Connect:
                ConnectedToServer();
                break;
            case ENetConnection.EventType.Disconnect:
                DisconnectedFromServer();
                return;
            case ENetConnection.EventType.Receive:
                EventService.Fire(new Events.Network.RecievedPacket(peer.GetPacket()));
                break;
            default:
                break;
        }
    }

    static void DisconnectedFromServer()
    {
        GD.Print("Disconnected From Server");
        EventService.Fire(new Events.Network.DisconnectedFromServer());
    }

    static void ConnectedToServer()
    {
        GD.Print("Connected To Server");
    }

    static void OnSetupClient(RemoteEvents.SetupClient evnt)
    {
        foreach (int playerId in evnt.PlayerIds)
        {
            if (playerId != evnt.UserId)
            {
                PlayersService.CreatePlayer(playerId);
            }
        }

        Entities.Player player = PlayersService.CreatePlayer(evnt.UserId);
        PeerId = evnt.PeerId;
        Player = player;

        EventService.Fire(new Events.Network.ConnectedToServer());
    }
}
