using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyGame;

namespace MonopolyUnitTest
{
    [TestClass]
    public class LandCardUnitTest
    {
        [TestMethod]
        public void LandCardConstructorTest()
        {
            Dictionary<PropertyType, int> prices = new Dictionary<PropertyType, int>();
            prices[PropertyType.House] = 100;
            prices[PropertyType.Hotel] = 500;
            
            Dictionary<int, int> rents = new Dictionary<int, int>();
            rents[0] = 5;
            rents[1] = 10;
            rents[2] = 20;
            rents[3] = 30;
            rents[4] = 40;
            rents[5] = 50;

            LandCard mediteranianCard = new LandCard("Mediterranian Avenue", prices, rents, 50);

            ReturnState actualReturn;

            int price = mediteranianCard.RentPrice;
            Assert.AreEqual(5, price);

            
            // cek for buy 2 house
            bool validation = mediteranianCard.ValidateBuyProperty(PropertyType.House, 2);
            Assert.AreEqual(true, validation);

            // cek for buy 1 hotel
            validation = mediteranianCard.ValidateBuyProperty(PropertyType.Hotel, 1);
            Assert.AreEqual(false, validation);

            // add 1 hotel error
            actualReturn = mediteranianCard.AddProperties(PropertyType.Hotel, 1);
            Assert.AreEqual(ReturnState.ValidateBuyPropertyError, actualReturn);

            // add 2 house
            mediteranianCard.AddProperties(PropertyType.House, 2);            

            // cek properties count
            Assert.AreEqual(2, mediteranianCard.Properties.Count);

            // cek rent prince 
            Assert.AreEqual(rents[mediteranianCard.Properties.Count], mediteranianCard.RentPrice);

            // add 2 house
            mediteranianCard.AddProperties(PropertyType.House, 2);

            // cek properties count
            Assert.AreEqual(4, mediteranianCard.Properties.Count);
            // cek rent price
            Assert.AreEqual(rents[mediteranianCard.Properties.Count], mediteranianCard.RentPrice);

            // cek for buy 1 hotel
            validation = mediteranianCard.ValidateBuyProperty(PropertyType.Hotel, 1);
            Assert.AreEqual(true, validation);

            // add 1 hotel
            actualReturn = mediteranianCard.AddProperties(PropertyType.Hotel, 1);
            Assert.AreEqual(ReturnState.Success, actualReturn);

            // cek properties count
            Assert.AreEqual(5, mediteranianCard.Properties.Count);
            // cek rent price
            Assert.AreEqual(rents[mediteranianCard.Properties.Count], mediteranianCard.RentPrice);

            // sell 1 house
            // cek
            validation = mediteranianCard.ValidateSellProperty(PropertyType.House, 1);
            Assert.AreEqual(false, validation);

            actualReturn = mediteranianCard.RemoveProperties(PropertyType.House, 1);
            Assert.AreEqual(ReturnState.ValidateSellPropertyError, actualReturn);

            // sell 1 hotel
            validation = mediteranianCard.ValidateSellProperty(PropertyType.Hotel, 1);
            Assert.AreEqual(true, validation);
            actualReturn = mediteranianCard.RemoveProperties(PropertyType.Hotel, 1);
            Assert.AreEqual(ReturnState.Success, actualReturn);

            // sell 3 house
            validation = mediteranianCard.ValidateSellProperty(PropertyType.House, 3);
            Assert.AreEqual(true, validation);
            actualReturn =  mediteranianCard.RemoveProperties(PropertyType.House, 3);
            Assert.AreEqual(ReturnState.Success, actualReturn);
            // cek properties count
            Assert.AreEqual(1, mediteranianCard.Properties.Count);
            // cek rent price
            Assert.AreEqual(rents[mediteranianCard.Properties.Count], mediteranianCard.RentPrice);


        }
    }
}
