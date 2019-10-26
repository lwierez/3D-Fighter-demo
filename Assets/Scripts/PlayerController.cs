using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
	// Speed of the plane
	public float speed = 20.0f;
	// Rotation speed when pulling nose up or down
	public float rotationSpeedVertical = 45.0f;
	// Rotation speed when inclining the plane
	public float rotationSpeedHorizontal = 90.0f;

	// Input of the horizontal axis
	protected float horizontalInput;
	// Input of the vertical axis
	protected float verticalInput;

    void FixedUpdate()
    {
		// Capturing player inputs
		horizontalInput = Input.GetAxis("Horizontal");
		verticalInput = Input.GetAxis("Vertical");

		// Moving and rotating the plane with inputs values
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
		transform.Rotate(Vector3.right, rotationSpeedVertical * Time.deltaTime * verticalInput);
		transform.Rotate(Vector3.back, rotationSpeedHorizontal * Time.deltaTime * horizontalInput);
	}

	void OnCollisionEnter(Collision other)
	{
		var emission = GetComponent<ParticleSystem>().emission;
		emission.enabled = true;
		StartCoroutine("KillPlayer");
	}

	IEnumerator KillPlayer()
	{
		yield return new WaitForSeconds(5.0f);
		SceneManager.LoadScene(0);
	}
}