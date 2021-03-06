﻿namespace MonopolyGame
{
    public class JailCell : MonopolyCell
    {
        public JailCell(int id, int column, int row, int color, string name, string backgroundImage)
            : base(id, column, row, color, name, backgroundImage)
        {
            _id = id;
            _column = column;
            _row = row;
            _color = color;
            _name = name;
            _backgroundImage = backgroundImage;
        }

        public new int ID
        {
            get
            {
                return _id;
            }
        }

    }
}
