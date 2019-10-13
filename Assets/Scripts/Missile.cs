using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    void Update()
    {
		transform.Translate(Vector3.forward * 25 * Time.deltaTime);
    }
}
