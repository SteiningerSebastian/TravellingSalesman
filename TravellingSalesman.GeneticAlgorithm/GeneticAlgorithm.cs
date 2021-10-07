using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TravellingSalesman.GeneticAlgorithm
{
    public class GeneticAlgorithm
    {
        public Graph Graph { get; private set; }
        private PopulationBuilder populationBuilder_;
        public int Epochs { get; set; }
        private Action<Chromosome> UIUpdate { get; set; }
        public int UseBestParents { get; set; } = 10;
        public GeneticAlgorithm(Graph graph, int epochs, Action<Chromosome> uiUpdate)
        {
            this.Graph = graph;
            populationBuilder_ = new PopulationBuilder(0.2, 0.5, 0.3, 100000);
            Epochs = epochs;
            Chromosome.Graph = graph;
            UIUpdate = uiUpdate;
        }

        /// <summary>
        /// Executest the Genetic Algorithm in an new thread
        /// </summary>
        public void ExecuteInNewThread()
        {
            Thread t = new Thread(new ThreadStart(Execute));
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
        }

        /// <summary>
        /// Executest the GeneticAlgorithm
        /// </summary>
        public void Execute()
        {
            List<Chromosome> bestChromosomes = new List<Chromosome>();
            List<Chromosome> population = null;
            for (int epoch = 0; epoch < Epochs; epoch++)
            {


                if (epoch == 0)
                {
                    population = populationBuilder_.Build();
                }
                else
                {

                    if (bestChromosomes.Count > 0)
                    {
                        for (int i = 0; i < bestChromosomes.Count; i++)
                        {
                            population.Add((Chromosome)bestChromosomes[i].Clone());
                        }
                    }

                    population = populationBuilder_.Build(population);
                }

                population = population.OrderBy(chromosome => chromosome.Evaluate()).ToList();

                bestChromosomes.Clear();

                for(int i = 0; i<UseBestParents; i++)
                {
                    bestChromosomes.Add((Chromosome)population[i].Clone());
                }

                Thread t = new Thread(new ThreadStart(()=>UIUpdate(bestChromosomes[0])));
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
            }
        }
    }
}
