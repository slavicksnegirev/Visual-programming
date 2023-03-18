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

namespace _15_Puzzle
{
    // Направление
    public enum Dir
    {
        Up, Down, Left, Right, None
    }

    public partial class Puzzle_15 : UserControl
    {
        Label[,] labels= new Label[4,4];
        Label prevLabel;

        public Puzzle_15()
        {
            InitializeComponent();

            int number = 0;

            for (int i = 0; i < labels.GetLength(0); i++)
            {
                for(int j = 0; j < labels.GetLength(1); j++)
                {
                    number++;

                    labels[i, j] = InitLabel(Brushes.White, $"{number}");
                    Grid.SetRow(labels[i, j], i);
                    Grid.SetColumn(labels[i, j],j);

                    grid1.Children.Add(labels[i, j]);
                }
            }
            labels[labels.GetLength(0) - 1, labels.GetLength(1) - 1].Background = Brushes.Black;
            labels[labels.GetLength(0) - 1, labels.GetLength(1) - 1].Content = " ";
        }

        public int[,] GetStatus()
        {
            int[,] status = new int[labels.GetLength(0), labels.GetLength(1)];

            for (int i = 0; i < labels.GetLength(0); i++)
            {
                for (int j = 0; j < labels.GetLength(1); j++)
                {
                    status[i, j] = labels[i, j].Background == Brushes.White ? 1 : 0;
                }
            }

            return status;
        }

        Label InitLabel(Brush color, object content)
        {
            return new Label()
            {
                Background = color,
                FontSize = 70,
                Content = content,
                VerticalContentAlignment = VerticalAlignment.Center,
                HorizontalContentAlignment = HorizontalAlignment.Center,
            };
        }

        public void Move(Dir dir)
        {
            for(int i = 0; i < labels.GetLength(0); i++)
            {
                for (int j = 0; j < labels.GetLength(1); j++)
                {
                    if(labels[i,j].Background == Brushes.Black)
                    {
                        if (dir == Dir.Up && i - 1 >= 0)
                        {
                            Grid.SetColumn(labels[i - 1, j], j);
                            Grid.SetRow(labels[i - 1, j], i);

                            Grid.SetColumn(labels[i, j], j);
                            Grid.SetRow(labels[i, j], i - 1);

                            prevLabel = labels[i - 1, j];
                            labels[i - 1, j] = labels[i, j];
                        }
                        else if (dir == Dir.Down && i + 1 < labels.GetLength(0))
                        {
                            Grid.SetColumn(labels[i + 1, j], j);
                            Grid.SetRow(labels[i + 1, j], i);

                            Grid.SetColumn(labels[i, j], j);
                            Grid.SetRow(labels[i, j], i + 1);

                            prevLabel = labels[i + 1, j];
                            labels[i + 1, j] = labels[i, j];
                        }
                        else if (dir == Dir.Left && j - 1 >= 0)
                        {
                            Grid.SetColumn(labels[i, j - 1], j);
                            Grid.SetRow(labels[i, j - 1], i);

                            Grid.SetColumn(labels[i, j], j - 1);
                            Grid.SetRow(labels[i, j], i);

                            prevLabel = labels[i, j - 1];
                            labels[i, j - 1] = labels[i, j];
                        }
                        else if (dir == Dir.Right && j + 1 < labels.GetLength(0))
                        {
                            Grid.SetColumn(labels[i, j + 1], j);
                            Grid.SetRow(labels[i, j + 1], i);

                            Grid.SetColumn(labels[i, j], j + 1);
                            Grid.SetRow(labels[i, j], i);

                            prevLabel = labels[i, j + 1];
                            labels[i, j + 1] = labels[i, j];
                        }
                        else
                            return;

                        labels[i, j] = prevLabel; 

                        return;
                    }
                }
            }
        }
    }
}
