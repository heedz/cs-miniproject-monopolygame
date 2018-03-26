using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyGame;

namespace MonopolyUnitTest
{
    [TestClass]
    public class MonopolyBoardUnitTest
    {
        
        [TestMethod]
        public void InitializeBoardTest()
        {
            MonopolyBoard board = new MonopolyBoard(11, 11, 1, CellsData);
            int expectedNumberOfCell = 40;
            int expextedNumberOfLandCell = 28;
            int expextedNumberOfActionCell = 6;
            int expectedNumberOfTaxCell = 2;

            // cek jumlah MonopolyCell
            Assert.AreEqual(board.ListOfMonopolyCell.Count, expectedNumberOfCell);

            // cek jumlah LandCell
            List<MonopolyCell> cells = board.ListOfMonopolyCell.FindAll(FindLandCell);        
            Assert.AreEqual(cells.Count, expextedNumberOfLandCell);

            // cek jumlah ActionCell
            cells = board.ListOfMonopolyCell.FindAll(FindActionCell);
            Assert.AreEqual(cells.Count, expextedNumberOfActionCell);

            // cek jumlah TaxCell
            cells = board.ListOfMonopolyCell.FindAll(FindTaxCell);
            Assert.AreEqual(cells.Count, expectedNumberOfTaxCell);

            // cek StartCell exist
            StartCell start = board.ListOfMonopolyCell[0] as StartCell;
            Assert.AreNotEqual(start, null);

            // cek JailCell exist
            JailCell jail = board.ListOfMonopolyCell[10] as JailCell;
            Assert.AreNotEqual(jail, null);

            // cek FreeParkingCell exist
            FreeParkingCell free = board.ListOfMonopolyCell[20] as FreeParkingCell;
            Assert.AreNotEqual(free, null);

            // cek GotoJailCell exist
            GotoJailCell gotojail = board.ListOfMonopolyCell[30] as GotoJailCell;
            Assert.AreNotEqual(gotojail, null);
        }        

        private static bool FindLandCell(MonopolyCell cell)
        {
            if (cell as LandCell != null)            
                return true;          
                        
            return false;      
        }

        private static bool FindActionCell(MonopolyCell cell)
        {
            if (cell as ActionCell != null)
                return true;

            return false;
        }

        private static bool FindTaxCell(MonopolyCell cell)
        {
            if (cell as PayTaxCell != null)
                return true;

            return false;
        }

        public List<Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>> CellsData { get; }
        = new List<Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>>
        {
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (0,CellType.StartCell,new Tuple<int, int, int>(11,1,1),
                new Tuple<string, string>("Start","Start"),0,LandType.None,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (1,CellType.LandCell,new Tuple<int, int, int>(10,1,1),
                new Tuple<string, string>("USA","USA"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (2,CellType.ActionCell,new Tuple<int, int, int>(9,1,1),
                new Tuple<string, string>("Community Chest","Community Chest"),0,LandType.None,CardType.CommunityChest)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (3,CellType.LandCell,new Tuple<int, int, int>(8,1,1),
                new Tuple<string, string>("Canada","Canada"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (4,CellType.PayTaxCell,new Tuple<int, int, int>(7,1,1),
                new Tuple<string, string>("Start","Start"),20,LandType.None,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (5,CellType.LandCell,new Tuple<int, int, int>(6,1,1),
                new Tuple<string, string>("Station 1","Station 1"),100,LandType.Station,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (6,CellType.LandCell,new Tuple<int, int, int>(5,1,1),
                new Tuple<string, string>("Start","Start"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (7,CellType.ActionCell,new Tuple<int, int, int>(4,1,1),
                new Tuple<string, string>("Start","Start"),0,LandType.None,CardType.Chance)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (8,CellType.LandCell,new Tuple<int, int, int>(3,1,1),
                new Tuple<string, string>("Start","Start"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (9,CellType.LandCell,new Tuple<int, int, int>(2,1,1),
                new Tuple<string, string>("Start","Start"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (10,CellType.JailCell,new Tuple<int, int, int>(1,1,1),
                new Tuple<string, string>("Start","Start"),0,LandType.None,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (11,CellType.LandCell,new Tuple<int, int, int>(1,2,1),
                new Tuple<string, string>("Start","Start"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (12,CellType.LandCell,new Tuple<int, int, int>(1,3,1),
                new Tuple<string, string>("Start","Start"),100,LandType.Coorporation,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (13,CellType.LandCell,new Tuple<int, int, int>(1,4,1),
                new Tuple<string, string>("Start","Start"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (14,CellType.LandCell,new Tuple<int, int, int>(1,5,1),
                new Tuple<string, string>("Start","Start"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (15,CellType.LandCell,new Tuple<int, int, int>(1,6,1),
                new Tuple<string, string>("Start","Start"),100,LandType.Station,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (16,CellType.LandCell,new Tuple<int, int, int>(1,7,1),
                new Tuple<string, string>("Start","Start"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (17,CellType.ActionCell,new Tuple<int, int, int>(1,8,1),
                new Tuple<string, string>("Start","Start"),0,LandType.None,CardType.CommunityChest)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (18,CellType.LandCell,new Tuple<int, int, int>(1,9,1),
                new Tuple<string, string>("Start","Start"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (19,CellType.LandCell,new Tuple<int, int, int>(1,10,1),
                new Tuple<string, string>("Start","Start"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (20,CellType.FreeParkingCell,new Tuple<int, int, int>(1,11,1),
                new Tuple<string, string>("Start","Start"),0,LandType.None,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (21,CellType.LandCell,new Tuple<int, int, int>(2,11,1),
                new Tuple<string, string>("Start","Start"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (22,CellType.ActionCell,new Tuple<int, int, int>(3,11,1),
                new Tuple<string, string>("Start","Start"),0,LandType.None,CardType.Chance)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (23,CellType.LandCell,new Tuple<int, int, int>(4,11,1),
                new Tuple<string, string>("Start","Start"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (24,CellType.LandCell,new Tuple<int, int, int>(5,11,1),
                new Tuple<string, string>("Start","Start"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (25,CellType.LandCell,new Tuple<int, int, int>(6,11,1),
                new Tuple<string, string>("Start","Start"),0,LandType.Station,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (26,CellType.LandCell,new Tuple<int, int, int>(7,11,1),
                new Tuple<string, string>("Start","Start"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (27,CellType.LandCell,new Tuple<int, int, int>(8,11,1),
                new Tuple<string, string>("Start","Start"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (28,CellType.LandCell,new Tuple<int, int, int>(9,11,1),
                new Tuple<string, string>("Start","Start"),0,LandType.Coorporation,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (29,CellType.LandCell,new Tuple<int, int, int>(10,11,1),
                new Tuple<string, string>("Start","Start"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (30,CellType.GotoJailCell,new Tuple<int, int, int>(11,11,1),
                new Tuple<string, string>("Start","Start"),0,LandType.None,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (31,CellType.LandCell,new Tuple<int, int, int>(11,10,1),
                new Tuple<string, string>("Start","Start"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (32,CellType.LandCell,new Tuple<int, int, int>(11,9,1),
                new Tuple<string, string>("Start","Start"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (33,CellType.ActionCell,new Tuple<int, int, int>(11,8,1),
                new Tuple<string, string>("Start","Start"),0,LandType.None,CardType.CommunityChest)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (34,CellType.LandCell,new Tuple<int, int, int>(11,7,1),
                new Tuple<string, string>("Start","Start"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (35,CellType.LandCell,new Tuple<int, int, int>(11,6,1),
                new Tuple<string, string>("Start","Start"),0,LandType.Station,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (36,CellType.ActionCell,new Tuple<int, int, int>(11,5,1),
                new Tuple<string, string>("Start","Start"),0,LandType.None,CardType.Chance)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (37,CellType.LandCell,new Tuple<int, int, int>(11,4,1),
                new Tuple<string, string>("Start","Start"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (38,CellType.PayTaxCell,new Tuple<int, int, int>(11,3,1),
                new Tuple<string, string>("Start","Start"),20,LandType.None,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (39,CellType.LandCell,new Tuple<int, int, int>(11,2,1),
                new Tuple<string, string>("Start","Start"),100,LandType.State,CardType.None)},

        };


    }
}
