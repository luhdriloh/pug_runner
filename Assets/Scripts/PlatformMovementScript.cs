using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovementScript : MonoBehaviour
{
	private void Update ()
    {
        transform.position -= (Vector3.right * GameController._gameController._gameMoveSpeed * Time.deltaTime);
	}
}
