using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatingState : MonoBehaviour
{
	[SerializeField]
	private float eatingTime = 5;

	private float stopEatingTime = 0;

	private NavMeshController navMeshController;
	private StateMachine stateMachine;

	private void Awake()
	{
		navMeshController = GetComponent<NavMeshController>();
		stateMachine = GetComponent<StateMachine>();
	}

	private void Update()
	{
		if (Time.time >= stopEatingTime)
		{
			stateMachine.NewState(stateMachine.patrolState);
			stateMachine.anim.SetBool("Comer", false);
		}
	}
	private void OnEnable()
	{
		stopEatingTime = Time.time + eatingTime;
		stateMachine.anim.SetBool("Comer", true);
		navMeshController.StopAgent();
	}
}
