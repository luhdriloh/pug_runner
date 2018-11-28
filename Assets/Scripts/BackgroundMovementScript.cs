using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovementScript : MonoBehaviour
{
	private void Start ()
    {
		
	}
	
	private void Update ()
    {
        transform.position -= (Vector3.right * GameValues._gameMoveSpeed * Time.deltaTime);
	}
}
