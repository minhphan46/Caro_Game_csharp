using CaroGame.bin.Debug;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaroGame
{
    internal class Square : PictureBox
    {
        // board contain square
        protected Board _board;
        public Board Board
        {
            get { return _board; }
            set { _board = value; }
        }
        protected Item _item;
        public Item Item
        {
            get { return _item; }
            set
            {
                _item = value;
                if (_item != null) Image = _item.Image;
                else Image = null;
            }
        }
        protected int _x;
        protected int _y;
        public int X { get { return _x; } set { _x = value; } }
        public int Y { get { return _y; } set { _y = value; } }

        protected bool _selected;
        public bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
            }
        }

        public Square(int x, int y, Board board, Item item = null)
        {
            Board = board;
            X = x;
            Y = y;

            SizeMode = PictureBoxSizeMode.StretchImage;
            BorderStyle = BorderStyle.FixedSingle;
            BackColor = Color.White;

            Item = item;
            this.MouseClick += new MouseEventHandler(OnMouse_Click);
        }
        private void OnMouse_Click(object sender, MouseEventArgs e)
        {
            Square sq = (Square)sender;
            Board board = sq._board;

            if (sq.Item == null && board.colorBefore == ItemColor.Blue)
            {
                sq.Selected = true;
                sq.Item = new Item(sq, ItemColor.Red);
                board.colorBefore = ItemColor.Red;
            }
            else if (sq.Item == null && board.colorBefore == ItemColor.Red)
            {
                sq.Selected = true;
                sq.Item = new Item(sq, ItemColor.Blue);
                board.colorBefore = ItemColor.Blue;
            }
            if (board.CheckGameOver(sq))
            {
                DialogResult result;
                if (sq.Item.Color == ItemColor.Red)
                    result = MessageBox.Show("BLUE WIN");
                else
                    result = MessageBox.Show("RED WIN");
                if (result == DialogResult.OK)
                {
                    for (int i = 0; i < board.SizeOfBoard; i++)
                    {
                        for (int j = 0; j < board.SizeOfBoard; j++)
                        {
                            board.Squares[i, j].Item = null;
                            board.Squares[i, j].BackColor = Color.White;
                        }
                    }
                }
            }
        }
    }
}
