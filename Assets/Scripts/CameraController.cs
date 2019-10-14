using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	// Object followed by the camera
	public GameObject player;

	// Vector between the camera and the followed object
	protected Vector3 Offset;

    void Start()
    {
		Offset = new Vector3(0.0f, 1.0f, -3.5f);
    }

    void Update()
    {
		// Placing the camera behind the player
		transform.position = player.transform.position + Quaternion.AngleAxis(player.transform.eulerAngles.y, Vector3.up) * Offset;
		// Pointing the camera on the player
		transform.forward = (player.transform.position + Vector3.up - transform.position).normalized;
	}
}
