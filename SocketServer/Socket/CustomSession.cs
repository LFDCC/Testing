using System;
using SuperSocket.SocketBase;

namespace SocketServer.Socket
{
    public class CustomSession : AppSession<CustomSession, CustomRequestInfo>
    {
        protected override void HandleException(Exception e)
        {
            base.HandleException(e);
        }

        protected override void OnSessionStarted()
        {
            base.OnSessionStarted();
        }

        protected override int GetMaxRequestLength()
        {
            return base.GetMaxRequestLength();
        }

        protected override void HandleUnknownRequest(CustomRequestInfo requestInfo)
        {
            base.HandleUnknownRequest(requestInfo);
        }
    }
}