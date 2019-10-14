using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour
{
    void FixedUpdate()
    {
		transform.Translate(Vector3.forward * Time.deltaTime * 15);
    }
}
