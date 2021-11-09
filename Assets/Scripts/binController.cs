using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class binController : MonoBehaviour ,IInteractable
{
	private Transform playerHands;

	private Animator anim;
	private AudioSource audio;

	private int totalBottles;
	[SerializeField]
	private GameObject helpCanvas;

	//vars
	int i;
	Transform child;

	private void Awake()
	{
		anim = GetComponent<Animator>();
		audio = GetComponent<AudioSource>();
	}
	public void Interact(PlayerInteraction player)
	{
		totalBottles = 0;
		playerHands = player.GetHands();
		if (player.hasEmptyBottle())
		{
			audio.Play();
			anim.SetTrigger("close");
			helpCanvas.SetActive(false);
			StartCoroutine(ShowHelp());

			for (i = 0; i < playerHands.childCount; i++)
			{
				if (playerHands.GetChild(i).CompareTag("Honey"))
				{
					child = playerHands.GetChild(i);

					totalBottles++;
					Destroy(child.gameObject);
					player.DeliverHoney();

				}
			}
		}
		GameManager.Instance.RecycleBottle(totalBottles);
	}

	IEnumerator ShowHelp()
	{
		yield return new WaitForSeconds(1);
		helpCanvas.SetActive(true);
	}

	public bool ShowUI(PlayerInteraction player)
	{
		return player.hasEmptyBottle();
	}
}
