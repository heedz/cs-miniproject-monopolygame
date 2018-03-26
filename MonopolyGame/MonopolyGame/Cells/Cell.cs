namespace MonopolyGame
{
    public abstract class Cell
    {
        protected int _column;
        protected int _row;
        protected int _color;

        public Cell(int column, int row, int color)
        {
            _color = color;
            _row = row;
            _column = column;
        }
        public int Column
        {
            get { return _column; }
        }

        public int Row
        {
            get { return _row; }
        }

        public int Color
        {
            get { return _color; }
        }
    }
}
