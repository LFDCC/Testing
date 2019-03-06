using SuperSocket.SocketBase.Protocol;

namespace SocketServer.Socket
{
    public class CustomRequestInfo : IRequestInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="body">接收的数据体</param>
        public CustomRequestInfo(string key, byte[] body)
        {
            Key = key;
            Body = body;
        }

        public string Key
        {
            get;
            set;
        }

        /// <summary>
        /// 请求信息缓存
        /// </summary>
        public byte[] Body { get; set; }
    }
}