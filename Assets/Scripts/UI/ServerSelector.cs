using Networking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UI.GameList;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    class ServerSelector : MonoBehaviour, IDisposable
    {
        [SerializeField] private RectTransform selector;
        [SerializeField] private Text ipText;
        [SerializeField] private Text instructions;
        [SerializeField] private RectTransform client;
        [SerializeField] private IPHandler ip;
        [SerializeField] private Text ipWarning;
        [SerializeField] private Text connectionSuccessClient;
        [SerializeField] private Text connectionSuccessServer;

        public void OnServer()
        {
            Core.Globals.IsServer = true;
            string ip = NetworkUtilites.GetLocalIPAddress();
            NetworkHandler.Instance.CreateServer(ip);
            NetworkHandler.Instance.ServerMsgReceived += ServerMsgReceived;
            selector.gameObject.SetActive(false);
            instructions.gameObject.SetActive(true);
            ipText.text = ip;
        }

        public void OnClient()
        {
            Core.Globals.IsServer = false;
            NetworkHandler.Instance.ClientMsgReceived += ClientMsgReceived;
            NetworkHandler.Instance.CreateClient();
            selector.gameObject.SetActive(false);
            client.gameObject.SetActive(true);
        }

        public void OnStart()
        {
            string address = ip.GetIPFromInput();
            IPAddress outip;
            if (string.IsNullOrEmpty(address) || !IPAddress.TryParse(address, out outip))
            {
                ShowIPWarning(true);
            }
            else
            {
                ShowIPWarning(false);
                NetworkHandler.Instance.ServerIP = outip;
                NetworkHandler.Instance.SendMsg("anything", GameCommand.Dummy);
            }
        }

        private void ServerMsgReceived(string msg, GameCommand cmd)
        {
            connectionSuccessServer.gameObject.SetActive(true);
            UIComponentManager.Instance.AddUIComponent<GameSync>().SetAsServer();
        }

        private void ClientMsgReceived(string msg)
        {
            connectionSuccessClient.gameObject.SetActive(true);
            UIComponentManager.Instance.AddUIComponent<PreviousGamesSelector>().LoadList();
        }


        private void ShowIPWarning(bool show)
        {
            ipWarning.gameObject.SetActive(show);
        }

        public void Dispose()
        {
            NetworkHandler.Instance.ClientMsgReceived -= ClientMsgReceived;
            NetworkHandler.Instance.ServerMsgReceived -= ServerMsgReceived;
        }
    }
}
