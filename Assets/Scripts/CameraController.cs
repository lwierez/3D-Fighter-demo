using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject player;

	protected Vector3 Offset;

    void Start()
    {
		Offset = new Vector3(0.0f, 1.0f, -3.5f);

		//Vector3 rotatedVector3 = Quaternion.AngleAxis(45, Vector3.forward) * new Vector3(1, 1, 1);
		//Debug.Log(rotatedVector3);
    }

    void Update()
    {
		Vector3 rotatedOffset = Quaternion.AngleAxis(player.transform.eulerAngles.y, Vector3.up) * Offset;
		transform.position = player.transform.position + rotatedOffset;

		Vector3 positionDifference = player.transform.position + Vector3.up - transform.position;
		transform.forward = positionDifference.normalized;
	}
}
