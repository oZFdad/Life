
using Life_DLL;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Life
{
    public partial class Form1 : Form
    {
        private int _count = 0;
        private Cell [,] _cellGrid;
        private bool db = false;
        public Form1()
        {
            InitializeComponent();
            FillCellList();
            AddNeighbors();
        }

        private void painBox_Paint(object sender, PaintEventArgs e)
        {
            GetGrid(e.Graphics);
            foreach (var cell in _cellGrid)
            {
                cell.Draw(e.Graphics);
            }
        }

        private void GetGrid(Graphics graphics)
        {
            var pen = new Pen(Color.Black);
            
            for (var i = 0; i <= painBox.Width; i += 10)
            {
                graphics.DrawLine(pen, i, 0, i, painBox.Height);
            }

            for (var i = 0; i <= painBox.Height; i += 10)
            {
                graphics.DrawLine(pen, 0, i, painBox.Width, i);
            }
        }

        private void FillCellList()
        {
            _cellGrid = new Cell[painBox.Width / 10, painBox.Height / 10];
            for (var i = 0; i < painBox.Width - 1; i += 10)
            {
                for (var j = 0; j < painBox.Height - 1; j += 10)
                {
                    _cellGrid[i / 10, j / 10] = new Cell(i, j);
                }
            }
        }

        private void AddNeighbors()
        {
            for (var i = 0; i < _cellGrid.GetLength(0); i++)
            {
                for (var j = 0; j < _cellGrid.GetLength(1); j++)
                {
                    if (i - 1 >= 0)
                    {
                        if (j - 1 >= 0)
                        {
                            _cellGrid[i, j].AddNeighbor(_cellGrid[i - 1, j - 1]);
                        }
                        _cellGrid[i, j].AddNeighbor(_cellGrid[i - 1, j]);
                        if (j + 1 < _cellGrid.GetLength(1))
                        {
                            _cellGrid[i, j].AddNeighbor(_cellGrid[i - 1, j + 1]);
                        }
                    }
                    if (j - 1 >= 0)
                    {
                        _cellGrid[i, j].AddNeighbor(_cellGrid[i, j - 1]);
                    }
                    if (j + 1 < _cellGrid.GetLength(1))
                    {
                        _cellGrid[i, j].AddNeighbor(_cellGrid[i, j + 1]);
                    }
                    if (i + 1 < _cellGrid.GetLength(0))
                    {
                        if (j - 1 >= 0)
                        {
                            _cellGrid[i, j].AddNeighbor(_cellGrid[i + 1, j - 1]);
                        }
                        _cellGrid[i, j].AddNeighbor(_cellGrid[i + 1, j]);
                        if (j + 1 < _cellGrid.GetLength(1))
                        {
                            _cellGrid[i, j].AddNeighbor(_cellGrid[i + 1, j + 1]);
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            int count = 0;
            int count2 = 0;
            int count3 = 0;
            while (true)
            {
                var cellGrid = new Cell[painBox.Width / 10, painBox.Height / 10];
                for (var i = 0; i < _cellGrid.GetLength(0); i++)
                {
                    for (var j = 0; j < _cellGrid.GetLength(1); j++)
                    {
                        cellGrid[i, j] = _cellGrid[i, j].CheckCell();
                    }
                }
                _cellGrid = cellGrid;
                AddNeighbors();
                //Thread.Sleep(100);
                painBox.Refresh();
                _count++;
                label1.Text ="Итераций: " + Convert.ToString(_count);
                foreach(var cell in _cellGrid)
                {
                    if (cell.State)
                    {
                        count++;
                    }
                }
                if (count2 == count)
                {
                    count3++;
                }
                else
                {
                    count3 = 0;
                }
                if (count != 0)
                {
                    count2 = count;
                    count = 0;
                }
                else
                {
                    return;
                }
                if (count3 > 100)
                {
                    _count -= 100;
                    return;
                }
                //if (_count % 100 == 0)
                //{
                //    return;
                //}
            }
        }

        private void painBox_MouseDown(object sender, MouseEventArgs e)
        {
            var point = new Point(e.X - e.X % 10, e.Y - e.Y % 10);
            foreach(var cell in _cellGrid)
            {
                cell.CompirePoint(point);
            }
            painBox.Refresh();
        }

        private void btRandom_Click(object sender, EventArgs e)
        {
            _count = 0;
            var rnd = new Random();
            foreach (var cell in _cellGrid)
            {
                if (rnd.Next(100) % 5 == 0)
                {
                    cell.RevertStatus();
                }
            }
            var x1 = rnd.Next(0, _cellGrid.GetLength(0));
            var x2 = rnd.Next(0, _cellGrid.GetLength(0));
            var x3 = rnd.Next(0, _cellGrid.GetLength(0));
            var y1 = rnd.Next(0, _cellGrid.GetLength(1));
            var y2 = rnd.Next(0, _cellGrid.GetLength(1));
            var y3 = rnd.Next(0, _cellGrid.GetLength(1));
            for (var i = x2; i < x3; i++)
            {
                _cellGrid[i, y1].RevertStatus();
            }
            for (var j = y2; j < y3; j++)
            {
                _cellGrid[x1, j].RevertStatus();
            }
            AddNeighbors();
            painBox.Refresh();
            if (db)
            {
                db = false;
                return;
            }
            else
            {
                db = true;
                btRandom_Click(sender, e);
            }
        }
    }
}
