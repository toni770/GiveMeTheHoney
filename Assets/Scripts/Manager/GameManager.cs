using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private float startHoney = 50;
	[SerializeField]
	private int startBottles = 3;
	[SerializeField]
	private float honeyToDeliver = 30;

	public HiveController hive;
	public TruckController truck;

	private bool paused = false;

	private float currentHoney;
	private float deliveredHoney;
	public int currentBottles;

	public bool playing { get; private set; } = false;
	private UIManager uiManager;

	private AudioSource audio;

	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (_instance == null)
				Debug.LogError("GameManager is NULL");

			return _instance;
		}
	}

	private void Awake()
	{
		_instance = this;
		audio = GetComponent < AudioSource>();
		uiManager = GetComponent<UIManager>();
		Time.timeScale = 1;
	}
	private void Start()
	{
		StartGame();
	}

	private void StartGame()
	{
		playing = false;
		hive.RefillBottles(currentBottles, startBottles);

		deliveredHoney = 0;
		currentHoney = startHoney;
		currentBottles = startBottles;

		paused = false;

		uiManager.UpdateActualHoney(currentHoney, startHoney);
		uiManager.UpdateHoneyToDeliver(deliveredHoney, honeyToDeliver);
		uiManager.OpenTutorial(true);
	}

	public void StartMusic()
	{
		audio.Play();
		playing = true;
	}

	private void EndGame(bool win)
	{
		truck.End();
		StartCoroutine(finishGame(win));
	}

	IEnumerator finishGame(bool win)
	{
		yield return new WaitForSeconds(4.5f);
		uiManager.EndGame(win);
	}
	public void DeliverHoney(float amount)
	{
		deliveredHoney += amount;
		uiManager.UpdateHoneyToDeliver(deliveredHoney, honeyToDeliver);
		CheckEnd();

	}

	public void PauseGame()
	{
		if(paused)
		{
			Time.timeScale= 1;
			audio.Play();
		}
		else
		{
			Time.timeScale = 0;
			audio.Pause();
		}
		uiManager.Pausa();

		paused = !paused;
	}

	public void RecycleBottle(int num)
	{
		hive.RefillBottles(currentBottles, currentBottles+num);
		currentBottles += num;
	}

	public void CheckEnd()
	{
		if (deliveredHoney >= honeyToDeliver)
		{
			EndGame(true);
		}
		else if (currentHoney <= 0)
		{
			EndGame(false);
		}
	}

	public void GetHoney(float amount)
	{
		currentHoney -= amount;
		hive.UseBottle(currentBottles);
		currentBottles--;
		uiManager.UpdateActualHoney(currentHoney, startHoney);
	}

	public bool IsEnoughHoney(float amount)
	{
		return currentHoney >= amount;
	}
	public bool IsEnoughBottles()
	{
		return currentBottles > 0;
	}

}
