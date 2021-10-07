using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravellingSalesman.GeneticAlgorithm
{
    public class GraphBuilder
    {
        public Graph Graph { get; private set; }
        public GraphBuilder()
        {
            this.Graph = new Graph();
        }

        /// <summary>
        /// Adds a node to the graph
        /// </summary>
        /// <returns>Returns the index of the node in the List<List<int>></returns>
        public int AddNode()
        {
            //Add a Node to the Graph
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a edge/connection to the nodes
        /// </summary>
        /// <param name="nodeId">The id of the first node.</param>
        /// <param name="connectedNodeId">The id of the second node.</param>
        /// <param name="weight">The weight between these nodes.</param>
        public void AddEdge(int nodeId, int connectedNodeId, double weight)
        {
            //Add a weighted edge to the graph
            throw new NotImplementedException();
        }

        /// <summary>
        /// This function creates a checked array that contains information about connections
        /// </summary>
        /// <returns>Returns the information as List<int>[]</returns>
        public List<int>[] GetConnectionInformationList()
        {
            List<int>[] connectionList = new List<int>[Graph.GraphData.Count];
            for (int id = 0; id < Graph.GraphData.Count; id++)
            {
                connectionList[id] = new List<int>();
                for (int idCon = 0; idCon < Graph.GraphData.Count; idCon++)
                {
                    if (Graph.GraphData[id][idCon] >= 0)
                    {
                        connectionList[id].Add(idCon);
                    }
                }
            }
            return connectionList;
        }
    }
}
