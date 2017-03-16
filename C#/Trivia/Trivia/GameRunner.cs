using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trivia
{
    public class GameRunner
    {

        private static bool _winner;

        public static void Main(String[] args)
        {
            for (int i = 0; i <= 10; i++)
            {
                Game aGame = new Game();

                aGame.Add("Chet");
                aGame.Add("Pat");
                aGame.Add("Sue");

                Random rand = new Random(i);

                do
                {

                    aGame.Roll(rand.Next(5) + 1);

                    if (rand.Next(9) == 7)
                    {
                        _winner = !aGame.WrongAnswer();
                    }
                    else
                    {
                        _winner = !aGame.WasCorrectlyAnswered();
                    }



                } while (!_winner);
            }
        }


    }

}

