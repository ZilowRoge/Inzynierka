using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
	public GameObject main_menu;
	public GameObject help;
	public GameObject controls;

	public GameObject credits;
	
	public void on_new_game()
	{
		SceneManager.LoadScene("Demo_presentation_backup", LoadSceneMode.Single);
	}

	public void on_load()
	{

	}

	public void on_help()
	{
		main_menu.SetActive(false);
		help.SetActive(true);
	}

	public void on_exit()
	{
		Application.Quit();
	}

	public void on_controls()
	{
		help.SetActive(false);
		controls.SetActive(true);
	}

	public void on_credits()
	{
		help.SetActive(false);
		credits.SetActive(true);
	}

	public void on_credits_back()
	{
		credits.SetActive(false);
		help.SetActive(true);
	}

	public void on_controls_bck()
	{
		controls.SetActive(false);
		help.SetActive(true);
	}

	public void on_help_back()
	{
		help.SetActive(false);
		main_menu.SetActive(true);
	}
}
