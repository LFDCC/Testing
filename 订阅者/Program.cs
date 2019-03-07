using System;
using CSRedis;

namespace 订阅者
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var csredis = new CSRedisClient("127.0.0.1:6379");

            var sub3 = csredis.SubscribeListBroadcast("testchannel", "sub1", msg => Console.WriteLine($"sub1 -> testchannel : {msg}"));
            var sub4 = csredis.SubscribeListBroadcast("testchannel", "sub2", msg => Console.WriteLine($"sub2 -> testchannel : {msg}"));
            var sub5 = csredis.SubscribeListBroadcast("testchannel", "sub3", msg => Console.WriteLine($"sub3 -> testchannel : {msg}"));

            //csredis.Subscribe(
            //    ("testchannel",
            //    msg =>
            //    {
            //        Console.WriteLine(msg.Body);
            //    }
            //)
            //);
            //性能高，但会丢失消息
        }
    }
}
