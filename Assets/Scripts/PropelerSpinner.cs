using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropelerSpinner : MonoBehaviour
{
    void Update()
    {
		transform.Rotate(Vector3.forward, 360 * 3 * Time.deltaTime);
    }
}
