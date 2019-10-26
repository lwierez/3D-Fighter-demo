using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
	float timer = 0;

    void Update()
    {
		timer += Time.deltaTime;
		transform.Translate(Vector3.forward * Time.deltaTime * 50);

		if (timer > 10)
		{
			Destroy(gameObject);
		}
    }
}
