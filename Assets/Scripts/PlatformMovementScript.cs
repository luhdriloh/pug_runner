using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementScript : MonoBehaviour
{
	private void Start ()
    {
	}
	
	private void Update ()
    {
        //GameValues._gamevalues._gameMoveSpeed += Time.deltaTime * .1f;
        transform.position -= (Vector3.right * GameValues._gamevalues._gameMoveSpeed * Time.deltaTime);
	}
}
