using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Kralko;
using Microsoft.Win32;
using System.IO;

namespace Kralko_WPF
{
    /// <summary>
    /// Логика взаимодействия для SaveWindow.xaml
    /// </summary>
    public partial class SaveWindow : Window
    {
        private double _xStart;
        private double _xEnd;
        private double _h;
        private double _eps;
        private double _g;
        double[] _y;
        double[] _a;
        double[,] _b;
        double[] _c;
        StringBuilder resultBuilder = new StringBuilder();
        public SaveWindow(double[] yArray, double xStart, double xEnd, double h, double eps, double g, double[] a, double[,] b, double[] c)
        {
            InitializeComponent();
            _y = yArray;
            _xStart = xStart;
            _xEnd = xEnd;
            _h = h;
            _eps = eps;
            _g = g;
            _a = a;
            _b = b;
            _c = c;
            Load();
        }
        private void Load()
        {
            resultBuilder.AppendLine("Исходные данные:");
            resultBuilder.AppendLine($"х начальное = {_xStart}");
            resultBuilder.AppendLine($"х конечное = {_xEnd}");
            resultBuilder.AppendLine($"шаг h = {_h}");
            resultBuilder.AppendLine($"точность eps = {((decimal)(_eps)).ToString()}");
            resultBuilder.AppendLine($"шаг g = {_g}");

            resultBuilder.AppendLine("Массив А:");
            resultBuilder.AppendLine(string.Join(" ", _a));
            double[] con = new double[_a.Length];
            int dop = 0;
            for (double i = 0; i < _a.Length; i++)
            {
                double x = _xStart + i * _h;
                con[dop] = ClassArrays.ControlFormula(x);
                dop++;
            }
            resultBuilder.AppendLine("Контрольная формула дл массива A:");
            resultBuilder.AppendLine(string.Join(" ", con));
            resultBuilder.AppendLine("Массив B:");
            for (int i = 0; i < _b.GetLength(0); i++)
            {
                for (int j = 0; j < _b.GetLength(1); j++)
                {
                    resultBuilder.Append(_b[i, j] + "\t");
                }
                resultBuilder.AppendLine();
            }
            resultBuilder.AppendLine("Массив C:");
            resultBuilder.AppendLine(string.Join(" ", _c));
            resultBuilder.AppendLine("Массив Y:");
            resultBuilder.AppendLine(string.Join(" ", _y));


            double[] ysort = ClassArrays.MassivY(_c, _g);
            ClassArrays.ShellSort(ysort);
            resultBuilder.AppendLine("Массив Y (отсортированный):");
            resultBuilder.AppendLine(string.Join(" ", ysort));
            FlowDocument document = new FlowDocument();
            document.Blocks.Add(new Paragraph(new Run(resultBuilder.ToString())));
            spravkaView.Document = document;
        }
        public void SaveButton_Click(object sender, System.EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.DefaultExt = ".txt";
            saveFileDialog.FileName = "Результат.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                File.WriteAllText(filePath, resultBuilder.ToString());

                MessageBox.Show("Файл успешно сохранён!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void GoToFirstForm_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow()
            {
                WindowStartupLocation = WindowStartupLocation,
                Left = Left,
                Top = Top,
                Height = Height,
                Width = Width
            };
            mainWindow.txtXStart.Text = _xStart.ToString();
            mainWindow.txtXEnd.Text = _xEnd.ToString();
            mainWindow.txtH.Text = _h.ToString();
            mainWindow.txtEpsilon.Text = _eps.ToString();
            mainWindow.txtG.Text = _g.ToString();
            mainWindow.Chet();
            mainWindow.Show();
            this.Close();
        }
        private void GoToSecondForm_Click(object sender, RoutedEventArgs e)
        {
            GraphWindow graphWindow = new GraphWindow(_y, _xStart, _xEnd, _h, _eps, _g, _a, _b, _c) { WindowStartupLocation = WindowStartupLocation, Left = Left, Top = Top, Height = Height, Width = Width };
            graphWindow.Show();
            this.Close();
        }
        private void GoToOutputForm_Click(object sender, RoutedEventArgs e)
        {
            OutputWindow outputWindow = new OutputWindow(_y, _xStart, _xEnd, _h, _eps, _g, _a, _b, _c) { WindowStartupLocation = WindowStartupLocation, Left = Left, Top = Top, Height = Height, Width = Width };
            outputWindow.Show();
            this.Close();
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
    }
}
