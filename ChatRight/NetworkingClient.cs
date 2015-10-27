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
        REGISTER,
        ACTIVATION,
        LOGIN,
        SENDMESSAGE
    }

    public static class NetworkingClient
    {
        private static NetPeer Peer;
        private static NetClient Client;
        private static NetPeerConfiguration Config;
        public static bool IsHost;
        public static bool IsInitialized;
        private static bool IsConnected;
        private static string hostIp;
        private static Timer connectionTimer;

        private static NetIncomingMessage inc;

        public static void InitializeClient(string ip)
        {
            Config = new NetPeerConfiguration("ChatRight");
            IsHost = false;
            hostIp = ip;
            Client = new NetClient(Config);
            Client.Start();
            SendConnectionMessage();
            IsInitialized = true;
            IsConnected = false;
            Peer = Client;
            connectionTimer = new Timer();
            connectionTimer.Tick += connectionTimer_Tick;
            connectionTimer.Interval = 100;
            connectionTimer.Start();
        }

        private static void connectionTimer_Tick(object sender, EventArgs e)
        {
            if (!IsConnected)
                SendConnectionMessage();
        }

        private static void SendConnectionMessage()
        {
            NetOutgoingMessage outMsg = Client.CreateMessage();
            outMsg.Write((byte)Packets.CONNECT);
            Client.Connect(hostIp, 14242, outMsg);
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
                    if (!IsConnected)
                    {
                        IsConnected = true;
                        connectionTimer.Stop();
                    }

                    break;

                case Packets.REGISTER:
                    if (inc.ReadBoolean())
                    {
                        MainForm.ChangeMenu(MenuType.Activation);
                    }
                    else
                    {
                        MessageBox.Show("Registration failed!");
                    }
                    break;

                case Packets.ACTIVATION:
                    if (inc.ReadBoolean())
                    {
                        MainForm.ChangeMenu(MenuType.MainScreen);
                    }
                    else
                    {
                        MessageBox.Show("Activation failed!");
                    }
                    break;

                case Packets.LOGIN:
                    switch (inc.ReadInt32())
                    {
                        case -1:
                            MessageBox.Show("Invalid username or password");
                            break;

                        case 0:
                            MainForm.ChangeMenu(MenuType.MainScreen);
                            break;

                        case 1:
                            MainForm.ChangeMenu(MenuType.Activation);
                            break;
                    }
                    break;

                case Packets.SENDMESSAGE:
                    MainForm.RecieveMessage(inc.ReadString(), inc.ReadString());
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

        public static void SendActivationCode(string code)
        {
            NetOutgoingMessage outMsg = Client.CreateMessage();
            outMsg.Write((byte)Packets.ACTIVATION);
            outMsg.Write(MainForm.UserName);
            outMsg.Write(code);
            Client.SendMessage(outMsg, NetDeliveryMethod.ReliableOrdered);
        }

        private static string EncryptData(string dataToEncrypt)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(dataToEncrypt);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            return System.Text.Encoding.ASCII.GetString(data);
        }

        public static void SendLoginData(string username, string password)
        {
            NetOutgoingMessage outMsg = Client.CreateMessage();
            outMsg.Write((byte)Packets.LOGIN);
            outMsg.Write(username);
            outMsg.Write(EncryptData(password));
            Client.SendMessage(outMsg, NetDeliveryMethod.ReliableOrdered);
        }

        public static void SendChatMessage(string username, string message)
        {
            NetOutgoingMessage outMsg = Client.CreateMessage();
            outMsg.Write((byte)Packets.SENDMESSAGE);
            outMsg.Write(username);
            outMsg.Write(message);
            Client.SendMessage(outMsg, NetDeliveryMethod.ReliableOrdered);
        }
    }
}