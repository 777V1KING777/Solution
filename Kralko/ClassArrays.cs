using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace Kralko
{
    public class ClassArrays
    {
        //Заполнение массива С
        public static double[] CalculateC(double[,] arrayB_T, double[] arrayA)
        {
            int n = arrayA.Length;
            double[] C = new double[n];

            // Умножение транспонированной матрицы B^T на вектор A
            for (int i = 0; i < n; i++)
            {
                C[i] = 0;
                for (int j = 0; j < n; j++)
                {
                    C[i] += arrayB_T[i, j] * arrayA[j];
                }
            }
            return C;
        }

        public static double[,] TransposeMatrix(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);
            double[,] transposedMatrix = new double[m, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    transposedMatrix[j, i] = matrix[i, j];
                }
            }
            return transposedMatrix;
        }
       
        // Функция для вычисления интерполированного значения в точке x
        public static double NewtonInterpolation(double[,] diff, double x, double g) 
        {
            double result = diff[0, 0];
            double term = 1;
            double u = x / g;

            for (int j = 1; j < diff.GetLength(0); j++)
            {
                term *= (u - (j - 1)) / j;
                result += term * diff[0, j];
            }
            return result;
        }

  
        //Сортировка массива Y
        public static void ShellSort(double[] array)
        {
            int n = array.Length;

            int gap = n / 2;

            

            while (gap > 0)
            {

                for (int i = gap; i < n; i++)
                {

                    double temp = array[i];

                    int j = i;
                    while (j >= gap && array[j - gap] > temp)
                    {

                        array[j] = array[j - gap];
                        j -= gap;
                    }

                    array[j] = temp;
                }

                gap /= 2;
            }
        }

        
        //факториал
        public static long Factorial(int n)
        {
            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }
        //массив А
        public static double[] MassivA(double xStart, double xEnd, double h, double eps)
        {
            if (h <= 0 || eps <= 0 || xStart > xEnd || xStart < -1 || xEnd > 1)
                return null;

            int arraySize = (int)Math.Round((xEnd - xStart) / h) + 1;
            double[] seriesArray = new double[arraySize];

            for (int i = 0; i < arraySize; i++)
            {
                double x = xStart + i * h;
                if (i == arraySize - 1) x = xEnd;

                double seriesSum = 0;
                double term;
                int n = 0;
                const int maxIterations = 1000;

                do
                {
                    term = Math.Pow(-1, n) * ((2 * n + 1) * Math.Pow(x, 2 * n)) / (Factorial(2 * n + 2));
                    seriesSum += term;
                    n++;

                    if (n > maxIterations)
                        break;
                }
                while (Math.Abs(term) > eps);

                 
                seriesArray[i] = seriesSum;
            }

            return seriesArray;
        }

        // Контрольная формула 
        public static double ControlFormula(double x)
        {
            if (Math.Abs(x) < 1e-10) return 0.5;
            return (x*Math.Sin(x) +  Math.Cos(x) - 1 ) / (x * x);
        }
        //это массив B
        public static double[,] MassivB(double xStart, double xEnd, double h, double eps)
        {
            if (h <= 0 || xStart > xEnd || xStart < -1 || xEnd > 1 || eps <= 0 || h > 1 || eps > 1)
            {
                return null;
            }
            double[] a = MassivA(xStart, xEnd, h, eps);
            if (a == null) return null;
            int razm = a.Length;
            double[,] res = new double[razm, razm];
            int currentValue = 1; 
            for (int col = razm - 1; col >= 0; col--)
            {
                for (int row = 0; row < razm; row++)  
                {
                    res[row, col] = currentValue;
                    currentValue++;
                }
            }
            return res;
        }
        
        //Массив Y 
        public static double[] MassivY(double[] c, double step)
        {
            if (c == null || c.Length == 0 || step <= 0)
            {
                return null;
            }
            double[] xNodes = new double[c.Length];
            for (int i = 0; i < xNodes.Length; i++)
            {
                xNodes[i] = i;
            }
            double[] coeffs = CalculateDividedDifferences(xNodes, c);

            int resultSize = (int)Math.Round((xNodes.Length - 1) / step) + 1;
            double[] result = new double[resultSize];

            for (int i = 0; i < resultSize; i++)
            {
                double x = i * step;
                result[i] = EvaluateNewtonPolynomial(xNodes, coeffs, x);
            }

            return result;
        }

        private static double[] CalculateDividedDifferences(double[] x, double[] y)
        {
            int n = y.Length;
            double[] diffs = new double[n];
            Array.Copy(y, diffs, n);

            for (int j = 1; j < n; j++)
            {
                for (int i = n - 1; i >= j; i--)
                {
                    diffs[i] = (diffs[i] - diffs[i - 1]) / (x[i] - x[i - j]);
                }
            }

            return diffs;
        }
        private static double EvaluateNewtonPolynomial(double[] xNodes, double[] coeffs, double x)
        {
            double result = coeffs[coeffs.Length - 1];

            for (int i = coeffs.Length - 2; i >= 0; i--)
            {
                result = result * (x - xNodes[i]) + coeffs[i];
            }

            return result;
        }

    }
}
