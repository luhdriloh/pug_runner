using UnityEngine;

public class CoinItemScript : MonoBehaviour 
{
    public bool _inUse = false;
    public float _speedUp;

    public void SetPotionOnPlatform(Vector3 position)
    {
        position.z = -1;
        gameObject.SetActive(true);
        transform.position = position;
    }

    private void Update()
    {
        if (OutOfBounds())
        {
            GameController._gameController.SpeedDown();
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            CoinParticleSpawn._coinSpawnInstance.Spawn(transform.position);
            GameController._gameController.SpeedUp();
            gameObject.SetActive(false);
        }
    }

    public bool OutOfBounds()
    {
        return transform.position.x < -10;
    }

    private void OnEnable()
    {
        _inUse = true;
    }

    private void OnDisable()
    {
        _inUse = false;
    }
}
