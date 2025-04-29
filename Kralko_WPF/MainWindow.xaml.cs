using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Kralko;
using OxyPlot;
using static System.Net.Mime.MediaTypeNames;


namespace Kralko_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private double _xStart;
        private double _xEnd;
        private double _h;
        private double _eps;
        private double _g;
        double[] _y;
        double [] _a;
        double [,] _b;
        double [] _c;

       
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtXStart.Text.ToString().Length == 0 || txtXEnd.Text.ToString().Length == 0 || txtH.Text.ToString().Length == 0 || txtEpsilon.Text.ToString().Length == 0 || txtG.Text.ToString().Length == 0)
            {
                MessageBox.Show("Сначала введите значения");
                return;
            }
            if (!double.TryParse(txtXStart.Text.ToString(), out var parsedNumber) || !double.TryParse(txtXEnd.Text.ToString(), out var parsedNumber1) ||
                !double.TryParse(txtH.Text.ToString(), out var parsedNumber2) || !double.TryParse(txtEpsilon.Text.ToString(), out var parsedNumber3) ||
                !double.TryParse(txtG.Text.ToString(), out var parsedNumber4))
            {
                MessageBox.Show("Не число!");
                return;
            }

            double ixStart = double.Parse(txtXStart.Text);
            _xStart= ixStart;
            double ixEnd = double.Parse(txtXEnd.Text);
            _xEnd= ixEnd;
            double ih = double.Parse(txtH.Text);
            _h = ih;
            double ieps = double.Parse(txtEpsilon.Text);
            _eps = ieps;
            double ig = double.Parse(txtG.Text);
            _g = ig;

            double[] a = ClassArrays.MassivA(ixStart, ixEnd, ih, ieps);
            _a= a;  
            double[,] b = ClassArrays.MassivB(ixStart, ixEnd, ih, ieps);
            _b= b;
            double[,] btrans = ClassArrays.TransposeMatrix(b);
            double[] c = ClassArrays.CalculateC(btrans, a);
            _c= c;
            double[] y = ClassArrays.MassivY(c, ig);
            _y = y;
            double[] ysort = ClassArrays.MassivY(c, ig);
            ClassArrays.ShellSort(ysort);
            int dop = 0;
            if (a == null || b == null || c == null || y == null || ysort == null)
            {
                MessageBox.Show("вы ввели что-то некорректное");
                return;
            }
            double[] con = new double[a.Length];
            for (int i = 0; i < a.Length; i++) 
            {
                double x = ixStart + i * ih;
                con[i] = ClassArrays.ControlFormula(x);
            }
            string[] stra1 = new string[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                string temp = "";
                string strai = a[i].ToString();
                int count = 0;
                while (count < txtEpsilon.Text.Length)
                {
                    if (count >= strai.Length)
                    {
                        break;
                    }
                    else
                    {
                        temp += strai[count];
                        count++;
                    }
                }
                stra1[i] = temp;
                temp = "";
            }
            string stra = string.Join("  ", stra1);
            txtArrayA.Text = " ";
            txtArrayA.Text = stra;
            contrmas.Text = " ";
            string strcontrl = string.Join(" ", con);
            contrmas.Text = strcontrl;
            var listb = new List<string>();
            for (int i = 0; i < a.Length; i++)
            {
                string temp = " ";
                for (int j = 0; j < a.Length; j++)
                {
                    temp += b[i, j] + "\t";
                }
                listb.Add(temp);
            }
            txtArrayB.Text = " ";
            for (int i = 0; i < listb.Count; i++)
            {
                txtArrayB.Text += listb[i] + Environment.NewLine;
            }
            string strc = string.Join("  ", c);
            txtArrayC.Text = " ";
            txtArrayC.Text = strc;
            string stry = string.Join("  ", y);
            txtArrayY.Text = " ";
            txtArrayY.Text = stry;
            string strysort = string.Join("  ", ysort);
            sortmas.Text = strysort;
            WriteResultsToFile(a, con, c, y);

        }
        public void ButtonOchistClick(object sender, RoutedEventArgs e)
        {
            txtXStart.Text = "";
            txtXEnd.Text = "";
            txtH.Text = "";
            txtEpsilon.Text = "";
            txtG.Text = "";
            txtArrayA.Text = "";
            txtArrayB.Text = "";
            txtArrayC.Text = "";
            txtArrayY.Text = "";
            sortmas.Text = "";
            contrmas.Text = "";
        }

        // Обработчик для кнопки "Контрольный пример"
        private void ControlExample_Click(object sender, RoutedEventArgs e)
        {
            txtXStart.Text = "0";
            txtXEnd.Text = "1";
            txtH.Text = "0,1";
            txtEpsilon.Text = "0,0001";
            txtG.Text = "0,1";
        }

        // Функция для преобразования матрицы в строку для вывода
        private string MatrixToString(double[,] matrix)
        {
            string result = "";
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    result += matrix[i, j].ToString() + "\t";
                }
                result += Environment.NewLine;
            }
            return result;
        }

        // Функция для записи результатов в файл
        private void WriteResultsToFile(double[] arrayA, double[] controlSums, double[] C, double[] Y)
        {
            string path = "results.txt"; // Путь к файлу
            using (StreamWriter sw = new StreamWriter(path, append: false))
            {
                sw.WriteLine("Массив A (CalcSeries):");
                sw.WriteLine(string.Join(", ", arrayA));
                sw.WriteLine();

                sw.WriteLine("Массив контрольных сумм (ControlSum):");
                sw.WriteLine(string.Join(", ", controlSums));
                sw.WriteLine();

                if (C != null)
                {
                    sw.WriteLine("Результат C (B^T * A):");
                    sw.WriteLine(string.Join(", ", C));
                    sw.WriteLine();
                }

                sw.WriteLine("Массив Y (Интерполяция):");
                sw.WriteLine(string.Join(", ", Y));
                sw.WriteLine();
            }
        }
       

        private void GoToSecondForm_Click(object sender, RoutedEventArgs e)
        {
            
            if (txtXStart.Text.ToString().Length == 0 || txtXEnd.Text.ToString().Length == 0 || txtH.Text.ToString().Length == 0 || txtEpsilon.Text.ToString().Length == 0 || txtG.Text.ToString().Length == 0||txtArrayA.Text.ToString().Length==0)
            {
                MessageBox.Show("Вы не создали массивы!");
                return;
            }
            else
            {
                GraphWindow graphWindow = new GraphWindow(_y, _xStart, _xEnd, _h, _eps, _g, _a, _b, _c) { WindowStartupLocation = WindowStartupLocation, Left = Left, Top = Top, Height = Height, Width = Width };
                graphWindow.Show();
                this.Close();
            }
        }
        private void GoToOutputForm_Click(object sender, RoutedEventArgs e)
        {
            if (txtXStart.Text.ToString().Length == 0 || txtXEnd.Text.ToString().Length == 0 || txtH.Text.ToString().Length == 0 || txtEpsilon.Text.ToString().Length == 0 || txtG.Text.ToString().Length == 0)
            {
                MessageBox.Show("Вы не создали массивы!");
                return;
            }
            else
            {
                OutputWindow outputWindow = new OutputWindow(_y, _xStart, _xEnd, _h, _eps, _g, _a, _b, _c)
                {
                    WindowStartupLocation = WindowStartupLocation,
                    Left = Left,
                    Top = Top,
                    Height = Height,
                    Width = Width
                };
                outputWindow.Show();
                this.Close();
            }
        }
        private void GoToSaveForm_Click(object sender, RoutedEventArgs e)
        {
            if (txtXStart.Text.ToString().Length == 0 || txtXEnd.Text.ToString().Length == 0 || txtH.Text.ToString().Length == 0 || txtEpsilon.Text.ToString().Length == 0 || txtG.Text.ToString().Length == 0)
            {
                MessageBox.Show("Вы не создали массивы!");
                return;
            }
            else
            {
                SaveWindow saveWindow = new SaveWindow(_y, _xStart, _xEnd, _h, _eps, _g, _a, _b, _c)
                {
                    WindowStartupLocation = WindowStartupLocation,
                    Left = Left,
                    Top = Top,
                    Height = Height,
                    Width = Width
                };
                saveWindow.Show();
                this.Close();
            }
        }
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void HelpMenuItem_Click(object sender, RoutedEventArgs e)
        {

            string fullPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "spravkahelper123.chm");

            if (!File.Exists(fullPath))
            {
                MessageBox.Show($"Файл справки не найден по пути:\n{fullPath}");
                return;
            }
            System.Diagnostics.Process.Start(fullPath);
        }
        public void Chet()
        {
            double ixStart = double.Parse(txtXStart.Text);
            _xStart = ixStart;
            double ixEnd = double.Parse(txtXEnd.Text);
            _xEnd = ixEnd;
            double ih = double.Parse(txtH.Text);
            _h = ih;
            double ieps = double.Parse(txtEpsilon.Text);
            _eps = ieps;
            double ig = double.Parse(txtG.Text);
            _g = ig;
            double[] a = ClassArrays.MassivA(ixStart, ixEnd, ih, ieps);
            _a = a;
            double[,] b = ClassArrays.MassivB(ixStart, ixEnd, ih, ieps);
            _b = b;
            double[,] btrans = ClassArrays.TransposeMatrix(b);
            double[] c = ClassArrays.CalculateC(btrans, a);
            _c = c;
            double[] y = ClassArrays.MassivY(c, ig);
            _y = y;
            double[] ysort = ClassArrays.MassivY(c, ig);
            ClassArrays.ShellSort(ysort);
            int dop = 0;
            if (a == null || b == null || c == null || y == null || ysort == null)
            {
                MessageBox.Show("вы ввели что-то некорректное");
                return;
            }
            double[] con = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                double x = ixStart + i * ih;
                con[i] = ClassArrays.ControlFormula(x);
            }
            string[] stra1 = new string[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                string temp = "";
                string strai = a[i].ToString();
                int count = 0;
                while (count < txtEpsilon.Text.Length)
                {
                    if (count >= strai.Length)
                    {
                        break;
                    }
                    else
                    {
                        temp += strai[count];
                        count++;
                    }
                }
                stra1[i] = temp;
                temp = "";
            }
            string stra = string.Join("  ", stra1);
            txtArrayA.Text = " ";
            txtArrayA.Text = stra;
            contrmas.Text = " ";
            string strcontrl = string.Join(" ", con);
            contrmas.Text = strcontrl;
            var listb = new List<string>();
            for (int i = 0; i < a.Length; i++)
            {
                string temp = " ";
                for (int j = 0; j < a.Length; j++)
                {
                    temp += b[i, j] + "\t";
                }
                listb.Add(temp);
            }
            txtArrayB.Text = " ";
            for (int i = 0; i < listb.Count; i++)
            {
                txtArrayB.Text += listb[i] + Environment.NewLine;
            }
            string strc = string.Join("  ", c);
            txtArrayC.Text = " ";
            txtArrayC.Text = strc;
            string stry = string.Join("  ", y);
            txtArrayY.Text = " ";
            txtArrayY.Text = stry;
            string strysort = string.Join("  ", ysort);
            sortmas.Text = strysort;
        }
    }
}
