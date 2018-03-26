using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MonopolyGame;
using System.Collections.Generic;
using System.Threading;
using System.IO;

namespace CellUnitTest
{
    [TestClass]
    public class MonopolyGameUnitTest
    {
        public MonopolyPlayer[] Players { get; } = {
            new MonopolyPlayer(111, "Sahid"),
            new MonopolyPlayer(112, "Nur"),
            new MonopolyPlayer(113, "Afrizal")
        };
        public MonopolyPlayer FirstPlayer;
        public Dice[] Dices { get; } = {
            new Dice(1, 6),
            new Dice(1, 6)
        };
        public Dictionary<MoneyType, int> BankInitialMoney { get; } = new Dictionary<MoneyType, int>
        {
            { MoneyType.FiveHundred, 20 },
            { MoneyType.Hundred, 20 },
            { MoneyType.Fifty, 30 },
            { MoneyType.Twenty, 50 },
            { MoneyType.Ten, 40 },
            { MoneyType.Five, 40 },
            { MoneyType.One, 40 }
        };
        public Dictionary<MoneyType, int> PlayersInitialMoney = new Dictionary<MoneyType, int>()
        {
            { MoneyType.FiveHundred, 2 },
            { MoneyType.Hundred, 2 },
            { MoneyType.Fifty, 2 },
            { MoneyType.Twenty, 6 },
            { MoneyType.Ten, 5 },
            { MoneyType.Five, 5 },
            { MoneyType.One, 5 }
        };
        public Tuple<int, int, int> BoardParams { get; } = new Tuple<int, int, int>(11, 11, 1);
        public Dictionary<int, Tuple<string, string>> ChanceCardData { get; } = new Dictionary<int, Tuple<string, string>>
        {
            { 0, new Tuple<string, string>("CText 1", "CImage 1") },
            { 1, new Tuple<string, string>("CText 2", "CImage 2") },
            { 2, new Tuple<string, string>("CText 3", "CImage 3") },
            { 3, new Tuple<string, string>("CText 4", "CImage 4") },
            { 4, new Tuple<string, string>("CText 5", "CImage 5") },
        };
        public Dictionary<int, Tuple<string, string>> CommunityCardData { get; } = new Dictionary<int, Tuple<string, string>>
        {
            { 0, new Tuple<string, string>("CCText 1", "CCImage 1") },
            { 1, new Tuple<string, string>("CCText 2", "CCImage 2") },
            { 2, new Tuple<string, string>("CCText 3", "CCImage 3") },
            { 3, new Tuple<string, string>("CCText 4", "CCImage 4") },
            { 4, new Tuple<string, string>("CCText 5", "CCImage 5") },
        };
         public List<Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>> LandCardData { get; } = new List<Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>>
        {
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Mediterranean Avenue",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 2 },
                    { 1, 10 },
                    { 2, 30 },
                    { 3, 90 },
                    { 4, 160 },
                    { 5, 250 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Baltic Avenue",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Oriental Avenue",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 6 },
                    { 1, 30 },
                    { 2, 90 },
                    { 3, 270 },
                    { 4, 400 },
                    { 5, 550 }
                },
                50
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Vermont Avenue",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Connecticut Avenue",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "St. Charles Place",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "States Avenue",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Virginia Avenuee",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "St. James Place",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Tennessee Avenue",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "New York Avenue",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Kentucky Avenue",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Indiana Avenuee",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Illinois Avenue",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Atlantic Avenue",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Ventnor Avenue",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Marvin Gardens",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Pacific Avenue",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "North Carolina Avenue",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Pennsylvania Avenue",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Park Place",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Boardwalk",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Reading Railroad",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Pennsylvania Railroad",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "B. & O. Railroad",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Short Line",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Electric Company",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) },
            { new Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>(
                "Water Works",
                new Dictionary<PropertyType, int>
                {
                    { PropertyType.House, 50 },
                    { PropertyType.Hotel, 50 },
                },
                new Dictionary<int, int>
                {
                    { 0, 4 },
                    { 1, 20 },
                    { 2, 60 },
                    { 3, 180 },
                    { 4, 320 },
                    { 5, 450 }
                },
                30
            ) }
        };
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
                new Tuple<string, string>("PayTax","PayTax"),20,LandType.None,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (5,CellType.LandCell,new Tuple<int, int, int>(6,1,1),
                new Tuple<string, string>("Station 1","Station 1"),100,LandType.Station,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (6,CellType.LandCell,new Tuple<int, int, int>(5,1,1),
                new Tuple<string, string>("SomeLand","SomeLand"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (7,CellType.ActionCell,new Tuple<int, int, int>(4,1,1),
                new Tuple<string, string>("Chance","Chance"),0,LandType.None,CardType.Chance)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (8,CellType.LandCell,new Tuple<int, int, int>(3,1,1),
                new Tuple<string, string>("SomeLand","SomeLand"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (9,CellType.LandCell,new Tuple<int, int, int>(2,1,1),
                new Tuple<string, string>("SomeLand","SomeLand"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (10,CellType.JailCell,new Tuple<int, int, int>(1,1,1),
                new Tuple<string, string>("Jail","Jail"),0,LandType.None,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (11,CellType.LandCell,new Tuple<int, int, int>(1,2,1),
                new Tuple<string, string>("SomeLand","SomeLand"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (12,CellType.LandCell,new Tuple<int, int, int>(1,3,1),
                new Tuple<string, string>("SomeLand","SomeLand"),100,LandType.Coorporation,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (13,CellType.LandCell,new Tuple<int, int, int>(1,4,1),
                new Tuple<string, string>("SomeLand","SomeLand"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (14,CellType.LandCell,new Tuple<int, int, int>(1,5,1),
                new Tuple<string, string>("SomeLand","SomeLand"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (15,CellType.LandCell,new Tuple<int, int, int>(1,6,1),
                new Tuple<string, string>("SomeLand","SomeLand"),100,LandType.Station,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (16,CellType.LandCell,new Tuple<int, int, int>(1,7,1),
                new Tuple<string, string>("SomeLand","SomeLand"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (17,CellType.ActionCell,new Tuple<int, int, int>(1,8,1),
                new Tuple<string, string>("CommunityChest","CommunityChest"),0,LandType.None,CardType.CommunityChest)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (18,CellType.LandCell,new Tuple<int, int, int>(1,9,1),
                new Tuple<string, string>("SomeLand","SomeLand"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (19,CellType.LandCell,new Tuple<int, int, int>(1,10,1),
                new Tuple<string, string>("SomeLand","SomeLand"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (20,CellType.FreeParkingCell,new Tuple<int, int, int>(1,11,1),
                new Tuple<string, string>("FreeParking","FreeParking"),0,LandType.None,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (21,CellType.LandCell,new Tuple<int, int, int>(2,11,1),
                new Tuple<string, string>("SomeLand","SomeLand"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (22,CellType.ActionCell,new Tuple<int, int, int>(3,11,1),
                new Tuple<string, string>("Chance","Chance"),0,LandType.None,CardType.Chance)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (23,CellType.LandCell,new Tuple<int, int, int>(4,11,1),
                new Tuple<string, string>("SomeLand","SomeLand"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (24,CellType.LandCell,new Tuple<int, int, int>(5,11,1),
                new Tuple<string, string>("SomeLand","SomeLand"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (25,CellType.LandCell,new Tuple<int, int, int>(6,11,1),
                new Tuple<string, string>("SomeLand","SomeLand"),0,LandType.Station,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (26,CellType.LandCell,new Tuple<int, int, int>(7,11,1),
                new Tuple<string, string>("SomeLand","SomeLand"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (27,CellType.LandCell,new Tuple<int, int, int>(8,11,1),
                new Tuple<string, string>("SomeLand","SomeLand"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (28,CellType.LandCell,new Tuple<int, int, int>(9,11,1),
                new Tuple<string, string>("SomeLand","SomeLand"),0,LandType.Coorporation,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (29,CellType.LandCell,new Tuple<int, int, int>(10,11,1),
                new Tuple<string, string>("SomeLand","SomeLand"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (30,CellType.GotoJailCell,new Tuple<int, int, int>(11,11,1),
                new Tuple<string, string>("GoToJail","GoToJail"),0,LandType.None,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (31,CellType.LandCell,new Tuple<int, int, int>(11,10,1),
                new Tuple<string, string>("SomeLand","SomeLand"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (32,CellType.LandCell,new Tuple<int, int, int>(11,9,1),
                new Tuple<string, string>("SomeLand","SomeLand"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (33,CellType.ActionCell,new Tuple<int, int, int>(11,8,1),
                new Tuple<string, string>("CommunityChest","CommunityChest"),0,LandType.None,CardType.CommunityChest)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (34,CellType.LandCell,new Tuple<int, int, int>(11,7,1),
                new Tuple<string, string>("SomeLand","SomeLand"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (35,CellType.LandCell,new Tuple<int, int, int>(11,6,1),
                new Tuple<string, string>("SomeLand","SomeLand"),0,LandType.Station,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (36,CellType.ActionCell,new Tuple<int, int, int>(11,5,1),
                new Tuple<string, string>("Chance","Chance"),0,LandType.None,CardType.Chance)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (37,CellType.LandCell,new Tuple<int, int, int>(11,4,1),
                new Tuple<string, string>("SomeLand","SomeLand"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (38,CellType.PayTaxCell,new Tuple<int, int, int>(11,3,1),
                new Tuple<string, string>("PayTax","PayTax"),20,LandType.None,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (39,CellType.LandCell,new Tuple<int, int, int>(11,2,1),
                new Tuple<string, string>("SomeLand","SomeLand"),100,LandType.State,CardType.None)},

        };
        public Dictionary<int, int> LinkCellToCardData = new Dictionary<int, int>()
        {
            //States
            { 1, 0 },
            { 3, 1 },
            { 6, 2 },
            { 8, 3 },
            { 9, 4 },
            { 11, 5 },
            { 13, 6 },
            { 14, 7 },
            { 16, 8 },
            { 18, 9 },
            { 19, 10 },
            { 21, 11 },
            { 23, 12 },
            { 24, 13 },
            { 26, 14 },
            { 27, 15 },
            { 29, 16 },
            { 31, 17 },
            { 32, 18 },
            { 34, 19 },
            { 37, 20 },
            { 39, 21 },
            //Railroads
            { 35, 22 },
            { 25, 23 },
            { 15, 24 },
            { 5, 25 },
            //Corps
            { 28, 26 },
            { 12, 27 }
        };

        MonopolyGame.MonopolyGame Game;
        public void InitializeTest()
        {
            Game = new MonopolyGame.MonopolyGame(
                Players,
                Dices,
                BankInitialMoney,
                PlayersInitialMoney,
                BoardParams,
                CellsData,
                ChanceCardData,
                CommunityCardData,
                LandCardData,
                LinkCellToCardData

            );

            FirstPlayer = Players[0];
        }

        [TestMethod]
        public void InitializeGame()
        {
            var sw = new StringWriter();

            using (sw)
            {
                Console.SetOut(sw);
                InitializeTest();
            }

            Assert.IsTrue(sw.ToString().Contains("Game initialized! Call RunGame() to run the game"));
        }

        [TestMethod]
        public void RunNormalGameWithoutInvokingTurn()
        {
            InitializeTest();
            Thread main = new Thread(delegate () { Game.RunGame(); });
            var sw = new StringWriter();

            using (sw)
            {
                Console.SetOut(sw);
                main.Start();
                Thread.Sleep(3000);
                main.Abort();
            }

            Assert.IsTrue(sw.ToString().Contains("Player ") && sw.ToString().Contains(" please run your turn"));
        }

        [TestMethod]
        public void RunNormalGameWithInvokingWrongPersonTurn()
        {
            InitializeTest();

            Thread main = new Thread(delegate () { Game.RunGame(); });
            Thread turn = new Thread(delegate () { Game.RunTurn(new Player(1234, "Random")); });

            var sw = new StringWriter();
            using (sw)
            {
                Console.SetOut(sw);
                main.Start();

                turn.Start();

                Thread.Sleep(3000);

                turn.Abort();

                main.Abort();
            }

            Assert.IsTrue(sw.ToString().Contains("this is not your turn!"));
        }

        [TestMethod]
        public void InitializeChanceCards()
        {
            InitializeTest();
            PrivateObject obj = new PrivateObject(Game);

            // Compile external data first
            var resp = obj.Invoke("CompileActionCardsExternalData", new object[] { ChanceCardData });
            Tuple<Dictionary<int, string>, Dictionary<int, string>> chanceCardExternalData = (Tuple<Dictionary<int, string>, Dictionary<int, string>>)resp;

            var chanceCardData = new Tuple<int, Dictionary<int, Action<MonopolyPlayer>>, Dictionary<int, string>, Dictionary<int, string>>(
                5,
                new Dictionary<int, Action<MonopolyPlayer>>() { // Actions
                    { 0, delegate(MonopolyPlayer p){ Console.WriteLine("Test OK 1"); } },
                    { 1, delegate(MonopolyPlayer p){ Console.WriteLine("Test OK 2"); } },
                    { 2, delegate(MonopolyPlayer p){ Console.WriteLine("Test OK 3"); } },
                    { 3, delegate(MonopolyPlayer p){ Console.WriteLine("Test OK 4"); } },
                    { 4, delegate(MonopolyPlayer p){ Console.WriteLine("Test OK 5"); } },
                },
                chanceCardExternalData.Item1,    // Texts
                chanceCardExternalData.Item2    // Images path
            );

            // Initialize the chance cards
            var retVal = obj.Invoke("InitializeActionCards", new object[] { typeof(ChanceCard),  chanceCardData.Item1, chanceCardData.Item2, chanceCardData.Item3, chanceCardData.Item4});

            // Check it returns true
            Assert.AreEqual(retVal, true);

            // Check it contains 5 cards
            Assert.AreEqual(Game.ChanceCards.Count, 10); // Added 5 from InitializeGame
        }

        [TestMethod]
        public void InitializeCommunityChestCards()
        {
            InitializeTest();
            PrivateObject obj = new PrivateObject(Game);

            // Compile external data first
            var resp = obj.Invoke("CompileActionCardsExternalData", new object[] { CommunityCardData });
            Tuple<Dictionary<int, string>, Dictionary<int, string>> communityCardExternalData = (Tuple<Dictionary<int, string>, Dictionary<int, string>>)resp;

            var communityCardData = new Tuple<int, Dictionary<int, Action<MonopolyPlayer>>, Dictionary<int, string>, Dictionary<int, string>>(
                5,
                new Dictionary<int, Action<MonopolyPlayer>>() { // Actions
                    { 0, delegate(MonopolyPlayer p){ Console.WriteLine("Test OK 1"); } },
                    { 1, delegate(MonopolyPlayer p){ Console.WriteLine("Test OK 2"); } },
                    { 2, delegate(MonopolyPlayer p){ Console.WriteLine("Test OK 3"); } },
                    { 3, delegate(MonopolyPlayer p){ Console.WriteLine("Test OK 4"); } },
                    { 4, delegate(MonopolyPlayer p){ Console.WriteLine("Test OK 5"); } },
                },
                communityCardExternalData.Item1,    // Texts
                communityCardExternalData.Item2    // Images path
            );

            // Initialize the chance cards
            var retVal = obj.Invoke("InitializeActionCards", new object[] { typeof(CommunityChestCard), communityCardData.Item1, communityCardData.Item2, communityCardData.Item3, communityCardData.Item4 });

            // Check it returns true
            Assert.AreEqual(retVal, true);

            // Check it contains 5 cards
            Assert.AreEqual(Game.CommunityChestCards.Count, 10); // Added 5 from InitializeGame
        }

        [TestMethod]
        public void MovePlayer()
        {
            InitializeTest();
            PrivateObject obj = new PrivateObject(Game);

            var resp = obj.Invoke("MovePlayer", new object[] { FirstPlayer, 3 });

            Assert.IsTrue(Game.PlayerPosition[FirstPlayer].ID == 3);
        }

        [TestMethod]
        public void MovePlayerToChanceCardTest()
        {
            InitializeTest();
            PrivateObject obj = new PrivateObject(Game);

            var resp = obj.Invoke("MovePlayer", new object[] { FirstPlayer, 7 });

            // Check for actioncell
            Assert.IsTrue(((MonopolyBoard)Game.CurrentBoard).ListOfMonopolyCell[7].GetType() == typeof(ActionCell));
        }

        // From action cards test
        [TestMethod]
        public void ChanceCard1ActionTest()
        {
            InitializeTest();

            Assert.AreEqual(FirstPlayer.TotalMoney, 1500);
            Assert.AreEqual(Game.Bank.RemainingMoney, 15140);

            PrivateObject obj = new PrivateObject(Game);

            using (StringReader sr = new StringReader("0\n0\n0\n0\n0\n2"))
            {
                //Console.SetOut(sw);
                Console.SetIn(sr);
                var resp = obj.Invoke("ChanceCard1Action", new object[] { FirstPlayer });
            }

            Assert.AreEqual(14940, Game.Bank.RemainingMoney);
            Assert.AreEqual(1700, FirstPlayer.TotalMoney);


        }

        [TestMethod]
        public void ChanceCard2ActionTest()
        {
            InitializeTest();

            PrivateObject obj = new PrivateObject(Game);
            using (StringReader sr = new StringReader("5\n2\n0\n0\n0\n0"))
            {
                Console.SetIn(sr);
                var resp = obj.Invoke("ChanceCard2Action", new object[] { FirstPlayer });
            }

            Assert.AreEqual(15155, Game.Bank.RemainingMoney);
            Assert.AreEqual(1485, FirstPlayer.TotalMoney);
        }

        [TestMethod]
        public void ChanceCard3ActionTest()
        {
            InitializeTest();
            MonopolyCell current = Game.PlayerPosition[FirstPlayer];
            Assert.AreEqual(0, current.ID);

            PrivateObject obj = new PrivateObject(Game);
            using (StringReader sr = new StringReader("N"))
            {
                Console.SetIn(sr);
                var resp = obj.Invoke("ChanceCard3Action", new object[] { FirstPlayer });
            }

            current = Game.PlayerPosition[FirstPlayer];
            Assert.AreEqual(3, current.ID);
        }

        [TestMethod]
        public void ChanceCard4ActionTest()
        {
            InitializeTest();
            MonopolyCell current = Game.PlayerPosition[FirstPlayer];
            Assert.AreEqual(0, current.ID);

            PrivateObject obj = new PrivateObject(Game);
            var resp = obj.Invoke("ChanceCard4Action", new object[] { FirstPlayer });

            current = Game.PlayerPosition[FirstPlayer];
            Assert.AreEqual(10, current.ID);
        }

        [TestMethod]
        public void ChanceCard5ActionTest()
        {
            InitializeTest();
            MonopolyCell current = Game.PlayerPosition[FirstPlayer];
            Assert.AreEqual(0, current.ID);

            PrivateObject obj = new PrivateObject(Game);
            using (StringReader sr = new StringReader("N"))
            {
                Console.SetIn(sr);
                var resp = obj.Invoke("ChanceCard5Action", new object[] { FirstPlayer });
            }

            current = Game.PlayerPosition[FirstPlayer];
            Assert.AreEqual(19, current.ID);
            Assert.AreEqual(1500, FirstPlayer.TotalMoney);
        }

        [TestMethod]
        public void CommunityCard1Action()
        {
            InitializeTest();
            PrivateObject obj = new PrivateObject(Game);            

            using (StringReader sr = new StringReader("0\n0\n0\n0\n0\n2"))
            {
                Console.SetIn(sr);
                var resp = obj.Invoke("CommunityCard1Action", new object[] { FirstPlayer });
            }

            Assert.AreEqual(14940, Game.Bank.RemainingMoney);
            Assert.AreEqual(1700, FirstPlayer.TotalMoney);
        }

        [TestMethod]
        public void CommunityCard2Action()
        {
            InitializeTest();
            PrivateObject obj = new PrivateObject(Game);
            using (StringReader sr = new StringReader("0\n0\n0\n0\n1\n0"))
            {
                Console.SetIn(sr);
                var resp = obj.Invoke("CommunityCard2Action", new object[] { FirstPlayer });
            }

            Assert.AreEqual(15190, Game.Bank.RemainingMoney);
            Assert.AreEqual(1450, FirstPlayer.TotalMoney);
        }

        [TestMethod]
        public void CommunityCard3Action()
        {
            InitializeTest();           
            MonopolyCell current = Game.PlayerPosition[FirstPlayer];
            Assert.AreEqual(0, current.ID);
            PrivateObject obj = new PrivateObject(Game);
            using (StringReader sr = new StringReader("N"))
            {
                Console.SetIn(sr);
                var resp = obj.Invoke("CommunityCard3Action", new object[] { FirstPlayer });
            }
            current = Game.PlayerPosition[FirstPlayer];
            Assert.AreEqual(3, current.ID);
        }

        [TestMethod]
        public void CommunityCard4Action()
        {
            InitializeTest();
            MonopolyCell current = Game.PlayerPosition[FirstPlayer];
            Assert.AreEqual(0, current.ID);

            PrivateObject obj = new PrivateObject(Game);
            var resp = obj.Invoke("CommunityCard4Action", new object[] { FirstPlayer });

            current = Game.PlayerPosition[FirstPlayer];
            Assert.AreEqual(10, current.ID);
        }

        [TestMethod]
        public void CommunityCard5Action()
        {
            InitializeTest();
            PrivateObject obj = new PrivateObject(Game);

            
            using (StringReader sr = new StringReader("0\n0\n0\n0\n2\n0"))
            {
                Console.SetIn(sr);
                var resp = obj.Invoke("CommunityCard5Action", new object[] { FirstPlayer });
            }

            Assert.AreEqual(15040, Game.Bank.RemainingMoney);
            Assert.AreEqual(1600, FirstPlayer.TotalMoney);
        }

        [TestMethod]
        public void StartOnPassed()
        {
            InitializeTest();
            PrivateObject obj = new PrivateObject(Game);
            Game.CurrentPlayer = FirstPlayer;
            var resp = obj.Invoke("MovePlayer", new object[] { FirstPlayer, 20 });
            using (StringReader sr = new StringReader("0\n0\n0\n0\n0\n2\n0\n0\n0\n0\n0\n1\n0\n0\n0\n0\n4\n0\n0\n0\n0"))
            {
                Console.SetIn(sr);
                resp = obj.Invoke("MovePlayer", new object[] { FirstPlayer,24 });
            }

            Assert.AreEqual(14960, Game.Bank.RemainingMoney);
            Assert.AreEqual(1680, FirstPlayer.TotalMoney);
        }
    }
}
