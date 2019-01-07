using Microsoft.Extensions.Logging;
using System;

namespace TorrentCore2_0
{
    public static class LogManager
    {
        private static readonly ILoggerFactory LoggerFactory = new LoggerFactory();

        public static void Configure(Action<ILoggerFactory> factory)
        {
            factory(LoggerFactory);
        }

        public static ILogger<T> GetLogger<T>()
        {
            return LoggerFactory.CreateLogger<T>();
        }
    }
}
