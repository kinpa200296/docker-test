using System;

namespace VectorCalc.Infrastructure
{
    public static class LoggerFactory
    {
        private static Func<string, ILogger> _loggerProvider;


        static LoggerFactory()
        {
            _loggerProvider = GetStubLogger;
        }


        public static void SetProvider(Func<string, ILogger> loggerProvider)
        {
            _loggerProvider = loggerProvider;
        }

        public static ILogger GetLogger(string name)
        {
            return _loggerProvider(name);
        }


        private static ILogger GetStubLogger(string name)
        {
            return new StubLogger();
        }
    }
}
