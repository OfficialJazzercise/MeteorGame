using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerupAnimation : MonoBehaviour
{

    public GameObject ps;
private void OnEnable()
{
        GBullet.BigBulletPowerUp += startAnimation;
        Firing.GiantBulletEnded += endAnimation;
}

private void OnDisable()
{
        GBullet.BigBulletPowerUp -= startAnimation;
        Firing.GiantBulletEnded -= endAnimation;
}

private void startAnimation()
{
    ps.SetActive(true);
}

private void endAnimation()
{
    ps.SetActive(false);
}

}
