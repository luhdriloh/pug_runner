using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverScript : MonoBehaviour
{
    public GameObject _scoreTextToTurnOff;
    public GameObject _scoreMuliplierTextToTurnOff;

    public Text _currentScoreText;
    public Text _highscoreText;

    public Button _startGameButton;
    public Button _leaderboardsButton;

    private void OnEnable()
    {
        _scoreTextToTurnOff.SetActive(false);
        _scoreMuliplierTextToTurnOff.SetActive(false);

        long currentScore = GameController._gameController._points;
        List<long> highscores = PlayerSaveData._playerSaveDataInstance.GetHighscores();
        long highscore = highscores[highscores.Count - 1];

        _currentScoreText.text = currentScore.ToString();
        _highscoreText.text = highscore.ToString();

        // set up the buttons
        _startGameButton.onClick.AddListener(StartGameplayScene);
    }

    private void StartLeaderboardsScene()
    {

    }

    private void StartGameplayScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
