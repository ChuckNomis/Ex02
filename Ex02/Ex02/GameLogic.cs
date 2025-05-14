using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex02
{
    internal class GameLogic
    {
        public GameLogic()
        {
            StartGame();
        }

        public void StartGame()
        {
            GameUI gameUI = new GameUI();
            bool _keepPlaying = true;
            while (_keepPlaying) {
                bool _gameWon = false;
                int _numberOfGuesses = gameUI.getNumberOfGuesses();
                List<Pin> _historyOfPins = new List<Pin>();
                List<Result> _historyOfFeedbacks = new List<Result>();
                Pin _targetPin = Pin.GenerateTargetPin();
                for (int i = 0; i < _numberOfGuesses; i++)
                {
                    gameUI.clearScreen();
                    gameUI.printTheBoard(_historyOfPins,_historyOfFeedbacks); 
                    Pin currentPin = gameUI.getUserGuess();
                    Result currentResult = new Result(currentPin, _targetPin);
                    _historyOfFeedbacks.Add(currentResult);
                    _historyOfPins.Add(currentPin);
                    if (currentResult.getResult() == "VVVV")
                    {
                        _gameWon = true;
                        gameUI.clearScreen();
                        gameUI.printTheBoard(_historyOfPins, _historyOfFeedbacks);
                        gameUI.showWin();
                        break;
                    }
                }
                if(!_gameWon)
                {
                    gameUI.showLose();
                }
                gameUI.showPlayAgain(ref _keepPlaying);
            }

        }
    }
}
