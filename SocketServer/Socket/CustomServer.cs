using System;
using System.Configuration;
using System.Timers;

using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Config;
using SuperSocket.SocketBase.Protocol;

namespace SocketServer.Socket
{
    public class CustomServer : AppServer<CustomSession, CustomRequestInfo>
    {
        private Timer requestTimer = null;

        public CustomServer()
            : base(new DefaultReceiveFilterFactory<CustomReceiveFilter, CustomRequestInfo>())
        {
            if (ConfigurationManager.AppSettings["sendInterval"] != null)
            {
                //定时发送请求压力的报文
                double sendInterval = double.Parse(ConfigurationManager.AppSettings["sendInterval"]);
                requestTimer = new Timer(sendInterval);
                requestTimer.Elapsed += RequestTimer_Elapsed;
                requestTimer.Enabled = true;
                requestTimer.Start();
            }
        }

        private void RequestTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            //发送请求报文
            var sessionList = GetAllSessions();
            //Logger.Error(sessionList);
            foreach (var session in sessionList)
            {
                try
                {
                    session.Send(new byte[] { 1 }, 1, 1);
                }
                catch (Exception ex)
                {
                    //写入日志
                    Logger.Info(ex.Message);
                }
            }
        }

        protected override void OnNewSessionConnected(CustomSession session)
        {
            base.OnNewSessionConnected(session);
        }

        protected override void ExecuteCommand(CustomSession session, CustomRequestInfo requestInfo)
        {
            base.ExecuteCommand(session, requestInfo);
        }

        protected override void OnStarted()
        {
            base.OnStarted();
        }

        protected override bool Setup(IRootConfig rootConfig, IServerConfig config)
        {
            return base.Setup(rootConfig, config);
        }
    }
}