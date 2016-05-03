using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

    public GameObject bubble;            // Saying Bubble go
   
    private Transform myTrans;           // player's transform component
    private UIUtil UIUtil_script;        // reference to the main Canvas UI utility script
    private bool active = false;         // If true, The bubble will be rendered

	private AudioSource goSound;         // This sound is played when intercepting NPCs
    void Start()
    {
        myTrans = transform;

		goSound = GetComponent<AudioSource>();

        UIUtil_script = GameObject.FindGameObjectWithTag(Tags.mainCanvas).GetComponent<UIUtil>();
    }

	public void DisplayBubble(string text)
    {
        StartCoroutine(timeBubble(text));
    }

    IEnumerator timeBubble(string text)
    {
		// play go sound
		if (!goSound.isPlaying)
			goSound.Play();

        bubble.transform.GetChild(0).GetComponent<Text>().text = "I GOT\nYOU";
        bubble.SetActive(true);

        active = true;

        yield return new WaitForSeconds(.4f);

        active = false;

        bubble.SetActive(false);
    }

    void Update()
    {
        if (active)
        {
            bubble.GetComponent<RectTransform>().anchoredPosition = UIUtil_script.WorldToCanvasPoint(Camera.main, myTrans.position + (new Vector3(.5f, .5f, 0f)));
        }
    }
}
