using Model;
using Networking;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    class GameSync : UIComponent
    {
        public void SyncGames()
        {
            Networking.NetworkHandler.Instance.ClientMsgReceived += MsgReceived;
            string json = JsonConvert.SerializeObject(Workspace.Instance.CurrentGame);
            Networking.NetworkHandler.Instance.SendMsg(json, Networking.GameCommand.Project);
        }

        private void MsgReceived(string msg)
        {
            UIComponentManager.Instance.AddUIComponent<GameStarterTutorial>().StartSetup();
        }

        public override void Dispose()
        {
            Networking.NetworkHandler.Instance.ClientMsgReceived -= MsgReceived;
            Networking.NetworkHandler.Instance.ServerMsgReceived -= SrvMsgReceived;
        }

        internal void SetAsServer()
        {
            Networking.NetworkHandler.Instance.ServerMsgReceived += SrvMsgReceived;
        }

        private void SrvMsgReceived(string msg, GameCommand cmd)
        {
            if(cmd == GameCommand.Project)
            {
                Workspace.Instance.CreateFromState(msg);
                UIComponentManager.Instance.AddUIComponent<GameUI>().InitServer();
            }
        }
    }
}
