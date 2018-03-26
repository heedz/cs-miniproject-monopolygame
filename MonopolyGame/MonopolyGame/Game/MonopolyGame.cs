using System;
using System.Collections.Generic;
using System.Threading;

namespace MonopolyGame
{
    public class MonopolyGame : BoardGame
    {
        Bank _bank;
        new MonopolyBoard _board;
        Dictionary<MonopolyPlayer, MonopolyCell> _playersPosition;
        Dictionary<MonopolyCell, MonopolyPlayer> _cellsOwner;
        Queue<ChanceCard> _chanceCards;
        Queue<CommunityChestCard> _communityChestCards;
        List<LandCard> _landCards;
        Player _currentPlayer;
        List<Player> _losers;
        int _moneyAtStartCell;
        Dictionary<LandCell, LandCard> _cellToCardLink;

        Dictionary<MoneyType, int> _initialMoney;
        Dictionary<MoneyType, int> _playerInitialMoney;
        Tuple<int, int, int> _boardParams;
        List<Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>> _cellsData;
        Tuple<int, Dictionary<int, Action<MonopolyPlayer>>, Dictionary<int, string>, Dictionary<int, string>> _chanceCardData;
        Tuple<int, Dictionary<int, Action<MonopolyPlayer>>, Dictionary<int, string>, Dictionary<int, string>> _communityCardData;
        List<Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>> _landCardData;
        Dictionary<int, int> _cellToCardLinkData;

        // For unit testing purposes only
        public Dictionary<MonopolyPlayer, MonopolyCell> PlayerPosition { get { return _playersPosition; } }
        public MonopolyBoard Board { get { return _board; } }
        public Bank Bank { get { return _bank; } }
        public Player CurrentPlayer { get { return _currentPlayer; } set { _currentPlayer = value; } }
        public Queue<ChanceCard> ChanceCards { get { return _chanceCards; } }
        public Queue<CommunityChestCard> CommunityChestCards { get { return _communityChestCards; } }
        public Board CurrentBoard { get { return _board; } }
        public Dictionary<LandCell,LandCard> CellToCardLink { set { _cellToCardLink = value; } get { return _cellToCardLink; } }
        public List<LandCard> LandCards { get { return _landCards; } }

        public MonopolyGame(
            MonopolyPlayer[] players,                   // Players
            Dice[] dices,                               // Dices
            Dictionary<MoneyType, int> initialMoney,    // Initial money for bank
            Dictionary<MoneyType, int> playerInitialMoney,    // Initial money for each players
            Tuple<int, int, int> boardParams,           // Board parameters <row, col, color>
            List<Tuple<int, CellType, Tuple<int, int, int>, Tuple<string, string>, int, LandType, CardType>> cellsData, // Cell data
            Dictionary<int, Tuple<string, string>> chanceCardData,    // Text and image data for chance cards
            Dictionary<int, Tuple<string, string>> communityCardData,  // Text and image data for community chest cards
            List<Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int>> landCardData,  // Data for land cards
            Dictionary<int, int> cellToCardLinkData     // Data link for cell -> card
            )
        {
            _playersPosition = new Dictionary<MonopolyPlayer, MonopolyCell>();
            _cellsOwner = new Dictionary<MonopolyCell, MonopolyPlayer>();
            _chanceCards = new Queue<ChanceCard>();
            _communityChestCards = new Queue<CommunityChestCard>();
            _landCards = new List<LandCard>();
            _currentPlayer = null;
            _losers = new List<Player>();
            _cellToCardLink = new Dictionary<LandCell, LandCard>();

            // Dices data
            _dices = dices;

            // Initial money for bank data
            _initialMoney = initialMoney;

            // Player initial money
            _playerInitialMoney = playerInitialMoney;

            // Board parameters data
            _boardParams = boardParams;

            // Cells data
            _cellsData = cellsData;

            // Chance cards data
            var chanceCardExternalData = CompileActionCardsExternalData(chanceCardData);

            _chanceCardData = new Tuple<int, Dictionary<int, Action<MonopolyPlayer>>, Dictionary<int, string>, Dictionary<int, string>>(
                5,
                new Dictionary<int, Action<MonopolyPlayer>>() { // Actions
                    { 0, delegate(MonopolyPlayer p){ ChanceCard1Action(p); } },
                    { 1, delegate(MonopolyPlayer p){ ChanceCard2Action(p); } },
                    { 2, delegate(MonopolyPlayer p){ ChanceCard3Action(p); } },
                    { 3, delegate(MonopolyPlayer p){ ChanceCard4Action(p); } },
                    { 4, delegate(MonopolyPlayer p){ ChanceCard5Action(p); } }
                },
                chanceCardExternalData.Item1,    // Texts
                chanceCardExternalData.Item2    // Images path
            );

            // Community chest cards data
            var communityCardExternalData = CompileActionCardsExternalData(communityCardData);

            _communityCardData = new Tuple<int, Dictionary<int, Action<MonopolyPlayer>>, Dictionary<int, string>, Dictionary<int, string>>(
                5,
                new Dictionary<int, Action<MonopolyPlayer>>() { // Actions
                    { 0, delegate(MonopolyPlayer p){ CommunityCard1Action(p); } },
                    { 1, delegate(MonopolyPlayer p){ CommunityCard2Action(p); } },
                    { 2, delegate(MonopolyPlayer p){ CommunityCard3Action(p); } },
                    { 3, delegate(MonopolyPlayer p){ CommunityCard4Action(p); } },
                    { 4, delegate(MonopolyPlayer p){ CommunityCard5Action(p); } }
                },
                communityCardExternalData.Item1,    // Texts
                communityCardExternalData.Item2    // Images path
            );

            // Land cards data
            _landCardData = landCardData;

            // Cell to card link (temp)
            _cellToCardLinkData = cellToCardLinkData;

            // set value for moneyAtStart
            _moneyAtStartCell = 200;

            // Randomize players turn
            RandomizeTurn(players);

            //Initializes game
            InitializeGame();
        }


        public override void RunGame()
        {
            var currentTurn = 0;
            Player legitimatePlayer;

            while (_winners.Count < 1)
            {
                // Check if current player ald lose
                while (_losers.Contains(_players[currentTurn]))
                {
                    if (currentTurn + 1 < _players.Count)
                        currentTurn++;
                    else
                        currentTurn = 0;
                }

                legitimatePlayer = _players[currentTurn];
                Console.WriteLine("Player " + legitimatePlayer.Username + " please run your turn");

                // Wait until runTurn invoke
                while (_currentPlayer == null)
                {
                    System.Threading.Thread.Sleep(500);
                }

                // Validate player turn
                if (!legitimatePlayer.Equals(_currentPlayer))
                {
                    Console.WriteLine(_currentPlayer.Username + ", this is not your turn!");
                    _currentPlayer = null;
                    continue;
                }

                // Main logic
                var steps = 0;
                var doubleSix = false;
                var doubleSixOccurences = 0;
                do
                {
                    // Roll dices
                    Console.WriteLine("Rolling dices ...");
                    steps = 0;
                    foreach (Dice dice in _dices)
                        steps += dice.Roll();

                    // Check for double six
                    if (steps == 12)
                    {
                        doubleSix = true;
                        doubleSixOccurences++;
                    }
                    else
                    {
                        doubleSix = false;
                        doubleSixOccurences = 0;
                    }

                    Console.WriteLine("Dice rolled : " + steps);

                    // If double six occurs 3 times, goto jail
                    if (doubleSixOccurences > 2)
                    {
                        MovePlayer((MonopolyPlayer)_currentPlayer, _board.ListOfMonopolyCell[10]);
                    }

                    // Move player
                    MovePlayer((MonopolyPlayer)_currentPlayer, steps);
                }
                while (doubleSix == true);

                Console.WriteLine("End of turn");
                Thread.Sleep(2000);
                Console.Clear();

                // Switch to next player
                if (currentTurn + 1 < _players.Count)
                    currentTurn++;
                else
                    currentTurn = 0;

                _currentPlayer = null;
            }
        }

        static object _turnLocker = new object();
        public override void RunTurn(Player player)
        {
            lock (_turnLocker)
            {
                _currentPlayer = player;

                // Wait until the player finishes its turn
                while (_currentPlayer != null)
                {
                    System.Threading.Thread.Sleep(500);
                }
            }
        }

        protected override void InitializeGame()
        {
            // Init board
            Console.WriteLine("Initialize board ...");
            _board = new MonopolyBoard(_boardParams.Item1, _boardParams.Item2, _boardParams.Item3, _cellsData);

            // Init cells event
            foreach (MonopolyCell cell in _board.ListOfMonopolyCell)
            {
                if (cell.GetType().Equals(typeof(LandCell)))
                    cell.CellStepped += LandOnCellStepped;
                else if (cell.GetType().Equals(typeof(JailCell)))
                    cell.CellStepped += JailOnCellStepped;
                else if (cell.GetType().Equals(typeof(PayTaxCell)))
                    cell.CellStepped += PayTaxOnCellStepped;
                else if (cell.GetType().Equals(typeof(ActionCell)))
                    cell.CellStepped += ActionOnCellStepped;
                else if (cell.GetType().Equals(typeof(FreeParkingCell)))
                    cell.CellStepped += FreeParkingOnCellStepped;
                else if (cell.GetType().Equals(typeof(GotoJailCell)))
                    cell.CellStepped += GoToJailOnCellStepped;
                else if (cell.GetType().Equals(typeof(StartCell)))
                    cell.CellStepped += StartOnPassed;
                else
                    Console.WriteLine("Cell type not recognized");
            }

            // Init bank
            Console.WriteLine("Initialize bank ...");
            _bank = new Bank(_initialMoney);

            // Init players position
            Console.WriteLine("Initialize player positions ...");
            foreach (MonopolyPlayer player in _players.Values)
            {
                _playersPosition.Add(player, _board.ListOfMonopolyCell[0]);
            }

            // Init players money
            Console.WriteLine("Initialize players money ...");
            Dictionary<MoneyType, int> playerInitialMoney;
            foreach (MonopolyPlayer player in _players.Values)
            {
                playerInitialMoney = new Dictionary<MoneyType, int>(_playerInitialMoney);
                player.Receive(playerInitialMoney);
            }

            // Init cells owner
            Console.WriteLine("Initialize cells owner ...");
            foreach (MonopolyCell cell in _board.ListOfMonopolyCell)
            {
                if (cell.GetType().Equals(typeof(LandCell)))
                    _cellsOwner.Add(cell, null); // Null means no player owned the cell
            }

            // Initialize chance cards
            Console.WriteLine("Initialize chance cards ...");
            if (!InitializeActionCards(typeof(ChanceCard), _chanceCardData.Item1, _chanceCardData.Item2, _chanceCardData.Item3, _chanceCardData.Item4))
            {
                Console.WriteLine("Initialize chance card error!");
                return;
            }

            // Initialize community chest cards
            Console.WriteLine("Initialize community chest cards ...");
            if (!InitializeActionCards(typeof(CommunityChestCard), _communityCardData.Item1, _communityCardData.Item2, _communityCardData.Item3, _communityCardData.Item4))
            {
                Console.WriteLine("Initialize community chest card error!");
                return;
            }

            // Initialize land cards
            Console.WriteLine("Initialize land cards ...");
            foreach (Tuple<string, Dictionary<PropertyType, int>, Dictionary<int, int>, int> land in _landCardData)
            {
                _landCards.Add(new LandCard(land.Item1, land.Item2, land.Item3, land.Item4));
            }

            // Initialize cell to card link
            Console.WriteLine("Initialize land cells to land cards link ...");
            foreach (int cellId in _cellToCardLinkData.Keys)
            {
                _cellToCardLink.Add((LandCell)_board.ListOfMonopolyCell[cellId], _landCards[_cellToCardLinkData[cellId]]);
            }

            Console.WriteLine("Game initialized!");
            Thread.Sleep(500);
            Console.Clear();
        }

        // This method compiles action cards texts and image paths
        private Tuple<Dictionary<int, string>, Dictionary<int, string>> CompileActionCardsExternalData(
            Dictionary<int, Tuple<string, string>> actionCardParams)
        {
            var actionCardTexts = new Dictionary<int, string>();
            var actionCardImages = new Dictionary<int, string>();

            foreach (int key in actionCardParams.Keys)
                actionCardTexts.Add(key, actionCardParams[key].Item1);
            foreach (int key in actionCardParams.Keys)
                actionCardImages.Add(key, actionCardParams[key].Item2);

            return new Tuple<Dictionary<int, string>, Dictionary<int, string>>(actionCardTexts, actionCardImages);
        }

        // This method initializes action cards and shuffles them
        private bool InitializeActionCards
            (
                Type cardType,
                int maxActionId,
                Dictionary<int, Action<MonopolyPlayer>> actions,
                Dictionary<int, string> descriptions,
                Dictionary<int, string> images
            )
        {
            Random rand = new Random();
            List<int> results = new List<int>();
            int actionId = 0;

            for (int i = 0; i < maxActionId; i++)
            {
                do
                {
                    actionId = rand.Next(0, maxActionId);
                    Thread.Sleep(200);
                } while (results.Contains(actionId));

                results.Add(actionId);

                if (cardType.Equals(typeof(ChanceCard)))
                    _chanceCards.Enqueue(new ChanceCard(descriptions[actionId], images[actionId], actions[actionId]));
                else if (cardType.Equals(typeof(CommunityChestCard)))
                    _communityChestCards.Enqueue(new CommunityChestCard(descriptions[actionId], images[actionId], actions[actionId]));
                else
                    return false;
            }

            return true;
        }

        // This method moves player in n steps
        private void MovePlayer(MonopolyPlayer player, int steps)
        {
            bool startPassed = false;
            var currentCell = _playersPosition[player];
            int next = currentCell.ID + steps;
            if (next > _board.ListOfMonopolyCell.Count)
                next = next - _board.ListOfMonopolyCell.Count;

            if (steps > 0 && next < currentCell.ID)
                startPassed = true;

            _playersPosition[player] = _board.ListOfMonopolyCell[next];

            Console.WriteLine("Moving " + player.Username + " to " + _playersPosition[player].Name);

            if (startPassed)
            {
                MonopolyCell cell = _board.ListOfMonopolyCell[0];
                ((StartCell)cell).Step();
            }
                

            CallCellEvent(_playersPosition[player]);
        }

        // This method moves player directly to a cell
        private void MovePlayer(MonopolyPlayer player, MonopolyCell cell)
        {
            _playersPosition[player] = cell;

            Console.WriteLine("Moving " + player.Username + " to " + _playersPosition[player].Name);

            CallCellEvent(_playersPosition[player]);
        }

        // This method call cell events
        private void CallCellEvent(MonopolyCell cell)
        {
            if (cell.GetType().Equals(typeof(ActionCell)))
                ((ActionCell)cell).Step();
            else if (cell.GetType().Equals(typeof(LandCell)))
                ((LandCell)cell).Step();
            else if (cell.GetType().Equals(typeof(PayTaxCell)))
                ((PayTaxCell)cell).Step();
            else
                cell.Step();
        }

        // Cell event handlers
        public void GoToJailOnCellStepped(object sender, EventArgs evt)
        {
            MonopolyCell cell = (MonopolyCell)sender;
            MonopolyPlayer player = GetPlayerFromCell(cell);

            Console.WriteLine(player.Username + " stepped in " + cell.Name);
            Console.WriteLine("Your position will move to jail");

            // Move to jail cell
            MovePlayer(player, _board.ListOfMonopolyCell[10]);
        }

        public void LandOnCellStepped(object sender, EventArgs evt)
        {
            LandCell cell = (LandCell)sender;
            MonopolyPlayer player = GetPlayerFromCell(cell);
            LandCard card = _cellToCardLink[cell];

            Console.WriteLine(player.Username + " stepped in " + cell.Name);

            Tuple<ReturnState, Dictionary<MoneyType, int>> buyState;
            string resp;
            // Check for ownership
            if (_cellsOwner[cell] == null) // If not owned by anyone
            {
                Console.Write("Do you want to buy this land? (y/n)");
                resp = Console.ReadLine();

                if (!resp.Equals("y") && !resp.Equals("Y"))
                    return;

                buyState = player.BuyLand(card, cell);
                if(buyState.Item1 != ReturnState.Success)
                    Console.WriteLine("Error while buy this land");
                else
                    _bank.DepositMoney(buyState.Item2);
            }
            else if (_cellsOwner[cell] != player) // If owned by other player
            {
                Console.WriteLine("This cell is owned by " + _cellsOwner[cell].Username);

                // Current player need to pay rent to the cell owner
                if (!TransferMoneyToPlayer(player, card.RentPrice, _cellsOwner[cell]))
                    GameLose(player);

                return;
            }
            else if (_cellsOwner[cell] == null) // If owned by himself
            {
                if (card.Properties.Count < 4)
                {
                    // Ask player want to buy house or not?
                    Console.Write("Do you want to buy house(s) on this land? (y/n)");
                    resp = Console.ReadLine();
                    Console.WriteLine();

                    if (!resp.Equals("y") && !resp.Equals("Y"))
                        return;

                    // Buy house 1-4
                    int parsed = -1;
                    while (parsed < 1 || parsed > 4)
                    {
                        Console.Write("How many? (1-4)");
                        parsed = int.Parse(Console.ReadLine());
                    }
                    buyState = player.BuyProperty(card, PropertyType.House, parsed);
                    if (buyState.Item1 != ReturnState.Success)
                        Console.WriteLine("Error while buy house on this land");
                    else
                        _bank.DepositMoney(buyState.Item2);
                }
                else if (card.Properties.Count == 4)
                {
                    // Ask player want to buy hotel or not?
                    Console.Write("Do you want to buy hotel on this land? (y/n)");
                    resp = Console.ReadLine();
                    Console.WriteLine();

                    if (!resp.Equals("y") && !resp.Equals("Y"))
                        return;

                    // Buy hotel
                    buyState = player.BuyProperty(card, PropertyType.Hotel, 1);
                    if (buyState.Item1 != ReturnState.Success)
                        Console.WriteLine("Error while buy hotel on this land");
                    else
                        _bank.DepositMoney(buyState.Item2);
                }
            }
            else 
            {
                Console.WriteLine("Undefined behavior");
                return;
            }
        }

        public void FreeParkingOnCellStepped(object sender, EventArgs evt)
        {
            MonopolyCell cell = (MonopolyCell)sender;
            MonopolyPlayer player = GetPlayerFromCell(cell);

            Console.WriteLine(player.Username + " stepped in " + cell.Name);
            Console.WriteLine("No event");
        }

        public void ActionOnCellStepped(object sender, EventArgs evt)
        {
            MonopolyCell cell = (MonopolyCell)sender;
            MonopolyPlayer player = GetPlayerFromCell(cell);
            ActionCellEvtArgs args = (ActionCellEvtArgs)evt;

            Console.WriteLine(player.Username + " stepped in " + cell.Name);

            // Get action
            IActionCard card = GetActionCard(args.TypeCard);
            Action<MonopolyPlayer> cardAction = card.Action;

            // Do action
            cardAction(GetPlayerFromCell((MonopolyCell)sender));
        }

        public void StartOnPassed(object sender, EventArgs evt)
        {
            int price = _moneyAtStartCell;
            MonopolyPlayer player = (MonopolyPlayer)_currentPlayer;

            Console.WriteLine(player.Username + " Passed Start Cell. Get " + _moneyAtStartCell);

            Dictionary<MoneyType, int> inputPay = _bank.getInput(price);
            int counter = 1;
            int change = _bank.ValidateGetInputPrice(price, inputPay, "pay");
            while (change < 0 && counter <= 5)
            {
                inputPay = _bank.getInput(price);
                counter++;

            }
            if (counter > 5)
                return;

            // check if change exist
            if (change > 0)
            {
                // change always true
                Dictionary<MoneyType, int> inputChange = _bank.getInput(change);
                if (_bank.ValidateGetInputPrice(change, inputChange, "receive") != 0)
                    return;
                _bank.DepositMoney(inputChange);
                player.Pay(inputChange);
            }

            // receive money from bank
            _bank.WithdrawMoney(inputPay);
            player.Receive(inputPay);


        }

        public void PayTaxOnCellStepped(object sender, EventArgs evt)
        {
            int price = ((PayTaxCell)sender).Price;
            MonopolyPlayer player = GetPlayerFromCell((MonopolyCell)sender);
            MonopolyCell cell = (MonopolyCell)sender;

            Console.WriteLine(player.Username + " stepped in " + cell.Name);
            Console.WriteLine("Please pay " + price);

            if (!TransferMoneyToBank(player, price))
                GameLose(player);
        }

        public void JailOnCellStepped(object sender, EventArgs evt)
        {
            MonopolyCell cell = (MonopolyCell)sender;
            MonopolyPlayer player = GetPlayerFromCell((MonopolyCell)sender);

            Console.WriteLine(player.Username + " stepped in " + cell.Name);
            Console.WriteLine("No event");
        }

        private MonopolyPlayer GetPlayerFromCell(MonopolyCell cell)
        {
            foreach (MonopolyPlayer player in _playersPosition.Keys)
            {
                if (_playersPosition[player].Equals(cell))
                    return player;
            }

            Console.WriteLine("Cell " + cell.Name + " isnt stepped by anyone");
            return null;
        }

        // This method get an action card based on card type
        private IActionCard GetActionCard(CardType cardType)
        {
            IActionCard card = null;

            if (cardType == CardType.Chance)
            {
                card = _chanceCards.Dequeue();
                _chanceCards.Enqueue((ChanceCard)card);
            }
            else if (cardType == CardType.CommunityChest)
            {
                card = _communityChestCards.Dequeue();
                _communityChestCards.Enqueue((CommunityChestCard)card);
            }

            return card;
        }

        // Chance cards action
        private void ChanceCard1Action(MonopolyPlayer p)
        {
            // Advance to Start : Collect 200
            Console.WriteLine("Advance to Start : Collect {0}", _moneyAtStartCell);

            // change player position
            _playersPosition[p] = _board.ListOfMonopolyCell[0];
            
            int price = _moneyAtStartCell;
            Dictionary<MoneyType, int> inputPay = _bank.getInput(price);
            int counter = 1;
            int change = _bank.ValidateGetInputPrice(price, inputPay, "pay");
            while (change < 0 && counter <= 5)
            {
                inputPay = _bank.getInput(price);
                counter++;

            }
            if (counter > 5)
                return;

            // check if change exist
            if (change > 0)
            {
                // change always true
                Dictionary<MoneyType, int> inputChange = _bank.getInput(change);
                if (_bank.ValidateGetInputPrice(change, inputChange, "receive") != 0)
                    return;
                _bank.DepositMoney(inputChange);
                p.Pay(inputChange);
            }

            // receive money from bank
            _bank.WithdrawMoney(inputPay);
            p.Receive(inputPay);

        }

        private void ChanceCard2Action(MonopolyPlayer p)
        {
            // Pay poor tax  : Pay 15
            Console.WriteLine("Pay poor tax  : Pay 15");

            int price = 15;
            Dictionary<MoneyType, int> inputPay = p.getInput(price);
            int counter = 1;

            int change = p.ValidateGetInputPrice(price, inputPay, "pay");
            while (change < 0 && counter <= 5)
            {
                inputPay = p.getInput(price);
                counter++;

            }
            if (counter > 5)
                return;

            // check if change exist
            if (change > 0)
            {
                // change always true
                Dictionary<MoneyType, int> inputChange = p.getInput(change);
                if (p.ValidateGetInputPrice(change, inputChange, "receive") != 0)
                    return;
                p.Receive(inputChange);
                _bank.WithdrawMoney(inputChange);
            }

            //  pay to bank
            _bank.DepositMoney(inputPay);
            p.Pay(inputPay);
        }

        private void ChanceCard3Action(MonopolyPlayer p)
        {
            // Move 3 spaces
            Console.WriteLine("Move 3 spaces");

            MovePlayer(p, 3);

        }
        private void ChanceCard4Action(MonopolyPlayer p)
        {
            // Go To Jail
            Console.WriteLine("Go To Jail");

            _playersPosition[p] = _board.ListOfMonopolyCell[10];
            CallCellEvent(_playersPosition[p]);
        }

        private void ChanceCard5Action(MonopolyPlayer p)
        {
            // Advance to Cell 19 : Collect 200 if pass Start
            Console.WriteLine("Advance to Cell 19 : Collect {0} if pass Start", _moneyAtStartCell);

            MonopolyCell curretPosition = _playersPosition[p];
            int step = 0;
            if (curretPosition.ID >= 19)
            {
                step = 19 + (_board.ListOfMonopolyCell.Count - curretPosition.ID);
            }
            else
            {
                step = 19 - curretPosition.ID;
            }
            // change position
            MovePlayer(p, step);

        }

        // Community chest cards action
        private void CommunityCard1Action(MonopolyPlayer p)
        {
            // Bank Error : collect 200            
            Console.WriteLine("Bank Error : collect 200");

            int price = 200;
            Dictionary<MoneyType, int> inputPay = _bank.getInput(price);
            int counter = 1;

            int change = _bank.ValidateGetInputPrice(price, inputPay, "pay");
            while (change < 0 && counter <= 5)
            {
                inputPay = _bank.getInput(price);
                counter++;

            }
            if (counter > 5)
                return;

            // check if change exist
            if (change > 0)
            {
                // change always true
                Dictionary<MoneyType, int> inputChange = _bank.getInput(change);
                if (_bank.ValidateGetInputPrice(change, inputChange, "receive") != 0)
                    return;
                _bank.DepositMoney(inputChange);
                p.Pay(inputChange);
            }

            // receive money from bank
            _bank.WithdrawMoney(inputPay);
            p.Receive(inputPay);
        }

        private void CommunityCard2Action(MonopolyPlayer p)
        {
            // Doctor's fee : pay 50
            Console.WriteLine("Doctor's fee : pay 50");

            int price = 50;
            Dictionary<MoneyType, int> inputPay = p.getInput(price);
            int counter = 1;
            int change = p.ValidateGetInputPrice(price, inputPay, "pay");
            while (change < 0 && counter <= 5)
            {
                inputPay = p.getInput(price);
                counter++;

            }
            if (counter > 5)
                return;

            // check if change exist
            if (change > 0)
            {
                // change always true
                Dictionary<MoneyType, int> inputChange = p.getInput(change);
                if (p.ValidateGetInputPrice(change, inputChange, "receive") != 0)
                    return;
                p.Receive(inputChange);
                _bank.WithdrawMoney(inputChange);
            }

            // receive money from bank
            _bank.DepositMoney(inputPay);
            p.Pay(inputPay);
        }

        private void CommunityCard3Action(MonopolyPlayer p)
        {
            // Move 3 spaces
            Console.WriteLine("Move 3 spaces");

            MovePlayer(p, 3);
        }

        private void CommunityCard4Action(MonopolyPlayer p)
        {
            // Go to jail
            Console.WriteLine("Go to jail");

            // change playersPosition to jail
            _playersPosition[p] = _board.ListOfMonopolyCell[10];
            CallCellEvent(_playersPosition[p]);
        }

        private void CommunityCard5Action(MonopolyPlayer p)
        {
            // You won competition : collect 100
            Console.WriteLine("You won competition : collect 100");

            int price = 100;
            Dictionary<MoneyType, int> inputPay = _bank.getInput(price);
            int counter = 1;
            int change = _bank.ValidateGetInputPrice(price, inputPay, "pay");
            while (change < 0 && counter <= 5)
            {
                inputPay = _bank.getInput(price);
                counter++;

            }
            if (counter > 5)
                return;

            // check if change exist
            if (change > 0)
            {
                // change always true
                Dictionary<MoneyType, int> inputChange = _bank.getInput(change);
                if (_bank.ValidateGetInputPrice(change, inputChange, "receive") != 0)
                    return;
                _bank.DepositMoney(inputChange);
                p.Pay(inputChange);
            }

            // receive money from bank
            _bank.WithdrawMoney(inputPay);
            p.Receive(inputPay);
        }

        // Extra func
        private Dictionary<MoneyType, int> DeductPlayerMoney(MonopolyPlayer player, int price)
        {
            Tuple<ReturnState, Dictionary<MoneyType, int>> resp = new Tuple<ReturnState, Dictionary<MoneyType, int>>(0, null);

            while (resp.Item1 != ReturnState.Success)
            {
                resp = player.ValidateAndPay(price);

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

            }

            if (resp.Item1 == ReturnState.Success)
                return resp.Item2;
            else
                return null;
        }

        private bool TransferMoneyToBank(MonopolyPlayer fromPlayer, int price)
        {
            Dictionary<MoneyType, int> amountPaid = DeductPlayerMoney(fromPlayer, price);
            if (amountPaid != null)
            {
                //_bank.DepositMoney(amountPaid);
                _bank.DepositAndWithdrawWithChange(amountPaid);
                return true;
            }
            else
                return false;
        }

        private bool TransferMoneyToPlayer(MonopolyPlayer fromPlayer, int price, MonopolyPlayer toPlayer)
        {
            Dictionary<MoneyType, int> amountPaid = DeductPlayerMoney(fromPlayer, price);
            if (amountPaid != null)
            {
                toPlayer.Receive(amountPaid);

                return true;
            }
            else
                return false;
        }

        private void GameLose(MonopolyPlayer player)
        {
            Console.WriteLine("Player " + player.Username + " is bankrupt!");
            _losers.Add(player);
        }
        
    }
}
