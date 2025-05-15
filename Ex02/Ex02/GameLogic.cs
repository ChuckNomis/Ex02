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
            GameUI       gameUI = new GameUI();
            bool         _keepPlaying = true;
            bool         _gameWon;
            int          _numberOfGuesses;
            List<Pin>    _historyOfPins;
            List<Result> _historyOfFeedbacks;
            Pin          _targetPin;
            Pin          currentPin;
            Result       currentResult;
            while (_keepPlaying) {
                _gameWon = false;
                _numberOfGuesses = gameUI.getNumberOfGuesses();
                _historyOfPins = new List<Pin>();
                _historyOfFeedbacks = new List<Result>();
                _targetPin = Pin.GenerateTargetPin();

                for (int i = 0; i < _numberOfGuesses; i++)
                {
                    gameUI.clearScreen();
                    gameUI.printTheBoard(_historyOfPins,_historyOfFeedbacks); 
                    currentPin = gameUI.getUserGuess();

                    if (currentPin._pinValue == "Q")
                    {
                        gameUI.clearScreen();
                        gameUI.showExit();
                        return;
                    }

                    currentResult = new Result(currentPin, _targetPin);
                    _historyOfFeedbacks.Add(currentResult);
                    _historyOfPins.Add(currentPin);

                    if (currentResult.getResult() == Result.WinResult)
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
