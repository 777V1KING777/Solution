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
using System.Windows.Shapes;
using Kralko_WPF;
using Microsoft.Win32; // Для открытия диалогового окна выбора файла

namespace Kralko_WPF
{
    public partial class OutputWindow : Window
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
        public OutputWindow(double[] yArray, double xStart, double xEnd, double h, double eps, double g, double[] a, double[,] b, double[] c)
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
        }

        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            // Диалоговое окно для выбора файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*"; // Фильтр файлов

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Чтение содержимого файла
                    string filePath = openFileDialog.FileName;
                    string fileContent = File.ReadAllText(filePath);

                    // Вывод содержимого файла в текстовый редактор
                    txtFileContent.Text = fileContent;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при чтении файла: {ex.Message}");
                }
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
        private void SaveFileMenuItem_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Сохранить содержимое текстового редактора в файл
            MessageBox.Show("Здесь должна быть логика сохранения файла."); // Заглушка
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

        private void GoToSecondForm_Click(object sender, RoutedEventArgs e)
        {
            GraphWindow graphWindow = new GraphWindow(_y, _xStart, _xEnd, _h, _eps, _g, _a, _b, _c) { WindowStartupLocation = WindowStartupLocation, Left = Left, Top = Top, Height = Height, Width = Width };
            graphWindow.Show();
            this.Close();
        }
        private void GoToSaveForm_Click(object sender, RoutedEventArgs e)
        {
            SaveWindow saveWindow = new SaveWindow(_y, _xStart, _xEnd, _h, _eps, _g, _a, _b, _c) { WindowStartupLocation = WindowStartupLocation, Left = Left, Top = Top, Height = Height, Width = Width };
            saveWindow.Show();
            this.Close();
        }


        // Дополнительный метод для просмотра файла в MS Word (опционально)
        /*
        private void ViewInWord_Click(object sender, RoutedEventArgs e)
        {
            // Код для открытия файла в MS Word
        }
        */
    }
}
