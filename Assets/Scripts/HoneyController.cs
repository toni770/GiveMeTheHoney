using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HoneyController : MonoBehaviour, IInteractable
{
	[SerializeField]
	private float initialAmount = 10;
	[SerializeField]
	private float discountRatio = 2;
	[SerializeField]
	private Renderer honeyRend;

	public float amount = 20;

	private bool fell = false;

	private PlayerMovement playerMovement;
	private PlayerInteraction playerInteraction;
	private PlayerInput playerInput;

	private Rigidbody rb;
	private SphereCollider col;
	private BoxCollider boxCol;
	private AudioSource audio;

	[SerializeField]
	private ParticleSystem ps;

	private void Awake()
	{
		rb = GetComponent < Rigidbody>();
		col = GetComponent<SphereCollider>();
		boxCol = GetComponent<BoxCollider>();
		audio = GetComponent < AudioSource>();

		audio.Play();

		amount = initialAmount;
		honeyRend.material.SetFloat("_Fill", amount / 10);
	}
	private void Update()
	{
		if(fell && amount > 0)
		{
			discountHoney();
		}
	}

	public void Interact(PlayerInteraction player)
	{
		if(amount <= 0 && fell)
		{

			if (player.GetHoney(gameObject))
			{
				audio.Play();
				Fall(false);
			}
		}
	}

	public void SetPlayer(GameObject player)
	{
		playerMovement = player.GetComponent<PlayerMovement>();
		playerMovement.Run += WasteHoney;

		playerInteraction = player.GetComponent<PlayerInteraction>();
		playerInteraction.HoneyAway += LoseHoney;

		playerInput = player.GetComponent<PlayerInput>();
		playerInput.StartRunning += StartParticles;
	}
	void LoseHoney()
	{
		playerMovement.gameObject.GetComponent<PlayerInteraction>().DeliverHoney();
		Fall(true);
	}

	void StartParticles(bool start)
	{
		if (start)
			ps.Play();
		else
			ps.Stop();
	}

	void WasteHoney()
	{
		discountHoney();

		if (amount <= 0)
		{
			LoseHoney();
		}
	}

	private void OnDestroy()
	{
		disconectPlayerActions();
	}

	private void Fall(bool fall)
	{
		if(fall)
		{
			disconectPlayerActions();
			transform.parent = null;
			ps.Stop();
			if(amount == 0)
				honeyRend.gameObject.SetActive(false);
		}

		rb.isKinematic = !fall;
		col.enabled = fall;
		boxCol.enabled = fall;
		fell = fall;

	}

	private void discountHoney()
	{
		amount -= discountRatio * Time.deltaTime;
		honeyRend.material.SetFloat("_Fill", amount / 10);
		if(amount <= 0)
			honeyRend.gameObject.SetActive(false);
	}

	private void disconectPlayerActions()
	{
		if (playerMovement != null)
			playerMovement.Run -= WasteHoney;

		if (playerInteraction != null)
			playerInteraction.HoneyAway -= LoseHoney;

		if (playerInteraction != null)
			playerInput.StartRunning -= StartParticles;
	}

	public bool ShowUI(PlayerInteraction player)
	{
		return amount <= 0 && fell;
	}

}
