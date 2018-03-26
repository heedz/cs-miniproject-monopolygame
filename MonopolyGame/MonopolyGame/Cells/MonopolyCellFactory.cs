using System;
using System.Runtime.InteropServices;

namespace MonopolyGame
{
    [System.Runtime.Remoting.Contexts.Synchronization]
    public class MonopolyCellFactory
    {
        public MonopolyCell Create(int id, CellType type, int column, int row, int color, string name, string backgroundImage)
        { 
            switch(type)
            {
                case CellType.StartCell:
                    return new StartCell(id, column, row, color, name, backgroundImage);
                case CellType.FreeParkingCell:
                    return new FreeParkingCell(id, column, row, color, name, backgroundImage);
                case CellType.GotoJailCell:
                    return new GotoJailCell(id, column, row, color, name, backgroundImage);
                case CellType.JailCell:
                    return new JailCell(id, column, row, color, name, backgroundImage);              
                default:
                    return new StartCell(id, column, row, color, name, backgroundImage);

            }
        }
        public MonopolyCell Create(int id, CellType type, int column, int row, int color, string name, string backgroundImage,
            int price, [Optional] LandType landType)
        {
            switch (type)
            {                
                case CellType.PayTaxCell:
                    return new PayTaxCell(id, column, row, color, name, backgroundImage, price);
                case CellType.LandCell:
                    return new LandCell(id, column, row, color, name, backgroundImage, price, landType);
                default:
                    return new LandCell(id, column, row, color, name, backgroundImage, price, landType);

            }
        }
        public MonopolyCell Create(int id, CellType type, int column, int row, int color, string name, string backgroundImage,
            CardType cardType)
        {
            return new ActionCell(id, column, row, color, name, backgroundImage, cardType);
        }
    }
}
