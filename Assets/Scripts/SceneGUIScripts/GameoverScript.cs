﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameoverScript : MonoBehaviour
{
    public List<Sprite> _medalSprites;

    public GameObject _scoreTextToTurnOff;
    public GameObject _scoreMuliplierTextToTurnOff;
    public Image _medalImage;

    public Text _currentScoreText;
    public Text _highscoreText;

    public Button _startGameButton;
    public Button _leaderboardsButton;
    public Button _rateButton;

    private void OnEnable()
    {
        _scoreTextToTurnOff.SetActive(false);
        _scoreMuliplierTextToTurnOff.SetActive(false);

        long currentScore = (long)GameController._gameController._points;
        List<long> highscores = PlayerSaveData._playerSaveDataInstance.GetHighscores();
        long highscore = highscores[highscores.Count - 1];

        _currentScoreText.text = currentScore.ToString();
        _highscoreText.text = highscore.ToString();

        int medalSpriteToUse = PlayerSaveData._playerSaveDataInstance._lastPlaceAttained;
        medalSpriteToUse = medalSpriteToUse >= 4 ? 3 : medalSpriteToUse;
        _medalImage.sprite = _medalSprites[medalSpriteToUse];

        // set up the buttons
        _startGameButton.onClick.AddListener(StartGameplayScene);
        _leaderboardsButton.onClick.AddListener(StartLeaderboardsScene);
        _rateButton.onClick.AddListener(RateGame);
    }

    private void StartLeaderboardsScene()
    {
        GoogleLeaderboardsPost.ShowLeaderboards();
    }

    private void StartGameplayScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void RateGame()
    {
        Application.OpenURL("market://details?id=com.littleapex.foxyrunner");
    }
}
