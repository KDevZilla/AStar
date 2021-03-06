using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace AStar
{
    public class cBreathFirst:ISearch
    {   
        public List<cNode> lstResult = new List<cNode>();
        Queue<cNode> frontier = new Queue<cNode>();
       
        private bool _HasReachGoal = false;
        public bool HasReachGoal
        {
            get { return _HasReachGoal; }
        }
       
        public void Step()
        {
            int i = 0;
            
            if (frontier.Count ==0)
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
                    int NewCodeSoFar = NeighbourNode.Cost + currentNode.CostSoFar;
                    //if (!NeighbourNode.IsVisisted)
                    if (!NeighbourNode.IsVisisted ||
                        NeighbourNode.CostSoFar > NewCodeSoFar)
                    {

                        NeighbourNode.CostSoFar = NewCodeSoFar;
                        NeighbourNode.MarkasVisited();                        
                        frontier.Enqueue(NeighbourNode);
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

            frontier.Enqueue(c.Nodes[Start]);
            c.Nodes [Start].MarkasVisited();
            c.Nodes[Start].CostSoFar  = 0;
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
