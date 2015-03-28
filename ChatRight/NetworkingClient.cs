using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ChatRight
{
    public enum Packets
    {
        CONNECT,
    }

    public static class NetworkingClient
    {
        private static NetPeer Peer;
        private static NetClient Client;
        private static NetPeerConfiguration Config;
        public static bool IsHost;
        public static bool IsInitialized;
        private static string hostIp;

        private static NetIncomingMessage inc;

        public static void InitializeClient(string ip)
        {
            Config = new NetPeerConfiguration("ChatRight");
            IsHost = false;
            hostIp = ip;
            Client = new NetClient(Config);
            NetOutgoingMessage outMsg = Client.CreateMessage();
            Client.Start();
            outMsg.Write((byte)Packets.CONNECT);
            Client.Connect(hostIp, 14242, outMsg);
            MessageBox.Show("Client initialized");
            IsInitialized = true;
            Peer = Client;
        }

        public static void Update()
        {
            if (IsInitialized)
            {
                ClientUpdate();
            }
        }

        private static void ClientUpdate()
        {
            while ((inc = Client.ReadMessage()) != null)
            {
                switch (inc.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        HandleClientIncomingData();
                        break;
                }
            }
        }

        private static void HandleClientIncomingData()
        {
            HandlePacket((Packets)inc.ReadByte());
        }

        private static void HandlePacket(Packets packet)
        {
            switch (packet)
            {
                case Packets.CONNECT:
                    MessageBox.Show("Connection Approved");
                    break;
            }
        }
    }
}