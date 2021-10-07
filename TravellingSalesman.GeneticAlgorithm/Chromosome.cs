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
            throw new NotImplementedException();
        }

        /// <summary>
        /// initianlizes a blank chromosome
        /// </summary>
        public void InitializeBlank()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Let the chromosome mutate
        /// </summary>
        /// <returns>Return a mutate offspring</returns>
        public Chromosome Mutate()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Mixes the "DNA" of two parents to create a new offspring
        /// </summary>
        /// <param name="parent1">The first parent.</param>
        /// <param name="parent2">The second parent.</param>
        /// <returns></returns>
        public static Chromosome CrossOver(Chromosome parent1, Chromosome parent2)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function creates a checked array that contains infomration about connections
        /// </summary>
        /// <returns>Returns the information as List<int>[]</returns>
        public List<int>[] GetConnectionInformationList()
        {
            throw new NotImplementedException();
        }
    }
}
