using System;

namespace VectorCalc.Infrastructure
{
    public interface ILogger
    {
        void Info(string message);

        void Info(Exception ex, string message);

        void Error(string message);

        void Error(Exception ex, string message);
    }
}
