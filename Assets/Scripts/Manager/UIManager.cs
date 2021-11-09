using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
	[SerializeField]
	private Text actualHoneyText;
	[SerializeField]
	private Text honeyToDeliverText;
	[SerializeField]
	private Image honeySlider;
	[SerializeField]
	private Image honeyToDeliverSlider;

	[SerializeField]
	private GameObject pauseMenu;

	[SerializeField]
	private GameObject tutorialMenu;

	public void PlayGame()
	{
		SceneManager.LoadScene("MainScene");
	}

	public void PlayAgain()
	{
		SceneManager.LoadScene("MainScene");
	}

	public void MainMenu()
	{
		SceneManager.LoadScene("Menu");
	}

	public void ExitGame()
	{
		Application.Quit();
	}
    public void UpdateActualHoney(float amount, float max)
	{
		actualHoneyText.text = (Math.Round(amount,1)).ToString();
		honeySlider.fillAmount = amount / max;
	}
	public void UpdateHoneyToDeliver(float actual, float max)
	{
		honeyToDeliverText.text = (Math.Round(actual,1)).ToString() + '/' + (Math.Round(max,1).ToString());
		honeyToDeliverSlider.fillAmount = actual / max;
	}

	public void EndGame(bool win)
	{
		if (win)
			SceneManager.LoadScene("WinScene");
		else
			SceneManager.LoadScene("LoseScene");
	}

	public void Pausa()
	{
		pauseMenu.SetActive(!pauseMenu.activeSelf);
	}

	public void OpenTutorial(bool open)
	{
		if(!open)
		{
			GameManager.Instance.StartMusic();
			tutorialMenu.GetComponent<Animator>().SetTrigger("close");
			StartCoroutine(closeTutorial());
		}
		else
		{
			tutorialMenu.SetActive(open);
		}
	}

	IEnumerator closeTutorial()
	{
		yield return new WaitForSeconds(1);
		tutorialMenu.SetActive(false);
	}

}
