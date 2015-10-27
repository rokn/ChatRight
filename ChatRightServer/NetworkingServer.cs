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
        REGISTER,
        ACTIVATION,
        LOGIN,
        SENDMESSAGE
    }

    //public struct User
    //{
    //    public int Id;
    //    public NetConnection connection;
    //}

    public static class NetworkingServer
    {
        private static NetServer Server;
        private static NetPeerConfiguration Config;

        public static bool IsInitialized;
        private static NetIncomingMessage inc;

        public static void Initialize()
        {
            Config = new NetPeerConfiguration("ChatRight");
            Config.Port = 14242;
            Config.MaximumConnections = int.MaxValue;
            Config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            Server = new NetServer(Config);
            Server.Start();
            IsInitialized = true;
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
            NetOutgoingMessage outMsg = Server.CreateMessage();
            switch (packet)
            {
                case Packets.REGISTER:
                    bool registrationSuccessful = ServerForm.UserRegister(inc.ReadString(), inc.ReadString(), inc.ReadString());
                    outMsg.Write((byte)Packets.REGISTER);
                    outMsg.Write(registrationSuccessful);
                    Server.SendMessage(outMsg, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered);
                    ServerForm.UpdateDatabase();
                    break;

                case Packets.ACTIVATION:
                    bool codeActivated = ServerForm.CheckActivationCode(inc.ReadString(), inc.ReadString());
                    outMsg.Write((byte)Packets.ACTIVATION);
                    outMsg.Write(codeActivated);
                    Server.SendMessage(outMsg, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered);
                    ServerForm.UpdateDatabase();
                    break;

                case Packets.LOGIN:
                    int logInResult = ServerForm.CheckLoginStatus(inc.ReadString(), inc.ReadString()); // -1 Wrong, 0 Right, 1 Right but not activated
                    outMsg.Write((byte)Packets.LOGIN);
                    outMsg.Write(logInResult);
                    Server.SendMessage(outMsg, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered);
                    break;

                case Packets.SENDMESSAGE:
                    outMsg.Write((byte)Packets.SENDMESSAGE);
                    outMsg.Write(inc.ReadString());
                    outMsg.Write(inc.ReadString());
                    Server.SendMessage(outMsg, Server.Connections, NetDeliveryMethod.ReliableOrdered, 0);
                    break;
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