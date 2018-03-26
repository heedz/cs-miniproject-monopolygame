using System;
namespace MonopolyGame
{
    [System.Runtime.Remoting.Contexts.Synchronization]
    public class ActionCell : MonopolyCell
    {
        private CardType _typeCard;
        public override event EventHandler CellStepped;

        public ActionCell(int id, int column, int row, int color, string name, string backgroundImage, CardType typeCard)
            :base(id, column, row, color, name, backgroundImage)
        {
            _id = id;
            _column = column;
            _row = row;
            _color = color;
            _name = name;
            _backgroundImage = backgroundImage;
            _typeCard = typeCard;
        }        

        public CardType TypeCard
        {
            get
            {
                return _typeCard;
            }
        }

        public override void Step()
        {
            ActionCellEvtArgs args = new ActionCellEvtArgs();
            args.TypeCard = _typeCard;

            CellStepped?.Invoke(this, args);
        }

    }
}
