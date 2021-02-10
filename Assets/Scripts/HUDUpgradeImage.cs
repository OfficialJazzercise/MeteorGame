using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDUpgradeImage : MonoBehaviour
{
    public Sprite[] upgradeImages;
    public Image imageSwap;


    private void OnEnable()
    {
        GBullet.BigBulletPowerUp += changeImage;
        Firing.GiantBulletEnded += DefaultIcon;
    }
    private void OnDisable()
    {
        GBullet.BigBulletPowerUp -= changeImage;
        Firing.GiantBulletEnded -= DefaultIcon;
    }

    private void changeImage()
    {
        imageSwap.sprite = upgradeImages[1];
    }
    private void DefaultIcon()
    {
        imageSwap.sprite = upgradeImages[0];
    }
    // Start is called before the first frame update
    void Start()
    {
        DefaultIcon();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
