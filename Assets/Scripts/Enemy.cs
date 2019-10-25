using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Transform playerTransform;

    void FixedUpdate()
    {
		transform.forward = (playerTransform.position - transform.position).normalized;
		transform.Translate(Vector3.forward * Time.deltaTime * 15);
    }
}
