using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Networking
{
    public delegate void GameMsgReceived(string msg);

    public delegate void GameMsgReceivedCommand(string msg, GameCommand cmd);

    class NetworkHandler : Singleton<NetworkHandler>
    {
        private const int port = 13000;

        public GameMsgReceivedCommand ServerMsgReceived { get; set; }
        public GameMsgReceived ClientMsgReceived { get; set; }

        private GameServer server;
        private GameClient client;

        public IPAddress ServerIP { get; set; }

        public void CreateServer(string ip)
        {
            server = new GameServer(ip, port);
            server.Initialize();
            server.MsgReceived += ServerReceivedMessage;
        }

        public void CreateClient()
        {
            client = new GameClient(port);
            client.MsgReceived += ClientReceivedMessage;
        }


        private void ClientReceivedMessage(string msg)
        {
            ClientMsgReceived?.Invoke(msg);
        }

        private void ServerReceivedMessage(string msg, GameCommand cmd)
        {
            ServerMsgReceived?.Invoke(msg, cmd);
        }
               

        public void SendMsg(string msg, GameCommand cmd)
        {
            client?.Send(ServerIP.ToString(), msg, cmd);
        }

    }
}
