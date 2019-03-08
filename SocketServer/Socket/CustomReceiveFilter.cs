using System;
using System.Linq;

using SuperSocket.SocketBase.Protocol;

namespace SocketServer.Socket
{
    /// <summary>
    /// 数据接收过滤器
    /// </summary>
    public class CustomReceiveFilter : ReceiveFilterBase<CustomRequestInfo>
    {
        private readonly byte[] deviceBeginMark = new byte[] { 0X01 };
        private readonly byte[] cardBeginMark = new byte[] { 0X02 };
        private readonly byte[] cardEndMark = new byte[] { 0X0D, 0X0A, 0X03 };

        public CustomReceiveFilter()
        {
        }

        public override CustomRequestInfo Filter(byte[] readBuffer, int offset, int length, bool toBeCopied, out int rest)
        {
            rest = 0;
            var array = new byte[length];
            Array.Copy(readBuffer, offset, array, 0, length);

            if (length == 6)
            {
                if (array.StartsWith(deviceBeginMark))
                {
                    return new CustomRequestInfo("deviceid", array.Skip(1).Take(5).ToArray());
                }
            }
            else if (length == 14)
            {
                if (array.StartsWith(cardBeginMark) && array.EndsWith(cardEndMark))
                {
                    return new CustomRequestInfo("cardid", array.Skip(1).Take(10).ToArray());
                }
            }
            return null;
        }
    }
}