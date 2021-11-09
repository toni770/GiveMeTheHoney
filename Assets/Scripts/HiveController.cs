using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveController : MonoBehaviour, IInteractable
{
	public Transform[] bottlePosition;
	public List<GameObject> bottles;

	public GameObject honeyContainer;
	public GameObject emptyBottle;

	float amount;
	GameObject prefab;

	private void Awake()
	{
		amount = honeyContainer.GetComponent<HoneyController>().amount;
		bottles = new List<GameObject>();
	}
	public void Interact(PlayerInteraction player)
	{
		if (GameManager.Instance.IsEnoughHoney(amount) && GameManager.Instance.IsEnoughBottles())
		{
			if(player.GetHoney(honeyContainer))
				GameManager.Instance.GetHoney(amount);
		}
	}

	public void RefillBottles(int actual, int max)
	{
		print("REFILLING: ACTUAL: " + actual + ", NEWS: " + max);
		for(int i=actual;i<max;i++)
		{
			prefab = Instantiate(emptyBottle, bottlePosition[i].position, Quaternion.identity, transform);
			bottles.Add(prefab);
		}
	}

	public void UseBottle(int bottle)
	{
		Destroy(bottles[bottle-1]);
		bottles.RemoveAt(bottle - 1);
		print(bottles.Count);
	}

	public bool ShowUI(PlayerInteraction player)
	{
		return GameManager.Instance.IsEnoughHoney(amount) && GameManager.Instance.IsEnoughBottles() && player.CanGetHoney();
	}
}
