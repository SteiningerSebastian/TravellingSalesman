using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesman.GeneticAlgorithm
{
    public class PopulationBuilder
    {
        public double Percentage_Random { get; set; } = 0.3;
        public double Percentage_Mutation { get; set; } = 0.3;
        public double Percentage_CrossOver { get; set; } = 0.4;
        public int PopulationSize { get; set; } = 500;
        public int MutationOfTheFittest { get; set; } = 3;
        public int CorssOverOftheFittest { get; set; } = 10;

        public PopulationBuilder()
        {

        }

        public PopulationBuilder(double percentage_random, double percentage_mutation, double percentage_crossOver, int populationSize)
        {
            Percentage_Random = percentage_random;
            Percentage_Mutation = percentage_mutation;
            Percentage_CrossOver = percentage_crossOver;

            if (Percentage_CrossOver + Percentage_Mutation + Percentage_Random != 1)
            {
                throw new ArgumentOutOfRangeException("All three percentages must have a sum of 1");
            }

            PopulationSize = populationSize;
        }

        /// <summary>
        /// Build a new population based on the parents
        /// </summary>
        /// <param name="parents">A sorted list of parents, starting at 0 with the best parent-</param>
        /// <returns></returns>
        public List<Chromosome> Build(List<Chromosome> sortedParents)
        {
            List<Chromosome> population = new List<Chromosome>();

            Parallel.For(0, PopulationSize, (i, state) =>
            {
                //Initialize Random offspring
                if (i < PopulationSize * Percentage_Random)
                {
                    //Get a random chromosome
                    throw new NotImplementedException();

                    //uncomment this code bellow
                    //lock (population)
                    //{
                    //    population.Add(chromosome);
                    //}
                }
                //Mutation of the best parents
                else if (i < PopulationSize * Percentage_Random + PopulationSize * Percentage_Mutation)
                {
                    //Get a mutated chromosome using the parents
                    throw new NotImplementedException();
                    //uncomment the code bellow
                    //lock (population)
                    //{
                    //    population.Add(chromosome);
                    //}
                }
                //Crossover parents to get the offspring
                else
                {
                    //create a new chromosome using crossover
                    throw new NotImplementedException();
                    //uncomment the code bellow
                    //lock (population)
                    //{
                    //    population.Add(chromosome);
                    //}
                }
            });

            return population;
        }

        /// <summary>
        /// Build a population based only on randomnes
        /// </summary>
        /// <returns>Returns a new </returns>
        public List<Chromosome> Build()
        {
            List<Chromosome> population = new List<Chromosome>();
            Parallel.For(0, PopulationSize, (i, state) =>
            {
                //init a random chromosome
                throw new NotImplementedException();
                //uncomment the code bellow
                //lock (population)
                //{
                //    population.Add(chromosome);
                //}
            });
            return population;
        }
    }
}
