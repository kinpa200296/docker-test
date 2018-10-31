using System;

namespace VectorCalc.Infrastructure
{
    public class StubLogger : ILogger
    {
        public void Error(string message)
        {
        }

        public void Error(Exception ex, string message)
        {
        }

        public void Info(string message)
        {
        }

        public void Info(Exception ex, string message)
        {
        }
    }
}
