using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : MonoBehaviour
{
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
		if(!playerDetection.PlayerInRange())
		{
			stateMachine.NewState(stateMachine.surprisedState);
			return;
		}
		navMeshController.UpdateNextWayPoint();
	}

	private void OnEnable()
	{
		stateMachine.aud.Play();
	}


}
