using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ChatRightServer
{
    public enum Packets
    {
        CONNECT,
    }

    public static class NetworkingServer
    {
        private static NetPeer Peer;
        private static NetServer Server;
        private static NetPeerConfiguration Config;

        public static bool IsInitialized;
        private static NetIncomingMessage inc;

        public static void Initialize()
        {
            Config = new NetPeerConfiguration("ChatRight");
            Config.Port = 14242;
            Config.MaximumConnections = 7;
            Config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            Server = new NetServer(Config);
            Server.Start();
            MessageBox.Show("Server started");
            IsInitialized = true;
            Peer = Server;
        }

        public static void Update()
        {
            if (IsInitialized)
            {
                ServerUpdate();
            }
        }

        private static void SendMessage(NetOutgoingMessage outMsg, string exceptName, bool sendToAll = false)
        {
            if (sendToAll)
            {
                Server.SendMessage(outMsg, Server.Connections, NetDeliveryMethod.ReliableOrdered, 0);
            }
            else
            {
                List<NetConnection> Connections = new List<NetConnection>();
                Server.SendMessage(outMsg, Connections, NetDeliveryMethod.ReliableOrdered, 0);
            }
        }

        private static void HandleServerIncomingData()
        {
            HandlePacket((Packets)inc.ReadByte());
        }

        private static void HandlePacket(Packets packet)
        {
            switch (packet)
            {
            }
        }

        private static void ServerUpdate()
        {
            while ((inc = Server.ReadMessage()) != null)
            {
                switch (inc.MessageType)
                {
                    case NetIncomingMessageType.ConnectionApproval:
                        //MessageBox.Show("Connection incloming");
                        if (inc.ReadByte() == (byte)Packets.CONNECT)
                        {
                            inc.SenderConnection.Approve();
                            NetOutgoingMessage outMsg = Server.CreateMessage();
                            outMsg.Write((byte)Packets.CONNECT);
                            Server.SendMessage(outMsg, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered);
                        }
                        break;

                    case NetIncomingMessageType.Data:
                        HandleServerIncomingData();
                        break;
                }
            }
        }
    }
}