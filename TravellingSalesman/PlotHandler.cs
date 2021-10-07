using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace TravellingSalesman
{
    public class PlotHandler
    {
        private double MaxX { get; set; } = 1;
        private double MaxY { get; set; } = 1;
        private List<Label> AxLabels { get; set; }
        private List<Point> Points { get; set; }
        private List<Line> Lines { get; set; }

        Canvas canvas_;
        public PlotHandler(Canvas canvas)
        {
            canvas_ = canvas;
            AxLabels = new List<Label>();
            Points = new List<Point>();
            Lines = new List<Line>();
            MaxY *= 1.2;
            SetupCanvas();
        }

        /// <summary>
        /// Sets up the plot.
        /// </summary>
        private void SetupCanvas()
        {
            Draw_Line(new Point(30, 0), new Point(30, canvas_.ActualHeight - 10), lineThickness: 2);
            Draw_Line(new Point(30, canvas_.ActualHeight - 10), new Point(canvas_.ActualWidth, canvas_.ActualHeight - 10), lineThickness: 2);
            SetupAxLabels();
        }

        /// <summary>
        /// Setup the labels for the axis
        /// </summary>
        private void SetupAxLabels()
        {
            if (AxLabels.Count > 0)
            {
                foreach (Label l in AxLabels)
                {
                    canvas_.Children.Remove(l);
                }
            }

            for (int x = 0; x < 11; x++)
            {
                var p = new Point(20 + x * (canvas_.ActualWidth - 80) / 10.0, canvas_.ActualHeight - 10);
                var l = AddLabel(p, Math.Round((MaxX / 10.0 * x), 1).ToString());
                AxLabels.Add(l);
            }

            for (int y = 0; y < 11; y++)
            {
                var p = new Point(0, canvas_.ActualHeight - canvas_.ActualHeight / 10 * y - 30);
                var l = AddLabel(p, Math.Round((MaxY / 10.0 * y), 1).ToString());
                AxLabels.Add(l);
            }
        }

        /// <summary>
        /// Draws a line between two points.
        /// </summary>
        /// <param name="pos1">The fist position, Start.</param>
        /// <param name="pos2">The second position, End.</param>
        /// <param name="brush">The brush used to draw the Line (black)</param>
        /// <param name="lineThickness">The Thickness of the drawn line.</param>
        public void Draw_Line(Point pos1, Point pos2, Brush brush = null, double lineThickness = 5)
        {
            if (brush == null)
            {
                brush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
            var line = new Line();
            line.X1 = pos1.X;
            line.Y1 = pos1.Y;
            line.X2 = pos2.X;
            line.Y2 = pos2.Y;
            line.StrokeThickness = lineThickness;
            line.Stroke = brush;
            canvas_.Children.Add(line);
        }

        /// <summary>
        /// Adds a Label to the Plot
        /// </summary>
        /// <param name="pos">The position of the label.</param>
        /// <param name="text">The text of the label.</param>
        /// <param name="brush">The brush used to wirte the text.</param>
        /// <param name="fontSize">The size of the text in the label.</param>
        public Label AddLabel(Point pos, string text, Brush brush = null, double fontSize = 12)
        {
            if (brush == null)
            {
                brush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }
            var label = new Label();
            label.Content = text;
            label.Foreground = brush;
            label.FontSize = fontSize;
            Canvas.SetLeft(label, pos.X);
            Canvas.SetTop(label, pos.Y);
            canvas_.Children.Add(label);
            return label;
        }

        /// <summary>
        /// Plots a Value onto the plot.
        /// </summary>
        /// <param name="val">The valut to plot.</param>
        /// <param name="brush">The brush to use to plot the values.</param>
        public void Plot(double val, Brush brush = null, double lineThickness = 3)
        {
            if (brush == null)
            {
                brush = new SolidColorBrush(Color.FromRgb(0, 0, 0));
            }

            if (Points.Count < 1)
            {
                Points.Add(new Point(0, val));
            }
            else
            {
                Points.Add(new Point(Points.Last().X + 1, val));
            }

            MaxX = Math.Max(MaxX, Points.Last().X + 3);
            MaxY = Math.Max(MaxY, val * 1.2);

            SetupAxLabels();

            if (Lines.Count > 0)
            {
                canvas_.Children.RemoveRange(canvas_.Children.IndexOf(Lines.First()), Lines.Count);
            }
            Lines.Clear();

            if (Points.Count > 1)
            {
                for (int i = 1; i < Points.Count; i++)
                {
                    var line = new Line();
                    line.X1 = Points[i - 1].X / MaxX * (canvas_.ActualWidth - 80) +30;
                    line.Y1 = canvas_.ActualHeight - Points[i - 1].Y / MaxY * (canvas_.ActualHeight-10) -10;
                    line.X2 = Points[i].X / MaxX * (canvas_.ActualWidth - 80) + 30;
                    line.Y2 = canvas_.ActualHeight - Points[i].Y / MaxY * (canvas_.ActualHeight -10) -10;
                    line.StrokeThickness = lineThickness;
                    line.Stroke = brush;
                    canvas_.Children.Add(line);
                    Lines.Add(line);
                }
            }
        }
    }
}
