using System;

namespace MonopolyGame
{
    [System.Runtime.Remoting.Contexts.Synchronization]
    public class PayTaxCell : MonopolyCell
    {
        private int _price;
        public override event EventHandler CellStepped;

        public PayTaxCell(int id, int column, int row, int color, string name, string backgroundImage, int price)
            : base(id, column, row, color, name, backgroundImage)
        {
            _id = id;
            _column = column;
            _row = row;
            _color = color;
            _name = name;
            _backgroundImage = backgroundImage;
            _price = price;
        }
                
        public int Price
        {
            get
            {
                return _price;
            }
        }
        public override void Step()
        {
            PayTaxCellEvtArgs args = new PayTaxCellEvtArgs();
            args.Price = _price;
            
            CellStepped?.Invoke(this, args);
        }

    }
}
