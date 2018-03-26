using System;
using System.Collections.Generic;
using System.Collections;

namespace MonopolyGame
{
    [System.Runtime.Remoting.Contexts.Synchronization]
    public class MonopolyPlayer : Player
    {
        private Dictionary<MoneyType, int> _money;
        private List<LandCard> _landCards;
        private List<IActionCard> _actionsCards;
        

        public MonopolyPlayer(int id, string username)
            : base(id, username)
        {

            _money = new Dictionary<MoneyType, int>();
            _landCards = new List<LandCard>();
            _actionsCards = new List<IActionCard>();

        }

        public Tuple<ReturnState, Dictionary<MoneyType, int>> BuyLand(LandCard landCard, LandCell landCell)
        {
            if (!_landCards.Contains(landCard))
            {
                int price = landCell.Price;
                if (TotalMoney < price)
                    return new Tuple<ReturnState, Dictionary<MoneyType, int>>(ReturnState.InsufficientMoney, null);
                Tuple<ReturnState, Dictionary<MoneyType, int>> resp = DeductMoneyFromPlayer(price);
                if(resp.Item1 == ReturnState.Success)
                    _landCards.Add(landCard);
                return new Tuple<ReturnState, Dictionary<MoneyType, int>>(resp.Item1, resp.Item2);
               

            }
            return new Tuple<ReturnState, Dictionary<MoneyType, int>>(ReturnState.LandAlreadyBoughtError, null);
        }

        public Tuple<ReturnState, Dictionary<MoneyType, int>> MortgageLand(LandCard landCard)
        {
            if (_landCards.Contains(landCard) && NumberOfProperty(landCard) == 0)
            {
                int price = landCard.MortgagePrice;

                Tuple<ReturnState, Dictionary<MoneyType, int>> resp = AddMoneyToPlayer(price);
                if (resp.Item1 == ReturnState.Success)
                    _landCards.Remove(landCard);
                return new Tuple<ReturnState, Dictionary<MoneyType, int>>(resp.Item1, resp.Item2);
            }
            return new Tuple<ReturnState, Dictionary<MoneyType, int>>(ReturnState.MortgageError, null);
        }

        public Tuple<ReturnState, Dictionary<MoneyType, int>> BuyProperty(LandCard landCard, PropertyType type, int number)
        {
            // check if landCard in _landCards;
            if (_landCards.Contains(landCard) && (landCard.ValidateBuyProperty(type, number)))
            {
                int price = landCard.BuyPrice(type, number);
                if (TotalMoney < price)
                    return new Tuple<ReturnState, Dictionary<MoneyType, int>>(ReturnState.InsufficientMoney, null);
                
                Tuple<ReturnState, Dictionary<MoneyType, int>> resp = DeductMoneyFromPlayer(price);
                if (resp.Item1 == ReturnState.Success)
                    landCard.AddProperties(type, number);
                return new Tuple<ReturnState, Dictionary<MoneyType, int>>(resp.Item1, resp.Item2);

            }
            return new Tuple<ReturnState, Dictionary<MoneyType, int>>(ReturnState.ValidateBuyPropertyError, null);
        }


        public Tuple<ReturnState, Dictionary<MoneyType, int>> SellProperty(LandCard landCard, PropertyType type, int number)
        {
            if (_landCards.Contains(landCard) && landCard.ValidateSellProperty(type, number))
            {
                int price = landCard.SellPrice(type, number);

                Tuple<ReturnState, Dictionary<MoneyType, int>> resp = AddMoneyToPlayer(price);
                if (resp.Item1 == ReturnState.Success)
                    landCard.RemoveProperties(type, number);
                return new Tuple<ReturnState, Dictionary<MoneyType, int>>(resp.Item1, resp.Item2);
            }
            return new Tuple<ReturnState, Dictionary<MoneyType, int>>(ReturnState.ValidateSellPropertyError, null);
        }

        public void Pay(Dictionary<MoneyType, int> price)
        {
            int number;
            Dictionary<MoneyType, int> money = new Dictionary<MoneyType, int>();
            foreach(MoneyType key in _money.Keys)
            {
                money[key] = _money[key];
                if(price.ContainsKey(key))
                {
                    number = _money[key] - price[key];
                    money[key] = number;
                }
                
            }
            _money = money;
        }       

        public void Receive(Dictionary<MoneyType,int> money)
        {
            if(_money.Count == 0)
            {
                foreach (MoneyType key in money.Keys)
                    _money[key] = money[key];
            }
            else
            {
                int number;
                Dictionary<MoneyType, int> storeMoney = new Dictionary<MoneyType, int>();
                storeMoney = _money;
                foreach (MoneyType key in money.Keys)
                {
                    number = money[key];
                    if (_money.ContainsKey(key))
                    {
                        number = _money[key] + money[key];                        
                    }
                    storeMoney[key] = number;
                }
                _money = storeMoney;
            }
        }

        public int TotalMoney
        {
            get
            {
                int total = 0;
                foreach (MoneyType key in _money.Keys)
                    total += (int)key * _money[key];
                return total;
            }
        }

        public void AddActionCard(IActionCard card)
        {
            if (!_actionsCards.Contains(card))
                _actionsCards.Add(card);
                
        }

        public void RemoveActionCard(IActionCard card)
        {
            if (_actionsCards.Contains(card))
                _actionsCards.Remove(card);
        }
        public List<IActionCard> ActionCards
        {
            get
            {
                return _actionsCards;
            }
        }

        public List<LandCard> LandCards
        {
            get
            {
                return _landCards;
            }
        }        

        
        private int NumberOfProperty(LandCard landCard)
        {                       
            return landCard.Properties.Count;
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
            

            Console.Write("{0}   (Remaining {1}) : ", MoneyType.One, _money.ContainsKey(MoneyType.One) ? _money[MoneyType.One] : 0);
            inputType[0] = Console.ReadLine();
            Console.Write("{0}   (Remaining {1}) : ", MoneyType.Five, _money.ContainsKey(MoneyType.Five) ? _money[MoneyType.Five] : 0);
            inputType[1] = Console.ReadLine();
            Console.Write("{0}   (Remaining {1}) : ", MoneyType.Ten, _money.ContainsKey(MoneyType.Ten) ? _money[MoneyType.Ten] : 0);
            inputType[2] = Console.ReadLine();
            Console.Write("{0}   (Remaining {1}) : ", MoneyType.Twenty, _money.ContainsKey(MoneyType.Twenty) ? _money[MoneyType.Twenty] : 0);
            inputType[3] = Console.ReadLine();
            Console.Write("{0}   (Remaining {1}) : ", MoneyType.Fifty, _money.ContainsKey(MoneyType.Fifty) ? _money[MoneyType.Fifty] : 0);
            inputType[4] = Console.ReadLine();
            Console.Write("{0}   (Remaining {1}) : ", MoneyType.Hundred, _money.ContainsKey(MoneyType.Hundred) ? _money[MoneyType.Hundred] : 0);
            inputType[5] = Console.ReadLine();
            Console.Write("{0}   (Remaining {1}) : ", MoneyType.FiveHundred, _money.ContainsKey(MoneyType.FiveHundred) ? _money[MoneyType.FiveHundred] : 0);
            inputType[6] = Console.ReadLine();

            for(int i = 0; i < 7; i++)
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
                if (_money.ContainsKey(key) && type == "pay")
                {
                    if (_money[key] < payPrice[key])
                        return -1;
                }
                total += (int)key * payPrice[key];
            }
            return total - price;
        }

        public Tuple<ReturnState, Dictionary<MoneyType, int>> ValidateAndPay(int price)
        {
            Dictionary<MoneyType, int> pricePerMoneyType = getInput(price);
            Dictionary<MoneyType, int> changePerMoneyType = new Dictionary<MoneyType, int>();

            if (TotalMoney < price)
            {
                return new Tuple<ReturnState, Dictionary<MoneyType, int>>(ReturnState.InsufficientMoney, null);
            }

            int change = ValidateGetInputPrice(price, pricePerMoneyType, "pay");

            if (change < 0)
            {
                return new Tuple<ReturnState, Dictionary<MoneyType, int>>(ReturnState.PayPriceMismatchError, null);
            }
            else
            {
                if(change > 0)
                {
                    changePerMoneyType = getInput(change);
                    if(ValidateGetInputPrice(change,changePerMoneyType,"receive") != 0)
                        return new Tuple<ReturnState, Dictionary<MoneyType, int>>(ReturnState.PayPriceMismatchError, null);
                    Receive(changePerMoneyType);
                }
                Pay(pricePerMoneyType);
            }

            Dictionary<MoneyType, int> totalPerMoneyType = TotalPerMoneyType(pricePerMoneyType, changePerMoneyType,"pay");
            return new Tuple<ReturnState, Dictionary<MoneyType, int>>(ReturnState.Success, totalPerMoneyType);
        }

        public Tuple<ReturnState, Dictionary<MoneyType, int>> ValidateAndReceive(int price)
        {
            Dictionary<MoneyType, int> pricePerMoneyType = getInput(price);
            Dictionary<MoneyType, int> changePerMoneyType = new Dictionary<MoneyType, int>();

            int change = ValidateGetInputPrice(price, pricePerMoneyType, "receive");

            if (change < 0)
            {
                return new Tuple<ReturnState, Dictionary<MoneyType, int>>(ReturnState.PayPriceMismatchError, null);
            }
            else
            {
                if (change > 0)
                {
                    changePerMoneyType = getInput(change);
                    if (ValidateGetInputPrice(change, changePerMoneyType, "pay") != 0)
                        return new Tuple<ReturnState, Dictionary<MoneyType, int>>(ReturnState.PayPriceMismatchError, null);
                    Pay(changePerMoneyType);
                }
                Receive(pricePerMoneyType);
            }

            Dictionary<MoneyType, int> totalPerMoneyType = TotalPerMoneyType(pricePerMoneyType, changePerMoneyType, "receive");
            return new Tuple<ReturnState, Dictionary<MoneyType, int>>(ReturnState.Success, totalPerMoneyType);
        }

        private Dictionary<MoneyType,int> TotalPerMoneyType(Dictionary<MoneyType,int> price, Dictionary<MoneyType,int> change, string payOrReceive)
        {
            Dictionary<MoneyType, int> total = new Dictionary<MoneyType, int>();
            int number;
            foreach (MoneyType key in price.Keys)
            {
                number = 0;
                if (change.ContainsKey(key))
                    number = change[key];
                if(payOrReceive == "receive")
                    total[key] = number - price[key];
                else
                    total[key] = price[key] - number;
            }
                

            return total;
        }

        private Tuple<ReturnState, Dictionary<MoneyType, int>>  DeductMoneyFromPlayer(int price)
        {
            Tuple<ReturnState, Dictionary<MoneyType, int>> resp = new Tuple<ReturnState, Dictionary<MoneyType, int>>(0, null);
            int counter = 1;
            while (resp.Item1 != ReturnState.Success && counter <= 5)
            {
                resp = ValidateAndPay(price);

                switch (resp.Item1)
                {
                    case ReturnState.InsufficientMoney:
                        Console.WriteLine("Insufficient money");
                        break;
                    case ReturnState.PayPriceMismatchError:
                        Console.WriteLine("Error while paying, please retry");
                        break;
                    case ReturnState.Success:
                        Console.WriteLine("Pay success");
                        break;
                    default:
                        Console.WriteLine("Unrecognized return state");
                        break;
                }

                // Need to add function to check all assets
                // If still have assets, call sell prop or mrtgage
                // Else break;
                counter++;

            }
            if (counter > 5)
                return new Tuple<ReturnState, Dictionary<MoneyType, int>>(ReturnState.PayPriceMismatchError, null);

            return new Tuple<ReturnState, Dictionary<MoneyType, int>>(ReturnState.Success, resp.Item2);
        }

        private Tuple<ReturnState, Dictionary<MoneyType, int>> AddMoneyToPlayer(int price)
        {
            Tuple<ReturnState, Dictionary<MoneyType, int>> resp = new Tuple<ReturnState, Dictionary<MoneyType, int>>(0, null);
            int counter = 1;
            while (resp.Item1 != ReturnState.Success && counter <= 5)
            {
                resp = ValidateAndReceive(price);

                switch (resp.Item1)
                {
                    case ReturnState.InsufficientMoney:
                        Console.WriteLine("Insufficient money");
                        break;
                    case ReturnState.PayPriceMismatchError:
                        Console.WriteLine("Error while paying, please retry");
                        break;
                    case ReturnState.Success:
                        Console.WriteLine("Pay success");
                        break;
                    default:
                        Console.WriteLine("Unrecognized return state");
                        break;
                }

                // Need to add function to check all assets
                // If still have assets, call sell prop or mrtgage
                // Else break;
                counter++;

            }
            if (counter > 5)
                return new Tuple<ReturnState, Dictionary<MoneyType, int>>(ReturnState.PayPriceMismatchError, null);

            return new Tuple<ReturnState, Dictionary<MoneyType, int>>(ReturnState.Success, resp.Item2);
        }

        public void PayAndReceiveWithChange(Dictionary<MoneyType, int> money)
        {
            Dictionary<MoneyType, int> forReceive = new Dictionary<MoneyType, int>();
            Dictionary<MoneyType, int> forPay = new Dictionary<MoneyType, int>();

            foreach (MoneyType key in money.Keys)
            {
                forPay[key] = 0;
                forReceive[key] = 0;
                if (money[key] < 0)
                    forPay[key] = -money[key];
                else
                    forReceive[key] = money[key];

            }
            Receive(forReceive);
            Pay(forPay);
        }
    }
}
