using System;

namespace VectorCalc.Calculator
{
    public enum Operation : int
    {
        Sum = 0,
        Minus = 1,
        ScalarMultiply = 2,
    }


    public class CalcRequest
    {
        public Operation Operation { get; set; }

        public double[] Vector1 { get; set; }

        public double[] Vector2 { get; set; }


        public static CalcRequest Sum(double[] v1, double[] v2)
        {
            return new CalcRequest { Operation = Operation.Sum, Vector1 = v1, Vector2 = v2 };
        }

        public static CalcRequest Minus(double[] v1, double[] v2)
        {
            return new CalcRequest { Operation = Operation.Minus, Vector1 = v1, Vector2 = v2 };
        }

        public static CalcRequest ScalarMultiply(double[] v1, double[] v2)
        {
            return new CalcRequest { Operation = Operation.ScalarMultiply, Vector1 = v1, Vector2 = v2 };
        }

        public static CalcRequest FromBytes(byte[] bytes)
        {
            var res = new CalcRequest();
            var offset = 0;

            res.Operation = (Operation)BitConverter.ToInt32(bytes, offset); offset += sizeof(int);
            res.Vector1 = new double[BitConverter.ToInt32(bytes, offset)]; offset += sizeof(int);
            res.Vector2 = new double[BitConverter.ToInt32(bytes, offset)]; offset += sizeof(int);
            Buffer.BlockCopy(bytes, offset, res.Vector1, 0, res.Vector1.Length * sizeof(double)); offset += res.Vector1.Length * sizeof(double);
            Buffer.BlockCopy(bytes, offset, res.Vector2, 0, res.Vector2.Length * sizeof(double)); offset += res.Vector2.Length * sizeof(double);

            return res;
        }


        public byte[] GetBytes()
        {
            var res = new byte[3 * sizeof(int) + Vector1.Length * sizeof(double) + Vector2.Length * sizeof(double)];
            var offset = 0;

            Buffer.BlockCopy(BitConverter.GetBytes((int)Operation), 0, res, offset, sizeof(int)); offset += sizeof(int);
            Buffer.BlockCopy(BitConverter.GetBytes(Vector1.Length), 0, res, offset, sizeof(int)); offset += sizeof(int);
            Buffer.BlockCopy(BitConverter.GetBytes(Vector2.Length), 0, res, offset, sizeof(int)); offset += sizeof(int);
            Buffer.BlockCopy(Vector1, 0, res, offset, Vector1.Length * sizeof(double)); offset += Vector1.Length * sizeof(double);
            Buffer.BlockCopy(Vector2, 0, res, offset, Vector2.Length * sizeof(double)); offset += Vector2.Length * sizeof(double);

            return res;
        }
    }
}
