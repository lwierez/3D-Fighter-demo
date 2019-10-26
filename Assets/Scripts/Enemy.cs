using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public Transform playerTransform;
	public GameObject bulletPrefab;

	protected float timeSinceLastShot = 0;
	protected float coolDown = 1 / 20;

    void FixedUpdate()
    {
		float distanceToPlayer = (playerTransform.position - transform.position).magnitude;

		if (timeSinceLastShot > coolDown)
		{
			if (distanceToPlayer < 30)
			{
				shoot();
			}
		}
		else
		{
			timeSinceLastShot += Time.deltaTime;
		}

		transform.forward = (playerTransform.position - transform.position).normalized;
		transform.Translate(Vector3.forward * Time.deltaTime * 15);
    }

	void shoot()
	{
		timeSinceLastShot = 0;
		GameObject newBulletRight = Instantiate(bulletPrefab, transform.position + transform.right * 0.1f, transform.rotation);
		GameObject newBulletLeft = Instantiate(bulletPrefab, transform.position + -transform.right * 0.1f, transform.rotation);
	}
}