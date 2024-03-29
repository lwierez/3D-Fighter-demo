﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
	// Filetime of the missile
	protected const float lifetime = 120;
	// Object locked by the missile
	protected GameObject lockedObject;
	// Particle emitter
	public GameObject particleObject;

	// Time since the missile lives
	protected float timer = 0;
	// Array with accepted layers
	protected int[] destroyableLayers = { 11 };
	// Should the missile forward
	protected bool isLaunched = false;
	// Should fall
	protected bool isFalling = false;

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
		if (isLaunched)
		{
			// Make the missile follow the target
			if (lockedObject != null)
			{
				transform.LookAt(lockedObject.transform);
			}
			// Make the missile forward
			transform.Translate(Vector3.forward * 50 * Time.deltaTime);
		}
		if (isFalling)
		{
			transform.Translate((Vector3.down * 1 + Vector3.forward * 20) * Time.deltaTime);
		}
	}

	/// <summary>
	///  Make the missile lock an object
	/// </summary>
	/// <param name="lockedObject">GameObject that the missile will try to hit</param>
	public void LockTo(GameObject lockedObject)
	{
		this.lockedObject = lockedObject;
	}

	void OnCollisionEnter(Collision collision)
	{
		int destroyable = 0;
		int colliderLayer = collision.gameObject.layer;
		foreach (int testedLayer in destroyableLayers)
		{
			destroyable += testedLayer == colliderLayer ? 1 : 0;
		}
		if (destroyable > 0)
		{
			Destroy(collision.gameObject);
		}
		Destroy(gameObject);
	}

	/// <summary>
	/// Trigger the missile porjection
	/// </summary>
	public void Launch()
	{
		StartCoroutine("DropMissile");
	}

	IEnumerator DropMissile()
	{
		this.isFalling = true;
		yield return new WaitForSeconds(0.5f);
		this.isLaunched = true;
		this.isFalling = false;
		particleObject.SetActive(true);
	}
}
