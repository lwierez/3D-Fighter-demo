using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
	public Quaternion newRotation;

    void Update()
    {
		transform.rotation = newRotation;
		transform.Translate(Vector3.forward * 25 * Time.deltaTime);
	}
}
