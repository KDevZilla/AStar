using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing ;
namespace AStar
{
    public class cGraph
    {
        //.new.public HashSet<cNode> Nodes = new HashSet<cNode>();
        public Dictionary<Point, cNode> Nodes = new Dictionary<Point, cNode>();
        int _MaxRow = 0;
        int _MaxCol = 0;
        public cNode Node(int X, int Y)
        {
            return Nodes[new Point(X,Y)];
        }
        public cGraph Clone()
        {
            cGraph cNew = new cGraph(this.MaxRow, this.MaxCol);
            foreach (Point P in Nodes.Keys)
            {
                cNew.Nodes[P].Value = this.Nodes[P].Value;
            }
            return cNew;

        }
        public int MaxRow
        {
            get { return _MaxRow; }
        }
        public int MaxCol
        {
            get { return _MaxCol; }
        }
        public List<cNode> Neighbour(cNode N)
        {
            List<cNode> lst = new List<cNode>();
            Point p;
            if (N.Position.Y > 0)
            {
                p = new Point(N.Position.X, N.Position.Y - 1);
                if (Nodes.ContainsKey(p))
                {
                    if (Nodes[p].Cost != int.MaxValue)
                    {
                        lst.Add(Nodes[p]);
                    }
                }
            }

            if (N.Position.Y < MaxRow -1)
            {
                p = new Point(N.Position.X, N.Position.Y + 1);
                if (Nodes.ContainsKey(p))
                {
                    if (Nodes[p].Cost != int.MaxValue)
                    {
                        lst.Add(Nodes[p]);
                    }
                }
            }

            if (N.Position.X > 0)
            {
                p = new Point(N.Position.X -1, N.Position.Y);
                if (Nodes.ContainsKey(p))
                {
                    if (Nodes[p].Cost != int.MaxValue)
                    {
                        lst.Add(Nodes[p]);
                    }
                }
            }

            if (N.Position.X < MaxCol - 1)
            {
                p = new Point(N.Position.X +1,N.Position.Y );
                if (Nodes.ContainsKey(p))
                {
                    if (Nodes[p].Cost != int.MaxValue)
                    {
                        lst.Add(Nodes[p]);
                    }
                }
            }
            return lst;
        }
        public cGraph(int pMaxRow, int pMaxCol)
        {
            _MaxCol = pMaxCol;
            _MaxRow = pMaxRow;
            GenerateNodes();
        }
        public Point InitialNodePosition;
        public Point GoalNodePosition;

        private void GenerateNodes()
        {
            int i;
            int j;
            Nodes.Clear();
            for (i = 0; i < MaxRow; i++)
            {
                for (j = 0; j < MaxCol; j++)
                {
                    System.Drawing.Point P = new System.Drawing.Point(j, i);
                    cNode N = new cNode(P);
                    Nodes.Add(P, N);
                }
            }
        }
    }
}
