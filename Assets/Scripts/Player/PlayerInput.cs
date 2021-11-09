using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{

	public float horizontal { get; private set; }
	public float vertical { get; private set; }

	public bool isRunning { get; private set; }

	public event Action interact = delegate { };

	public event Action<bool> StartRunning = delegate { };


	private void Update()
    {
		if(GameManager.Instance.playing)
			GetInput();
    }

	private void GetInput()
	{
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");

		if (Input.GetButtonDown("Run"))
		{
			if (StartRunning != null)
				StartRunning(true);

			isRunning = true;
		}

		if (Input.GetButtonUp("Run"))
		{
			if (StartRunning != null)
				StartRunning(false);

			isRunning = false;
		}

		if (Input.GetButtonDown("Interact"))
		{
			if (interact != null)
				interact();

		}

		if(Input.GetButtonDown("Pause"))
		{
			GameManager.Instance.PauseGame();
		}
	}

	public bool PlayerIsMoving()
	{
		return vertical != 0 || horizontal != 0;
	}
}
