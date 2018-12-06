using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinParticleSpawn : MonoBehaviour
{
    public static CoinParticleSpawn _coinSpawnInstance;
    private ParticleSystem _coinParticleSystem;

	private void Start ()
    {
        if (_coinSpawnInstance == null)
        {
            _coinSpawnInstance = this;
        }
        else if (_coinSpawnInstance != null)
        {
            Destroy(this);
        }

        _coinParticleSystem = GetComponent<ParticleSystem>();
    }

    public void Spawn(Vector3 location)
    {
        transform.position = location;
        _coinParticleSystem.Stop();
        _coinParticleSystem.Play();
    }
}
