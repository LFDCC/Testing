using System;
using CSRedis;

namespace 生产者
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var csredis = new CSRedisClient("127.0.0.1:6379");

            while (true)
            {
                Console.WriteLine("请输入：");
                var word = Console.ReadLine();
                csredis.RPush("list", word);
                //csredis.LPush("list", word);
            }
        }
    }
}