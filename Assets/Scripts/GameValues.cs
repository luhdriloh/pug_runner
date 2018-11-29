using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValues : MonoBehaviour
{
    public static GameValues _gamevalues;

    public float _gameMoveSpeed = 6f;
    public Vector3 _gameStartPlatformPosition = new Vector3(0f, -6f, 0f);

    public float _maxPlatformHeight = -2.5f;
    public float _minPlatformHeight = -8.5f;

    public float _minPlatformScale = 1f;
    public float _maxPlatformScale = 3.9f;

    public float _maxPlatformHeightDifference = 1.5f;

    private void Start()
    {
        if (_gamevalues == null)
        {
            _gamevalues = this;
        }
        else if (this != _gamevalues)
        {
            Destroy(this);
        }
    }
}

