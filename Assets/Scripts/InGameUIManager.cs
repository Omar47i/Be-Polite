using UnityEngine;
using System.Collections;

public class InGameUIManager : MonoBehaviour {

	public void OnReturn()
	{
		Application.LoadLevel("MainMenu");

		Screen.orientation = ScreenOrientation.Portrait;
	}
}
