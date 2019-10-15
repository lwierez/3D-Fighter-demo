﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	// Missile prefab
	public GameObject weapon;
	// Speed of the plane
	public float speed = 20.0f;
	// Rotation speed when pulling nose up or down
	public float rotationSpeedVertical = 45.0f;
	// Rotation speed when inclining the plane
	public float rotationSpeedHorizontal = 90.0f;

	// Input of the horizontal axis
	protected float horizontalInput;
	// Input of the vertical axis
	protected float verticalInput;
	// Delay between each missile shot
	protected float missileDelay;
	// Delay since the last shot
	protected float delayCount;
	// Lock on script
	protected LockOn lockOn;

	void Start()
    {
		// Initializing values for the missile reloading time
		missileDelay = 2.0f;
		delayCount = 0.0f;

		lockOn = GetComponentInChildren<LockOn>();
	}

	void Update()
	{
		// Handling missile shooting and reloading
		if (delayCount < missileDelay)
		{
			delayCount += Time.deltaTime;
		}
		else if (Input.GetAxis("Fire1") > 0 || Input.GetAxis("Xbox_RightTrigger") > 0.7)
		{
			Shoot();
		}
	}

    void FixedUpdate()
    {
		// Capturing player inputs
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");

		// Moving and rotating the plane with inputs values
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		transform.Rotate(Vector3.right, rotationSpeedVertical * Time.deltaTime * verticalInput);
		transform.Rotate(Vector3.back, rotationSpeedHorizontal * Time.deltaTime * horizontalInput);
	}

	/// <summary>
	/// Make the player shoot a missile
	/// </summary>
	void Shoot()
	{
		// Reset missile reload time
		delayCount = 0;

		// Instanciate and orientate the missile if a target is locked
		if (lockOn.isTargetInSight())
		{
			GameObject newMissile = Instantiate(weapon, transform.position - transform.rotation * Vector3.up, Quaternion.identity);
			newMissile.transform.rotation = transform.rotation;
			newMissile.GetComponent<Missile>().LockTo(lockOn.getLockedTarget());
		}
	}
}