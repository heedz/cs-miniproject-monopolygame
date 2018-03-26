using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
namespace MonopolyGame
{
    class Program
    {
        static public MonopolyPlayer[] Players { get; set; }
        static public Dice[] Dices { get; } = {
            new Dice(1, 6),
            new Dice(1, 6)
        };
        static public Dictionary<MoneyType, int> BankInitialMoney { get; } = new Dictionary<MoneyType, int>
        {
            { MoneyType.FiveHundred, 20 },
            { MoneyType.Hundred, 20 },
            { MoneyType.Fifty, 30 },
            { MoneyType.Twenty, 50 },
            { MoneyType.Ten, 40 },
            { MoneyType.Five, 40 },
            { MoneyType.One, 40 }
        };
        static public Dictionary<MoneyType, int> PlayersInitialMoney = new Dictionary<MoneyType, int>()
        {
            { MoneyType.FiveHundred, 2 },
            { MoneyType.Hundred, 2 },
            { MoneyType.Fifty, 2 },
            { MoneyType.Twenty, 6 },
            { MoneyType.Ten, 5 },
            { MoneyType.Five, 5 },
            { MoneyType.One, 5 }
        };
        static public Tuple<int, int, int> BoardParams { get; } = new Tuple<int, int, int>(11, 11, 1);
        static public Dictionary<int, Tuple<string, string>> ChanceCardData { get; } = new Dictionary<int, Tuple<string, string>>
        {
            { 0, new Tuple<string, string>("CText 1", "CImage 1") },
            { 1, new Tuple<string, string>("CText 2", "CImage 2") },
            { 2, new Tuple<string, string>("CText 3", "CImage 3") },
            { 3, new Tuple<string, string>("CText 4", "CImage 4") },
            { 4, new Tuple<string, string>("CText 5", "CImage 5") },
        };
        static public Dictionary<int, Tuple<string, string>> CommunityCardData { get; } = new Dictionary<int, Tuple<string, string>>
        {
            { 0, new Tuple<string, string>("CCText 1", "CCImage 1") },
            { 1, new Tuple<string, string>("CCText 2", "CCImage 2") },
            { 2, new Tuple<string, string>("CCText 3", "CCImage 3") },
            { 3, new Tuple<string, string>("CCText 4", "CCImage 4") },
            { 4, new Tuple<string, string>("CCText 5", "CCImage 5") },
        };
        static public List<Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>> LandCardData { get; } = new List<Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>>
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
            ) },
        };
        static public List<Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>> CellsData { get; }
        = new List<Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>>
        {
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (0,CellType.StartCell,new Tuple<int, int, int>(11,1,1),
                new Tuple<string, string>("Start","Start"),0,LandType.None,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (1,CellType.LandCell,new Tuple<int, int, int>(10,1,1),
                new Tuple<string, string>("Mediterranean Avenue","Mediterranean Avenue"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (2,CellType.ActionCell,new Tuple<int, int, int>(9,1,1),
                new Tuple<string, string>("Community Chest","Community Chest"),0,LandType.None,CardType.CommunityChest)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (3,CellType.LandCell,new Tuple<int, int, int>(8,1,1),
                new Tuple<string, string>("Baltic Avenue","Baltic Avenue"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (4,CellType.PayTaxCell,new Tuple<int, int, int>(7,1,1),
                new Tuple<string, string>("PayTax","PayTax"),20,LandType.None,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (5,CellType.LandCell,new Tuple<int, int, int>(6,1,1),
                new Tuple<string, string>("Reading Railroad","Reading Railroad"),100,LandType.Station,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (6,CellType.LandCell,new Tuple<int, int, int>(5,1,1),
                new Tuple<string, string>("Oriental Avenue","Oriental Avenue"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (7,CellType.ActionCell,new Tuple<int, int, int>(4,1,1),
                new Tuple<string, string>("Chance","Chance"),0,LandType.None,CardType.Chance)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (8,CellType.LandCell,new Tuple<int, int, int>(3,1,1),
                new Tuple<string, string>("Vermont Avenue","Vermont Avenue"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (9,CellType.LandCell,new Tuple<int, int, int>(2,1,1),
                new Tuple<string, string>("Connecticut Avenue","Connecticut Avenue"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (10,CellType.JailCell,new Tuple<int, int, int>(1,1,1),
                new Tuple<string, string>("Jail","Jail"),0,LandType.None,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (11,CellType.LandCell,new Tuple<int, int, int>(1,2,1),
                new Tuple<string, string>("St. Charles Place","St. Charles Place"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (12,CellType.LandCell,new Tuple<int, int, int>(1,3,1),
                new Tuple<string, string>("Electric Company","Electric Company"),100,LandType.Coorporation,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (13,CellType.LandCell,new Tuple<int, int, int>(1,4,1),
                new Tuple<string, string>("States Avenue","States Avenue"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (14,CellType.LandCell,new Tuple<int, int, int>(1,5,1),
                new Tuple<string, string>("Virginia Avenue","Virginia Avenue"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (15,CellType.LandCell,new Tuple<int, int, int>(1,6,1),
                new Tuple<string, string>("Pennsylvania Railroad","Pennsylvania Railroad"),100,LandType.Station,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (16,CellType.LandCell,new Tuple<int, int, int>(1,7,1),
                new Tuple<string, string>("St. James Place","St. James Place"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (17,CellType.ActionCell,new Tuple<int, int, int>(1,8,1),
                new Tuple<string, string>("CommunityChest","CommunityChest"),0,LandType.None,CardType.CommunityChest)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (18,CellType.LandCell,new Tuple<int, int, int>(1,9,1),
                new Tuple<string, string>("Tennessee Avenue","Tennessee Avenue"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (19,CellType.LandCell,new Tuple<int, int, int>(1,10,1),
                new Tuple<string, string>("New York Avenue","New York Avenue"),100,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (20,CellType.FreeParkingCell,new Tuple<int, int, int>(1,11,1),
                new Tuple<string, string>("FreeParking","FreeParking"),0,LandType.None,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (21,CellType.LandCell,new Tuple<int, int, int>(2,11,1),
                new Tuple<string, string>("Kentucky Avenue","Kentucky Avenue"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (22,CellType.ActionCell,new Tuple<int, int, int>(3,11,1),
                new Tuple<string, string>("Chance","Chance"),0,LandType.None,CardType.Chance)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (23,CellType.LandCell,new Tuple<int, int, int>(4,11,1),
                new Tuple<string, string>("Indiana Avenue","Indiana Avenue"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (24,CellType.LandCell,new Tuple<int, int, int>(5,11,1),
                new Tuple<string, string>("Illinois Avenue","Illinois Avenue"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (25,CellType.LandCell,new Tuple<int, int, int>(6,11,1),
                new Tuple<string, string>("B. & O. Railroad","B. & O. Railroad"),0,LandType.Station,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (26,CellType.LandCell,new Tuple<int, int, int>(7,11,1),
                new Tuple<string, string>("Atlantic Avenue","Atlantic Avenue"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (27,CellType.LandCell,new Tuple<int, int, int>(8,11,1),
                new Tuple<string, string>("Ventnor Avenue","Ventnor Avenue"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (28,CellType.LandCell,new Tuple<int, int, int>(9,11,1),
                new Tuple<string, string>("Water Works","Water Works"),0,LandType.Coorporation,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (29,CellType.LandCell,new Tuple<int, int, int>(10,11,1),
                new Tuple<string, string>("Marvin Gardens","Marvin Gardens"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (30,CellType.GotoJailCell,new Tuple<int, int, int>(11,11,1),
                new Tuple<string, string>("GoToJail","GoToJail"),0,LandType.None,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (31,CellType.LandCell,new Tuple<int, int, int>(11,10,1),
                new Tuple<string, string>("Pacific Avenue","Pacific Avenue"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (32,CellType.LandCell,new Tuple<int, int, int>(11,9,1),
                new Tuple<string, string>("North Carolina Avenue","North Carolina Avenue"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (33,CellType.ActionCell,new Tuple<int, int, int>(11,8,1),
                new Tuple<string, string>("CommunityChest","CommunityChest"),0,LandType.None,CardType.CommunityChest)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (34,CellType.LandCell,new Tuple<int, int, int>(11,7,1),
                new Tuple<string, string>("Pennsylvania Avenue","Pennsylvania Avenue"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (35,CellType.LandCell,new Tuple<int, int, int>(11,6,1),
                new Tuple<string, string>("Short Line","Short Line"),0,LandType.Station,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (36,CellType.ActionCell,new Tuple<int, int, int>(11,5,1),
                new Tuple<string, string>("Chance","Chance"),0,LandType.None,CardType.Chance)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (37,CellType.LandCell,new Tuple<int, int, int>(11,4,1),
                new Tuple<string, string>("Park Place","Park Place"),0,LandType.State,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (38,CellType.PayTaxCell,new Tuple<int, int, int>(11,3,1),
                new Tuple<string, string>("PayTax","PayTax"),20,LandType.None,CardType.None)},
            {
                new Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>
                (39,CellType.LandCell,new Tuple<int, int, int>(11,2,1),
                new Tuple<string, string>("Boardwalk","Boardwalk"),100,LandType.State,CardType.None)},

        };

        static public Dictionary<int, int> LinkCellToCardData = new Dictionary<int, int>()
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

        static void Main(string[] args)
        {

            //MonopolyPlayer mplayer = new MonopolyPlayer(1, "player 1");
            //Dictionary<MoneyType, int> init = new Dictionary<MoneyType, int>();
            //init[MoneyType.Ten] = 4;
            //init[MoneyType.One] = 10;
            //init[MoneyType.Five] = 2;
            //init[MoneyType.Twenty] = 2;
            //init[MoneyType.Fifty] = 2;
            //mplayer.Receive(init);

            //Dictionary<PropertyType, int> prices = new Dictionary<PropertyType, int>();
            //prices[PropertyType.House] = 100;
            //prices[PropertyType.Hotel] = 500;

            //Dictionary<int, int> rents = new Dictionary<int, int>();

            //rents[1] = 10;
            //rents[2] = 20;
            //rents[3] = 30;
            //rents[4] = 40;
            //rents[5] = 50;

            //LandCell cell = new LandCell(2, 2, 1, 1, "Mediterranian Avenue", "Mediterranian Avenue", 100, LandType.State);
            //LandCard mediteranianCard = new LandCard("Mediterranian Avenue", prices, rents, 50);

            //mplayer.BuyLand(mediteranianCard, cell);
            //Console.WriteLine(mplayer.LandCards.Count);
            //Console.ReadKey();

            // Welcome message
            Console.WriteLine("MONOPOLY GAME");
            Console.WriteLine("=============");
            Console.WriteLine();

            List<MonopolyPlayer> playerList = new List<MonopolyPlayer>();
            int playerCount = -1;
            string resp;
            while (playerCount < 1 || playerCount > 4)
            {
                Console.Write("How many players? (1-4) ");
                resp = Console.ReadLine();
                playerCount = int.Parse(resp);
            }

            for (int i = 0; i < playerCount; i++)
            {
                Console.Write("Enter name for Player " + (i+1) + " : ");
                resp = Console.ReadLine();
                playerList.Add(new MonopolyPlayer(i+1, resp));
            }

            Players = playerList.ToArray();

            Console.Clear();

            // Welcome message
            Console.WriteLine("MONOPOLY GAME");
            Console.WriteLine("=============");
            Console.WriteLine();

            MonopolyGame Game = new MonopolyGame(
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

            Thread main = new Thread(delegate() { Game.RunGame(); });

            main.Start();

            Thread.Sleep(1000);

            while (main.IsAlive)
            {
                Player player = null;
                while (main.IsAlive)
                {
                    Console.WriteLine("Select player to run turn:");
                    foreach (Player p in Players)
                    {
                        Console.WriteLine(p.Id + " - " + p.Username);
                    }

                    Console.Write("Enter ID :");
                    resp = Console.ReadLine();
                    int playerId = int.Parse(resp);

                    foreach (Player p in Players)
                    {
                        if (p.Id == playerId)
                        {
                            player = p;
                            break;
                        }
                    }

                    Console.WriteLine("Selected player :" + player.Username);
                    Game.RunTurn(player);
                }
            }

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }
        
    }
}
