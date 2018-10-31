using System;
using VectorCalc.Infrastructure;

namespace VectorCalc.Calculator
{
    public class SimpleVectorCalculator : IVectorCalculator
    {
        private static readonly ILogger _logger = LoggerFactory.GetLogger(typeof(SimpleVectorCalculator).Name);


        public SimpleVectorCalculator()
        {
        }


        public double[] Sum(double[] a, double[] b)
        {
            var res = new double[Math.Max(a.Length, b.Length)];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = double.NaN;
            }

            try
            {
                if (a.Length != b.Length)
                {
                    _logger.Error("Can't sum vectors of different sizes");
                }
                else
                {
                    _logger.Info("Executing sum vectors");
                    for (var i = 0; i < a.Length; i++)
                    {
                        res[i] = a[i] + b[i];
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to sum vectors");
            }

            return res;
        }

        public double[] Minus(double[] a, double[] b)
        {
            var res = new double[Math.Max(a.Length, b.Length)];
            for (var i = 0; i < res.Length; i++)
            {
                res[i] = double.NaN;
            }

            try
            {
                if (a.Length != b.Length)
                {
                    _logger.Error("Can't minus vectors of different sizes");
                }
                else
                {
                    _logger.Info("Executing minus vectors");
                    for (var i = 0; i < a.Length; i++)
                    {
                        res[i] = a[i] - b[i];
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to minus vectors");
            }

            return res;
        }

        public double ScalarMultiply(double[] a, double[] b)
        {
            var res = double.NaN;

            try
            {
                if (a.Length != b.Length)
                {
                    _logger.Error("Can't scalar multiply vectors of different sizes");
                }
                else
                {
                    _logger.Info("Executing scalar multiply vectors");
                    res = 0;
                    for (var i = 0; i < a.Length; i++)
                    {
                        res += a[i] * b[i];
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Failed to scalar multiply vectors");
            }

            return res;
        }
    }
}
