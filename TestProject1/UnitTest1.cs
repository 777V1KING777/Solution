using Kralko;
namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        //для класса Arrays
        //факториал
        [Test]
        public void TestFactorialPlus()
        {
            double fact = ClassArrays.Factorial(4);
            double res = 24;
            Assert.AreEqual(fact, res);
        }
        [Test]
        public void TestFactorialMinus()
        {
            double fact = ClassArrays.Factorial(-4);
            double res = 1;
            Assert.AreEqual(fact, res);
        }
        [Test]
        public void TestFactorialZero()
        {
            double fact = ClassArrays.Factorial(0);
            double res = 1;
            Assert.AreEqual(fact, res);
        }
        //Массив А
        [Test]
        public void TestMassivAPlus()
        {
            double xStart = 0.2;
            double xEnd = 0.8;
            double h = 0.01;
            double eps = 0.00001;
            double[] a = ClassArrays.MassivA(xStart, xEnd, h, eps);
            Console.WriteLine(a.Length);
            for (int i = 0; i < a.Length - 2; i++)
            {
                double x = xStart + i * h;
                double contr = ClassArrays.controlFormula(x);
                string astr = a[i].ToString();
                string contrstr = contr.ToString();
                string streps = eps.ToString();
                Console.WriteLine(a[i] + " " + contr);
                for (int j = 0; j < 3; j++)
                {
                    Assert.AreEqual(astr[j], contrstr[j]);

                }
            }
        }
        [Test]
        public void TestMassivAMinusPlus()
        {
            double xStart = -0.1;
            double xEnd = 0.1;
            double h = 0.01;
            double eps = 0.000001;
            double[] a = ClassArrays.MassivA(xStart, xEnd, h, eps);
            for (int i = 0; i < a.Length; i++)
            {
                double x = xStart + i * h;
                double contr = ClassArrays.controlFormula(x);
                Assert.AreEqual(contr, a[i], eps);
            }
        }

        [Test]
        public void TestMassivAMinus()
        {
            double xStart = -0.9;
            double xEnd = -0.1;
            double h = 0.2;
            double eps = 0.001;
            double[] a = ClassArrays.MassivA(xStart, xEnd, h, eps);
            for (int i = 0; i < a.Length; i++)
            {
                double x = xStart + i * h;
                double contr = ClassArrays.controlFormula(x);
                Console.WriteLine(a[i] + " " + contr);
                Assert.AreEqual(contr, a[i], eps);
            }
        }
        [Test]
        public void TestMassivARavn()
        {
            double xStart = 0.2;
            double xEnd = 0.2;
            double h = 0.1;
            double eps = 0.0001;
            double[] a = ClassArrays.MassivA(xStart, xEnd, h, eps);
            for (int i = 0; i < a.Length; i++)
            {
                double x = xStart + i * h;
                double contr = ClassArrays.controlFormula(x);
                Assert.AreEqual(contr, a[i], eps);
            }
        }
        [Test]
        public void TestMassivAZero()
        {
            double[] a = ClassArrays.MassivA(0.1, 0.7, 0, 0);
            Assert.AreEqual(a, null);
        }
        [Test]
        public void TestMassivAMelk()
        {
            double xStart = 0.12;
            double xEnd = 0.39;
            double h = 0.01;
            double eps = 0.000001;
            double[] a = ClassArrays.MassivA(xStart, xEnd, h, eps);
            for (int i = 0; i < a.Length; i++)
            {
                double x = xStart + i * h;
                double contr = ClassArrays.controlFormula(x);
                Console.WriteLine(a[i] + " " + contr);
                Assert.AreEqual(contr, a[i], eps);
            }
        }
        [Test]
        public void TestMassivAZeroEps()
        {
            double[] a = ClassArrays.MassivA(0.1, 0.7, 0.1, 0);
            Assert.AreEqual(a, null);
        }

        //массив B
        [Test]
        public void TestMassivB()
        {
            double[,] b = ClassArrays.MassivB(0.1, 0.3, 0.1, 0.0001);
            double[,] expected = { { 7, 4, 1 }, { 8, 5, 2 }, { 9, 6, 3 } };
            for (int i = 0; i < expected.GetLength(0); i++)
            {
                for (int j = 0; j < expected.GetLength(1); j++)
                {
                    Console.Write(b[i, j] + " ");
                }
                Console.WriteLine();
            }

            for (int i = 0; i < expected.GetLength(0); i++)
            {
                for (int j = 0; j < expected.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], b[i, j], 1e-10);
                }
            }
        }
        [Test]
        public void TestMassivBMinus()
        {
            double[,] b = ClassArrays.MassivB(-0.3, -0.1, 0.1, 0.0001);
            double[,] expected = { { 7, 4, 1 }, { 8, 5, 2 }, { 9, 6, 3 } };
            for (int i = 0; i < expected.GetLength(0); i++)
            {
                for (int j = 0; j < expected.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], b[i, j], 1e-10);
                }
            }
        }
        [Test]
        public void TestMassivBMinusPlus()
        {
            double[,] b = ClassArrays.MassivB(-0.1, 0.1, 0.1, 0.0001);
            double[,] expected = { { 7, 4, 1 }, { 8, 5, 2 }, { 9, 6, 3 } };
            for (int i = 0; i < expected.GetLength(0); i++)
            {
                for (int j = 0; j < expected.GetLength(1); j++)
                {
                    Assert.AreEqual(expected[i, j], b[i, j], 1e-10);
                }
            }
        }
        [Test]
        public void TestMssivBZero()
        {
            double[,] b = ClassArrays.MassivB(0, 0, 0, 0);
            double[,] expected = null;
            Assert.AreEqual(expected, b);
        }
        [Test]
        public void TestMassivBZeroNoth()
        {
            double[,] b = ClassArrays.MassivB(0, 0, 1, 0.0001);
            double[,] expected = { { 1 } };
            Assert.AreEqual(expected, b);
        }
        [Test]
        public void TestMassivBRavn()
        {
            double[,] b = ClassArrays.MassivB(0.2, 0.2, 0.2, 0.0001);
            double[,] expected = { { 1 } };
            Assert.AreEqual(expected, b);
        }
        [Test]
        public void TsetMassivBBigh()
        {
            double[,] b = ClassArrays.MassivB(0.2, 0.5, 0.6, 0.0001);
            double[] a = ClassArrays.MassivA(0.2, 0.4, 0.6, 0.0001);
            for (int i = 0; i < a.Length; i++) { Console.WriteLine(a[i]); }
            Console.WriteLine(a.Length + "length");
            double[,] expected = { { 1 }};
            Assert.AreEqual(expected, b);
        }
        [Test]
        public void TestMassivBMinush()
        {
            double[,] b = ClassArrays.MassivB(0.2, 0.8, -0.6, 0.0001);
            double[,] expected = null;
            Assert.AreEqual(expected, b);
        }
        //Массив Y
        [Test]
        public void TestMassivY()
        {
            double xStart = 0.2;
            double xEnd = 0.23;
            double h = 0.01;
            double eps = 0.0001;
            double g = 0.05;
            double[] a = ClassArrays.MassivA(xStart, xEnd, h, eps);
            for (int i = 0; i < a.Length; i++) { Console.WriteLine(a[i]); }
            double[,] b = ClassArrays.MassivB(xStart, xEnd, h, eps);
            double[,] btrans = ClassArrays.TransposeMatrix(b);
            double[] c = ClassArrays.CalculateC(btrans, a);
            double[] y = ClassArrays.MassivY(c, g);
            Console.WriteLine(" c:" + c.Length + " y: " + y.Length + " a: " + a.Length);
            int blen0 = b.GetLength(0);
            int blen1 = b.GetLength(1);
            Console.WriteLine("b str" + blen0 + " b stolb" + blen1);
            Console.WriteLine();
            for (int i = 0; i < c.Length; i++) { Console.WriteLine(c[i]); }


            Console.WriteLine();
            for (int i = 0; i < y.Length; i++) { Console.WriteLine(y[i]); }
            int temp = 0;
            for (int i = 0; i < y.Length; i++)
            {
                if (i % 20 == 0) { Assert.AreEqual(y[i], c[temp], eps); temp++; }
            }
            Console.WriteLine();
            Console.WriteLine("y: " + y.Length + "c: " + c.Length);
        }
        [Test]
        public void TestMassivYZerog()
        {
            double xStart = 0.2;
            double xEnd = 0.9;
            double h = 0.1;
            double eps = 0.0001;
            double g = 0;
            double[] a = ClassArrays.MassivA(xStart, xEnd, h, eps);
            double[,] b = ClassArrays.MassivB(xStart, xEnd, h, eps);
            double[,] btrans = ClassArrays.TransposeMatrix(b);
            double[] c = ClassArrays.CalculateC(btrans, a);
            for (int i = 0; i < c.Length; i++) { Console.WriteLine(c[i]); }
            double[] y = ClassArrays.MassivY(c, g);
            Assert.AreEqual(y, null);
        }
        [Test]
        public void TestMassivYMelk()
        {
            double xStart = 0.2;
            double xEnd = 0.6;
            double h = 0.1;
            double eps = 0.0001;
            double g = 0.005;
            double[] a = ClassArrays.MassivA(xStart, xEnd, h, eps);
            double[,] b = ClassArrays.MassivB(xStart, xEnd, h, eps);
            double[,] btrans = ClassArrays.TransposeMatrix(b);
            double[] c = ClassArrays.CalculateC(btrans, a);
            Console.WriteLine("c: " + c.Length);
            double[] y = ClassArrays.MassivY(c, g);
            Console.WriteLine("y: " + y.Length + "c: " + c.Length);
            for (int i = 0; i < c.Length; i++) { Console.WriteLine(c[i]); }
            Console.WriteLine();
            for (int i = 0; i < y.Length; i++) { Console.WriteLine(y[i]); }
            int temp = 0;
            for (int i = 0; i < y.Length; i++)
            {
                if (i % 200 == 0) { Assert.AreEqual(y[i], c[temp], eps); temp++; }
            }
            Console.WriteLine();

        }
        [Test]
        public void TestMassivYMelkDop()
        {
            double xStart = 0.2;
            double xEnd = 0.4;
            double h = 0.1;
            double eps = 0.0001;
            double g = 0.005;
            double[] a = ClassArrays.MassivA(xStart, xEnd, h, eps);
            double[,] b = ClassArrays.MassivB(xStart, xEnd, h, eps);
            double[,] btrans = ClassArrays.TransposeMatrix(b);
            double[] c = ClassArrays.CalculateC(btrans, a);
            double[] y = ClassArrays.MassivY(c, g);
            Console.WriteLine("y: " + y.Length + "c: " + c.Length);
            for (int i = 0; i < c.Length; i++) { Console.WriteLine(c[i]); }
            Console.WriteLine();
            for (int i = 0; i < y.Length; i++) { Console.WriteLine(y[i]); }
            int temp = 0;
            for (int i = 0; i < y.Length; i++)
            {
                if (i % 200 == 0) { Assert.AreEqual(y[i], c[temp], eps); temp++; }
            }
            Console.WriteLine();
        }
        [Test]
        public void TestnewMassivY()
        {
            double xStart = 0.2;
            double xEnd = 0.4;
            double h = 0.1;
            double eps = 0.0001;
            double g = 0.5;
            double[] a = ClassArrays.MassivA(xStart, xEnd, h, eps);
            double[,] b = ClassArrays.MassivB(xStart, xEnd, h, eps);
            double[,] btrans = ClassArrays.TransposeMatrix(b);
            double[] c = ClassArrays.CalculateC(btrans, a);
            double[] y = ClassArrays.MassivY(c, g);
            Console.WriteLine("y: " + y.Length + "c: " + c.Length);
            for (int i = 0; i < c.Length; i++) { Console.WriteLine(c[i]); }
            Console.WriteLine();
            for (int i = 0; i < y.Length; i++) { Console.WriteLine(y[i]); }
            int temp = 0;
            for (int i = 0; i < y.Length; i++)
            {
                if (i % 2 == 0) { Assert.AreEqual(y[i], c[temp], eps); temp++; }
            }
            Console.WriteLine();
        }
        //тесты для сортировки
        [Test]
        public void TestSort()
        {
            double xStart = 0.2;
            double xEnd = 0.4;
            double h = 0.1;
            double eps = 0.0001;
            double g = 0.5;
            double[] a = ClassArrays.MassivA(xStart, xEnd, h, eps);
            double[,] b = ClassArrays.MassivB(xStart, xEnd, h, eps);
            double[,] btrans = ClassArrays.TransposeMatrix(b);
            double[] c = ClassArrays.CalculateC(btrans, a);
            double[] y = ClassArrays.MassivY(c, g);
            double[] y1 = ClassArrays.MassivY(c, g);
            ClassArrays.ShellSort(y1);
            for (int i = 0; i < y.Length; i++)
            {
                Console.WriteLine(y1[i] + " " + y1[i]);
            }
            Assert.AreEqual(y.Length, y1.Length);
        }
        [Test]
        public void TestSortFixZnach()
        {
            double[] a = { 0.3, 0.7, 1.2, -0.001, 7.6, 0.34, 0.4 };
            double[] b = { -0.001, 0.3, 0.34, 0.4, 0.7, 1.2, 7.6 };
            ClassArrays.ShellSort(a);
            CollectionAssert.AreEqual(b, a);
        }
        
    }
}