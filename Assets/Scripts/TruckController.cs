using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour, IInteractable
{


	[SerializeField]
	private AudioSource motor;
	[SerializeField]
	private AudioSource claxon;
	[SerializeField]
	private AudioSource run;

	[SerializeField]
	private float[] angryTime;

	[SerializeField]
	private float[] levelTime;

	private float nextClaxonTime;
	private float nextLevelTime;

	private Animator anim;
	private Transform playerHands;

	private float totalHoney = 0;
	private int totalBottles = 0;

	private bool end = false;
	//vars
	int i;
	Transform child;

	int angryLevel = 0;
	private void Awake()
	{
		anim = GetComponent<Animator>();
		angryLevel = 0;
		nextClaxonTime = Time.time + angryTime[angryLevel];
		nextLevelTime = Time.time + levelTime[angryLevel];
	}

	private void Update()
	{
		if(!end)
		{
			if (Time.time >= nextClaxonTime && angryLevel > 0)
			{
				claxon.Play();
				nextClaxonTime = Time.time + angryTime[angryLevel - 1];
			}
			if (Time.time >= nextLevelTime)
			{
				if (angryLevel < angryTime.Length)
				{
					angryLevel++;
					nextClaxonTime = Time.time + angryTime[angryLevel - 1];
					print("NEXT LEVEL");
				}
				nextLevelTime = Time.time + levelTime[angryLevel];
			}
		}
	}
	public void Interact(PlayerInteraction player)
	{
		totalHoney = 0;
		totalBottles = 0;

		playerHands = player.GetHands();
		for(i = 0;i<playerHands.childCount;i++)
		{
			if (playerHands.GetChild(i).CompareTag("Honey"))
			{
				child = playerHands.GetChild(i);
				if(player.hasHoney())
				{
					totalBottles++;
					totalHoney += child.GetComponent<HoneyController>().amount;
					Destroy(child.gameObject);
					player.DeliverHoney();
				}
			}
		}
		GameManager.Instance.DeliverHoney(totalHoney);
		GameManager.Instance.RecycleBottle(totalBottles);
	}

	public void End()
	{
		anim.SetTrigger("arranca");
		motor.Stop();
		run.Play();
		end = true;
	}

	public bool ShowUI(PlayerInteraction player)
	{
		return player.hasHoney();
	}
}
