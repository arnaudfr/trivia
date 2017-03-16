using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
    class Player
    {
        public string Name { get; set; }
        public int Place { get; set; }
        public int Purse { get; set; }
        public bool InPenaltyBox { get; set; }

        public Player(string playerName)
        {
            this.Name = playerName;
            this.Place = 0;
            this.Purse = 0;
            this.InPenaltyBox = false;
        }
    }
}
