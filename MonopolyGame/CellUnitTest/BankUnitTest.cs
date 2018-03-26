using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyGame;
using System.Collections.Generic;

namespace CellUnitTest
{
    [TestClass]
    public class BankUnitTest
    {
        [TestMethod]
        public void InitBank()
        {
            Dictionary<MoneyType, int> initialMoney = new Dictionary<MoneyType, int>();
            initialMoney.Add(MoneyType.FiveHundred, 2);
            initialMoney.Add(MoneyType.Hundred, 2);
            initialMoney.Add(MoneyType.Fifty, 2);
            initialMoney.Add(MoneyType.Twenty, 6);
            initialMoney.Add(MoneyType.Ten, 5);
            initialMoney.Add(MoneyType.Five, 5);
            initialMoney.Add(MoneyType.One, 5);

            Bank bank = new Bank(initialMoney);

            Assert.AreEqual(bank.RemainingMoney, 1500);
        }

        [TestMethod]
        public void DepositMoney()
        {
            Dictionary<MoneyType, int> initialMoney = new Dictionary<MoneyType, int>
            {
                { MoneyType.FiveHundred, 2 },
                { MoneyType.Hundred, 2 },
                { MoneyType.Fifty, 2 },
                { MoneyType.Twenty, 6 },
                { MoneyType.Ten, 5 },
                { MoneyType.Five, 5 },
                { MoneyType.One, 5 }
            };

            Bank bank = new Bank(initialMoney);

            Dictionary<MoneyType, int> depositMoney = new Dictionary<MoneyType, int>
            {
                { MoneyType.FiveHundred, 1 },
                { MoneyType.Hundred, 1 },
                { MoneyType.Fifty, 1 },
                { MoneyType.Twenty, 1 },
                { MoneyType.Ten, 1 },
                { MoneyType.Five, 1 },
                { MoneyType.One, 1 }
            };

            //Deposit normal value
            bank.DepositMoney(depositMoney);

            Assert.AreEqual(bank.RemainingMoney, 2186);

            //Deposit negative value
            depositMoney[MoneyType.FiveHundred] = -2;

            Assert.ThrowsException<ApplicationException>(
                () => { bank.DepositMoney(depositMoney); }
            );
            Assert.AreEqual(bank.RemainingMoney, 2186);
        }

        [TestMethod]
        public void WithdrawMoney()
        {
            Dictionary<MoneyType, int> initialMoney = new Dictionary<MoneyType, int>
            {
                { MoneyType.FiveHundred, 2 },
                { MoneyType.Hundred, 2 },
                { MoneyType.Fifty, 2 },
                { MoneyType.Twenty, 6 },
                { MoneyType.Ten, 5 },
                { MoneyType.Five, 5 },
                { MoneyType.One, 5 }
            };

            Bank bank = new Bank(initialMoney);

            Dictionary<MoneyType, int> withdrawMoneyRequest = new Dictionary<MoneyType, int>
            {
                { MoneyType.FiveHundred, 2 },
                { MoneyType.Hundred, 2 },
                { MoneyType.Fifty, 2 },
                { MoneyType.Twenty, 6 },
                { MoneyType.Ten, 5 },
                { MoneyType.Five, 5 },
                { MoneyType.One, 5 }
            };

            //Withdraw all money
            Dictionary<MoneyType, int> withdrawedMoney = bank.WithdrawMoney(withdrawMoneyRequest);
            Assert.AreSame(withdrawedMoney, withdrawMoneyRequest);

            //Check bank is 0
            Assert.AreEqual(bank.RemainingMoney, 0);

            //Withdraw again
            Assert.AreEqual(bank.WithdrawMoney(withdrawMoneyRequest), null);
        }

        [TestMethod]
        public void ChangeMoney()
        {
            Dictionary<MoneyType, int> initialMoney = new Dictionary<MoneyType, int>
            {
                { MoneyType.FiveHundred, 20 },
                { MoneyType.Hundred, 20 },
                { MoneyType.Fifty, 30 },
                { MoneyType.Twenty, 50 },
                { MoneyType.Ten, 40 },
                { MoneyType.Five, 40 },
                { MoneyType.One, 40 }
            };

            Bank bank = new Bank(initialMoney);

            //Change money that isnt same
            Dictionary<MoneyType, int> changeMoneyRequestIn = new Dictionary<MoneyType, int>
            {
                { MoneyType.FiveHundred, 1 },
                { MoneyType.Hundred, 1 },
                { MoneyType.Fifty, 1 },
                { MoneyType.Twenty, 1 },
                { MoneyType.Ten, 1 },
                { MoneyType.Five, 1 },
                { MoneyType.One, 1 }
            };
            Dictionary<MoneyType, int> changeMoneyRequestOut = new Dictionary<MoneyType, int>
            {
                { MoneyType.FiveHundred, 1 },
                { MoneyType.Hundred, 2 },
                { MoneyType.Fifty, 1 },
                { MoneyType.Twenty, 1 },
                { MoneyType.Ten, 1 },
                { MoneyType.Five, 1 },
                { MoneyType.One, 1 }
            };

            Assert.AreEqual(bank.ChangeMoney(changeMoneyRequestIn, changeMoneyRequestOut), changeMoneyRequestIn);
            Assert.AreEqual(bank.RemainingMoney, 15140);

            //Normal change money
            changeMoneyRequestOut = new Dictionary<MoneyType, int>
            {
                { MoneyType.FiveHundred, 0 },
                { MoneyType.Hundred, 6 },
                { MoneyType.Fifty, 1 },
                { MoneyType.Twenty, 1 },
                { MoneyType.Ten, 1 },
                { MoneyType.Five, 1 },
                { MoneyType.One, 1 }
            };

            Assert.AreEqual(bank.ChangeMoney(changeMoneyRequestIn, changeMoneyRequestOut), changeMoneyRequestOut);
            Assert.AreEqual(bank.RemainingMoney, 15140);

            //Normal change money with one money type insufficient
            changeMoneyRequestOut = new Dictionary<MoneyType, int>
            {
                { MoneyType.FiveHundred, 0 },
                { MoneyType.Hundred, 1 },
                { MoneyType.Fifty, 1 },
                { MoneyType.Twenty, 1 },
                { MoneyType.Ten, 1 },
                { MoneyType.Five, 101 },
                { MoneyType.One, 1 }
            };

            Assert.AreEqual(bank.ChangeMoney(changeMoneyRequestIn, changeMoneyRequestOut), changeMoneyRequestIn);
            Assert.AreEqual(bank.RemainingMoney, 15140);
        }
    }
}
