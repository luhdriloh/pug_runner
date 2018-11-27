using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public void PutPlatformIntoPlay(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    public bool OutOfBounds()
    {
        return Mathf.Abs(transform.position.x) >= 16;
    }

	public void Start ()
    {
	}
	
	void Update ()
    {
        if (OutOfBounds())
        {
            gameObject.SetActive(false);
        }
	}
}
