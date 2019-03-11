using System;
using CSRedis;
namespace 分区模式
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var csredis = new CSRedisClient(null,
     "127.0.0.1:6379,password=123,defaultDatabase=11",
     "127.0.0.1:6380,password=6380,defaultDatabase=12",
     "127.0.0.1:6381,password=6381,defaultDatabase=13");
            for (var i = 10000000; i > 0; i--)
            {
                var word = $"这是第{i}条数据";
                Console.WriteLine(word);
                csredis.Set("list" + i, word);
            }
            Console.ReadLine();
        }
    }
}
