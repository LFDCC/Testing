using System;

using CSRedis;

namespace 消费者
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var csredis = new CSRedisClient("127.0.0.1:6379");

            //while (true)
            //{
            //    Console.WriteLine(csredis.BRPop(60, "list"));

            //}
            csredis.SubscribeList("list", t =>
            {
                Console.WriteLine($"客户端1：{t}");
            });
            csredis.SubscribeList("list", t =>
            {
                Console.WriteLine($"客户端2：{t}");
            });
            csredis.SubscribeList("list", t =>
            {
                Console.WriteLine($"客户端3：{t}");
            });
            csredis.SubscribeList("list", t =>
            {
                Console.WriteLine($"客户端4：{t}");
            });
        }
    }
}