using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStarPathing.Models
{
    class NodeConnection
    {
        public Node Node { get; set; }
        public double CostSoFar { get; set; }
        public double Heuristic { get; set; }
        public double TotalEstimatedCost { get; set; }
        public NodeConnection CameFrom { get; set; }
    }
}
