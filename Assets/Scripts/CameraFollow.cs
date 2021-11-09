using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

	[SerializeField]
	private Transform target;
	[SerializeField]
	private float smoothSpeed;
	[SerializeField]
	private Vector3 offset;

	private Vector3 velocity = Vector3.zero;


	private void Update()
    {
		//transform.position = Vector3.Lerp(transform.position + offset, target.position, smoothSpeed);
		transform.position = Vector3.SmoothDamp(transform.position, target.position + offset, ref velocity, smoothSpeed);
    }
}
