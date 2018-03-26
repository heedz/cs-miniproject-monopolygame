using System;
namespace MonopolyGame
{
    public class LandCellEvtArgs : EventArgs
    {
        public int Price { get; set; }
        public LandType TypeLand { get; set; }
    }

}
