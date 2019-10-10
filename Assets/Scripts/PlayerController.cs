using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float speed = 20.0f;
	public float rotationSpeedVertical = 45.0f;
	public float rotationSpeedHorizontal = 90.0f;

	protected float horizontalInput;
	protected float verticalInput;

	void Start()
    {
        
    }

    void FixedUpdate()
    {
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");

		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		transform.Rotate(Vector3.right, rotationSpeedVertical * Time.deltaTime * verticalInput);
		transform.Rotate(Vector3.back, rotationSpeedHorizontal * Time.deltaTime * horizontalInput);
    }
}