using UnityEngine;
using System.Collections;

public class HealthManager : MonoBehaviour {

    SpriteRenderer sp;
    Animation anim;

    void Start()
    {
        sp = GetComponent<SpriteRenderer>();

        anim = GetComponent<Animation>();

        anim.wrapMode = WrapMode.Once;
    }

	public void AnimateColor(int HP)
    {
        float targetColor = ((float)HP - 0f) / (10f - 0f);
        float Green = targetColor * 255f;
        float Blue = targetColor * 255f;

        float Green01 = (Green - 0f) / (255f - 0f);
        float Blue01 = (Blue - 0f) / (255f - 0f);

        sp.color = new Color(255f, Green01, Blue01);

        // play animation
        anim.CrossFade("scale1");
    }
}
