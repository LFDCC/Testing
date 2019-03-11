using System;
using System.Threading;
using CSRedis;
namespace Cache
{
    internal class Program
    {
        private static void Main(string[] args)
        {

            var csredis = new CSRedisClient("127.0.0.1:6379,password=123,defaultDatabase=1,poolsize=50,ssl=false,writeBuffer=10240");

            csredis.Set("name", "张三");//设置值。默认永不过期
            Console.WriteLine(csredis.Get("name"));

            csredis.Set("time", DateTime.Now, 1);
            Console.WriteLine(csredis.Get<DateTime>("time"));
            Thread.Sleep(1100);
            Console.WriteLine(csredis.Get<DateTime>("time"));

            // 列表
            csredis.RPush("list", "第一个元素");
            csredis.RPush("list", "第二个元素");
            csredis.LInsertBefore("list", "第二个元素", "我是新插入的第二个元素！");
            Console.WriteLine($"list的长度为{csredis.LLen("list")}");

            Console.WriteLine($"list的第二个元素为{csredis.LIndex("list", 1)}");
            // 哈希
            csredis.HSet("person", "name", "zhulei");
            csredis.HSet("person", "sex", "男");
            csredis.HSet("person", "age", "28");
            csredis.HSet("person", "adress", "hefei");
            Console.WriteLine($"person这个哈希中的age为{csredis.HGet<int>("person", "age")}");

            // 集合
            csredis.SAdd("students", "zhangsan", "lisi");
            csredis.SAdd("students", "wangwu");
            csredis.SAdd("students", "zhaoliu");
            Console.WriteLine($"students这个集合的大小为{csredis.SCard("students")}");
            Console.WriteLine($"students这个集合是否包含wagnwu:{csredis.SIsMember("students", "wangwu")}");

            //有序集合
            csredis.ZAdd("socre", (1, "张三"), (2, "李四"));
            csredis.ZCard("score");
            csredis.ZCount("score", 1, 3);
            
            Console.ReadLine();
        }
    }
}
