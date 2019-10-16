using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOn : MonoBehaviour
{
	// Canvas where ui object should be drown
	public GameObject Canvas;
	// Prefab of the locking sprite
	public GameObject lockUI;

	// List of lockable targets
	protected List<GameObject> lockableTargets;
	// List of sprite drown to indicate a lockable target
	protected List<GameObject> lockableTargetsUIs;
	// Current locked object
	protected GameObject lockedObject;
	// Index of the current locked object in "lockableTargets"
	protected int lockedObjectIndex;
	// Can the player lock a new target
	protected bool canLock = true;

	void Start()
	{
		// Initialize lists
		lockableTargets = new List<GameObject>();
		lockableTargetsUIs = new List<GameObject>();
		lockedObjectIndex = 0;
	}

	void Update()
	{
		// Destroy all elements on ui
		foreach (GameObject element in lockableTargetsUIs)
		{
			Destroy(element);
		}
		lockableTargetsUIs.Clear();

		// By default the locked target is considered out of the screen
		bool stillLockedObjectOnSight = false;

		// For each target that can be locked
		for (int i = 0 ; i < lockableTargets.Count ; i++)
		{
			GameObject target = lockableTargets[i];
			// We ensure the GameObject still exist
			if (target != null)
			{
				// We test if the GameObject is in direct sight
				RaycastHit raycastHit;
				Vector3 Offset = target.transform.position - GetComponentInParent<Transform>().position;
				float distanceToTarget = Offset.magnitude;
				Vector3 directionToTarget = Offset.normalized;

				// Excluding unwanted layers
				LayerMask layerMask = 1 << 5;
				layerMask |= 1 << 10;
				layerMask |= 1 << 13;
				layerMask = ~layerMask;

				bool rayColliderToTargetResult = Physics.Raycast(GetComponentInParent<Transform>().position, directionToTarget, out raycastHit, distanceToTarget, layerMask);
				if (rayColliderToTargetResult)
				{
					if (raycastHit.collider.gameObject == target)
					{
						// We create a sprite to indicate the possibilty to lock on
						GameObject newUIElement = Instantiate(lockUI, target.transform.position, Quaternion.identity, Canvas.transform);
						// If one of these objects is the already locked target we draw the prefab with a different color
						// We also update the index of the object and indicate the target still in sight
						if (target == lockedObject)
						{
							stillLockedObjectOnSight = true;
							lockedObjectIndex = lockableTargets.IndexOf(target);
							newUIElement.GetComponent<SpriteRenderer>().color = Color.red;
						}
						// Each sprite is added to the list
						lockableTargetsUIs.Add(newUIElement);
					}
				}
			}
			else
			{
				lockableTargets.Remove(target);
			}
		}

		// We the previously locked target weren't found during the collided objects scan
		// We reset the values that could reference to it
		if (!stillLockedObjectOnSight)
		{
			lockedObject = null;
			lockedObjectIndex = 0;
		}

		// The lock move is restricted so the target won't change if the player let the trigger pushed
		if (Input.GetAxis("Lock") > 0.6 && canLock)
		{
			canLock = false;
			// If there is at least a target
			if (lockableTargets.Count > 0)
			{
				// We switch to the next target
				if (lockedObjectIndex + 1 < lockableTargets.Count)
				{
					lockedObjectIndex++;
				}
				// If there is only one target or if we will hit an undefined reference we select the first object
				else
				{
					lockedObjectIndex = 0;
				}
				lockedObject = lockableTargets[lockedObjectIndex];
			}
		}
		if (Input.GetAxis("Lock") < 0.1)
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

	/// <summary>
	/// Get the object that is currently locked by the player.
	/// Will create error we none is locked.
	/// </summary>
	/// <returns>The object locked by the player</returns>
	public GameObject getLockedTarget()
	{
		return this.lockedObject;
	}

	/// <summary>
	/// Tell if there is actually a target locked by player.
	/// </summary>
	/// <returns>true if a gameobject is locked</returns>
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
