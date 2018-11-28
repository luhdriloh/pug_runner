using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public bool _inPlay;
    private float _yHalfSize;

    public void PutPlatformIntoPlay(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position - (Vector3.up * _yHalfSize);
        _inPlay = true;
    }

    public bool OutOfBounds()
    {
        return transform.position.x < -16;
    }

    private void Awake()
    {
        _yHalfSize = (GetComponent<SpriteRenderer>().size.y / 2) * transform.localScale.y;
    }

    private void Update ()
    {
        if (OutOfBounds())
        {
            _inPlay = false;
            gameObject.SetActive(false);
        }
	}
}
