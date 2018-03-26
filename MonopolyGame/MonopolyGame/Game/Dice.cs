using System;
using System.Threading;

namespace MonopolyGame
{
    public class Dice
    {
        private int _min;
        private int _max;

        public Dice(int min, int max)
        {
            _min = min;
            _max = max + 1;
        }

        public int Roll()
        {
            Thread.Sleep(200);  // Add sleep a bit to make random working properly
            Random rnd = new Random();
            int number = rnd.Next(_min, _max);
            return number;
        }
    }
}
