using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaroGame.bin.Debug
{
    public enum ItemColor { Red, Blue };
    internal class Item
    {
        protected Square _square;
        public Square Square { get { return _square; } set { _square = value; } }

        protected Image _image;
        public Image Image
        {
            get { return _image; }
        }
        protected ItemColor _color;
        public ItemColor Color
        {
            get { return _color; }
            set
            {
                if (value == ItemColor.Blue)
                    _image = Images.oImage;
                else if (value == ItemColor.Red)
                    _image = Images.xImage;

                _color = value;
            }
        }
        public Item(Square sq, ItemColor color)
        {
            Color = color;
            _square = sq;
        }
    }
}
