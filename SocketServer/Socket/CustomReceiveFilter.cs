using System.Text;

namespace SocketServer.Socket
{
    /// <summary>
    /// 数据接收过滤器
    /// </summary>
    public class CustomReceiveFilter : CustomReceiveFilterHelper<CustomRequestInfo>
    {
        public CustomReceiveFilter()
       : base()
        {
        }

        /// <summary>
        /// 重写方法
        /// </summary>
        /// <param name="readBuffer">过滤之后的数据缓存</param>
        /// <param name="offset">数据起始位置</param>
        /// <param name="length">数据缓存长度</param>
        /// <returns></returns>
        protected override CustomRequestInfo ProcessMatchedRequest(byte[] readBuffer, int offset, int length)
        {
            //返回构造函数指定的数据格式
            return new CustomRequestInfo(Encoding.UTF8.GetString(readBuffer, offset, length), readBuffer);
        }
    }
}