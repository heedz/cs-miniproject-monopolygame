using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyGame;
using System.Collections.Generic;

namespace MonopolyUnitTest
{
    [TestClass]
    public class CellUnitTest
    {
        [TestMethod]
        public void OnLandCellSteppedTest()
        {
            List<int> receivedEvents = new List<int>();
            LandCell cell = new LandCell(2, 2, 1, 1, "Mediterranian Avenue", "Mediterranian Avenue", 100, LandType.State);
            
            cell.CellStepped += (object sender, LandCellEvtArgs e) => receivedEvents.Add(e.Price);
            
            cell.Step();

            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual(cell.Price, receivedEvents[0]);
        }

        [TestMethod]
        public void OnStartCellTest()
        {
            List<EventArgs> receivedEvents = new List<EventArgs>();
            StartCell cell = new StartCell(1, 11, 1, 1, "Start", "Start");

            cell.CellPassed += (object sender, EventArgs e) => receivedEvents.Add(e);
            
            cell.CellStepped +=  (object sender, EventArgs e) => receivedEvents.Add(e);
            
            cell.Pass();
            cell.Step();
            Assert.AreEqual(2, receivedEvents.Count);
            Assert.AreEqual(EventArgs.Empty, receivedEvents[0]);
        }

        [TestMethod]
        public void OnCellSteppedTest()
        {
            List<EventArgs> receivedEvents = new List<EventArgs>();
            JailCell cell = new JailCell(1, 11, 1, 1, "Jail", "Jail");

            cell.CellStepped += (object sender, EventArgs e) => receivedEvents.Add(e);
            
            cell.Step();
            
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual(EventArgs.Empty, receivedEvents[0]);
        }

        [TestMethod]
        public void OnTaxCellSteppedTest()
        {
            List<int> receivedEvents = new List<int>();
            PayTaxCell cell = new PayTaxCell(1, 11, 1, 1, "Pay Tax", "Pay Tax", 100);

            cell.CellStepped += (object sender, PayTaxCellEvtArgs e) => receivedEvents.Add(e.Price);
            
            cell.Step();

            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual(cell.Price, receivedEvents[0]);
        }

        [TestMethod]
        public void OnActionCellSteppedTest()
        {
            List<CardType> receivedEvents = new List<CardType>();
            ActionCell cell = new ActionCell(1, 11, 1, 1, "Chance", "Chance", CardType.Chance);

            cell.CellStepped += (object sender, ActionCellEvtArgs e) => receivedEvents.Add(e.TypeCard);
            
            cell.Step();
            Assert.AreEqual(1, receivedEvents.Count);
            Assert.AreEqual(cell.TypeCard, receivedEvents[0]);
        }
    }
}
