using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonopolyGame
{
    public class ActionCellEvtArgs : EventArgs
    {
        public CardType TypeCard { get; set; }
    }
}
