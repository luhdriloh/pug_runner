using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public GameObject _platformPrototype;
    public int _platformPoolAmount;

    private List<GameObject> _platformPool;
    private List<Vector3> _spawnPositions;

	private void Start ()
    {
        _spawnPositions = new List<Vector3>();
        _platformPool = new List<GameObject>();

        for (int i = 0; i < _platformPoolAmount; i++)
        {
            _platformPool.Add(Instantiate(_platformPrototype, transform.position, Quaternion.identity));
            _platformPool[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            _spawnPositions.Add(transform.GetChild(i).position);
        }

        CreateTunnel();
	}
	
	private void Update ()
    {
		
	}

    private void CreateTunnel()
    {
        int spawnPointIndex = Random.Range(0, _spawnPositions.Count);

        foreach (GameObject platform in _platformPool)
        {
            Platform platformScript = platform.GetComponent<Platform>();
            if (!platformScript._inPlay)
            {
                platformScript.PutPlatformIntoPlay(_spawnPositions[spawnPointIndex]);
                break;
            }
        }

        // this should be determined by the speed of the platforms
        Invoke("CreateTunnel", 1f);
    }
}
