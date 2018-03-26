using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyGame;
using System.IO;


namespace MonopolyUnitTest
{
    [TestClass]
    public class MonopolyPlayerUnitTest
    {
        private MonopolyPlayer mplayer;
        Dictionary<MoneyType, int> init;
        LandCard mediteranianCard;
        LandCell cell;
        [TestInitialize]
        public void Setup()
        {
           
            mplayer = new MonopolyPlayer(1,"player 1");
            init = new Dictionary<MoneyType, int>();
            init[MoneyType.Ten] = 4;
            init[MoneyType.One] = 10;
            init[MoneyType.Five] = 2;
            init[MoneyType.Twenty] = 2;
            init[MoneyType.Fifty] = 2;
            init[MoneyType.Hundred] = 2;
            mplayer.Receive(init);
            
        }

        [TestMethod]
        public void MonopolyPlayerTest()
        {
            Assert.AreEqual(mplayer.TotalMoney, 400);
        }

        [TestMethod]
        public void MonopolyPlayerBuyLandTest()
        {
            
            // Act
            BuyLand();
            // Assert
            Assert.AreEqual(mplayer.LandCards.Count, 1);
            Assert.AreEqual(mplayer.TotalMoney, 300);

        }

        [TestMethod]
        public void MonopolyPlayerBuyLandErrorTest()
        {

            // Act
            BuyLand();
            Tuple<ReturnState, Dictionary<MoneyType, int>> actualReturn;

            Dictionary<PropertyType, int> prices = new Dictionary<PropertyType, int>();
            prices[PropertyType.House] = 100;
            prices[PropertyType.Hotel] = 500;

            Dictionary<int, int> rents = new Dictionary<int, int>();

            rents[1] = 10;
            rents[2] = 20;
            rents[3] = 30;
            rents[4] = 40;
            rents[5] = 50;

            // Buy same land again --LandAlreadyBoughtError 
            using (StringReader sr = new StringReader("10\n2\n4\n2\n0"))
            {
                Console.SetIn(sr);
                actualReturn = mplayer.BuyLand(mediteranianCard, cell);
            }
            Assert.AreEqual(ReturnState.LandAlreadyBoughtError, actualReturn.Item1);
            Assert.AreEqual(mplayer.LandCards.Count, 1);
            Assert.AreEqual(mplayer.TotalMoney, 300);

            // buy another land -- InsufficientMoney

            LandCell cellUS = new LandCell(5, 5, 1, 1, "US Avenue", "US Avenue", 500, LandType.State);
            LandCard USCard = new LandCard("US Avenue", prices, rents, 50);

            using (StringReader sr = new StringReader("0\n0\n0\n0\n4\n3"))
            {
                Console.SetIn(sr);
                actualReturn = mplayer.BuyLand(USCard, cellUS);
            }
            Assert.AreEqual(ReturnState.InsufficientMoney, actualReturn.Item1);
            Assert.AreEqual(mplayer.LandCards.Count, 1);
            Assert.AreEqual(mplayer.TotalMoney, 300);
        }

        [TestMethod]
        public void MonopolyPlayerBuyWithChangeTest()
        {
            Dictionary<PropertyType, int> prices = new Dictionary<PropertyType, int>();
            prices[PropertyType.House] = 100;
            prices[PropertyType.Hotel] = 500;

            Dictionary<int, int> rents = new Dictionary<int, int>();

            rents[1] = 10;
            rents[2] = 20;
            rents[3] = 30;
            rents[4] = 40;
            rents[5] = 50;

            Tuple<ReturnState, Dictionary<MoneyType, int>> actualReturn;
            cell = new LandCell(2, 2, 1, 1, "Mediterranian Avenue", "Mediterranian Avenue", 80, LandType.State);
            mediteranianCard = new LandCard("Mediterranian Avenue", prices, rents, 50);

            using (StringReader sr = new StringReader("0\n0\n0\n0\n0\n1\n0\n0\n0\n2")) 
            {
                Console.SetIn(sr);
                actualReturn = mplayer.BuyLand(mediteranianCard, cell);
            }

            Assert.AreEqual(ReturnState.Success, actualReturn.Item1);
            Assert.AreEqual(mplayer.LandCards.Count, 1);
            Assert.AreEqual(mplayer.TotalMoney, 320);

        }


        [TestMethod]
        public void MonopolyPlayerBuyPropertyTest()
        {
            // Buy Land
            BuyLand();

            Tuple<ReturnState, Dictionary<MoneyType, int>> actualReturn;

            // buy 3 house            
            using (StringReader sr = new StringReader("0\n0\n0\n0\n2\n2"))
            {
                Console.SetIn(sr);
                actualReturn = mplayer.BuyProperty(mediteranianCard, PropertyType.House, 3);
            }            
                    
            Assert.AreEqual(ReturnState.Success, actualReturn.Item1);
            Assert.AreEqual(3, mediteranianCard.Properties.Count);
            Assert.AreEqual(mplayer.TotalMoney, 0);

        }

        [TestMethod]
        public void MonopolyPlayerBuyPropertyErrorTest()
        {
            // Buy Land
            BuyLand();

            // buy 1 hotel -- ValidateBuyPropertyError
            Tuple<ReturnState, Dictionary<MoneyType, int>> actualReturn = mplayer.BuyProperty(mediteranianCard, PropertyType.Hotel, 1);
            Assert.AreEqual(ReturnState.ValidateBuyPropertyError, actualReturn.Item1);

            // buy 3 house: PayPriceMismatchError            
            using (StringReader sr = new StringReader("0\n0\n0\n0\n2\n3"))
            {
                Console.SetIn(sr);
                actualReturn = mplayer.BuyProperty(mediteranianCard, PropertyType.House, 3);
            }

            Assert.AreEqual(ReturnState.PayPriceMismatchError, actualReturn.Item1);
            Assert.AreEqual(0, mediteranianCard.Properties.Count);
            Assert.AreEqual(mplayer.TotalMoney, 300);

            // buy 4 house --  InsufficientMoney          
            using (StringReader sr = new StringReader("0\n0\n0\n0\n2\n3"))
            {
                Console.SetIn(sr);
                actualReturn = mplayer.BuyProperty(mediteranianCard, PropertyType.House, 4);
            }

            Assert.AreEqual(ReturnState.InsufficientMoney, actualReturn.Item1);
            Assert.AreEqual(0, mediteranianCard.Properties.Count);
            Assert.AreEqual(mplayer.TotalMoney, 300);

        }


        [TestMethod]
        public void MonopolyPlayerSellPropertyTest()
        {
            AddMoney();
            Assert.AreEqual(1000,mplayer.TotalMoney);
            BuyLand();
            BuyPropertyHouse();
            BuyPropertyHotel();
            Assert.AreEqual(0, mplayer.TotalMoney);

            // sell 2 house
            Tuple<ReturnState, Dictionary<MoneyType, int>> actualReturn = mplayer.SellProperty(mediteranianCard, PropertyType.House, 2);
            Assert.AreEqual(ReturnState.ValidateSellPropertyError, actualReturn.Item1);

            // sell 1 hotel
            using (StringReader sr = new StringReader("0\n0\n0\n0\n3\n1\n0"))
            {
                Console.SetIn(sr);
                actualReturn = mplayer.SellProperty(mediteranianCard, PropertyType.Hotel, 1);
                Assert.AreEqual(ReturnState.Success, actualReturn.Item1);
            }
            
            Assert.AreEqual(250, mplayer.TotalMoney);

        }

        [TestMethod]
        public void MonopolyPlayerSellPropertyWithChangeTest()
        {
            AddMoney();
            Assert.AreEqual(1000, mplayer.TotalMoney);
            BuyLand();
            BuyPropertyHouse();
            BuyPropertyHotel();
            Assert.AreEqual(0, mplayer.TotalMoney);

            // sell 2 house
            Tuple<ReturnState, Dictionary<MoneyType, int>> actualReturn = mplayer.SellProperty(mediteranianCard, PropertyType.House, 2);
            Assert.AreEqual(ReturnState.ValidateSellPropertyError, actualReturn.Item1);

            // sell 1 hotel
            using (StringReader sr = new StringReader("0\n0\n0\n0\n3\n1\n0"))
            {
                Console.SetIn(sr);
                actualReturn = mplayer.SellProperty(mediteranianCard, PropertyType.Hotel, 1);
                Assert.AreEqual(ReturnState.Success, actualReturn.Item1);
            }

            Assert.AreEqual(250, mplayer.TotalMoney);
            // sell 1 house : 50
            using (StringReader sr = new StringReader("0\n0\n0\n0\n0\n1\n0\n0\n0\n0\n0\n1\n0\n0"))
            {
                Console.SetIn(sr);
                actualReturn = mplayer.SellProperty(mediteranianCard, PropertyType.House, 1);
                Assert.AreEqual(ReturnState.Success, actualReturn.Item1);
            }
            Assert.AreEqual(300, mplayer.TotalMoney);

        }

        [TestMethod]
        public void MonopolyPlayerMortgageErrorTest()
        {
            Tuple<ReturnState, Dictionary<MoneyType, int>> actualReturn;
            AddMoney();
            Assert.AreEqual(1000, mplayer.TotalMoney);
            BuyLand();
            BuyPropertyHouse();         
            
            // sell 3 house
            using (StringReader sr = new StringReader("0\n0\n0\n0\n3\n0\n0"))
            {
                Console.SetIn(sr);
                actualReturn = mplayer.SellProperty(mediteranianCard, PropertyType.House, 3);
                Assert.AreEqual(ReturnState.Success, actualReturn.Item1);
            }
            
            Assert.AreEqual(650, mplayer.TotalMoney);

            actualReturn = MortgageLand(mediteranianCard);
            Assert.AreEqual(ReturnState.MortgageError, actualReturn.Item1);
            Assert.AreEqual(650, mplayer.TotalMoney);
        }

        [TestMethod]
        public void MonopolyPlayerMortgageTest()
        {
            Tuple<ReturnState, Dictionary<MoneyType, int>> actualReturn;
            AddMoney();
            BuyLand();
            BuyPropertyHouse();

            // sell 3 house            
            using (StringReader sr = new StringReader("0\n0\n0\n0\n2\n1\n0"))
            {
                Console.SetIn(sr);
                actualReturn = mplayer.SellProperty(mediteranianCard, PropertyType.House, 4);
                Assert.AreEqual(ReturnState.Success, actualReturn.Item1);
            }
            
            Assert.AreEqual(700, mplayer.TotalMoney);

            actualReturn = MortgageLand(mediteranianCard);
            Assert.AreEqual(ReturnState.Success, actualReturn.Item1);
            Assert.AreEqual(750, mplayer.TotalMoney);
        }

        public Tuple<ReturnState, Dictionary<MoneyType, int>> MortgageLand(LandCard card)
        {
            Tuple<ReturnState, Dictionary<MoneyType, int>> actualReturn;
            
            using (StringReader sr = new StringReader("0\n0\n0\n0\n1\n0\n0"))
            {
                Console.SetIn(sr);
                actualReturn = mplayer.MortgageLand(card);                    
            }            
            return actualReturn;
        }

        [TestMethod]
        public void AddAndRemoveActionCardTest()
        {
            IActionCard chance = new ChanceCard("Advance to Start", "Start", null);
            
            // add chance card
            mplayer.AddActionCard(chance);
            Assert.AreEqual(mplayer.ActionCards.Contains(chance), true);

            // remove chance card
            mplayer.RemoveActionCard(chance);
            Assert.AreEqual(mplayer.ActionCards.Contains(chance), false);
        }

        public void BuyPropertyHouse()
        {
            
            using (StringReader sr = new StringReader("0\n0\n0\n0\n2\n3"))
            {
                Console.SetIn(sr);
                mplayer.BuyProperty(mediteranianCard, PropertyType.House, 4);
            }            
        }

        public void AddMoney()
        {
            Dictionary<MoneyType, int> add = new Dictionary<MoneyType, int>
            {
                {MoneyType.Hundred,1 },
                {MoneyType.FiveHundred,1 }

            };
            mplayer.Receive(add);
        }

        public void BuyPropertyHotel()
        {
            using (StringReader sr = new StringReader("0\n0\n0\n0\n0\n0\n1"))
            {
                Console.SetIn(sr);
                mplayer.BuyProperty(mediteranianCard, PropertyType.Hotel, 1);
            }            
        }

        public void BuyLand()
        {
            Dictionary<PropertyType, int> prices = new Dictionary<PropertyType, int>();
            prices[PropertyType.House] = 100;
            prices[PropertyType.Hotel] = 500;

            Dictionary<int, int> rents = new Dictionary<int, int>();

            rents[1] = 10;
            rents[2] = 20;
            rents[3] = 30;
            rents[4] = 40;
            rents[5] = 50;

            cell = new LandCell(2, 2, 1, 1, "Mediterranian Avenue", "Mediterranian Avenue", 100, LandType.State);
            mediteranianCard = new LandCard("Mediterranian Avenue", prices, rents, 50);

            using (StringReader sr = new StringReader("10\n2\n4\n2\n0"))            
            {
                Console.SetIn(sr);
                mplayer.BuyLand(mediteranianCard, cell);
            }            
        }

    }
}
