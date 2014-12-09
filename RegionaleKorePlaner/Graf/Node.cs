using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegionaleKorePlaner
{
    public class Node
    {
        public List<Edge> Edges;
        public string Name;

        public Node(string name)
        {
            Name = name;
            Edges = new List<Edge>();
        }

        public Node(string name, List<Edge> edges)
        {
            Name = name;
            Edges = edges;
        }

        public void AddEdge(Edge edge)
        {
            Edges.Add(edge);
        }

        public Edge GetUnitedEdge(Node connectNode)
        {
            return Edges.Find(n => n.FromNode.Name == connectNode.Name || n.ToNode.Name == connectNode.Name);
        }

    }
}
