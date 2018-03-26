using System;
using System.Collections.Generic;

namespace MonopolyGame
{
    [System.Runtime.Remoting.Contexts.Synchronization]
    public class Bank
    {
        private Dictionary<MoneyType, int> _listOfMoney;
        public int RemainingMoney
        {
            get
            {
                return MoneyToInt(_listOfMoney);
            }
        }

        public Bank(Dictionary<MoneyType, int> initialMoney)
        {
            _listOfMoney = initialMoney;
        }

        public void DepositMoney(Dictionary<MoneyType, int> money)
        {
            if (
                money[MoneyType.FiveHundred] >= 0 &&
                money[MoneyType.Hundred] >= 0 &&
                money[MoneyType.Fifty] >= 0 &&
                money[MoneyType.Twenty] >= 0 &&
                money[MoneyType.Ten] >= 0 &&
                money[MoneyType.Five] >= 0 &&
                money[MoneyType.One] >= 0
                )
            {
                foreach (MoneyType type in money.Keys)
                {
                    _listOfMoney[type] += money[type];
                }
            }
            else
            {
                throw new ApplicationException("Deposit money error! Cant deposit negative values!");
            }
        }

        public Dictionary<MoneyType, int> WithdrawMoney(Dictionary<MoneyType, int> money)
        {
            if (
                _listOfMoney[MoneyType.FiveHundred] - money[MoneyType.FiveHundred] >= 0 &&
                _listOfMoney[MoneyType.Hundred] - money[MoneyType.Hundred] >= 0 &&
                _listOfMoney[MoneyType.Fifty] - money[MoneyType.Fifty] >= 0 &&
                _listOfMoney[MoneyType.Twenty] - money[MoneyType.Twenty] >= 0 &&
                _listOfMoney[MoneyType.Ten] - money[MoneyType.Ten] >= 0 &&
                _listOfMoney[MoneyType.Five] - money[MoneyType.Five] >= 0 &&
                _listOfMoney[MoneyType.One] - money[MoneyType.One] >= 0
                )
            {
                foreach (MoneyType type in money.Keys)
                {
                    _listOfMoney[type] -= money[type];
                }

                return money;
            }
            else
            {
                Console.WriteLine("Some money are insufficient! Can't withdraw money");
                PrintMoney("Bank money", _listOfMoney);
                return null;
            }
        }

        public Dictionary<MoneyType, int> ChangeMoney(Dictionary<MoneyType, int> fromMoney, Dictionary<MoneyType, int> toMoney)
        {
            if (MoneyToInt(fromMoney) != MoneyToInt(toMoney))
            {
                return fromMoney;
            }
            else
            {
                if (
                    (fromMoney[MoneyType.FiveHundred] + _listOfMoney[MoneyType.FiveHundred] - toMoney[MoneyType.FiveHundred]) >= 0 &&
                    (fromMoney[MoneyType.Hundred] + _listOfMoney[MoneyType.Hundred] - toMoney[MoneyType.Hundred]) >= 0 &&
                    (fromMoney[MoneyType.Fifty] + _listOfMoney[MoneyType.Fifty] - toMoney[MoneyType.Fifty]) >= 0 &&
                    (fromMoney[MoneyType.Twenty] + _listOfMoney[MoneyType.Twenty] - toMoney[MoneyType.Twenty]) >= 0 &&
                    (fromMoney[MoneyType.Ten] + _listOfMoney[MoneyType.Ten] - toMoney[MoneyType.Ten]) >= 0 &&
                    (fromMoney[MoneyType.Five] + _listOfMoney[MoneyType.Five] - toMoney[MoneyType.Five]) >= 0 &&
                    (fromMoney[MoneyType.One] + _listOfMoney[MoneyType.One] - toMoney[MoneyType.One]) >= 0
                    )
                {
                    DepositMoney(fromMoney);

                    return WithdrawMoney(toMoney);
                }
                else
                {
                    Console.WriteLine("Some money are insufficient! Can't change money");
                    PrintMoney("Bank money", _listOfMoney);
                    return fromMoney;
                }
            }
        }

        public int MoneyToInt(Dictionary<MoneyType, int> money)
        {
            int total = 0;
            foreach (MoneyType type in money.Keys)
                total += (int)type * money[type];
            return total;
        }

        public void PrintMoney(string label, Dictionary<MoneyType, int> money)
        {
            Console.WriteLine(label + " = " + MoneyToInt(money));
            foreach (MoneyType type in money.Keys)
                Console.WriteLine((int) type + " = " + money[type]);
            Console.WriteLine();
        }

        public Dictionary<MoneyType, int> getInput(int price)
        {
            Dictionary<MoneyType, int> input = new Dictionary<MoneyType, int>();
            string[] inputType = new string[7];
            MoneyType[] moneyType = new MoneyType[7]
            {
                MoneyType.One,
                MoneyType.Five,
                MoneyType.Ten,
                MoneyType.Twenty,
                MoneyType.Fifty,
                MoneyType.Hundred,
                MoneyType.FiveHundred

            };
            Console.WriteLine("Total must equal to {0}", price);
            Console.WriteLine("Please input number for each MoneyType");


            Console.Write("{0}   (Remaining {1}) : ", MoneyType.One, _listOfMoney.ContainsKey(MoneyType.One) ? _listOfMoney[MoneyType.One] : 0);
            inputType[0] = Console.ReadLine();
            Console.Write("{0}   (Remaining {1}) : ", MoneyType.Five, _listOfMoney.ContainsKey(MoneyType.Five) ? _listOfMoney[MoneyType.Five] : 0);
            inputType[1] = Console.ReadLine();
            Console.Write("{0}   (Remaining {1}) : ", MoneyType.Ten, _listOfMoney.ContainsKey(MoneyType.Ten) ? _listOfMoney[MoneyType.Ten] : 0);
            inputType[2] = Console.ReadLine();
            Console.Write("{0}   (Remaining {1}) : ", MoneyType.Twenty, _listOfMoney.ContainsKey(MoneyType.Twenty) ? _listOfMoney[MoneyType.Twenty] : 0);
            inputType[3] = Console.ReadLine();
            Console.Write("{0}   (Remaining {1}) : ", MoneyType.Fifty, _listOfMoney.ContainsKey(MoneyType.Fifty) ? _listOfMoney[MoneyType.Fifty] : 0);
            inputType[4] = Console.ReadLine();
            Console.Write("{0}   (Remaining {1}) : ", MoneyType.Hundred, _listOfMoney.ContainsKey(MoneyType.Hundred) ? _listOfMoney[MoneyType.Hundred] : 0);
            inputType[5] = Console.ReadLine();
            Console.Write("{0}   (Remaining {1}) : ", MoneyType.FiveHundred, _listOfMoney.ContainsKey(MoneyType.FiveHundred) ? _listOfMoney[MoneyType.FiveHundred] : 0);
            inputType[6] = Console.ReadLine();

            for (int i = 0; i < 7; i++)
            {
                if (inputType[i] == "" || inputType[i] == null)
                    inputType[i] = "0";

                input[moneyType[i]] = int.Parse(inputType[i]);
            }

            return input;

        }

        public int ValidateGetInputPrice(int price, Dictionary<MoneyType, int> payPrice, string type)
        {
            int total = 0;
            foreach (MoneyType key in payPrice.Keys)
            {
                if (_listOfMoney.ContainsKey(key) && type == "pay")
                {
                    if (_listOfMoney[key] < payPrice[key])
                        return -1;
                }
                total += (int)key * payPrice[key];
            }
            return total - price;
        }

        public void DepositAndWithdrawWithChange(Dictionary<MoneyType,int> money)
        {
            Dictionary<MoneyType, int> forDeposit = new Dictionary<MoneyType, int>();
            Dictionary<MoneyType, int> forWithdraw = new Dictionary<MoneyType, int>();

            foreach(MoneyType key in money.Keys)
            {
                forWithdraw[key] = 0;
                forDeposit[key] = 0;
                if (money[key] < 0)
                    forWithdraw[key] = -money[key];
                else
                    forDeposit[key] = money[key];
                
            }
            DepositMoney(forDeposit);
            WithdrawMoney(forWithdraw);
        }
    }

    
}
