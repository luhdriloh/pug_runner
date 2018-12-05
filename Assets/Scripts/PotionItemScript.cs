using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionItemScript : MonoBehaviour 
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
            _inUse = false;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameController._gameController.SpeedUp();
            _inUse = false;
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
