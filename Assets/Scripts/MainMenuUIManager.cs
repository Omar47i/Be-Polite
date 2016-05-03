using UnityEngine;
using System.Collections;

public class MainMenuUIManager : MonoBehaviour {

	public GameObject AboutUI;

	public void OnPlay()
	{
		Application.LoadLevel("Level_1");

		Screen.orientation = ScreenOrientation.Landscape;
	}
	
	public void OnAbout()
	{
		AboutUI.SetActive (true);
	}
	
	public void OnQuit()
	{
		Application.Quit ();
	}
	
	void Update()
	{
		if (Input.GetKeyDown (KeyCode.Escape))
		{
			Application.Quit();
		}
	}
}
