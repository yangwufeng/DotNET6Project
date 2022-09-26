using Common.Config;
using CSRedis;
using Newtonsoft.Json;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DAL
{
    public class RedisDAL : SqlSugarDAL
    {
        /// <summary>
        /// 使用单例处理
        /// </summary>
        public static CSRedisClient csredis = new CSRedisClient(ConnectionStrings.RedisConnectionStrings.RedisIP);

        //public static CSRedisClient csredis;
        private readonly static object _Obj = new object();
        public RedisDAL(string config)
        {
            if (csredis == null)
            {
                lock (_Obj)
                {
                    if (csredis == null)
                    {
                        csredis = new CSRedisClient(config);
                    }
                }
            }
        }

        #region 字符串(string)

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="keys"></param>
        public static void RedisSet(Dictionary<object, object> keys)
        {
            foreach (var key in keys)
            {
                csredis.Set(key.Key.ToString(), key.Value);
            }
        }

        /// <summary>
        /// 读取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string RedisGet(string key)
        {
            return csredis.Get(key);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool RedisDel(string key)
        {
            return csredis.Del(key) > 0;
        }

        /// <summary>
        ///  数值操作    更新
        /// </summary>
        /// <param name="keys"></param>
        public static void RedisIncrBy(Dictionary<object, object> keys)
        {
            foreach (var key in keys)
            {
                csredis.IncrBy(key.Key.ToString(), (long)key.Value);
            }
        }


        /// <summary>
        ///在指定key的value末尾追加字符串   
        /// </summary>
        /// <param name="keys"></param>
        public static void RedisAppend(Dictionary<object, object> keys)
        {
            foreach (var key in keys)
            {
                csredis.Append(key.Key.ToString(), key.Value);
            }
        }


        /// <summary>
        /// 获取从指定范围所有字符构成的子串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static string RedisGetRange(string key, int start, int end)
        {
            return csredis.GetRange(key, start, end);
        }


        /// <summary>
        /// 用新字符串从指定位置覆写原value（index:4）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void RedisSetRange(string key, int index, object value)
        {
            csredis.SetRange(key, (uint)index, value);
        }
        #endregion

        #region 列表(list)
        /// <summary>
        /// 从右端推入元素
        /// </summary>
        /// <param name="list"></param>
        public static void RedisRPush(string key, params object[] list)
        {
            csredis.RPush(key, list);
        }

        /// <summary>
        /// 从右端弹出元素
        /// </summary>
        /// <param name="key"></param>
        public static string RedisRPop(string key)
        {
            return csredis.RPop(key);
        }

        /// <summary>
        /// 从左端推入元素
        /// </summary>
        /// <param name="list"></param>
        public static void RedisLPush(string key, params object[] list)
        {
            csredis.LPush(key, list);
        }

        /// <summary>
        /// 从左端弹出元素
        /// </summary>
        /// <param name="key"></param>
        public static string RedisLPop(string key)
        {
            return csredis.LPop(key);
        }

        /// <summary>
        /// 遍历链表元素 （start:0,end:-1即可返回所有元素）
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static string[] RedisLRange(string key, int start, int end)
        {
            // 遍历链表元素（start:0,end:-1即可返回所有元素）
            return csredis.LRange(key, start, end);
        }

        /// <summary>
        /// 按索引值获取元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string RedisLIndex(string key, int index)
        {
            // 按索引值获取元素（当索引值大于链表长度，返回空值，不会报错）
            return csredis.LIndex(key, index);
        }
        /// <summary>
        /// 修剪指定范围内的元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static void RedisLTrim(string key, int start, int end)
        {
            // 修剪指定范围内的元素（start: 4,end: 10）
            csredis.LTrim(key, start, end);
        }

        /// <summary>
        /// 将my-list最后一个元素弹出并压入another-list的头部
        /// </summary>
        /// <param name="keys"></param>
        public static string RedisRPopLPush(string source, string destination)
        {
            // 将my-list最后一个元素弹出并压入another-list的头部
            return csredis.RPopLPush(source, destination);
        }

        #endregion

        #region  集合(set)
        /// <summary>
        /// 添加集合数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="list"></param>
        public static void RedisSAdd(string key, params object[] list)
        {
            // 实际上只插入了两个元素("item1","item2")
            csredis.SAdd(key, list);
        }
        /// <summary>
        /// 获取集合数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string[] RedisSMembers(string key)
        {
            return csredis.SMembers(key);
        }

        /// <summary>
        /// 验证是否数据存在
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool RedisSIsMember(string key, string value)
        {
            //  判断元素是否存在
            //    string member = "item1";
            //    Console.WriteLine($"{member}是否存在:{csredis.SIsMember("my-set", member)}"); 
            //// output -> True
            return csredis.SIsMember(key, value);
        }

        /// <summary>
        /// 移除一个，或者多个
        /// </summary>
        /// <param name="key"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool RedisSRem(string key, params object[] list)
        {
            Console.WriteLine($"{key}是否存在:{csredis.SIsMember(key, list)}");
            // output ->  False
            return csredis.SRem(key, list) > 0;
        }


        /// <summary>
        /// 随机移除一个元素,或者多个
        /// </summary>
        /// <param name="key"></param>
        /// <param name="count"></param>
        public static void RedisSPop(string key, int count = 0)
        {
            // 随机移除一个元素，或者多个
            csredis.SPop(key, count);
        }



        /// <summary>
        /// 差集
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string[] RedisSDiff(string a, string b)
        {
            // 差集
            return csredis.SDiff(a, b);
            // output -> "item1", "item3","item4"
        }

        /// <summary>
        /// 交集
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void RedisSInter(string a, string b)
        {
            // 交集
            csredis.SInter(a, b);
            // output -> "item2","item5"
        }

        /// <summary>
        /// 并集
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static void RedisSUnion(string a, string b)
        {
            // 并集
            csredis.SUnion(a, b);
            // output -> "item1","item2","item3","item4","item5","item6","item7"
        }

        #endregion

        #region 散列(hashmap)

        /// <summary>
        /// 向散列添加元素
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public static bool RedisHSet(string key, string field, object value)
        {
            // 向散列添加元素
            //csredis.HSet("ArticleID:10001", "Title", "了解简单的Redis数据结构");
            //csredis.HSet("ArticleID:10001", "Author", "xscape");
            //csredis.HSet("ArticleID:10001", "PublishTime", "2019-01-01");
            return csredis.HSet(key, field, value);
        }
        /// <summary>
        /// 获取散列中元素， field 代表标题
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static string RedisHGet(string key, string field)
        {
            // 根据Key获取散列中的元素
            //csredis.HGet("ArticleID:10001", "Title");
            return csredis.HGet(key, field);
        }

        public static Dictionary<string, string> RedisHGetAll(string key)
        {
            // 获取散列中的所有元素
            //foreach (var item in csredis.HGetAll(key))
            //{
            //    Console.WriteLine(item.Value);
            //}
            return csredis.HGetAll(key);
        }

        public static string[] RedisHMGet(string key, params string[] list)
        {
            //HMGet和HMSet是他们的多参数版本，一次可以处理多个键值对
            //var keys = new string[] { "Title", "Author", "publishTime" };
            //csredis.HMGet("ID:10001", keys);
            return csredis.HMGet(key, list);
        }


        public static void RedisHIncrByt()
        {
            //和处理字符串一样，我们也可以对散列中的值进行自增、自减操作，原理同字符串是一样的
            csredis.HSet("ArticleID:10001", "votes", "257");
            csredis.HIncrBy("ID:10001", "votes", 40);
            // output -> 297
        }

        #endregion

        #region 有序集合

        /// <summary>
        /// 向有序集合添加元素
        /// </summary>
        public static void RedisZAdd(string key, params (decimal, object)[] ps)
        {
            // 向有序集合添加元素
            csredis.ZAdd(key, ps);
        }

        /// <summary>
        /// 返回集合中的元素数量
        /// </summary>
        /// <param name="key"></param>
        public static void RedisZCard(string key)
        {
            //返回集合中的元素数量
            csredis.ZCard(key);
        }

        /// <summary>
        /// 获取集合中指定范围(90~100)的元素集合
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="mix"></param>
        public static void RedisZRangeByScore(string key, decimal min, decimal mix)
        {
            // 获取集合中指定范围(90~100)的元素集合
            csredis.ZRangeByScore(key, min, mix);
        }

        /// <summary>
        /// 获取集合所有元素并升序排序
        /// </summary>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        public static (string, decimal)[] RedisZRangeWithScores(string key, long start, long stop)
        {
            // 获取集合所有元素并升序排序
            return csredis.ZRangeWithScores(key, start, stop);
        }

        /// <summary>
        /// 移除集合中的元素
        /// </summary>
        public static void RedisZRem(string key, params string[] list)
        {
            // 移除集合中的元素
            csredis.ZRem(key, list);
        }

        /// <summary>
        /// Key的过期 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="seconds">秒</param>
        public static void RedisExpire(string key, int seconds)
        {
            //Key的过期
            csredis.Set("MyKey", "hello,world");
            Console.WriteLine(csredis.Get("MyKey"));
            // output -> "hello,world"

            csredis.Expire("MyKey", 5); // key在5秒后过期，也可以使用ExpireAt方法让它在指定时间自动过期

            Thread.Sleep(6000); // 线程暂停6秒
            Console.WriteLine(csredis.Get("MyKey"));
            // output -> ""

        }


        #endregion

        #region 高级玩法：发布订阅

        public static void RedisSubscribe()
        {
            //普通订阅
            csredis.Subscribe(
              ("chan1", msg => Console.WriteLine(msg.Body)),
              ("chan2", msg => Console.WriteLine(msg.Body)));
        }

        public static void RedisPSubscribe(string[] channelPatterns)
        {
            //模式订阅（通配符）
            csredis.PSubscribe(channelPatterns, msg =>
            {
                Console.WriteLine($"PSUB   {msg.MessageId}:{msg.Body}    {msg.Pattern}: chan:{msg.Channel}");
            });
            //模式订阅已经解决的难题：
            //1、分区的节点匹配规则，导致通配符最大可能匹配全部节点，所以全部节点都要订阅
            //2、本组 "test*", "*test001", "test*002" 订阅全部节点时，需要解决同一条消息不可执行多次
        }
        public static void RedisPublish(string key, string msg)
        {
            //发布
            csredis.Publish(key, msg);
            //无论是分区或普通模式，rds.Publish 都可以正常通信
        }


        #endregion

        #region 高级玩法：缓存壳

        public static void RedisssCacheShell()
        {
            //不加缓存的时候，要从数据库查询
            var t1 = DB.Queryable<User>().Where(t => true).ToList();

            //一般的缓存代码，如不封装还挺繁琐的
            var cacheValue = csredis.Get("test1");
            if (!string.IsNullOrEmpty(cacheValue))
            {
                try
                {
                    JsonConvert.DeserializeObject(cacheValue);
                }
                catch
                {
                    //出错时删除key
                    csredis.Del("test1");
                    throw;
                }
            }
            var users = DB.Queryable<User>().Where(t => true).ToList();
            csredis.Set("test1", JsonConvert.SerializeObject(users), 10); //缓存10秒

            //使用缓存壳效果同上，以下示例使用 string 和 hash 缓存数据
            var a = csredis.CacheShell("test1", 10, () => DB.Queryable<User>().Where(t => true).ToList());
            var b = csredis.CacheShell("test", "1", 10, () => DB.Queryable<User>().Where(t => true).ToList());
            var c = csredis.CacheShell("test", new[] { "1", "2" }, 10, notCacheFields => new[] {
            ("1",DB.Queryable<User>().Where(t=>true).ToList()),
            ("2", DB.Queryable<User>().Where(t=>true).ToList())
            });
        }
        #endregion

        #region 高级玩法：管道

        public static void RedisStartPipe()
        {
            var ret1 = csredis.StartPipe().Set("a", "1").Get("a").EndPipe();
            var ret2 = csredis.StartPipe(p => p.Set("a", "1").Get("a"));
            var ret3 = csredis.StartPipe().Get("b").Get("a").Get("a").EndPipe();
            //与 rds.MGet("b", "a", "a") 性能相比，经测试差之毫厘
        }
        #endregion

        #region 高级玩法：多数据库
        /// <summary>
        ///配置多个数据库
        /// </summary>
        public static void RedisConnects()
        {
            var connectionString = "127.0.0.1:6379,password=123,poolsize=10,ssl=false,writeBuffer=10240,prefix=key前辍";
            var redis = new CSRedisClient[14]; //定义成单例
            for (var a = 0; a < redis.Length; a++) redis[a] = new CSRedisClient(connectionString + "; defualtDatabase=" + a);

            //访问数据库1的数据
            redis[1].Get("test1");
        }
        #endregion
    }
}
