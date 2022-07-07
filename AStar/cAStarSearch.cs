using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace AStar
{
    class cAStarSearch
    {
        public List<cNode> lstResult = new List<cNode>();
        //Queue<cNode> frontier = new Queue<cNode>();
        PriorityQueue<cNode> frontier = new PriorityQueue<cNode>();
        private bool _HasReachGoal = false;
        public bool HasReachGoal
        {
            get { return _HasReachGoal; }
        }
        private int heuristic(cNode c1, cNode c2)
        {
            //return abs(a.x - b.x) + abs(a.y - b.y)
            return Math.Abs(c1.Position.X - c2.Position.X) + Math.Abs(c1.Position.Y - c2.Position.Y);

        }
        public void Step()
        {
            int i = 0;

            if (frontier.IsEmpty())
            {
                return;
            }

            cNode currentNode = frontier.Dequeue();

            lstResult.Add(currentNode);
            if (currentNode.Position == Goal)
            {
                _HasReachGoal = true;
                return;
            }
            List<cNode> lstNeighbour = Graph.Neighbour(currentNode);
            for (i = 0; i < lstNeighbour.Count; i++)
            {
                cNode NeighbourNode = lstNeighbour[i];
                if (!NeighbourNode.IsVisisted)
                {                                       
                    NeighbourNode.CostSoFar  = NeighbourNode.Cost + currentNode.CostSoFar;
                    NeighbourNode.MarkasVisited();
                    int Priority = heuristic(Graph.Nodes[Goal], NeighbourNode) + NeighbourNode.CostSoFar ;

                    frontier.Enqueue(NeighbourNode, Priority);
                    NeighbourNode.CameFromNode = currentNode;
                }
            }

        }
        Point Start;
        Point Goal;
        private bool _DebugMode = false;
        private cGraph Graph;
        public void Search(cGraph c)
        {
            Graph = c;
            int i;
            int iNodeCount = 1;
            Start = c.InitialNodePosition;
            Goal = c.GoalNodePosition;

            frontier.Enqueue(c.Nodes[Start], 0);
            c.Nodes[Start].MarkasVisited();
            c.Nodes[Start].CostSoFar = 0;
            if (!_DebugMode)
            {
                while (!HasReachGoal)
                {
                    Step();
                }

            }
            else
            {

            }


        }
    }
}
