using System;

namespace VectorCalc.Calculator
{
    public class CalcResult
    {
        public double[] Result { get; set; }


        public static CalcResult From(double[] res)
        {
            return new CalcResult { Result = res };
        }

        public static CalcResult From(double res)
        {
            return new CalcResult { Result = new double[] { res } };
        }

        public static CalcResult FromBytes(byte[] bytes)
        {
            var res = new CalcResult();
            var offset = 0;

            res.Result = new double[BitConverter.ToInt32(bytes, offset)]; offset += sizeof(int);
            Buffer.BlockCopy(bytes, offset, res.Result, 0, res.Result.Length * sizeof(double)); offset += res.Result.Length * sizeof(double);

            return res;
        }


        public byte[] GetBytes()
        {
            var res = new byte[sizeof(int) + Result.Length * sizeof(double)];
            var offset = 0;

            Buffer.BlockCopy(BitConverter.GetBytes(Result.Length), 0, res, offset, sizeof(int)); offset += sizeof(int);
            Buffer.BlockCopy(Result, 0, res, offset, Result.Length * sizeof(double)); offset += Result.Length * sizeof(double);

            return res;
        }
    }
}
