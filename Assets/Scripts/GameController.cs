using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameController : MonoBehaviour
{
    public static GameController _gameController;
    public GameObject _gameoverGUI;
    public bool _gameOver = false;

    public float _minMoveSpeed = 6f;
    public float _maxMoveSpeed = 12f;
    public float _gameMoveSpeed = 6f;
    public Vector3 _gameStartPlatformPosition = new Vector3(0f, -2.5f, 0f);
   
    public float _maxPlatformHeight = -1.5f;
    public float _minPlatformHeight = -4f;

    public float _minPlatformXDistance = 2f;
    public float _maxPlatformXDistance = 4.5f;

    public float _speedUpValue = .2f;
    public float _speedDownValue = .2f;

    public float _maxPlatformHeightDifference = 1.3f;
    public long _points;

    public int _speedUpMultiplier = 1;


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

    public void SpeedUp()
    {
        if (_gameMoveSpeed >= _maxMoveSpeed)
        {
            return;
        }

        _speedUpMultiplier++;
        _gameMoveSpeed += _speedUpValue;
    }

    public void SpeedDown()
    {
        if (_gameMoveSpeed <= _minMoveSpeed)
        {
            _gameMoveSpeed = _minMoveSpeed;
            return;
        }

        _gameMoveSpeed -= _speedDownValue;
        _speedUpMultiplier--;
    }

    public void Score()
    {
        _points += _speedUpMultiplier;
    }

    public void GameOver()
    {
        _gameOver = true;
        Time.timeScale = 0f;
        PlayerSaveData._playerSaveDataInstance.UpdateHighscore(_points);
        _gameoverGUI.SetActive(true);
        GetComponentInChildren<GameMusic>().SetGameOverMusic();
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

