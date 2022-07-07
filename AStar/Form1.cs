using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AStar
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private cCell  GetCell(cNode Node)
        {
            return Pnl.Table.Rows[Node.Position.Y].Cols[Node.Position.X];
        }
        private void RenderResultV2(cGraph G)
        {
            cNode GoalNode = G.Nodes[G.GoalNodePosition];
            cNode Current = GoalNode;
            
            while (Current.CameFromNode != null)
            {
                GetCell(Current).Text = Current.CostSoFar.ToString();
                Pnl.Table.Rows[Current.Position.Y].Cols[Current.Position.X].Text = Current.CostSoFar.ToString();
              //  Pnl.Table.Rows[Current.Position.Y].Cols[Current.Position.X].BackColor = Color.Blue;
                Current = Current.CameFromNode;
            }
            /*
            Pnl.Table.Rows[Current.Position.Y].Cols[Current.Position.X].BackColor = Color.Orange;
            Pnl.Table.Rows[Current.Position.Y].Cols[Current.Position.X].Text = Current.CostSoFar.ToString();

            Pnl.Table.Rows[GoalNode.Position.Y].Cols[GoalNode.Position.X].BackColor = Color.Red;
            Pnl.Table.Rows[GoalNode.Position.Y].Cols[GoalNode.Position.X].Text  = GoalNode.Cost.ToString();
            */
        }
        private void RenderResult(List<cNode> lst)
        {
            StringBuilder strB = new StringBuilder();
            int i;
            int j;
         
          //  Pnl.Table.Cells[0].BackColor = Color.White;
            for (i = 0; i < lst.Count; i++)
            {
                cNode c = lst[i];
                Color col = Color.Blue;

                if (c.Cost == 20)
                {
                    col = Color.Gray;
                }
                Pnl.Table.Rows[c.Position.Y].Cols[c.Position.X].BackColor = col;

                Pnl.Table.Rows[c.Position.Y].Cols[c.Position.X].Text = c.CostSoFar.ToString();
            }
            Pnl.Table.Rows[lst[0].Position.Y].Cols[lst[0].Position.X].BackColor = Color.Orange;
            Pnl.Table.Rows[lst[lst.Count -1].Position.Y].Cols[lst[lst.Count -1].Position.X].BackColor = Color.Red;
            

        }
        private void RenderTable()
        {
            foreach (cCell c2 in Pnl.Table.Cells)
            {
                RenderCell(c2);
                
            }
            /*
            foreach (Point P in c.Nodes.Keys )
            {
                if (c.Nodes [P].Cost == 20)
                {
                    GetCell(c.Nodes [P]).BackColor = Color.Gray;
                }
            }
             */

        }
        cBreathFirst Breath;
        private void button1_Click(object sender, EventArgs e)
        {
            ClearTable();
            PutMapValueToGraph();

           Breath = new cBreathFirst();
            Breath.Search(c);
          //  RenderResultV2 (c);
            RenderResult(Breath.lstResult);




        }
        private cPanelGrid Pnl;
        private cGraph c;
      
        private void Form1_Load(object sender, EventArgs e)
        {
            Pnl = new cPanelGrid(this.doubleBufferedPanel1, 15, 25, 34);
            Pnl.IsCustomRendering = false;
            Pnl.IsRenderingOntime = true;
            Pnl.CellClickEvent+=new cPanelGrid.CellClickEventHandler(Pnl_CellClickEvent);
            
            LoadMap();
            PutMapValueToGraph();
            RenderTable();

            this.cboBoxtype.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cboBoxtype.Items.Add("Space");
            this.cboBoxtype.Items.Add("Water");
            this.cboBoxtype.Items.Add("Wall");
            this.cboBoxtype.Items.Add("Begin");
            this.cboBoxtype.Items.Add("Goal");
            this.cboBoxtype.SelectedIndex = 0;


            
        }
        bool IsEditMode = true;
        enum enBlockType
        {
            Space=0,
            Water=1,
            Wall=2,
            Begin=3,
            End=4
        }
        private enBlockType EditBlockType = enBlockType.Space;
        private enBlockType GetCellValue(cCell cell)
        {
            if (cell.Value == "")
            {
                return enBlockType.Space;
            }

            return (enBlockType)int.Parse(cell.Value);
        }
        private void PutMapValueToGraph()
        {
            c = new cGraph(this.Pnl.Table.Rows.Count, this.Pnl.Table.Rows[0].Cols.Count);
            foreach (cCell cell in Pnl.Table.Cells)
            {
                switch (GetCellValue(cell))
                {
                    case enBlockType.Space :
                        c.Nodes[cell.Position].Cost = 1;
                        break;
                    case  enBlockType.Water :
                        c.Nodes[cell.Position].Cost = 4;
                        break;
                    case  enBlockType.Wall :
                        c.Nodes[cell.Position].Cost = int.MaxValue;
                        break;
                    case enBlockType.Begin :
                        c.InitialNodePosition = new Point(cell.Position.X, cell.Position.Y);
                        c.Nodes[cell.Position].Cost = 1;
                        break;
                    case enBlockType.End :
                        c.GoalNodePosition  = new Point(cell.Position.X, cell.Position.Y);
                        c.Nodes[cell.Position].Cost = 1;
                        break;
                }
            }        
        }
        private void RenderCell(cCell cell)
        {

            enBlockType BlockType = enBlockType.Space;
            if (cell.Value != "")
            {
              BlockType =    (enBlockType)int.Parse(cell.Value);
            }
            Color BlockColor = Color.White;
            switch (BlockType)
            {
                case enBlockType.Space :
                    break;
                case  enBlockType.Water :
                    BlockColor = Color.LightBlue;
                    break;
                case enBlockType.Wall :
                    BlockColor = Color.DarkGray;
                    break;
                case enBlockType.Begin :
                    BlockColor = Color.Blue;
                    break;
                case enBlockType.End :
                    BlockColor = Color.Red;
                    break;
            }
            cell.BackColor = BlockColor;
        }
        private void Pnl_CellClickEvent(cCell sender)
        {
            if (!IsEditMode)
            {
                return;
            }

            sender.Value = this.cboBoxtype.SelectedIndex.ToString();
            //sender.Value = ((int)EditBlockType).ToString();
            RenderCell(sender);



        }
        private void button2_Click(object sender, EventArgs e)
        {
            Breath.Step();
            RenderResult(Breath.lstResult);

        }
        private void ClearTable()
        {
            foreach (cCell c in Pnl.Table.Cells)
            {
                c.BackColor = Color.White;
            }

        }
        private void ClearMapText()
        {
            foreach (cCell cell in Pnl.Table.Cells)
            {
                cell.Text = "";
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            ClearMapText();
            PutMapValueToGraph();
            cAStarSearch Greed = new cAStarSearch();
            Greed.Search(c);
            RenderResultV2 (c);
          //  RenderResult(Greed.lstResult);

        }

        private void btnSaveMap_Click(object sender, EventArgs e)
        {
            Dictionary<Point, string> DicMap = new Dictionary<Point, string>();
            foreach (cCell cell in Pnl.Table.Cells)
            {
                DicMap.Add(cell.Position, cell.Value);
            }
            cUtil.WriteObj(DicMap, @"D:\CODE\visual studio 2010\Projects\AStar\AStar\AStar\MyMap.bin");
        }
        private void LoadMap()
        {
            Dictionary<Point, string> DicMap = new Dictionary<Point, string>();
            DicMap =(Dictionary<Point,string>) cUtil.ReadObj(@"D:\CODE\visual studio 2010\Projects\AStar\AStar\AStar\MyMap.bin");
            foreach (Point P in DicMap.Keys)
            {
                this.Pnl.Table.Cell(P).Value = DicMap[P];
            }
        }

        private void cboBoxtype_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (cCell cell in Pnl.Table.Cells)
            {
                cell.Text = "";
                cell.Value = "";
                RenderCell(cell);
            }
        }
    }
}
