using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverPage = default;
        [SerializeField] private GameObject _gameStartPage = default;
        [SerializeField] private Button _replayButton = default;
        [SerializeField] private Button _fireButton = default;
        [SerializeField] private Text _scoreText = default;
        public Button FireButton => _fireButton;
        public Button ReplayButton => _replayButton;
        private ScoreManager _scoreManager = default;

        [Inject]
        private void Setup(IScoreManager scoreManager)
        {
            _scoreManager = (ScoreManager)scoreManager;
        }
        
        internal enum PageState
        {
            None,
            StartGame,
            GameOver
        }
    
        internal void SetPageState(PageState state)
        {
            switch (state)
            {
                case PageState.None:
                    _gameOverPage.SetActive(false);
                    _gameStartPage.SetActive(false);
                    break;
                case PageState.StartGame:
                    _gameOverPage.SetActive(false);
                    _gameStartPage.SetActive(true);
                    break;
                case PageState.GameOver:
                    _gameOverPage.SetActive(true);
                    _gameStartPage.SetActive(false);
                    break;
            }
        }

        private void Start()
        {
            _fireButton.onClick.AddListener(ConfirmGameStart);
            _scoreManager.OnScoreUpdate += UpdateCurrentScore;
        }

        private void ConfirmGameStart()
        {
            SetPageState(PageState.None);
        }

        private void UpdateCurrentScore(int score)
        {
            _scoreText.text = _scoreManager.CurrentScore.ToString();
        }
    }
}
