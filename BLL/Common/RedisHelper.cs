using Microsoft.EntityFrameworkCore.Storage;
using StackExchange.Redis;

namespace BLL.Common
{
    public static class RedisHelper
    {
        private static readonly Lazy<StackExchange.Redis.ConnectionMultiplexer> lazyConnection = new(() =>
        {
            var config = Environment.GetEnvironmentVariable("REDIS_CONNECTION") ?? "localhost:6379";
            return StackExchange.Redis.ConnectionMultiplexer.Connect(config);
        });

        public static StackExchange.Redis.ConnectionMultiplexer Connection => lazyConnection.Value;

        public static StackExchange.Redis.IDatabase GetDatabase() => Connection.GetDatabase();
    }
}
