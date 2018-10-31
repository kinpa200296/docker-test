namespace VectorCalc.Calculator
{
    public interface IVectorCalculator
    {
        double[] Sum(double[] a, double[] b);

        double[] Minus(double[] a, double[] b);

        double ScalarMultiply(double[] a, double[] b);
    }
}
