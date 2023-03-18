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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab5
{
    public partial class MainWindow : Window
    {
        DrawPoint selectedPoint = null; // выбранная точка

        bool isAutoPoint { get; set; } = false;
        bool isAutoLine { get; set; } = false;

        Random random = new Random();

        List<DrawPoint> drawPoints = new List<DrawPoint>();  // список точек
        List<Line> lines = new List<Line>(); // список линий

        public MainWindow()
        {
            InitializeComponent();
        }

        // добавляет точку в canvas
        void AddToCanvas(UIElement element, Point position)
        {
            Canvas.SetLeft(element, position.X);
            Canvas.SetTop(element, position.Y);

            myCanvas.Children.Add(element);
        }

        // создает точку
        DrawPoint CreatePoint(Point position)
        {
            DrawPoint drawPoint = new DrawPoint(position);

            drawPoint.MouseLeftButtonDown += DrawPoint_MouseLeftButtonDown;

            AddToCanvas(drawPoint, position);
            drawPoints.Add(drawPoint);

            return drawPoint;
        }

        bool Distance(DrawPoint point1, DrawPoint point2, int distance)
        {
            Point position = new Point();

            position.X = Math.Abs(point1.Position.X - point2.Position.X);
            position.Y = Math.Abs(point1.Position.Y - point2.Position.Y);

            if (position.X < distance && position.Y < distance)
            {
                return true;
            }

            return false;
        }

        // создает точку при нажатии на canvas
        private void myCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.OriginalSource is Canvas)
            {
                if (isAutoPoint == false) // если не выбрана авто добавление точек
                {
                    CreatePoint(e.GetPosition(myCanvas));
                }
                else // если выбрана авто добавление точек
                {
                    int count = 0;

                    if (int.TryParse(CountAutoPoint.Text, out count))
                    {
                        for (int i = 0; i < count; i++)
                        {
                            int x = random.Next(10,500);
                            int y = random.Next(10,400);

                            CreatePoint(new Point(x, y));
                        }
                    }
                }
            }
        }

        // добавление линии
        private void DrawPoint_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // если не выбрана авто добавление линии
            if (isAutoLine == false)
            {
                // если выбрана точка
                if (selectedPoint != null && selectedPoint != sender as DrawPoint)
                {
                    // создает линии
                    Line line = new Line()
                    {
                        X1 = selectedPoint.Position.X,
                        Y1 = selectedPoint.Position.Y,
                        X2 = (sender as DrawPoint).Position.X,
                        Y2 = (sender as DrawPoint).Position.Y,
                        Stroke = Brushes.Red,
                        StrokeThickness = 2,
                    };

                    // добавляем в canvas и список
                    myCanvas.Children.Add(line);
                    lines.Add(line);

                    selectedPoint = null;
                }
                else // если не выбрана точка
                {
                    selectedPoint = sender as DrawPoint;
                }
            }
            else // если выбрана авто добавление линии
            {
                selectedPoint = sender as DrawPoint;

                // читаем точки
                foreach (var point in drawPoints)
                {
                    if (point == selectedPoint) continue;

                    int distance = 0;

                    int.TryParse(DistanceAutoPoint.Text, out distance);
                    if(Distance(selectedPoint, point, distance))
                    {
                        // создаем линии
                        Line line = new Line()
                        {
                            X1 = selectedPoint.Position.X,
                            Y1 = selectedPoint.Position.Y,
                            X2 = point.Position.X,
                            Y2 = point.Position.Y,
                            Stroke = Brushes.Blue,
                            StrokeThickness = 2,
                        };

                        // добавляем в canvas и список
                        myCanvas.Children.Add(line);
                        lines.Add(line);
                    }
                }
                selectedPoint = null;
            }
        }

        // Удаляем все точки из canvas и списка
        private void clearBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach(var point in drawPoints)
                myCanvas.Children.Remove(point);
            foreach (var line in lines)
                myCanvas.Children.Remove(line);
            lines.Clear();
            drawPoints.Clear();
            selectedPoint = null;
        }

        // удаляем линии
        private void clearLinessBtn_Click(object sender, RoutedEventArgs e)
        {
            foreach (var line in lines)
                myCanvas.Children.Remove(line);
            lines.Clear();

        }

        private void checkPoint_Checked(object sender, RoutedEventArgs e)
        {
            isAutoPoint = true;
        }

        private void checkLine_Checked(object sender, RoutedEventArgs e)
        {
            isAutoLine= true;
            selectedPoint = null;
        }

        private void checkPoint_Unchecked(object sender, RoutedEventArgs e)
        {
            isAutoPoint= false;
        }

        private void checkLine_Unchecked(object sender, RoutedEventArgs e)
        {
            isAutoLine= false;
            selectedPoint = null;
        }
    }
}
