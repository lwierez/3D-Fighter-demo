using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
	// Filetime of the missile
	protected const float lifetime = 10;

	// Time since the missile lives
	protected float timer = 0;

    void Update()
    {
		// Destroy the missile when it lifetime is over
		timer += Time.deltaTime;
		if (timer > lifetime)
		{
			Destroy(gameObject);
		}
	}

	void FixedUpdate()
	{
		// Make the missile forward
		transform.Translate(Vector3.forward * 25 * Time.deltaTime);
	}
}
