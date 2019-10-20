using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissileLauncherController : MonoBehaviour
{
	// Missile prefab
	public GameObject weapon;
	// CanShoot image on UI
	public Image CanShootUI;

	// Delay between each missile shot
	protected float missileDelay;
	// Script that manage the locking system
	protected LockOn lockOn;
	// List of missile launchers
	protected List<MissileLauncherUnit> missileLaunchersUnits;
	// Shooting timer
	protected float shootTimer = 0.0f;
	// Roation verifications
	bool isRotationCorrect;

	void Start()
	{
		// Initializing values for the missile reloading time
		missileDelay = 10.0f;

		lockOn = transform.parent.gameObject.GetComponentInChildren<LockOn>();
		missileLaunchersUnits = new List<MissileLauncherUnit>();

		MissileLauncherUnit[] missileLauncherUnitsArray = GetComponentsInChildren<MissileLauncherUnit>();
		foreach (MissileLauncherUnit currentMissileLauncher in missileLauncherUnitsArray)
		{
			currentMissileLauncher.Missile = Instantiate(weapon, currentMissileLauncher.gameObject.transform.position, currentMissileLauncher.gameObject.transform.rotation);
			currentMissileLauncher.missileLauncherController = GetComponent<MissileLauncherController>();
			missileLaunchersUnits.Add(currentMissileLauncher);
		}
	}

	void Update()
	{
		isRotationCorrect = transform.parent.transform.rotation.eulerAngles.x < 40 || transform.parent.transform.rotation.eulerAngles.x > 320 && transform.parent.transform.rotation.eulerAngles.z < 40 || transform.parent.transform.rotation.eulerAngles.z > 320;
		bool isReadyToShoot = shootTimer > 0.5f;

		if (!isReadyToShoot)
		{
			shootTimer += Time.deltaTime;
		}

		if (isRotationCorrect)
		{
			CanShootUI.color = Color.green;
		}
		else
		{
			CanShootUI.color = Color.red;
		}

		if (Input.GetAxis("Shoot") > 0.6 && isReadyToShoot)
		{
			shootTimer = 0.0f;
			Shoot();
		}

		foreach (MissileLauncherUnit missileLauncher in missileLaunchersUnits)
		{
			if (missileLauncher.Missile == null)
			{
				if (missileLauncher.IsReloadReady())
				{
					missileLauncher.Missile = Instantiate(weapon, missileLauncher.gameObject.transform.position, missileLauncher.gameObject.transform.rotation);
				}
			}
			else
			{
				missileLauncher.Missile.transform.position = missileLauncher.gameObject.transform.position;
				missileLauncher.Missile.transform.rotation = missileLauncher.gameObject.transform.rotation;
			}
		}
	}

	/// <summary>
	/// Make the player shoot a missile
	/// </summary>
	void Shoot()
	{
		// Instanciate and orientate the missile if a target is locked
		if (lockOn.isTargetInSight() && isRotationCorrect)
		{
			bool missileFired = false;
			foreach (MissileLauncherUnit missileLauncher in missileLaunchersUnits)
			{
				if (missileLauncher.Missile != null && !missileFired)
				{
					missileLauncher.Missile.GetComponent<Missile>().LockTo(lockOn.getLockedTarget());
					missileLauncher.Missile.GetComponent<Missile>().Launch();
					missileLauncher.resetDelayCount();
					missileLauncher.Missile = null;
					missileFired = true;
				}
			}
		}
	}

	public float getMissileDelay()
	{
		return this.missileDelay;
	}
}
