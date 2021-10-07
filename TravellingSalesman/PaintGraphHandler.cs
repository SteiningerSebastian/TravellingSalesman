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
    public class PaintGraphHandler
    {
        Canvas canvas_;
        internal List<Point> NodePositions { get; set; }
        private List<Line> Lines { get; set; }

        public PaintGraphHandler(Canvas canvas)
        {
            canvas_ = canvas;
            NodePositions = new List<Point>();
            Lines = new List<Line>();
        }

        /// <summary>
        /// Draws a node on the Cnavas.
        /// </summary>
        /// <param name="pos">the position on the canvas the node should be drawn</param>
        public void DrawNode(Point pos)
        {
            var ellipse = new Ellipse();
            ellipse.Width = 10;
            ellipse.Height = 10;
            var b = new SolidColorBrush(Color.FromRgb(0, 0, 100));
            ellipse.Stroke = b;
            ellipse.StrokeThickness = 2;
            var bFill = new SolidColorBrush(Color.FromRgb(0, 0, 200));
            ellipse.Fill = bFill;
            Canvas.SetLeft(ellipse, pos.X- ellipse.Width/2);
            Canvas.SetTop(ellipse, pos.Y- ellipse.Height/2);
            NodePositions.Add(new Point(pos.X, pos.Y));
            canvas_.Children.Add(ellipse);
        }

        /// <summary>
        /// Draw a Edge on the canvas.
        /// </summary>
        /// <param name="pos1">The first Position, Start.</param>
        /// <param name="pos2">The second Position, End.</param>
        public void DrawEdge(Point pos1, Point pos2)
        {
            var edge = new Line();
            edge.X1 = pos1.X;
            edge.Y1 = pos1.Y;
            edge.X2 = pos2.X;
            edge.Y2 = pos2.Y;
            edge.StrokeThickness = 2;
            var b = new SolidColorBrush(Color.FromRgb(0, 0, 150));
            edge.Stroke = b;
            canvas_.Children.Add(edge);
            Lines.Add(edge);
        }

        /// <summary>
        /// Draws all edges between all nodes.
        /// </summary>
        /// <param name="graphConnections">the List<int>[] contianing the needed information.</param>
        public void DrawEdge(List<int>[] graphConnections)
        {
            ClanAllEdges();
            for (int id = 0; id < graphConnections.Length; id++)
            {
                List<int> l = graphConnections[id];
                foreach(int idCon in l)
                {
                    Point pos1 = NodePositions[id];
                    Point pos2 = NodePositions[idCon];
                    DrawEdge(pos1, pos2);
                }
            }
        }

        /// <summary>
        /// Remove all edges from canvas
        /// </summary>
        public void ClanAllEdges()
        {
           foreach(var edge in Lines)
           {
                canvas_.Children.Remove(edge);
           }
        }

        /// <summary>
        /// Removes all children
        /// </summary>
        public void Clean()
        {
            canvas_.Children.Clear();
        }
    }
}
