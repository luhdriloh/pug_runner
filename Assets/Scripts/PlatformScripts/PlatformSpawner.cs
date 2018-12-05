using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject _potionPrototype;
    public PlatformData _platformData;
    public float _speedUpPotionSpawnRate;

    private int _platformPoolAmount;
    private List<Stack<GameObject>> _platformsNotInUse;
    private List<GameObject> _platformsInUse;
    private PotionItemScript _potion;

    private void Start()
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

        GameObject newPotionGameObject = Instantiate(_potionPrototype, transform.position, Quaternion.identity);
        newPotionGameObject.SetActive(false);
        _potion = newPotionGameObject.GetComponent<PotionItemScript>();

        CreateStartPlatform();
    }


    private void Update()
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
        int whichTypeOfPlatform = ReturnTypeOfPlatformToSpawn();
        GameObject platform = _platformsNotInUse[whichTypeOfPlatform].Pop();
        _platformsInUse.Add(platform);

        platform.GetComponent<Platform>().PutPlatformIntoPlay(GameController._gameController._gameStartPlatformPosition);
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
        float rangeToAdd = Random.Range(GameController._gameController._minPlatformXDistance, GameController._gameController._maxPlatformXDistance);
        float platformSpeedPart = GameController._gameController._gameMoveSpeed - GameController._gameController._minMoveSpeed;
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

        yPosition = Mathf.Clamp(yPosition + Random.Range(-(GameController._gameController._maxPlatformHeightDifference), GameController._gameController._maxPlatformHeightDifference), GameController._gameController._minPlatformHeight, GameController._gameController._maxPlatformHeight);
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

        if (Random.value > _speedUpPotionSpawnRate && _potion._inUse == false)
        {
            float xPosOffset = Random.Range(platformWidth / -2f, platformWidth / 2f);
            Vector3 position = newPlatformPosition + new Vector3(xPosOffset, 1f, _potion.transform.position.z);

            _potion.SetPotionOnPlatform(position);
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
