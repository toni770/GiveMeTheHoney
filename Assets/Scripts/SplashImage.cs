using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashImage : MonoBehaviour
{

	[SerializeField]
	private UIManager uiManager;

	[SerializeField]
	private float logoTime;

	[SerializeField]
	private float endTime;

	private float nextTime;

	private bool end;

	private Animator anim;
	// Start is called before the first frame update
	void Start()
    {
		anim = GetComponent<Animator>();
		nextTime = Time.time + logoTime;
		end = false;
    }

    // Update is called once per frame
    void Update()
    {
		if (Time.time >= nextTime)
		{
			if(!end)
			{
				anim.SetTrigger("End");
				nextTime = Time.time + endTime;
				end = true;
			}
			else
			{
				print("E_Y");
				uiManager.MainMenu();
			}

		}

		
    }
}
