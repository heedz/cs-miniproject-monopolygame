using System;

namespace MonopolyGame
{
    [System.Runtime.Remoting.Contexts.Synchronization]
    public class LandCell : MonopolyCell
    {
        private LandType _typeLand;
        private int _price;
        public override event EventHandler CellStepped;

        public LandCell(int id, int column, int row, int color, string name, string backgroundImage, int price, LandType typeLand)
            : base(id,column, row, color, name, backgroundImage)
        {
            _id = id;
            _column = column;
            _row = row;
            _color = color;
            _name = name;
            _backgroundImage = backgroundImage;
            _price = price;
            _typeLand = typeLand;
        }

        public LandType TypeLand
        {
            get
            {
                return _typeLand;
            }
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
            LandCellEvtArgs args = new LandCellEvtArgs();
            args.Price = _price;
            args.TypeLand = _typeLand;
            CellStepped?.Invoke(this, args);
        }
    }    
}
