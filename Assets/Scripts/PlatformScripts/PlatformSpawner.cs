using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject _platformPrototype;
    public int _platformPoolAmount;

    private Stack<GameObject> _platformsNotInUse;
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

    // h

	private void Start ()
    {
        _platformsInUse = new List<GameObject>();
        _platformsNotInUse = new Stack<GameObject>();

        for (int i = 0; i < _platformPoolAmount; i++)
        {
            GameObject newGameObject = Instantiate(_platformPrototype, transform.position, Quaternion.identity);
            newGameObject.SetActive(false);
            _platformsNotInUse.Push(newGameObject);

        }

        CreateStartPlatform();
	}


    private void Update ()
    {
        RecycleOutOfBoundsPlatforms();

        // check each platform out of bounds
        Vector3 platformPosition = _platformsInUse[_platformsInUse.Count - 1].gameObject.transform.position;
        if (platformPosition.x <= 0)
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
        }

        foreach (GameObject platformToRemove in toRemove)
        {
            Platform platformScript = platformToRemove.GetComponent<Platform>();
            platformScript.PutPlatformIntoRest();

            _platformsInUse.Remove(platformToRemove);
            _platformsNotInUse.Push(platformToRemove);
        }
    }


    private void CreateStartPlatform()
    {
        GameObject platform = _platformsNotInUse.Pop();
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

        return farRightSidePosition;
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


    private float ReturnNewScale()
    {
        return Random.Range(GameValues._gamevalues._minPlatformScale, GameValues._gamevalues._maxPlatformScale);
    }


    private void CreatePlatform()
    {
        GameObject gameobject = _platformsNotInUse.Pop();

        Platform platformScript = gameobject.GetComponent<Platform>();

        // set new platform width
        float newXScale = ReturnNewScale();
        gameobject.transform.localScale = new Vector3(newXScale, gameobject.transform.localScale.y, 0f);

        float newPlatformWidth = newXScale * gameobject.GetComponent<SpriteRenderer>().size.x;

        Vector3 newPlatformPosition = ReturnNewPlatformPosition(newPlatformWidth);
        _platformsInUse.Add(gameobject);

        platformScript.PutPlatformIntoPlay(newPlatformPosition);
    }
}
