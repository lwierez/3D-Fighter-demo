using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLauncherUnit : MonoBehaviour
{
	// Controller object of this script
	public MissileLauncherController missileLauncherController;
	// Linked missile
	public GameObject Missile;

	// Delay since the last shot
	protected float delayCount = 0.0f;
	// Is the launcher ready to load a new missile
	protected bool isReloadReady;

	void Update()
	{
		try
		{
			isReloadReady = delayCount > missileLauncherController.getMissileDelay();
		}
		catch (System.NullReferenceException)
		{
			isReloadReady = false;
			throw;
		}

		// Handling missile shooting and reloading
		if (!isReloadReady)
		{
			delayCount += Time.deltaTime;
		}
	}

	public bool IsReloadReady()
	{
		return this.isReloadReady;
	}

	public void resetDelayCount()
	{
		this.isReloadReady = false;
		this.delayCount = 0;
	}
}
