using System;
namespace MonopolyGame
{
    public abstract class MonopolyCell : Cell
    {
        protected string _name;
        protected string _backgroundImage;
        protected int _id;
        public virtual event EventHandler CellStepped;
        

        public MonopolyCell(int _id, int column, int row, int color, string name, string backgroundImage)
            : base(column,row,color)
        {

        }

        public int ID
        {
            get
            {
                return _id;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string BackgroundImage
        {
            get
            {
                return _backgroundImage;
            }
        }

        public virtual void Step()
        {
            CellStepped?.Invoke(this, null);
        }

    }
}
