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
            //get the index of the next node
            int nodeId = Graph.GraphData.Count;

            //Build a 2D array with numRows = numCols
            Graph.GraphData.Add(new List<double>());

            //Add all col to the new node
            for (int i = 0; i < Graph.GraphData.Count - 1; i++)
            {
                Graph.GraphData[nodeId].Add(-1);
            }

            //Add a new col to the existin nodes
            foreach (List<double> l in Graph.GraphData)
            {
                l.Add(-1);
            }

            return nodeId;
        }

        /// <summary>
        /// Adds a edge/connection to the nodes
        /// </summary>
        /// <param name="nodeId">The id of the first node.</param>
        /// <param name="connectedNodeId">The id of the second node.</param>
        /// <param name="weight">The weight between these nodes.</param>
        public void AddEdge(int nodeId, int connectedNodeId, double weight)
        {
            Graph.GraphData[nodeId][connectedNodeId] = weight;
            Graph.GraphData[connectedNodeId][nodeId] = weight;
        }

        /// <summary>
        /// This function creates a checked array that contains infomration about connections
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
