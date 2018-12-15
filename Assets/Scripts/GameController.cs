using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameController : MonoBehaviour
{
    public static GameController _gameController;
    public GameObject _gameoverGUI;
    public bool _gameOver = false;

    public float _minMoveSpeed = 6.2f;
    public float _maxMoveSpeed = 11f;
    public float _gameMoveSpeed = 6.2f;
    public Vector3 _gameStartPlatformPosition = new Vector3(0f, -2.5f, 0f);
   
    public float _maxPlatformHeight = 30f;
    public float _minPlatformHeight = -4f;

    public float _minPlatformXDistance = 2f;
    public float _maxPlatformXDistance = 4.5f;

    public float _speedUpValue = .2f;
    public float _speedDownValue = 1f;

    public float _maxPlatformHeightDifference = 1.3f;
    public float _points;

    public int _speedUpMultiplier;
    public int _minSpeedMultiplier = 1;
    public int _maxSpeedMultiplier = 25;


    private void Awake()
    {
        if (_gameController == null)
        {
            _gameController = this;
            Time.timeScale = 1f;
        }
        else if (this != _gameController)
        {
            Destroy(this);
        }

        Time.timeScale = 1f;
    }

    public void Score()
    {
        _points += _speedUpMultiplier;
    }

    public void SpeedUp()
    {
        if (_speedUpMultiplier >= _maxSpeedMultiplier)
        {
            _points += _speedUpMultiplier;
            return;
        }

        _gameMoveSpeed += _speedUpValue;
        _speedUpMultiplier++;
    }

    public void SpeedDown()
    {
        if (_gameMoveSpeed - _speedDownValue <= _minMoveSpeed)
        {
            _speedUpMultiplier = _minSpeedMultiplier;
            _gameMoveSpeed = _minMoveSpeed;
            return;
        }

        _gameMoveSpeed -= _speedDownValue;
        _speedUpMultiplier -= 5;
    }

    public void GameOver()
    {
        _gameOver = true;
        Time.timeScale = 0f;
        PlayerSaveData._playerSaveDataInstance.UpdateHighscore(_points);
        _gameoverGUI.SetActive(true);
        GetComponentInChildren<GameMusic>().SetGameOverMusic();
        GoogleLeaderboardsPost.PostScoreToGoogleLeaderboards((long)_points);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

