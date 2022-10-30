using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CaroGame
{
    internal class Program
    {
        static public int heightForm = 300;
        static public int widthForm = 300;

        static void Main(string[] args)
        {
            Form form = new Form();
            form.Text = "Caro Game";
            Board board = new Board(form);
            heightForm = board.SizeOfBoard * board.SquareHeight + (int)(board.SquareHeight * 0.65) + 20;
            widthForm = board.SizeOfBoard * board.SquareWidth + (int)(board.SquareWidth * 0.65);
            form.Size = new System.Drawing.Size(widthForm, heightForm);
            Application.Run(form);
        }
    }
}
