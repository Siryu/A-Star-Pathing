using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathing.Models
{
    class Node
    {
        public string Name { get; set; }
        public List<Connection> Connections;
        public Location Location { get; set; }

        public Node(int x, int y, string name)
        {
            Connections = new List<Connection>();
            this.Name = name;
            this.Location = new Location();
            this.Location.X = x;
            this.Location.Y = y;
        }

        public void AddConnection(Connection connection)
        {
            this.Connections.Add(connection);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
