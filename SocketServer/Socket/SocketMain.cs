using System;
using System.Text;

using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;

namespace SocketServer.Socket
{
    public class SocketMain
    {
        public SocketMain()
        {
            var config = new ServerConfig()
            {
                Name = "SSServer",
                ServerTypeName = "SServer",
                ClearIdleSession = true, //60秒执行一次清理90秒没数据传送的连接
                ClearIdleSessionInterval = 60,
                IdleSessionTimeOut = 90,
                MaxRequestLength = 2048, //最大包长度
                Ip = "0.0.0.0",
                Port = 2018,
                MaxConnectionNumber = 100000
            };

            var appServer = new CustomServer();

            if (!appServer.Setup(config)) //Setup with listening port
            {
                appServer.Logger.Info("socket启动成功！");
            }
            if (!appServer.Start())
            {
                appServer.Logger.Error("socket启动失败！");
            }
            //注册事件
            appServer.NewRequestReceived += new RequestHandler<CustomSession, CustomRequestInfo>(appServer_NewRecivede);
            appServer.SessionClosed += new SessionHandler<CustomSession, CloseReason>(appServer_SessionClose);
            appServer.NewSessionConnected += new SessionHandler<CustomSession>(appServer_NewConnected);
        }

        private void appServer_NewConnected(CustomSession session)
        {
        }

        private void appServer_SessionClose(CustomSession session, CloseReason reason)
        {
            session.Logger.Warn("客户端关闭：" + reason);

            if (session.Items.ContainsKey("deviceid"))
            {
                string deviceid = session.Items["deviceid"]?.ToString();
            }
        }

        private void appServer_NewRecivede(CustomSession session, CustomRequestInfo requestInfo)
        {
            var bytes = requestInfo.Body;
            var key = requestInfo.Key;

            if (key == "deviceid")
            {
                var deviceid = byteToHexStr(bytes);
                session.Items["deviceid"] = deviceid;
            }
            else if (key == "cardid")
            {
                var card = getcardno(bytes);
            }
        }

        public string byteToHexStr(byte[] bytes)
        {
            var returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");//ToString("X2") 为C#中的字符串格式控制符
                }
            }
            return returnStr;
        }

        public string getcardno(byte[] bytes)
        {
            var str = Encoding.ASCII.GetString(bytes);
            var card = Convert.ToInt64(str, 16).ToString();
            var zero = "";
            for (int i = 0; i < (10 - card.Length); i++)
            {
                zero += "0";
            }
            return zero + card;
        }
    }
}