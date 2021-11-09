using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{

	public MonoBehaviour patrolState;

	public MonoBehaviour followState;

	public MonoBehaviour surprisedState;

	public MonoBehaviour eatingState;

	public MonoBehaviour currentState { get; private set; }

	public Animator anim;

	public AudioSource aud;



	private void Start()
	{
		currentState = patrolState;
		currentState.enabled = true;
	}
	public void NewState(MonoBehaviour state)
	{
		if (currentState != null)
			currentState.enabled = false;

		currentState = state;
		currentState.enabled = true;
	}
}
