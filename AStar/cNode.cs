using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
namespace AStar
{
    public class cNode
    {
        public cNode CameFromNode;
        public int Cost = 0;
        public int CostSoFar = 0;
        private bool _IsVisited = false;
        public bool IsVisisted
        {
            get { return _IsVisited; }
        }
        public void MarkasVisited()
        {
            _IsVisited = true;
        }
        public string Value = "";
        private Point _Position;
        public Point Position
        {
            get { return _Position; }
        }
        public cNode(string pValue, Point pPosition)
        {
            _Position = new Point(pPosition.X, pPosition.Y);
            Value = pValue;
        }
        public cNode(Point pPosition)
        {

            _Position = new Point(pPosition.X, pPosition.Y);
        }
    }
}
