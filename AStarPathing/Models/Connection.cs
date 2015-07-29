using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathing.Models
{
    class Connection
    {
        public double Weight { get; set; }
        public Node Node { get; set; }

        public Connection(Node node, Location originNodeLocation)
        {
            this.Node = node;
            this.Weight = this.GetDistance(originNodeLocation);
        }

        public double GetDistance(Location l2)
        {
            return Math.Sqrt(Math.Pow(this.Node.Location.X - l2.X, 2) + Math.Pow(this.Node.Location.Y - l2.Y, 2));
        }
    }
}
