using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesman.GeneticAlgorithm
{
    public class Chromosome : ICloneable
    {
        public List<int> Route { get; set; }
        public static Graph Graph { get; set; }
        public static int Length => Graph.GraphData.Count;

        public Chromosome()
        {
            if (Length == -1)
            {
                throw new ArgumentException("Length not jet set!");
            }
            Route = new List<int>();
        }

        public Chromosome(List<int> route)
        {
            this.Route = route;
        }

        /// <summary>
        /// Initialize the chromosome with random values
        /// </summary>
        public void InitializeRandom()
        {
            for (int i = 0; i < Length; i++)
            {
                Route.Add(i);
            }
            lock (GlobalRandom.random)
            {
                Route = Route.OrderBy(node => GlobalRandom.random.Next()).ToList();
            }

            for (int i = 0; i < Length; i++)
            {
                if (!Route.Contains(i) || Route.Count < Length)
                {
                    throw new Exception("Mutation failed.");
                }
            }
        }

        /// <summary>
        /// initianlizes a blank chromosome
        /// </summary>
        public void InitializeBlank()
        {
            for (int i = 0; i < Length; i++)
            {
                Route.Add(i);
            }
        }

        /// <summary>
        /// Let the chromosome mutate
        /// </summary>
        /// <returns>Return a mutate offspring</returns>
        public Chromosome Mutate()
        {
            Chromosome chromosome = (Chromosome)this.Clone();
            int swId1, swId2;
            while (true)
            {
                lock (GlobalRandom.random)
                {
                    swId1 = GlobalRandom.random.Next(0, Length - 1);
                    swId2 = GlobalRandom.random.Next(0, Length - 1);
                }
                if (swId1 != swId2)
                {
                    break;
                }
            }
            chromosome.Route[swId1] = this.Route[swId2];
            chromosome.Route[swId2] = this.Route[swId1];

            for (int i = 0; i < Length; i++)
            {
                if (!chromosome.Route.Contains(i) || chromosome.Route.Count < Length)
                {
                    throw new Exception("Mutation failed.");
                }
            }

            return chromosome;
        }

        /// <summary>
        /// Mixes the "DNA" of two parents to create a new offspring
        /// </summary>
        /// <param name="parent1">The first parent.</param>
        /// <param name="parent2">The second parent.</param>
        /// <returns></returns>
        public static Chromosome CrossOver(Chromosome parent1, Chromosome parent2)
        {
            if(parent1.Route.Count < Length || parent2.Route.Count < Length)
            {
                throw new Exception("Parents are not the right length.");
            }

            if (Length == -1)
            {
                throw new ArgumentException("Length not jet set!");
            }

            Chromosome chromosome = new Chromosome();

            //Performe PMX corssover

            //set the values based on the first gen
            for (int i = 0; i < Length; i++)
            {
                lock (GlobalRandom.random)
                {
                    if (GlobalRandom.random.Next(0, 2) == 1)
                    {
                        chromosome.Route.Add(parent1.Route[i]);
                    }
                }
            }

            //Fill the remaining with not allready existing values.
            for (int i = 0; i < Length; i++)
            {
                if (chromosome.Route.Count < Length)
                {
                    if (!chromosome.Route.Contains(parent2.Route[i]))
                    {
                        chromosome.Route.Add(parent2.Route[i]);
                    }
                }
                else
                {
                    break;
                }
            }

            if(chromosome.Route.Count < Length)
            {
                throw new Exception("CorssOver failed!");
            }

            for (int i = 0; i < Length; i++)
            {
                if (!chromosome.Route.Contains(i) || chromosome.Route.Count < Length)
                {
                    throw new Exception("Mutation failed.");
                }
            }

            return chromosome;
        }


        /// <summary>
        /// Clone the chromosome.
        /// </summary>
        /// <returns>Returns a clone.</returns>
        public object Clone()
        {
            return new Chromosome(new List<int> (Route));
        }

        /// <summary>
        /// Evaluate the chromosome based on the distance between the nodes.
        /// </summary>
        /// <param name="graph">The graph containing the distance infomation.</param>
        /// <returns>Returns the absulute distance that must be traveld taking that rout.</returns>
        public double Evaluate()
        {

            double sum = Graph.GraphData[Route[0]][Route[Length - 1]];
            for (int i = 0; i < Length - 1; i++)
            {
                sum += Graph.GraphData[Route[i]][Route[i + 1]];
            }
            return sum;
        }

        /// <summary>
        /// This function creates a checked array that contains infomration about connections
        /// </summary>
        /// <returns>Returns the information as List<int>[]</returns>
        public List<int>[] GetConnectionInformationList()
        {
            List<int>[] connectionList = new List<int>[Graph.GraphData.Count];

            for (int id = 0; id < Length; id++)
            {
                connectionList[id] = new List<int>();
            }

            for (int id = 0; id < Length - 1; id++)
            {
                connectionList[Route[id + 1]].Add(Route[id]);
                connectionList[Route[id]].Add(Route[id + 1]);
            }

            connectionList[Route[Length - 1]].Add(Route[0]);
            connectionList[Route[0]].Add(Route[Length - 1]);

            return connectionList;
        }
    }
}
