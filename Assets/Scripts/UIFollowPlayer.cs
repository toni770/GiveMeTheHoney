using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowPlayer : MonoBehaviour
{

	[SerializeField]
	private Transform target;

	[SerializeField]
	private Vector3 offset;

    // Update is called once per frame
    void Update()
    {
		transform.position = Vector3.Lerp(transform.position, new Vector3(target.position.x, transform.position.y, target.position.z),1);
	}


}
