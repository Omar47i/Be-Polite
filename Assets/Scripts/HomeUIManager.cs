using UnityEngine;
using System.Collections;

public class HomeUIManager : MonoBehaviour {

	public void OnPlay()
	{
		Application.LoadLevel("Level1");
	}

	public void OnAbout()
	{

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
