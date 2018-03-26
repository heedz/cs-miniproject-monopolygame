namespace MonopolyGame
{
    public abstract class Board
    {
        protected int _maxColumn;
        protected int _maxRow;
        protected int _backgroundColor;

        
        protected abstract void InitializeBoard();
    }
}
