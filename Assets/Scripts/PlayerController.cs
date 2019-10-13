using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public GameObject weapon;
	public float speed = 20.0f;
	public float rotationSpeedVertical = 45.0f;
	public float rotationSpeedHorizontal = 90.0f;

	protected float horizontalInput;
	protected float verticalInput;
	protected float missileDelay;
	protected float delayCount;

	void Start()
    {
		missileDelay = 2.0f;
		delayCount = 0.0f;
	}

    void FixedUpdate()
    {
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");

		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		transform.Rotate(Vector3.right, rotationSpeedVertical * Time.deltaTime * verticalInput);
		transform.Rotate(Vector3.back, rotationSpeedHorizontal * Time.deltaTime * horizontalInput);

		if (delayCount < missileDelay)
		{
			delayCount += Time.deltaTime;
		}
		else if (Input.GetAxis("Fire1") > 0)
		{
			Shoot();
		}
	}

	void Shoot()
	{
		delayCount = 0;
		Instantiate(weapon, transform.position - Vector3.up, Quaternion.identity);
	}
}