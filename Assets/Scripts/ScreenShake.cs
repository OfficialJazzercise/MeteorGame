using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public static ScreenShake instance;

    private float shakeTimeRemaining;
    private float shakePower;
    private float shakeFadeTime;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartShake(.5f, 1f);
        }
    }

    void LateUpdate()
    {
        if(shakeTimeRemaining > 0) // may have to change either x or y to z
        {
            shakeTimeRemaining -= Time.deltaTime;

            float x = Random.Range(-1f, 1f) * shakePower;
            float y = Random.Range(-1f, 1f) * shakePower;

            transform.position += new Vector3(x, y, 0f);

            shakePower = Mathf.MoveTowards(shakePower, 0f, shakeFadeTime * Time.deltaTime);
        }
    }

    public void StartShake(float length, float power)
    {
        shakeTimeRemaining = length;
        shakePower = power;

        shakeFadeTime = power / length;
    }
}
