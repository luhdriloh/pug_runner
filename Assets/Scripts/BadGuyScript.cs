using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadGuyScript : MonoBehaviour
{
    public bool _inUse = false;

    // public
    public void SetBaddyUp(Vector3 position)
    {
        position.z = -1;
        gameObject.SetActive(true);
        transform.position = position;
    }

    public void PutBaddieIntoRest()
    {
        gameObject.SetActive(false);
    }

    public bool OutOfBounds()
    {
        return transform.position.x < -10;
    }

    // private
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameController._gameController.SpeedDown();
        }
    }

    private void Update()
    {
        if (OutOfBounds())
        {
            gameObject.SetActive(false);
        }
    }

    private void Awake()
    {
        gameObject.SetActive(false);
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
