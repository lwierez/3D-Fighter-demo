using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public GameObject player;

	protected Vector3 Offset;

    void Start()
    {
		Offset = new Vector3(0.0f, 1.6f, -3.32f);
    }

    void Update()
    {
		transform.position = player.transform.position + Offset;
    }
}
