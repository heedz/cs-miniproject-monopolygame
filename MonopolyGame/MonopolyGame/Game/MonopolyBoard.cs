using System;
using System.Collections.Generic;


namespace MonopolyGame
{
    [System.Runtime.Remoting.Contexts.Synchronization]
    public class MonopolyBoard : Board
    {
        private MonopolyCellFactory _factoryBoard;
        private List<MonopolyCell> _listOfMonopolyCell = new List<MonopolyCell>();
        private List<Tuple<int,CellType,Tuple<int,int,int>,Tuple<string,string>,int,LandType, CardType>> _inputCells;
        public List<MonopolyCell> ListOfMonopolyCell
        {
            get
            {
                return _listOfMonopolyCell;
            }
        }

        public MonopolyBoard(int maxColumn, int maxRow, int backgroundColor,
            List<Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>> inputCells) 
        {
            // MonopolyBoard Constructor

            _maxColumn = maxColumn;
            _maxRow = maxRow;
            _backgroundColor = backgroundColor;

            _factoryBoard = new MonopolyCellFactory();
            _inputCells = inputCells;
            InitializeBoard();
        }

        protected override void InitializeBoard()
        {
            // Initialize MonopolyBoard : Create MonopolyCells

            CreateCells();      

        }

        private void CreateCells()
        {
            MonopolyCell cell;
            for (int i = 0; i < _inputCells.Count; i++)
            {
                switch(_inputCells[i].Item2)
                {
                    case CellType.StartCell:
                        cell = _factoryBoard.Create(_inputCells[i].Item1,_inputCells[i].Item2,_inputCells[i].Item3.Item1, _inputCells[i].Item3.Item2,
                            _inputCells[i].Item3.Item3, _inputCells[i].Item4.Item1, _inputCells[i].Item4.Item2);
                        break;
                    case CellType.PayTaxCell:
                        cell = _factoryBoard.Create(_inputCells[i].Item1, _inputCells[i].Item2, _inputCells[i].Item3.Item1, _inputCells[i].Item3.Item2,
                            _inputCells[i].Item3.Item3, _inputCells[i].Item4.Item1, _inputCells[i].Item4.Item2, _inputCells[i].Item5);
                        break;
                    case CellType.JailCell:
                        cell = _factoryBoard.Create(_inputCells[i].Item1, _inputCells[i].Item2, _inputCells[i].Item3.Item1, _inputCells[i].Item3.Item2,
                            _inputCells[i].Item3.Item3, _inputCells[i].Item4.Item1, _inputCells[i].Item4.Item2);
                        break;
                    case CellType.GotoJailCell:
                        cell = _factoryBoard.Create(_inputCells[i].Item1, _inputCells[i].Item2, _inputCells[i].Item3.Item1, _inputCells[i].Item3.Item2,
                            _inputCells[i].Item3.Item3, _inputCells[i].Item4.Item1, _inputCells[i].Item4.Item2);
                        break;
                    case CellType.FreeParkingCell:
                        cell = _factoryBoard.Create(_inputCells[i].Item1, _inputCells[i].Item2, _inputCells[i].Item3.Item1, _inputCells[i].Item3.Item2,
                            _inputCells[i].Item3.Item3, _inputCells[i].Item4.Item1, _inputCells[i].Item4.Item2);
                        break;
                    case CellType.ActionCell:
                        cell = _factoryBoard.Create(_inputCells[i].Item1, _inputCells[i].Item2, _inputCells[i].Item3.Item1, _inputCells[i].Item3.Item2,
                            _inputCells[i].Item3.Item3, _inputCells[i].Item4.Item1, _inputCells[i].Item4.Item2,_inputCells[i].Item7);
                        break;
                    case CellType.LandCell:
                        cell = _factoryBoard.Create(_inputCells[i].Item1, _inputCells[i].Item2, _inputCells[i].Item3.Item1, _inputCells[i].Item3.Item2,
                            _inputCells[i].Item3.Item3, _inputCells[i].Item4.Item1, _inputCells[i].Item4.Item2, _inputCells[i].Item5, _inputCells[i].Item6);
                        break;
                    default:
                        cell = _factoryBoard.Create(_inputCells[i].Item1, _inputCells[i].Item2, _inputCells[i].Item3.Item1, _inputCells[i].Item3.Item2,
                            _inputCells[i].Item3.Item3, _inputCells[i].Item4.Item1, _inputCells[i].Item4.Item2, _inputCells[i].Item5, _inputCells[i].Item6);
                        break;                       
                }
                _listOfMonopolyCell.Add(cell);
            }
        }
        


    }
}
