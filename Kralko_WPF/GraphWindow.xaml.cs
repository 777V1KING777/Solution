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
using OxyPlot;
using OxyPlot.Series;
using OxyPlot.Axes;
using System.IO;

namespace Kralko_WPF
{
    /// <summary>
    /// Логика взаимодействия для GraphWindow.xaml
    /// </summary>
    public partial class GraphWindow : Window
    {
        private double[] _yArray; // Закрытое поле для хранения массива Y

        private double _xStart;
        private double _xEnd;
        private double _h;
        private double _eps;
        private double _g;
        //double[] _y;
        double[] _a;
        double[,] _b;
        double[] _c;

        public GraphWindow(double[] yArray, double xStart, double xEnd, double h, double eps, double g, double[] a, double[,] b, double[] c) // Конструктор, принимающий массив Y
        {
            InitializeComponent();
            _yArray = yArray; // Сохраняем переданный массив Y
            _xStart = xStart;
            _xEnd = xEnd;
            _h = h;
            _eps = eps;
            _g = g; 
            _a = a; 
            _b = b; 
            _c = c;

            CreatePlotModel(); // Создаем и отображаем график
        }

        private void CreatePlotModel()
        {
            var plotModel = new PlotModel { Title = "График массива Y" };

            // Создаем серии для несортированного и сортированного массивов
            var seriesUnsorted = new LineSeries { Title = "Неотсортированный Y" };
            var seriesSorted = new LineSeries { Title = "Отсортированный Y" };

            // Создаем копию массива Y для сортировки (чтобы не изменять исходный массив)
            double[] yArraySorted = new double[_yArray.Length];
            Array.Copy(_yArray, yArraySorted, _yArray.Length);
            Array.Sort(yArraySorted);

            // Заполняем серии данными
            for (int i = 0; i < _yArray.Length; i++)
            {
                seriesUnsorted.Points.Add(new DataPoint(i, _yArray[i]));
                seriesSorted.Points.Add(new DataPoint(i, yArraySorted[i]));
            }

            // Добавляем серии на график
            plotModel.Series.Add(seriesUnsorted);
            plotModel.Series.Add(seriesSorted);

            // Настраиваем оси
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Title = "Индекс" });
            plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Title = "Значение" });

            // Присваиваем модель графика элементу PlotView
            plotView.Model = plotModel;
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
        private void ExitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void GoToSecondForm_Click(object sender, RoutedEventArgs e)
        {
            OutputWindow outputWindow = new OutputWindow(_yArray, _xStart, _xEnd, _h, _eps, _g, _a, _b, _c)
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

        private void GoToOutputForm_Click(object sender, RoutedEventArgs e)
        {
            OutputWindow outputWindow = new OutputWindow(_yArray, _xStart, _xEnd, _h, _eps, _g, _a, _b, _c) { WindowStartupLocation = WindowStartupLocation, Left = Left, Top = Top, Height = Height, Width = Width };
            outputWindow.Show();
            this.Close();
        }
        private void GoToSaveForm_Click(object sender, RoutedEventArgs e)
        {
            SaveWindow saveWindow = new SaveWindow(_yArray, _xStart, _xEnd, _h, _eps, _g, _a, _b, _c) { WindowStartupLocation = WindowStartupLocation, Left = Left, Top = Top, Height = Height, Width = Width };
            saveWindow.Show();
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
