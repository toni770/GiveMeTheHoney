using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDetection : MonoBehaviour
{
	private bool playerDetected = false;
	private PlayerInteraction player;
	private void Awake()
	{
		playerDetected = false;
	}

	public bool PlayerInRange()
	{
		return playerDetected && player.hasHoney() && !player.isSafe;
	}
	private void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			playerDetected = true;
			player = other.GetComponent<PlayerInteraction>();
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			playerDetected = false;
			player = null;
		}
	}
}
