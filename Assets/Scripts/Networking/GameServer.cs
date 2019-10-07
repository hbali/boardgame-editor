using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Networking
{
    public class GameServer
    {
        public GameMsgReceivedCommand MsgReceived { get; set; }

        private HttpListener listener;
        
        private SynchronizationContext mainThreadContext;
        private string ip;
        private int port;

        public GameServer(string ip, int port)
        {
            this.ip = ip;
            this.port = port;
        }

        public void Initialize()
        {
            mainThreadContext = SynchronizationContext.Current;
            IPAddress serverAddress = IPAddress.Parse(ip);

            listener = new HttpListener();
            listener.Prefixes.Add("http://*:" + port + "/");
            listener.Start();
            listener.BeginGetContext(ProcessRequest, listener);
        }
        
        private void ProcessRequest(IAsyncResult result)
        {
            Task t = Task.Run(() =>
            {
                HttpListener listener = (HttpListener)result.AsyncState;
                HttpListenerContext context = listener.EndGetContext(result);
                HttpListenerRequest request = context.Request;

                try
                {
                    string cmd = request.QueryString["cmd"];
                    GameCommand command = (GameCommand)Enum.Parse(typeof(GameCommand), cmd);
                    using (Stream s = context.Request.InputStream)
                    {
                        using (StreamReader reader = new System.IO.StreamReader(s, request.ContentEncoding))
                        {
                            string text = reader.ReadToEnd();
                            mainThreadContext.Post((notused) => MsgReceived?.Invoke(text, command), null);
                        }
                    }
                }
                catch(KeyNotFoundException e)
                {

                }

               

                string responseString = "OK";
                byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                context.Response.StatusCode = 200;
                context.Response.ContentType = "json";
                context.Response.ContentLength64 = buffer.Length;

                using (Stream output = context.Response.OutputStream)
                {
                    output.Write(buffer, 0, buffer.Length);
                }
                listener.EndGetContext(result);
            });
            
            listener.BeginGetContext(ProcessRequest, listener);
        }

    }
}