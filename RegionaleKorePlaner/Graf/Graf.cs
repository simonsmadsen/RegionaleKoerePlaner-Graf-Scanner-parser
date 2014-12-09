using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RegionaleKorePlaner
{
    public class Graf : List<Node>
    {
        public void AddRoute(string cityFrom, int time, string cityTo)
        {
            if (this.Where(n => n.Name == cityFrom).ToList<Node>().Count < 1)
            {
                this.Add(new Node(cityFrom));
            }
            if (this.Where(n => n.Name == cityTo).ToList<Node>().Count < 1)
            {
                this.Add(new Node(cityTo)); 
            }
            Node fromNode = this.Where(n => n.Name == cityFrom).Single();
            Node toNode = this.Where(n => n.Name == cityTo).Single();

            Edge newEdge = new Edge(fromNode,toNode,time);
            fromNode.Edges.Add(newEdge);
            toNode.Edges.Add(newEdge);
        }

        public void PrintAllNodeEdges()
        {
            foreach (var node in this)
            {
                Console.WriteLine(node.Name);
                foreach (var edge in node.Edges)
                {
                    Console.WriteLine(edge.FromNode.Name + " - " + edge.Value + " - " + edge.ToNode.Name);
                }
                Console.WriteLine("");
            }
        }

        public string BreadthFirst()
        {
            string resultString = "";
            Queue<Node> queue = new Queue<Node>();
            List<Node> result = new List<Node>();

            Node selectedNode = this.First();
            result.Add(selectedNode);

            do
            {
                handleNodeEdges(selectedNode,queue,result);
                selectedNode = (queue.Count > 0 ? queue.Dequeue() : null);

            } while (selectedNode != null);


            foreach (Node node in result)
            {
                resultString += node.Name + ", ";
            }

            return resultString.Substring(0, resultString.Length - 2);
        }
        private void handleNodeEdges(Node selectedNode, Queue<Node> queue, List<Node> result)
        {
            List<Node> connectedNodeToSelectedNode = new List<Node>();
            foreach (Edge edge in selectedNode.Edges)
            {
                connectedNodeToSelectedNode.Add((edge.ToNode.Name == selectedNode.Name) ? edge.FromNode : edge.ToNode);
            }
            connectedNodeToSelectedNode = connectedNodeToSelectedNode.OrderBy(n => n.Name).ToList();

            foreach (Node node in connectedNodeToSelectedNode)
            {
                if (!result.Exists(n => n.Name == node.Name))
                {
                    result.Add(node);
                    queue.Enqueue(node);
                }
            }    

        }
    
        public string DepthFirst()
        {
            string resultString = "";
            Stack<Node> stack = new Stack<Node>();
            List<Node> result = new List<Node>();

            Node selectedNode = this.First();
            result.Add(selectedNode);
            stack.Push(selectedNode);

            do
            {
               
                Node nextNode = getNextDepthFirstNode(selectedNode, result);
                if (nextNode != null)
                {
                    selectedNode = nextNode;
                    result.Add(selectedNode);
                    stack.Push(selectedNode);
                    continue;                    
                }
                stack.Pop();
                selectedNode = (stack.Count > 0 ? stack.Peek() : null);
            }
            while(stack.Count > 0);

            foreach (Node node in result)
            {
                resultString += node.Name + ", ";
            }

            return resultString.Substring(0, resultString.Length - 2);
        }
        private Node getNextDepthFirstNode(Node selectedNode, List<Node> result)
        {
            // find de kendte noder
            List<Node> temp = new List<Node>();
            foreach (Edge edge in selectedNode.Edges)
            {
                temp.Add((edge.ToNode.Name == selectedNode.Name) ? edge.FromNode : edge.ToNode);
            }
            List<string> nodeNames = new List<string>();
            
            foreach (Node node in temp)
            {
                nodeNames.Add(node.Name);
            }
            nodeNames.Sort();

            foreach (string nodeName in nodeNames)
            {
                if (!result.Exists(n => n.Name == nodeName))
                {
                    return temp.Find(n => n.Name == nodeName);
                }
            }

            return null;
        }

        public Graf GetMinSpanningTreeByPrim()
        {
            Node selectedNode = this.First();
            Graf primGraf = new Graf();
            do
            {
                selectedNode = findNextRoute(selectedNode, primGraf);
            } while (selectedNode != null);
            return primGraf;
        }

        public Graf GetMinSpanningTreeByKruskal()
        {
            Graf kruskalGraf = new Graf();
            List<Edge> allEdges = new List<Edge>();
            foreach (Node node in this)
            {
                foreach (Edge edge in node.Edges)
                {
                    if (!allEdges.Exists(n => n.FromNode == edge.FromNode && n.ToNode == edge.ToNode && n.Value == edge.Value))
                    {
                        allEdges.Add(edge);
                    }
                }
            }

            allEdges = allEdges.OrderBy(e => e.Value).ToList();

            foreach (Edge edge in allEdges)
            {
                bool fromNodeExists = kruskalGraf.Exists(n => n.Name == edge.FromNode.Name);
                bool toNodeExists = kruskalGraf.Exists(n => n.Name == edge.ToNode.Name);
                if (fromNodeExists == false || toNodeExists == false)
                {
                    kruskalGraf.AddRoute(edge.FromNode.Name,edge.Value,edge.ToNode.Name);
                }
            }
            return kruskalGraf;
        }

        private Node findNextRoute(Node selectedNode, Graf primGraf)
        {
            List<Node> connectedAndNotHandledNodes = new List<Node>();
            foreach (Edge edge in selectedNode.Edges)
            {
                Node temp = (edge.ToNode.Name == selectedNode.Name) ? edge.FromNode : edge.ToNode;
                if (!primGraf.Exists(n => n.Name == temp.Name))
                {
                    connectedAndNotHandledNodes.Add(temp);
                }
            }
            Node lowestEdge = null;
            foreach (Node node in connectedAndNotHandledNodes)
            {
                if (lowestEdge == null)
                {
                    lowestEdge = node;
                    continue;
                }
                if (lowestEdge.GetUnitedEdge(selectedNode).Value > node.GetUnitedEdge(selectedNode).Value)
                {
                    lowestEdge = node;
                }
            }

            if (lowestEdge != null)
            {
                primGraf.AddRoute(selectedNode.Name, lowestEdge.GetUnitedEdge(selectedNode).Value, lowestEdge.Name);
                return lowestEdge;
            }
            return null;
        }

    }
}
