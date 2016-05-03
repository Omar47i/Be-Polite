using UnityEngine;
using System.Collections;

public class AboutUIManager : MonoBehaviour {

	public GameObject AboutUI;

	public void OnReturn()
	{
		AboutUI.SetActive (false);
	}
}
