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
    public class DrawPoint : Control
    {
        public Point Position { get; set; }

        static DrawPoint()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DrawPoint), new FrameworkPropertyMetadata(typeof(DrawPoint)));
        }

        public DrawPoint(Point position)
        {
            Position = position;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Pen pen = new Pen()
            {
                Brush = Brushes.Black,
                Thickness = 1,
            };

            drawingContext.DrawEllipse(Brushes.Black, pen, new Point(0, 0), 5, 5);
        }  
    }
}
