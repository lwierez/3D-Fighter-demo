using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
	// Filetime of the missile
	protected const float lifetime = 120;
	// Object locked by the missile
	protected GameObject lockedObject;

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
		// Make the missile follow the target
		transform.LookAt(lockedObject.transform);
		// Make the missile forward
		transform.Translate(Vector3.forward * 50 * Time.deltaTime);
	}

	/// <summary>
	///  Make the missile lock an object
	/// </summary>
	/// <param name="lockedObject">GameObject that the missile will try to hit</param>
	public void LockTo(GameObject lockedObject)
	{
		this.lockedObject = lockedObject;
	}
}
