﻿using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ChatRight
{
    public enum Packets
    {
        CONNECT,
        REGISTER
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
                    break;
            }
        }

        public static void SendRegistrationData(string username, string email, string password)
        {
            NetOutgoingMessage outMsg = Client.CreateMessage();
            outMsg.Write((byte)Packets.REGISTER);
            outMsg.Write(username);
            outMsg.Write(email);
            outMsg.Write(EncryptData(password));
            Client.SendMessage(outMsg, NetDeliveryMethod.ReliableOrdered);
        }

        private static string EncryptData(string dataToEncrypt)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(dataToEncrypt);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }
    }
}