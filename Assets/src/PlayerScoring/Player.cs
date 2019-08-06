using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.src.PlayerScoring
{
    class Player
    {
        private string name;
        private int roundOneScore;
        private int roundTwoScore;
        private int tokens;

        public string Name { get => name; set => name = value; }
        public int RoundOneScore { get => roundOneScore; set => roundOneScore = value; }
        public int RoundTwoScore { get => roundTwoScore; set => roundTwoScore = value; }
        public int Tokens { get => tokens; set => tokens = value; }

        public Player()
        {
            Console.WriteLine("Yo waddUp homie Im a pl8r");
        }

        public void UseToken()
        {
            tokens -= 1;
        }

        public void AddToken()
        {
            tokens += 1;
        }
    }
}
