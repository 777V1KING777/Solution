using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kralko
{
    internal class Mathematic
    {
        public static double CalcSeries(double x, double epsilon)
        {
           

            double Sum = 0;
            double SeriesElement;
            int i = 0;
            SeriesElement = ((2 * i + 1) * Math.Pow(x, 2 * i)) / Factorial(2 * i + 2);
            SeriesElement *= (i % 2 == 0) ? 1 : -1;
            while (Math.Abs(SeriesElement) > epsilon)
            {
                Sum += SeriesElement;
                i++; SeriesElement = ((2 * i + 1) * Math.Pow(x, 2 * i)) / Factorial(2 * i + 2);
                SeriesElement *= (i % 2 == 0) ? 1 : -1;
            }
            return Sum;
        }

        public static double Factorial(int n)
        {
            if (n == 0 || n == 1) return 1; double result = 1;
            for (int j = 2; j < n; j++)
            {
                result *= j;
            }
            return result;
        }

        public static double CalcControlFormule(double x, double epsilon)
        {
            double numerator = 1 - Math.Cos(x) - x * Math.Sin(x);
            double denominator = x * x;
            if (denominator == 0)
            {
                Console.WriteLine("Ошибка: деление на ноль!");

            }

            double result = numerator / denominator + 0.5;
            return result;

        }
    }
}
