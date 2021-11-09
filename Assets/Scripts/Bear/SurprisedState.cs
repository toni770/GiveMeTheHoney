using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurprisedState : MonoBehaviour
{
	[SerializeField]
	private float surprisedTime = 3;

	private float stopSurpriseTime = 0;

	private NavMeshController navMeshController;
	private StateMachine stateMachine;
	private PlayerDetection playerDetection;

	private void Awake()
	{
		navMeshController = GetComponent<NavMeshController>();
		stateMachine = GetComponent<StateMachine>();
		playerDetection = GetComponent<PlayerDetection>();
	}

	private void Update()
	{
		if (playerDetection.PlayerInRange())
		{
			stateMachine.NewState(stateMachine.followState);
			stateMachine.anim.SetBool("Impactado", false);
			return;
		}

		if (Time.time >= stopSurpriseTime)
		{
			stateMachine.NewState(stateMachine.patrolState);
			stateMachine.anim.SetBool("Impactado", false);
		}
	}
	private void OnEnable()
	{
		stopSurpriseTime = Time.time + surprisedTime;
		navMeshController.StopAgent();
		stateMachine.anim.SetBool("Impactado",true);
	}
}
