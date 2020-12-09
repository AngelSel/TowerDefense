using System;

namespace Managers
{
    public class ScoreManager: IScoreManager
    {
        public event  Action<int> OnScoreUpdate = delegate {  };
        private int _currentScore = 0;

        public int CurrentScore
        {
            get => _currentScore;
            set => _currentScore = value;
        }

        public void AddScore(int score)
        {
            CurrentScore += score;
            OnScoreUpdate.Invoke(_currentScore);
        }

        public void InitScore()
        {
            CurrentScore = 0;
            OnScoreUpdate.Invoke(_currentScore);
        }
    }
}
