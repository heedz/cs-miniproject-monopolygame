using System.Collections.Generic;
using System.Threading;
using System;

namespace MonopolyGame
{
    public abstract class BoardGame
    {
        protected List<Player> _winners;
        protected Dice[] _dices;
        protected Dictionary<int, Player> _players;
        protected Board _board;

        public BoardGame()
        {
            _players = new Dictionary<int, Player>();
            _winners = new List<Player>();
        }

        public abstract void RunTurn(Player player);

        public abstract void RunGame();

        protected abstract void InitializeGame();

        protected virtual void RandomizeTurn(Player[] players)
        {
            Random rand = new Random();
            int turn = 0;

            foreach (Player player in players)
            {
                do
                {
                    turn = rand.Next(0, players.Length);
                    Thread.Sleep(200);
                } while (_players.ContainsKey(turn));
                
                _players.Add(turn, player);
            }
        }
    }
}
