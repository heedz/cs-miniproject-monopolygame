using System;
using System.Collections.Generic;

namespace MonopolyGame
{
    [System.Runtime.Remoting.Contexts.Synchronization]
    public class LandCard
    {
        private string _name;
        private Dictionary<PropertyType, int> _propertyPrice;
        private Dictionary<int, int> _rentPrice;
        private int _mortgagePrice;       
        private List<Property> _properties;


        public LandCard(string name, Dictionary<PropertyType, int> propertyPrice, Dictionary<int, int> rentPrice, int mortgagePrice)
        {
            _name = name;
            _propertyPrice = propertyPrice;
            _rentPrice = rentPrice;
            _mortgagePrice = mortgagePrice;            
            _properties = new List<Property>();
        }

        public int RentPrice
        {
            get
            {
                return _rentPrice[_properties.Count]; ;
            }
        }

        public int MortgagePrice
        {
            get
            {
                return _mortgagePrice;
            }
        }        

        public int BuyPrice(PropertyType type, int number)
        {
            int price = 0;
            price += _propertyPrice[type] * number;

            return price;
        }        

        public int SellPrice(PropertyType type, int number)
        {
                        
            return BuyPrice(type, number)/ 2;            
        }

        public List<Property> Properties
        {            
            get
            {
                return _properties;
            }
        }
                
        public string Name
        {
            get
            {
                return _name;
            }
        }

        public ReturnState AddProperties(PropertyType type, int number)
        {
            if (!ValidateBuyProperty(type, number))
                return ReturnState.ValidateBuyPropertyError;

            for (int i = 0; i < number; i++)
                _properties.Add(new Property(1, type));

            return ReturnState.Success;
                      
        }

        public ReturnState RemoveProperties(PropertyType type,int number)
        {
            if (!ValidateSellProperty(type, number))
                return ReturnState.ValidateSellPropertyError;

            for (int i = 0; i < number; i++)
            {
                _properties.Remove(_properties[_properties.Count - 1]);
            }

            return ReturnState.Success;
        }

        public bool ValidateBuyProperty(PropertyType type, int number)
        {
            
            if (_properties.Count < 4)
            {
                if (type == PropertyType.Hotel || _properties.Count + number > 4)
                    return false;
            }
            else
            {
                if (type == PropertyType.House || number > 1)
                    return false;
            }
            return true;            
            
        }

        public bool ValidateSellProperty(PropertyType type, int number)
        {
            if(_properties.Count > 4)
            {
                if (type == PropertyType.House || number > 1)
                    return false;
            }
            else
            {
                if (type == PropertyType.Hotel || _properties.Count < number)
                    return false;
            }
            return true;
        }


    }
}
