using AStarPathing.Models;
using CSC160_ConsoleMenu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathing
{
    class Program
    {
        List<NodeConnection> OpenList;
        List<NodeConnection> ClosedList;
        List<Node> NodeList;

        public Program()
        {
            OpenList = new List<NodeConnection>();
            ClosedList = new List<NodeConnection>();
            NodeList = GetNodeList();
            AddConnections();
        }

        private void AddConnections()
        {
            Node x = NodeList[0];
            x.AddConnection(new Connection(NodeList[1], x.Location));
            x.AddConnection(new Connection(NodeList[4], x.Location));
            
            x = NodeList[1];
            x.AddConnection(new Connection(NodeList[0], x.Location));
            x.AddConnection(new Connection(NodeList[3], x.Location));

            x = NodeList[2];
            x.AddConnection(new Connection(NodeList[3], x.Location));
            x.AddConnection(new Connection(NodeList[15], x.Location));
            x.AddConnection(new Connection(NodeList[4], x.Location));

            x = NodeList[3];
            x.AddConnection(new Connection(NodeList[1], x.Location));
            x.AddConnection(new Connection(NodeList[2], x.Location));
            x.AddConnection(new Connection(NodeList[4], x.Location));

            x = NodeList[4];
            x.AddConnection(new Connection(NodeList[0], x.Location));
            x.AddConnection(new Connection(NodeList[2], x.Location));
            x.AddConnection(new Connection(NodeList[3], x.Location));
            x.AddConnection(new Connection(NodeList[6], x.Location));
            x.AddConnection(new Connection(NodeList[9], x.Location));
            x.AddConnection(new Connection(NodeList[13], x.Location));

            x = NodeList[5];
            x.AddConnection(new Connection(NodeList[6], x.Location));
            x.AddConnection(new Connection(NodeList[8], x.Location));

            x = NodeList[6];
            x.AddConnection(new Connection(NodeList[5], x.Location));
            x.AddConnection(new Connection(NodeList[4], x.Location));
            x.AddConnection(new Connection(NodeList[9], x.Location));

            x = NodeList[7];
            x.AddConnection(new Connection(NodeList[13], x.Location));
            x.AddConnection(new Connection(NodeList[15], x.Location));

            x = NodeList[8];
            x.AddConnection(new Connection(NodeList[5], x.Location));
            x.AddConnection(new Connection(NodeList[10], x.Location));

            x = NodeList[9];
            x.AddConnection(new Connection(NodeList[10], x.Location));
            x.AddConnection(new Connection(NodeList[11], x.Location));
            x.AddConnection(new Connection(NodeList[4], x.Location));
            x.AddConnection(new Connection(NodeList[6], x.Location));

            x = NodeList[10];
            x.AddConnection(new Connection(NodeList[8], x.Location));
            x.AddConnection(new Connection(NodeList[9], x.Location));
            x.AddConnection(new Connection(NodeList[11], x.Location));

            x = NodeList[11];
            x.AddConnection(new Connection(NodeList[9], x.Location));
            x.AddConnection(new Connection(NodeList[10], x.Location));
            x.AddConnection(new Connection(NodeList[12], x.Location));

            x = NodeList[12];
            x.AddConnection(new Connection(NodeList[11], x.Location));
            x.AddConnection(new Connection(NodeList[14], x.Location));
            x.AddConnection(new Connection(NodeList[15], x.Location));

            x = NodeList[13];
            x.AddConnection(new Connection(NodeList[7], x.Location));
            x.AddConnection(new Connection(NodeList[4], x.Location));

            x = NodeList[14];
            x.AddConnection(new Connection(NodeList[12], x.Location));
            x.AddConnection(new Connection(NodeList[15], x.Location));

            x = NodeList[15];
            x.AddConnection(new Connection(NodeList[14], x.Location));
            x.AddConnection(new Connection(NodeList[2], x.Location));
            x.AddConnection(new Connection(NodeList[7], x.Location));
            x.AddConnection(new Connection(NodeList[12], x.Location));
        }

        public List<Node> GetNodeList()
        {
            List<Node> nodes = new List<Node>
            {
                new Node(-19, 11, "A"),
                new Node(-13, 13, "B"),
                new Node(4, 14, "C"),
                new Node(-4, 12, "D"),
                new Node(-8, 3, "E"),
                new Node(-18, 1, "F"),
                new Node(-12, -8, "G"),
                new Node(12, -9, "H"),
                new Node(-18, -11, "I"),
                new Node(-4, -11, "J"),
                new Node(-12, -14, "K"),
                new Node(2, -18, "L"),
                new Node(18, -13, "M"),
                new Node(4, -9, "N"),
                new Node(22, 11, "O"),
                new Node(18, 3, "P")
            };

            return nodes;
        }

        public Node GetNode()
        {
            int choice = CIO.PromptForMenuSelection(GetNodeMenuOptions() , false);
            return NodeList[choice - 1];
        }

        public void Start(Node origin, Node end)
        {
            Node currentNode = origin;
            NodeConnection cnc = new NodeConnection();
            cnc.CameFrom = null;
            cnc.CostSoFar = 0;
            cnc.Heuristic = new Connection(origin, end.Location).Weight;
            cnc.Node = origin;
            cnc.TotalEstimatedCost = cnc.CostSoFar + cnc.Heuristic;

            while (cnc.Heuristic != 0)
            {
                foreach (var item in cnc.Node.Connections)
                {
                    NodeConnection nc = new NodeConnection();
                    nc.CameFrom = cnc;
                    nc.CostSoFar = cnc.CostSoFar + item.Weight;
                    nc.Node = item.Node;
                    nc.Heuristic = new Connection(nc.Node, end.Location).Weight;
                    nc.TotalEstimatedCost = nc.CostSoFar + nc.Heuristic;
                    OpenList.Add(nc);
                }

                ClosedList.Add(cnc);
                NodeConnection previous = cnc;
                OpenList.Remove(cnc);
                NodeConnection smallest = null;
                foreach (var item in OpenList)
                {
                    if(smallest == null)
                    {
                        smallest = item;
                    }
                    else if(smallest.TotalEstimatedCost > item.TotalEstimatedCost)
                    {
                        smallest = item;
                    }
                }

                cnc = new NodeConnection();
                cnc = smallest;
            }

            StringBuilder sb = new StringBuilder();
            while(cnc != null)
            {
                sb.Append(cnc.Node.Name + "  ");
                cnc = cnc.CameFrom;
            }
            string answer = sb.ToString();
            sb = new StringBuilder();
            for (int i = answer.Length - 2; i >= 0; i--)
            {
                sb.Append(answer[i]);
            }
            Console.WriteLine("\n\n\n\tShortest Path is " + sb.ToString());
        }

        private IEnumerable<string> GetNodeMenuOptions()
        {
            List<string> menuOptions = new List<string>();
            foreach (Node n in NodeList)
            {
                menuOptions.Add(n.ToString());
            }
            return menuOptions;
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            Console.WriteLine("What Node do you want to start at?");
            Node origin = p.GetNode();
            Console.WriteLine("What Node do you want to end at?");
            Node end = p.GetNode();
            p.Start(origin, end);
        }
    }
}
