using System.Collections.Generic;
using System.Drawing;

namespace Life_DLL
{
    public class Cell
    {
        private Point _point;
        private bool _state;
        private List<Cell> _neighborList = new List<Cell>();

        public bool State { get => _state; }

        public Cell(int x, int y)
        {
            _point = new Point(x, y);
            _state = false;
        }

        public void RevertStatus()
        {
            if (_state)
            {
                _state = false;
            }
            else
            {
                _state = true;
            }
        }

        public void AddNeighbor(Cell cell)
        {
            _neighborList.Add(cell);
        }

        public void Draw (Graphics graphics)
        {
            if(_state)
            {
                var pen = new Pen(Color.Red);
                var size = new Size(10, 10);
                var rect = new Rectangle(_point, size);
                var brush = new SolidBrush(Color.Red);
                graphics.DrawRectangle(pen, rect);
                graphics.FillRectangle(brush, rect);
            }
        }


        public void CompirePoint(Point point)
        {
            if (_point == point)
            {
                RevertStatus();
            }
        }

        public Cell CheckCell()
        {
            if (_state)
            {
                var count = 0;
                foreach(var cell in _neighborList)
                {
                    if (cell.State)
                    {
                        count++;
                    }
                }
                if(count == 2 || count == 3) //канон 2,3
                {
                    var cell = new Cell(this._point.X, this._point.Y);
                    cell._state = true;
                    return cell;
                }
                else
                {
                    return new Cell(this._point.X, this._point.Y);
                }
            }
            else
            {
                var count = 0;
                foreach (var cell in _neighborList)
                {
                    if (cell.State)
                    {
                        count++;
                    }
                }
                if (count == 3) //канон 3
                {
                    var cell = new Cell(this._point.X, this._point.Y);
                    cell._state = true;
                    return cell;
                }
                else
                {
                    return new Cell(this._point.X, this._point.Y);
                }
            }
        }
    }
}
