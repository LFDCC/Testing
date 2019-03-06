using NewLife.Agent;

namespace SocketServer
{
    public class AgentService : AgentServiceBase<AgentService>
    {
        #region 构造函数

        /// <summary>实例化一个代理服务</summary>
        public AgentService()
        {
        }

        #endregion 构造函数

        #region 核心

        /// <summary>开始工作</summary>
        /// <param name="reason"></param>
        protected override void StartWork(string reason)
        {
            WriteLog("业务开始……");

            base.StartWork(reason);
        }

        /// <summary>停止服务</summary>
        /// <param name="reason"></param>
        protected override void StopWork(string reason)
        {
            WriteLog("业务结束！");

            base.StopWork(reason);
        }

        #endregion 核心
    }
}