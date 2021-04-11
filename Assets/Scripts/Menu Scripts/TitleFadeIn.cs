using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleFadeIn : MonoBehaviour
{
    public TextMeshProUGUI title;
    public float timer = 3f;
    private bool show = false;

    // Update is called once per frame
    void Update()
    {
        if(timer > 0 && show == false)
        {
            timer -= Time.deltaTime;
        }
        
        if(timer <= 0 && show == false)
        {
            StartCoroutine(FadeTextToFullAlpha(1f, title));
            show = true;
        }
        else if(show == true)
        {

        }
    }

    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI i)
    {
        i.color = new Color(i.color.r, i.color.g, i.color.b, 0);
        while (i.color.a < 1.0f)
        {
            i.color = new Color(i.color.r, i.color.g, i.color.b, i.color.a + (Time.deltaTime / t));
            yield return null;
        }
    }
}
