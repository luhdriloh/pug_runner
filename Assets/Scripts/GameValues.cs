using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameValues : MonoBehaviour
{
    public static GameValues _gamevalues;

    public float _baseMoveSpeed = 6f;
    public float _gameMoveSpeed = 6f;
    public Vector3 _gameStartPlatformPosition = new Vector3(0f, -2.5f, 0f);
   
    public float _maxPlatformHeight = -1.5f;
    public float _minPlatformHeight = -4f;

    public float _minPlatformXDistance = 2f;
    public float _maxPlatformXDistance = 4.5f;

    public float _maxPlatformHeightDifference = 1.3f;

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

