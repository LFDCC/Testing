using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Logging;
using SuperSocket.SocketBase.Protocol;

namespace SocketServer.Socket
{
    public class SocketMain
    {
        private ILog log = new Log4NetLogFactory().GetLog("SocketMain");

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
                MaxConnectionNumber = 100000,
            };

            var appServer = new CustomServer();

            if (!appServer.Setup(config)) //Setup with listening port
            {
                log.Info("socket启动成功！");
            }
            if (!appServer.Start())
            {
                log.Error("socket启动失败！");
            }

            //注册事件
            appServer.NewRequestReceived += new RequestHandler<CustomSession, CustomRequestInfo>(appServer_NewRecivede);
            appServer.SessionClosed += new SessionHandler<CustomSession, CloseReason>(appServer_SessionClose);
            //appServer.NewRequestReceived += new RequestHandler<AppSession, StringRequestInfo>(appServer_NewRecivede);
            //appServer.SessionClosed += new SessionHandler<AppSession, SuperSocket.SocketBase.CloseReason>(appServer_SessionClose);
            //appServer.NewSessionConnected += new SessionHandler<AppSession>(appServer_NewConnected);
        }

        private void appServer_SessionClose(AppSession session, CloseReason reason)
        {
        }

        private void appServer_NewRecivede(AppSession session, StringRequestInfo requestInfo)
        {
        }

        private void appServer_NewConnected(AppSession session)
        {
        }

        private void appServer_SessionClose(CustomSession session, CloseReason reason)
        {
            log.Warn("客户端关闭：" + reason);
            string deviceid = session.Items["deviceid"]?.ToString();
        }

        private void appServer_NewRecivede(CustomSession session, CustomRequestInfo requestInfo)
        {
            byte[] bytes = requestInfo.Body;
            if (bytes.Length == 0)
            {
                log.Info("无效请求！");
                return;
            }

            if (bytes.Length == 6)
            {
                var cmd = bytes[0];
                if (cmd == 0X01)
                {
                    var deviceid = byteToHexStr(bytes.Skip(1).Take(5).ToArray());
                    session.Items["deviceid"] = deviceid;
                }
            }
            else if (bytes.Length == 14)
            {
                var stx = bytes[0];
                var cr = bytes[11];
                var lf = bytes[12];
                var etx = bytes[13];
                //ASCII值 0X02正文开始 0X0D正文结束 0X0A换行 0X03回车
                if (stx == 0X02 && cr == 0X0D && lf == 0X0A && etx == 0X03)
                {
                    var card = getcardno(Encoding.ASCII.GetString(bytes.Skip(1).Take(10).ToArray()));
                }
            }
        }

        public string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");//ToString("X2") 为C#中的字符串格式控制符
                }
            }
            return returnStr;
        }

        public string getcardno(string result)
        {
            string card = Convert.ToInt64(result, 16).ToString();
            string zero = "";
            for (int i = 0; i < (10 - card.Length); i++)
            {
                zero += "0";
            }
            return zero + card;
        }
    }
}