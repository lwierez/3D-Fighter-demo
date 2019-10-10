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
    }

    void Update()
    {
		transform.position = player.transform.position + Offset;

		Vector3 positionDifference = player.transform.position + Vector3.up - transform.position;
		transform.forward = positionDifference.normalized;
	}
}
