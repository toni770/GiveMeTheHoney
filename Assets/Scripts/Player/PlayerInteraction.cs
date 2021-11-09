using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteraction : MonoBehaviour
{

	private enum objectTypes {nothing, fullBottle, emptyBottle }

	[SerializeField]
	private Transform hands;
	[SerializeField]
	private int maxContainer = 3;

	[SerializeField]
	private GameObject helpCanvas;

	public int currentContainer { get; private set; } = 0;
	public bool isSafe { get; private set; } = true;
	private bool canInteract = false;
	private IInteractable interactable;

	private Transform[] honeyPosition;

	private PlayerMovement playerMovement;
	private GameObject prefab;
	private StateMachine bear;

	public event Action HoneyAway = delegate { };

	private objectTypes currentType;

	// Start is called before the first frame update
	void Awake()
    {
		GetComponent<PlayerInput>().interact += Interact;

		playerMovement = GetComponent<PlayerMovement>();

		LoadHoneyPosition();
    }

	private void Start()
	{
		currentContainer = 0;
		currentType = objectTypes.nothing;
		isSafe = true;
	}

	private void Interact()
	{
		if(canInteract)
		{
			if (interactable != null)
			{
				interactable.Interact(this);
				helpCanvas.SetActive(interactable.ShowUI(this));
			}
			
		}
	}


	private void OnTriggerEnter(Collider other)
	{
		if ((interactable = other.GetComponent<IInteractable>()) != null)
		{
			canInteract = true;
			helpCanvas.SetActive(other.GetComponent<IInteractable>().ShowUI(this));

		}
		if (other.CompareTag("Safe"))
			isSafe = true;

		if (other.CompareTag("Attack") && (currentContainer > 0 && currentType == objectTypes.fullBottle))
		{
			bear = other.transform.parent.GetComponent<StateMachine>();
			if (bear.currentState == bear.followState)
			{
				bear.NewState(bear.eatingState);
				if (HoneyAway != null)
					HoneyAway();
			}
		}
	}


	private void OnTriggerExit(Collider other)
	{
		if (other.GetComponent<IInteractable>() != null)
		{
			canInteract = false;
			interactable = null;
			helpCanvas.SetActive(false);
		}

		if (other.CompareTag("Safe"))
			isSafe = false;
	}

	private void LoadHoneyPosition()
	{
		honeyPosition = new Transform[hands.childCount];

		for(int i=0;i<hands.childCount;i++)
		{
			honeyPosition[i] = hands.GetChild(i).transform;
		}
	}

	public bool GetHoney(GameObject container)
	{
		bool isEmpty = container.GetComponent<HoneyController>().amount <= 0;
		objectTypes type = objectTypes.nothing;
		if (isEmpty)
			type = objectTypes.emptyBottle;
		else
			type = objectTypes.fullBottle;

		if (currentContainer < maxContainer && canGet(type))
		{
			if(!isEmpty)
			{
				prefab = Instantiate(container, honeyPosition[currentContainer].position, Quaternion.identity, hands);
				prefab.GetComponent<HoneyController>().SetPlayer(gameObject);
			}
			else
			{
				container.transform.position = honeyPosition[currentContainer].position;
				container.transform.parent = hands;
				container.transform.rotation = Quaternion.identity;
			}

			currentType = type;
			currentContainer++;
			return true;
		}
		else
		{
			return false;
		}
	}

	public bool CanGetHoney()
	{
		return currentContainer < maxContainer && canGet(objectTypes.fullBottle);
	}

	private bool canGet(objectTypes type)
	{
		return currentType == 0 || currentType == type;
	}

	public bool hasHoney()
	{
		return currentType == objectTypes.fullBottle;
	}

	public bool hasEmptyBottle()
	{
		return currentType == objectTypes.emptyBottle;
	}

	public bool hasSomething()
	{
		return currentType != objectTypes.nothing;
	}


	public void DeliverHoney()
	{
		currentContainer--;
		if(currentContainer == 0)
		{
			currentType = objectTypes.nothing;
			GameManager.Instance.CheckEnd();
		}
	}


	public Transform GetHands()
	{
		return hands;
	}

}
