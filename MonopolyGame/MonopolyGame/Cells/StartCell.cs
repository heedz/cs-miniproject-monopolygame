using System;

namespace MonopolyGame
{
    [System.Runtime.Remoting.Contexts.Synchronization]
    public class StartCell : MonopolyCell
    {
        public event EventHandler CellPassed;
        public StartCell(int id, int column, int row, int color, string name, string backgroundImage)
            : base(id, column, row, color, name, backgroundImage)
        {
            _id = id;
            _column = column;
            _row = row;
            _color = color;
            _name = name;
            _backgroundImage = backgroundImage;
            
        }
        
        public void Pass()
        {
            OnCellPassed(EventArgs.Empty);
        }

        public void OnCellPassed(EventArgs evt)
        {
            CellPassed?.Invoke(this, evt);
        }
    }
}
