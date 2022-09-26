//See https://aka.ms/new-console-template for more information

using Repository.DAL;
using Service;

try
{
    #region 数据库同步

    SyncStructureSevice.FreeSqlSyncTable();

    #endregion

    #region Redis测试
    {
        new RedisDAL("127.0.0.1:6379");

        #region get del  set 
        Console.WriteLine(RedisDAL.RedisGet("yang"));

        RedisDAL.RedisDel("yang");

        Dictionary<object, object> keys = new Dictionary<object, object>();

        for (int i = 0; i < 10; i++)
        {
            //keys.Add(i,i);
            RedisDAL.RedisDel(i.ToString());
        }
        RedisDAL.RedisSet(keys);
        for (int i = 0; i < 10; i++)
        {
            Console.WriteLine(RedisDAL.RedisGet(i.ToString()));
        }
        #endregion

        #region Hash

        RedisDAL.RedisHSet("yang", "HH", "ll");

        Console.WriteLine(RedisDAL.RedisHGet("yang", "HH"));


        foreach (var item in RedisDAL.RedisHGetAll("yang"))
        {
            Console.WriteLine(item.Key + item.Value);
        }


        #endregion


        #region List（列表）

        RedisDAL.RedisLPush("yang", 1, 2, 4, 5, 6, 7, 8, 9, 0);
        foreach (var item in RedisDAL.RedisLRange("yang", 0, -1))
        {
            Console.WriteLine(item);
        }
        //Console.WriteLine("************************************");
        //RedisDAL.RedisRPush("feng", 1, 2, 4, 5, 6, 7, 8, 9, 0);

        //foreach (var item in RedisDAL.RedisLRange("feng", 0, -1))
        //{
        //    Console.WriteLine(item);
        //}
        Console.WriteLine("************************************");
        Console.WriteLine(RedisDAL.RedisLIndex("yang", 3));
        Console.WriteLine("************************************");
        Console.WriteLine(RedisDAL.RedisLIndex("feng", 3));
        #endregion

        #region 集合(set)

        RedisDAL.RedisSAdd("a", 1, 1, 1, 1, 1, 12, 22, 3, 35, 68, 1545, 45, 45856, 4161, 846, 1465, 974, 84, 64894, 984, 494, 984, 98446, 8946548, 8946, 46446, 44894, 54984, 2165465, 6541651, 3231, 55415, 1251, 5);

        foreach (var item in RedisDAL.RedisSMembers("a"))
        {
            Console.WriteLine(item);
        }
        Console.WriteLine(RedisDAL.RedisSIsMember("a", 1.ToString().ToString()));
        #endregion

        #region zset(sorted set：有序集合)
        RedisDAL.RedisZAdd("b", (1, 22), (2, 33), (3, 44));

        foreach (var s in RedisDAL.RedisZRangeWithScores("b", 0, -1))
        {
            Console.WriteLine(s.Item1, s.Item2);
        }
        #endregion
    }
    #endregion


    Console.Read();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
