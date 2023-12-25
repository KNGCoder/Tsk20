using LiveCharts;
using LiveCharts.Wpf;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tsk20
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
         public SeriesCollection SeriesCollection { get; set; }
            public List<string> Labels { get; set; }
            private double _trend;
            private double[] temp = { 1, 3, 2, 4, -3, 5, 2, 1 };
            public MainWindow()
            {
                InitializeComponent();
                // Создание линейного графика
                LineSeries mylineseries = new LineSeries();
                // Установить заголовок полилинии
                mylineseries.Title = "Temp";
                // Линейная форма линейного графика
                mylineseries.LineSmoothness = 0;
                // Бессмысленный стиль линейного графика
                mylineseries.PointGeometry = null;
                // Добавить абсциссу
                Labels = new List<string> { "1", "3", "2", "4", "-3", "5", "2", "1" };
                // Добавить данные линейного графика
                mylineseries.Values = new ChartValues<double>(temp);
                SeriesCollection = new SeriesCollection { };
                SeriesCollection.Add(mylineseries);
                _trend = 8;
                linestart();
                DataContext = this;
            }
            // Метод непрерывной линейной диаграммы
            public void linestart()
            {
                Task.Run(() =>
                {
                    var r = new Random();
                    while (true)
                    {
                        Thread.Sleep(1000);
                        _trend = r.Next(-10, 10);
                        // Обновляем элементы пользовательского интерфейса формы в рабочем потоке через Dispatcher
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            // Обновить время абсциссы
                            Labels.Add(DateTime.Now.ToString());
                            Labels.RemoveAt(0);
                            // Обновляем данные ординат
                            SeriesCollection[0].Values.Add(_trend);
                            SeriesCollection[0].Values.RemoveAt(0);
                        });
                    }
                });
            }
        }

    }
