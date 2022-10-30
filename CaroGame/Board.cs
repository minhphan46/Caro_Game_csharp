using CaroGame.bin.Debug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaroGame
{
    internal class Board
    {
        public const int DEFAULT_SQUARE_HEIGHT = 30;
        public const int DEFAULT_SQUARE_WIDTH = 30;
        public const int DEFAULT_NUMBER_OF_SQUARE = 20;

        protected Form _parentForm;
        public Form ParentForm
        {
            get { return _parentForm; }
        }

        // kich thuoc NxN cua bang
        protected int _sizeOfBoard;
        public int SizeOfBoard
        {
            get { return _sizeOfBoard; }
            set
            {
                if (value < 20)
                    _sizeOfBoard = 20;
                else _sizeOfBoard = value;
            }
        }

        protected int _squareHeight;
        protected int _squareWidth;
        public int SquareHeight { get { return _squareHeight; } }
        public int SquareWidth { get { return _squareWidth; } }

        protected Square[,] _squares;
        public Square[,] Squares
        {
            get { return _squares; }
        }
        public ItemColor colorBefore = ItemColor.Red;

        public Board(Form parentForm, int sizeOfBoard = DEFAULT_NUMBER_OF_SQUARE, int squareHeight = DEFAULT_SQUARE_HEIGHT, int squareWidth = DEFAULT_SQUARE_WIDTH)
        {
            _parentForm = parentForm;
            SizeOfBoard = sizeOfBoard;

            _squareHeight = squareHeight;
            _squareWidth = squareWidth;
            _squares = new Square[sizeOfBoard, sizeOfBoard];
            Init();
        }
        public void Init()
        {
            int left = 0;
            int top = 0;

            for (int i = 0; i < SizeOfBoard; i++)
            {
                left = 0;
                for (int j = 0; j < SizeOfBoard; j++)
                {
                    Square sq = new Square(i, j, this);
                    sq.Size = new System.Drawing.Size(_squareWidth, _squareHeight);
                    sq.Left = left;
                    sq.Top = top;

                    left += _squareWidth;

                    _squares[i, j] = sq;
                    _parentForm.Controls.Add(_squares[i, j]);
                }
                top += _squareHeight;
            }
        }
        public bool CheckGameOver(Square sq)
        {
            int x = sq.X;
            int y = sq.Y;

            Item curItem = sq.Item;

            int count = 1;
            // check ngang thang
            // len
            for (int i = x - 1; i > x - 5; i--)
            {
                try
                {
                    Item checkItem = Squares[i, sq.Y].Item;
                    if (checkItem != null)
                    {
                        if (checkItem.Color == curItem.Color)
                            count++;
                        if (count == 5)
                        {
                            for (i = x; i > x - 5; i--)
                            {
                                Squares[i, sq.Y].BackColor = System.Drawing.Color.LightGreen;
                            }
                            return true;
                        }
                    }
                    else
                    {
                        count = 1;
                        break;
                    }
                }
                catch { }
            }
            // xuong
            for (int i = x + 1; i < x + 5; i++)
            {
                try
                {
                    Item checkItem = Squares[i, sq.Y].Item;
                    if (checkItem != null)
                    {
                        if (checkItem.Color == curItem.Color)
                            count++;
                        if (count == 5)
                        {
                            for (i = x; i < x + 5; i++)
                            {
                                Squares[i, sq.Y].BackColor = System.Drawing.Color.LightGreen;
                            }
                            return true;
                        }
                    }
                    else
                    {
                        count = 1;
                        break;
                    }
                }
                catch { }
            }
            // trai
            for (int i = y - 1; i > y - 5; i--)
            {
                try
                {
                    Item checkItem = Squares[sq.X, i].Item;
                    if (checkItem != null)
                    {
                        if (checkItem.Color == curItem.Color)
                            count++;
                        if (count == 5)
                        {
                            for (i = y; i > y - 5; i--)
                            {
                                Squares[sq.X, i].BackColor = System.Drawing.Color.LightGreen;
                            }
                            return true;
                        }
                    }
                    else
                    {
                        count = 1;
                        break;
                    }
                }
                catch { }
            }
            // phai
            for (int i = y + 1; i < y + 5; i++)
            {
                try
                {
                    Item checkItem = Squares[sq.X, i].Item;
                    if (checkItem != null)
                    {
                        if (checkItem.Color == curItem.Color)
                            count++;
                        if (count == 5)
                        {
                            for (i = y; i < y + 5; i++)
                            {
                                Squares[sq.X, i].BackColor = System.Drawing.Color.LightGreen;
                            }
                            return true;
                        }
                    }
                    else
                    {
                        count = 1;
                        break;
                    }
                }
                catch { }
            }
            // check duong cheo
            // goc trai tren
            for (int i = x - 1; i > x - 5; i--)
            {
                for (int j = y - 1; j > y - 5; j--)
                {
                    int xSub = Math.Abs(i - x);
                    int ysub = Math.Abs(j - y);
                    if (xSub == ysub)
                    {
                        try
                        {
                            Item checkItem = Squares[i, j].Item;
                            if (checkItem != null)
                            {
                                if (checkItem.Color == curItem.Color)
                                    count++;
                                if (count == 5)
                                {
                                    for (i = x; i > x - 5; i--)
                                        for (j = y; j > y - 5; j--)
                                        {
                                            xSub = Math.Abs(i - x);
                                            ysub = Math.Abs(j - y);
                                            if (xSub == ysub)
                                                Squares[i, j].BackColor = System.Drawing.Color.LightGreen;
                                        }
                                    return true;
                                }
                            }
                            else
                            {
                                count = 1;
                                goto DoneLoop1;
                            }
                        }
                        catch { }
                    }
                }
            }
        DoneLoop1:
            // goc phai tren
            for (int i = x - 1; i > x - 5; i--)
            {
                for (int j = y + 1; j < y + 5; j++)
                {
                    int xSub = Math.Abs(i - x);
                    int ysub = Math.Abs(j - y);
                    if (xSub == ysub)
                    {
                        try
                        {
                            Item checkItem = Squares[i, j].Item;
                            if (checkItem != null)
                            {
                                if (checkItem.Color == curItem.Color)
                                    count++;
                                if (count == 5)
                                {
                                    for (i = x; i > x - 5; i--)
                                        for (j = y; j < y + 5; j++)
                                        {
                                            xSub = Math.Abs(i - x);
                                            ysub = Math.Abs(j - y);
                                            if (xSub == ysub)
                                                Squares[i, j].BackColor = System.Drawing.Color.LightGreen;
                                        }
                                    return true;
                                }
                            }
                            else
                            {
                                count = 1;
                                goto DoneLoop2;
                            }
                        }
                        catch { }
                    }
                }
            }
        DoneLoop2:
            // goc trai duoi
            for (int i = x + 1; i < x + 5; i++)
            {
                for (int j = y - 1; j > y - 5; j--)
                {
                    int xSub = Math.Abs(i - x);
                    int ysub = Math.Abs(j - y);
                    if (xSub == ysub)
                    {
                        try
                        {
                            Item checkItem = Squares[i, j].Item;
                            if (checkItem != null)
                            {
                                if (checkItem.Color == curItem.Color)
                                    count++;
                                if (count == 5)
                                {
                                    for (i = x; i < x + 5; i++)
                                        for (j = y; j > y - 5; j--)
                                        {
                                            xSub = Math.Abs(i - x);
                                            ysub = Math.Abs(j - y);
                                            if (xSub == ysub)
                                                Squares[i, j].BackColor = System.Drawing.Color.LightGreen;
                                        }
                                    return true;
                                }
                            }
                            else
                            {
                                count = 1;
                                goto DoneLoop3;
                            }
                        }
                        catch { }
                    }
                }
            }
        DoneLoop3:
            // goc trai tren
            for (int i = x + 1; i < x + 5; i++)
            {
                for (int j = y + 1; j < y + 5; j++)
                {
                    int xSub = Math.Abs(i - x);
                    int ysub = Math.Abs(j - y);
                    if (xSub == ysub)
                    {
                        try
                        {
                            Item checkItem = Squares[i, j].Item;
                            if (checkItem != null)
                            {
                                if (checkItem.Color == curItem.Color)
                                    count++;
                                if (count == 5)
                                {
                                    for (i = x; i < x + 5; i++)
                                        for (j = y; j < y + 5; j++)
                                        {
                                            xSub = Math.Abs(i - x);
                                            ysub = Math.Abs(j - y);
                                            if (xSub == ysub)
                                                Squares[i, j].BackColor = System.Drawing.Color.LightGreen;
                                        }
                                    return true;
                                }
                            }
                            else
                            {
                                count = 1;
                                goto DoneLoop4;
                            }
                        }
                        catch { }
                    }
                }
            }
        DoneLoop4:
            return false;
        }
    }
}
