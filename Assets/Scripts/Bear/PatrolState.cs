using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : MonoBehaviour
{
	[SerializeField]
	private Transform[] wayPoints;

	private NavMeshController navMeshController;
	private PlayerDetection playerDetection;
	private StateMachine stateMachine;

	private int nextWayPoint;

	private void Awake()
	{
		navMeshController = GetComponent<NavMeshController>();
		stateMachine = GetComponent<StateMachine>();
		playerDetection = GetComponent<PlayerDetection>();
	}

	private void OnEnable()
	{
		UpdateWayPoint();
	}

	private void Update()
	{
		if(playerDetection.PlayerInRange())
		{
			stateMachine.NewState(stateMachine.followState);
			return;
		}
		if(navMeshController.WayPointReached())
		{
			nextWayPoint = (nextWayPoint + 1) % wayPoints.Length;
			UpdateWayPoint();
		}
	}

	private void UpdateWayPoint()
	{
		navMeshController.UpdateNextWayPoint(wayPoints[nextWayPoint].position);
	}
}
