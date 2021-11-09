using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

	[SerializeField]
	private Animator anim;

	private PlayerInput playerInput;
	private PlayerInteraction playerInteraction;

	private void Awake()
	{
		playerInput = GetComponent<PlayerInput>();
		playerInteraction = GetComponent<PlayerInteraction>();
	}

	private void Update()
	{
		anim.SetBool("correr", playerInput.PlayerIsMoving());
		anim.SetBool("bote", playerInteraction.hasSomething());
	}
}
