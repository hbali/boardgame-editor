using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Networking
{
    public enum GameCommand
    {
        Project,
        GameState,
        Dummy
    }

    public class GameClient
    {
        public GameMsgReceived MsgReceived { get; set; }

        private int port;
        private SynchronizationContext mainThreadContext;

        public GameClient(int port)
        {
            this.port = port;
            mainThreadContext = System.Threading.SynchronizationContext.Current;
        }

        public void Send(String server, String message, GameCommand cmd)
        {
            string url = "http://" + server + ":" + port + "/?cmd=" + cmd.ToString();

            SendPost(message, url).ContinueWith((task) =>
            {
                if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted)
                {
                    MsgReceived?.Invoke(task.Result);
                }
                else
                {
                    //error
                }
            }, TaskContinuationOptions.ExecuteSynchronously);
        }

        private async Task<string> SendPost(string data, string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var result = await client.PostAsync("", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                return resultContent;
            }
        }
    }
}