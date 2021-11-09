using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshController : MonoBehaviour
{
	public Transform target;

	private NavMeshAgent agent;

	private void Awake()
	{
		agent = GetComponent<NavMeshAgent>();
	}

	public void UpdateNextWayPoint(Vector3 wayPoint)
	{
		agent.destination = wayPoint;
		agent.isStopped = false;
	}

	public void UpdateNextWayPoint()
	{
		UpdateNextWayPoint(target.position);
	}

	public void StopAgent()
	{
		agent.isStopped = true;
	}

	public bool WayPointReached()
	{
		return agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending;
	}
}
