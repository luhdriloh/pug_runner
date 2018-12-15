using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject _speedUpItemPrototype;
    public PlatformData _platformData;
    public float _speedUpItemSpawnRate;

    private int _platformPoolAmount;
    private List<Stack<Platform>> _platformsNotInUse;
    private List<Platform> _platformsInUse;
    private CoinItemScript _speedUpItem;

    private void Start()
    {
        _platformPoolAmount = _platformData._platformPrototypes.Count;
        _platformsInUse = new List<Platform>();
        _platformsNotInUse = new List<Stack<Platform>>();

        // create 5 of each different type of Platform
        for (int i = 0; i < _platformPoolAmount; i++)
        {
            _platformsNotInUse.Add(new Stack<Platform>());

            for (int j = 0; j < 5; j++)
            {
                GameObject newGameObject = Instantiate(_platformData._platformPrototypes[i], transform.position, Quaternion.identity);
                Platform newPlatform = newGameObject.GetComponent<Platform>();
                newPlatform._platformType = i;

                _platformsNotInUse[i].Push(newPlatform);
            }
        }

        GameObject newSpeedUpGameObject = Instantiate(_speedUpItemPrototype, transform.position, Quaternion.identity);
        newSpeedUpGameObject.SetActive(false);
        _speedUpItem = newSpeedUpGameObject.GetComponent<CoinItemScript>();

        CreateStartPlatform();
    }


    private void Update()
    {
        RecycleOutOfBoundsPlatforms();

        Vector3 platformPosition = _platformsInUse[_platformsInUse.Count - 1].gameObject.transform.position;
        if (platformPosition.x <= 10)
        {
            Platform platformCreated =  CreatePlatform();
            CreateSpeedUpCoin(platformCreated);
            //CreatePlatformBaddies(platformCreated);
        }
    }


    private void RecycleOutOfBoundsPlatforms()
    {
        List<Platform> toRemove = new List<Platform>();

        foreach (Platform platform in _platformsInUse)
        {

            if (platform.OutOfBounds())
            {
                toRemove.Add(platform);
            }
            else
            {
                break;
            }
        }

        foreach (Platform platformToRemove in toRemove)
        {
            platformToRemove.PutPlatformIntoRest();

            _platformsInUse.Remove(platformToRemove);
            _platformsNotInUse[platformToRemove._platformType].Push(platformToRemove);
        }
    }


    private void CreateStartPlatform()
    {
        int whichTypeOfPlatform = ReturnTypeOfPlatformToSpawn();
        Platform platform = _platformsNotInUse[whichTypeOfPlatform].Pop();
        _platformsInUse.Add(platform);

        platform.GetComponent<Platform>().PutPlatformIntoPlay(GameController._gameController._gameStartPlatformPosition);
    }


    private Vector3 ReturnNewPlatformPosition(float newPlatformWidth)
    {
        Platform previousPlatform = _platformsInUse[_platformsInUse.Count - 1];
        return new Vector3(ReturnXOfPlatformToSpawn(previousPlatform) + newPlatformWidth / 2, ReturnYOfPlatformToSpawn(previousPlatform), 0f);
    }


    private float ReturnXOfPlatformToSpawn(Platform previousPlatform)
    {
        float farRightSidePosition = previousPlatform.GetFarRightSide();
        float platformSpeedPart = GameController._gameController._gameMoveSpeed - GameController._gameController._minMoveSpeed;
        float rangeToAdd = Random.Range(GameController._gameController._minPlatformXDistance, GameController._gameController._maxPlatformXDistance + platformSpeedPart);
        return (farRightSidePosition + rangeToAdd);
    }


    private float ReturnYOfPlatformToSpawn(Platform previousPlatform)
    {
        float yPosition = previousPlatform.transform.position.y;
        bool movePosition = Random.Range(0f, 1f) > .5;
        if (!movePosition)
        {
            return yPosition;
        }

        yPosition = Mathf.Clamp(yPosition + Random.Range(-(GameController._gameController._maxPlatformHeightDifference), GameController._gameController._maxPlatformHeightDifference), GameController._gameController._minPlatformHeight, GameController._gameController._maxPlatformHeight);
        return yPosition;
    }


    private Platform CreatePlatform()
    {
        int whichTypeOfPlatform = ReturnTypeOfPlatformToSpawn();
        Platform platformScript = _platformsNotInUse[whichTypeOfPlatform].Pop();

        // get platform width
        float platformWidth = platformScript.GetPlatformWidth();

        Vector3 newPlatformPosition = ReturnNewPlatformPosition(platformWidth);
        _platformsInUse.Add(platformScript);

        platformScript.PutPlatformIntoPlay(newPlatformPosition);
        return platformScript;
    }

    private void CreateSpeedUpCoin(Platform lastPlatformCreated)
    {
        float platformWidth = lastPlatformCreated.GetPlatformWidth();
        if (Random.value < _speedUpItemSpawnRate && _speedUpItem._inUse == false)
        {
            float xPosOffset = Random.Range(platformWidth / -2.2f, platformWidth / 2.2f);
            Vector3 position = lastPlatformCreated.transform.position + new Vector3(xPosOffset, 1f, _speedUpItem.transform.position.z);

            _speedUpItem.SetPotionOnPlatform(position);
        }
    }

    private int ReturnTypeOfPlatformToSpawn()
    {
        float randNum = Random.value;
        for (int i = 0; i < _platformData._spawnRatePercentages.Count; i++)
        {
            if (randNum <= _platformData._spawnRatePercentages[i])
            {
                return i;
            }
        }

        return 0;
    }
}
