using UnityEngine;
using System.Collections;

public class CitizenEffects : MonoBehaviour {

    public float smoothTime = 0.3f;
    private SpriteRenderer spR;

	[HideInInspector]
    public bool start = true;

    void Start()
    {
        spR = GetComponent<SpriteRenderer>();
    }

    public IEnumerator FlickerOnSwitch()
    {
        if (start)
        {
            start = false;

            if (spR != null)
            {
                spR.color = Color.red;
            }

            yield return new WaitForSeconds(smoothTime);

            if (spR != null)
            {
                spR.color = Color.white;
            }

            start = true;
        }
    }
}
