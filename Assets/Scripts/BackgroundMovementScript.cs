using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovementScript : MonoBehaviour
{
    public Vector3 _movementSpeed;

	private void Start () {
		
	}
	
	private void Update ()
    {
        transform.position += (_movementSpeed * Time.deltaTime);
	}
}
