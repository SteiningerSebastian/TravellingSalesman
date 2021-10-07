using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using TravellingSalesman.GeneticAlgorithm;

namespace TravellingSalesman
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PaintGraphHandler paintGraphHandler_;
        PlotHandler plotHandler_;
        GraphBuilder gb_;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void myCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            paintGraphHandler_.DrawNode(Mouse.GetPosition(myCanvas));
        }

        private void btnCalc_Click(object sender, RoutedEventArgs e)
        {
            List<Point> nodePositions = paintGraphHandler_.NodePositions;
            foreach(Point nodePos in nodePositions)
            {
                gb_.AddNode();
            }
            for(int id = 0; id < nodePositions.Count; id++)
            {
                for (int idCon = 0; idCon < nodePositions.Count; idCon++)
                {
                    gb_.AddEdge(id, idCon, Math.Sqrt(Math.Pow(nodePositions[id].X - nodePositions[idCon].X, 2) + Math.Pow(nodePositions[id].Y - nodePositions[idCon].Y, 2)));
                }
            }

            GeneticAlgorithm.GeneticAlgorithm geneticAlgorithm = new TravellingSalesman.GeneticAlgorithm.GeneticAlgorithm(gb_.Graph, 1000000000, UpdateBasedOnBestChromosome);
            geneticAlgorithm.ExecuteInNewThread();

            //paintGraphHandler_.DrawEdge(gb_.GetConnectionInformationList());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            paintGraphHandler_ = new PaintGraphHandler(myCanvas);
            plotHandler_ = new PlotHandler(myCanvas_plot);
            gb_ = new GraphBuilder();
        }

        public void UpdateBasedOnBestChromosome(Chromosome chromosome)
        {
            Dispatcher.Invoke(()=>
            paintGraphHandler_.DrawEdge(chromosome.GetConnectionInformationList()));

            Dispatcher.Invoke(() =>
            plotHandler_.Plot(chromosome.Evaluate()));

            Dispatcher.Invoke(() =>
            result.Content = chromosome.Evaluate().ToString());
        }
    }
}
