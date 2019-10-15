using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
	public GameObject Canvas;
	public GameObject lockUI;

	protected List<GameObject> lockableTargets;
	protected List<GameObject> lockableTargetsUIs;
	protected GameObject lockedObject;
	protected int lockedObjectIndex;
	protected bool canLock = true;

	void Start()
	{
		lockableTargets = new List<GameObject>();
		lockableTargetsUIs = new List<GameObject>();
		lockedObjectIndex = 0;
	}

	void Update()
	{
		foreach (GameObject element in lockableTargetsUIs)
		{
			Destroy(element);
		}
		lockableTargetsUIs.Clear();

		bool stillLockedObjectOnSight = false;

		foreach (GameObject target in lockableTargets)
		{
			GameObject newUIElement = Instantiate(lockUI, target.transform.position, Quaternion.identity, Canvas.transform);
			if (target == lockedObject)
			{
				stillLockedObjectOnSight = true;
				lockedObjectIndex = lockableTargets.IndexOf(target);
				newUIElement.GetComponent<SpriteRenderer>().color = Color.red;
			}
			lockableTargetsUIs.Add(newUIElement);
		}

		if (!stillLockedObjectOnSight)
		{
			lockedObject = null;
			lockedObjectIndex = 0;
		}

		if (Input.GetAxis("Xbox_LeftTrigger") > 0.7 && canLock)
		{
			canLock = false;
			if (lockableTargets.Count != 0)
			{
				if (lockedObjectIndex + 1 < lockableTargets.Count)
				{
					lockedObjectIndex++;
				}
				else
				{
					lockedObjectIndex = 0;
				}
			}
			lockedObject = lockableTargets[lockedObjectIndex];
		}
		if (Input.GetAxis("Xbox_LeftTrigger") < 0.1)
		{
			canLock = true;
		}
	}

	void OnTriggerEnter(Collider Other)
	{
		lockableTargets.Add(Other.gameObject);
	}

	void OnTriggerExit(Collider Other)
	{
		lockableTargets.Remove(Other.gameObject);
	}

	public GameObject getLockedTarget()
	{
		return this.lockedObject;
	}

	public bool isTargetInSight()
	{
		if (this.lockedObject != null)
		{
			return true;
		}
		else
		{
			return false;
		}
	}
}
