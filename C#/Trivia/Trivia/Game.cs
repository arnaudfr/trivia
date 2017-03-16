using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        List<Player> _players = new List<Player>();

        int[] _places = new int[6];
        int[] _purses = new int[6];

        bool[] _inPenaltyBox = new bool[6];

        LinkedList<string> _popQuestions = new LinkedList<string>();
        LinkedList<string> _scienceQuestions = new LinkedList<string>();
        LinkedList<string> _sportsQuestions = new LinkedList<string>();
        LinkedList<string> _rockQuestions = new LinkedList<string>();

        int _currentPlayer = 0;
        bool _isGettingOutOfPenaltyBox;

        public Game()
        {
            for (int i = 0; i < 50; i++)
            {
                _popQuestions.AddLast("Pop Question " + i);
                _scienceQuestions.AddLast(("Science Question " + i));
                _sportsQuestions.AddLast(("Sports Question " + i));
                _rockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        public String CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool IsPlayable()
        {
            return (HowManyPlayers() >= 2);
        }

        public bool Add(String playerName)
        {
            Player player = new Player(playerName);
            _players.Add(player);

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.Count);
            return true;
        }

        public int HowManyPlayers()
        {
            return _players.Count;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(_players[_currentPlayer].Name + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_players[_currentPlayer].InPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;

                    Console.WriteLine(_players[_currentPlayer].Name + " is getting out of the penalty box");
                    _players[_currentPlayer].Place = _players[_currentPlayer].Place + roll;
                    if (_players[_currentPlayer].Place > 11) _players[_currentPlayer].Place = _players[_currentPlayer].Place - 12;

                    Console.WriteLine(_players[_currentPlayer].Name
                            + "'s new location is "
                            + _players[_currentPlayer].Place);
                    Console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(_players[_currentPlayer].Name + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }

            }
            else
            {

                _players[_currentPlayer].Place = _players[_currentPlayer].Place + roll;
                if (_players[_currentPlayer].Place > 11) _players[_currentPlayer].Place = _players[_currentPlayer].Place - 12;

                Console.WriteLine(_players[_currentPlayer].Name
                        + "'s new location is "
                        + _players[_currentPlayer].Place);
                Console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }

        }

        private void AskQuestion()
        {
            switch (CurrentCategory())
            {
                case "Pop":
                    Console.WriteLine(_popQuestions.First());
                    _popQuestions.RemoveFirst();
                    break;
                case "Science":
                    Console.WriteLine(_scienceQuestions.First());
                    _scienceQuestions.RemoveFirst();
                    break;
                case "Sports":
                    Console.WriteLine(_sportsQuestions.First());
                    _sportsQuestions.RemoveFirst();
                    break;
                case "Rock":
                    Console.WriteLine(_rockQuestions.First());
                    _rockQuestions.RemoveFirst();
                    break;
            }
        }


        private String CurrentCategory()
        {
            string[] category = {"Pop", "Science", "Sports", "Rock"};

            return category[_players[_currentPlayer].Place % 4];
        }

        public bool WasCorrectlyAnswered()
        {
            if (_players[_currentPlayer].InPenaltyBox)
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _players[_currentPlayer].Purse++;
                    Console.WriteLine(_players[_currentPlayer].Name
                            + " now has "
                            + _players[_currentPlayer].Purse
                            + " Gold Coins.");

                    bool winner = DidPlayerWin();
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;

                    return winner;
                }
                else
                {
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;
                    return true;
                }



            }
            else
            {

                Console.WriteLine("Answer was corrent!!!!");
                _players[_currentPlayer].Purse++;
                Console.WriteLine(_players[_currentPlayer].Name
                        + " now has "
                        + _players[_currentPlayer].Purse
                        + " Gold Coins.");

                bool winner = DidPlayerWin();
                _currentPlayer++;
                if (_currentPlayer == _players.Count) _currentPlayer = 0;

                return winner;
            }
        }

        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_players[_currentPlayer].Name + " was sent to the penalty box");
            _players[_currentPlayer].InPenaltyBox = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
            return true;
        }


        private bool DidPlayerWin()
        {
            return (_players[_currentPlayer].Purse != 6);
        }
    }

}
