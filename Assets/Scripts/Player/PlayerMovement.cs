using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField]
	private float speed = 5;
	[SerializeField]
	private float runExtraSpeed = 2;
	[SerializeField]
	private float honeyWeight = 0.2f;
	[SerializeField]
	private float rotSmooth = 5;

	private Vector3 movement;

	PlayerInput playerInput;
	PlayerInteraction playerInteraction;

	//vars
	float heading;
	Quaternion desiredRotQ;

	public event Action Run = delegate { };

	private Rigidbody rb;
	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		playerInteraction = GetComponent<PlayerInteraction>();
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
    {
		Move();
    }

	private void Move()
	{
		movement.Set(playerInput.horizontal, 0f, (playerInput.vertical));

		//Normal Movement
		movement = movement.normalized * speed * Time.deltaTime;

		//Add running
		if (playerInput.isRunning && playerInput.PlayerIsMoving())
		{
			movement *= runExtraSpeed;
			if (Run != null)
				Run();
		}

		//Add honey weight
		if(playerInteraction.hasHoney())
		for(int i =0;i<playerInteraction.currentContainer;i++)
			movement *= (1 - honeyWeight);


		transform.Translate(movement, Space.World);


		Rotate(playerInput.vertical, -playerInput.horizontal);

	}

	private void Rotate(float h, float v)
	{
		heading = Mathf.Atan2(h, v);
		if (h != 0 || v != 0)
		{
			//transform.localRotation = Quaternion.Euler(0, heading * Mathf.Rad2Deg, 0);
			desiredRotQ = Quaternion.Euler(0, heading * Mathf.Rad2Deg, 0);

			transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * rotSmooth);
		}
	}

	void OnCollisionEnter(Collision collision)
	{
		rb.velocity = Vector3.zero;
	}
}
