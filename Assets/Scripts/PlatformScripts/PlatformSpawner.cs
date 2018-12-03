using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public PlatformData _platformData;

    private int _platformPoolAmount;
    private List<Stack<GameObject>> _platformsNotInUse;
    private List<GameObject> _platformsInUse;


    // rules
    // start simple

    // we can spawn platforms of various sizes
    // we can use time to spawn the next platform as it will scale with

    // to determine time of next spawn, take into account
    // a. the base time between platform spawns
    // b. the size of the platform so we dont overlap them


    // rules
    // max y delta
    // max time between platforms

	private void Start ()
    {
        _platformPoolAmount = _platformData._platformPrototypes.Count;
        _platformsInUse = new List<GameObject>();
        _platformsNotInUse = new List<Stack<GameObject>>();

        // create 5 of each different type of Platform
        for (int i = 0; i < _platformPoolAmount; i++)
        {
            _platformsNotInUse.Add(new Stack<GameObject>());

            for (int j = 0; j < 5; j++)
            {
                GameObject newGameObject = Instantiate(_platformData._platformPrototypes[i], transform.position, Quaternion.identity);
                newGameObject.GetComponent<Platform>()._platformType = i;
                newGameObject.SetActive(false);
                _platformsNotInUse[i].Push(newGameObject);
            }
        }

        CreateStartPlatform();
	}


    private void Update ()
    {
        RecycleOutOfBoundsPlatforms();

        Vector3 platformPosition = _platformsInUse[_platformsInUse.Count - 1].gameObject.transform.position;
        if (platformPosition.x <= 10)
        {
            CreatePlatform();
        }
    }


    private void RecycleOutOfBoundsPlatforms()
    {
        List<GameObject> toRemove = new List<GameObject>();

        foreach (GameObject platform in _platformsInUse)
        {
            Platform platformScript = platform.GetComponent<Platform>();

            if (platformScript.OutOfBounds())
            {
                toRemove.Add(platform);
            }
            else
            {
                break;
            }
        }

        foreach (GameObject platformToRemove in toRemove)
        {
            Platform platformScript = platformToRemove.GetComponent<Platform>();
            platformScript.PutPlatformIntoRest();

            _platformsInUse.Remove(platformToRemove);
            _platformsNotInUse[platformScript._platformType].Push(platformToRemove);
        }
    }


    private void CreateStartPlatform()
    {
        GameObject platform = _platformsNotInUse[0].Pop();
        _platformsInUse.Add(platform);

        platform.GetComponent<Platform>().PutPlatformIntoPlay(GameValues._gamevalues._gameStartPlatformPosition);
    }


    private Vector3 ReturnNewPlatformPosition(float newPlatformWidth)
    {
        GameObject previousPlatform = _platformsInUse[_platformsInUse.Count - 1];

        return new Vector3(ReturnXOfPlatformToSpawn(previousPlatform) + newPlatformWidth / 2, ReturnYOfPlatformToSpawn(previousPlatform), 0f);
    }


    private float ReturnXOfPlatformToSpawn(GameObject previousPlatform)
    {
        float platformWidth = previousPlatform.GetComponent<SpriteRenderer>().size.x * previousPlatform.transform.localScale.x;
        float farRightSidePosition = previousPlatform.transform.position.x + (platformWidth / 2);
        float rangeToAdd = Random.Range(GameValues._gamevalues._minPlatformXDistance, GameValues._gamevalues._maxPlatformXDistance);
        float platformSpeedPart = GameValues._gamevalues._gameMoveSpeed - GameValues._gamevalues._baseMoveSpeed;
        return (farRightSidePosition + rangeToAdd + platformSpeedPart);
    }


    private float ReturnYOfPlatformToSpawn(GameObject previousPlatform)
    {
        float yPosition = previousPlatform.transform.position.y;
        bool movePosition = Random.Range(0f, 1f) > .5;
        if (!movePosition)
        {
            return yPosition;
        }

        yPosition = Mathf.Clamp(yPosition + Random.Range(-(GameValues._gamevalues._maxPlatformHeightDifference), GameValues._gamevalues._maxPlatformHeightDifference), GameValues._gamevalues._minPlatformHeight, GameValues._gamevalues._maxPlatformHeight);
        return yPosition;
    }


    private void CreatePlatform()
    {
        int whichTypeOfPlatform = ReturnTypeOfPlatformToSpawn();
        GameObject gameobject = _platformsNotInUse[whichTypeOfPlatform].Pop();

        Platform platformScript = gameobject.GetComponent<Platform>();

        // get platform width
        float platformWidth = gameobject.GetComponent<SpriteRenderer>().size.x * gameobject.transform.localScale.x;

        Vector3 newPlatformPosition = ReturnNewPlatformPosition(platformWidth);
        _platformsInUse.Add(gameobject);

        platformScript.PutPlatformIntoPlay(newPlatformPosition);
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
