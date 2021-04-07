using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDUpgradeImage : MonoBehaviour
{
    public Sprite[] upgradeImages;
    public Image imageSwap;

    //OnEnable activates when the specified delagate is called and preforms the function being added to the delegate
    private void OnEnable()
    {
        GBullet.BigBulletPowerUp += changeImage;
        Firing.GiantBulletEnded += DefaultIcon;
        Spawner.resetArena += DefaultIcon;
    }
    private void OnDisable()
    {
        GBullet.BigBulletPowerUp -= changeImage;
        Firing.GiantBulletEnded -= DefaultIcon;
        Spawner.resetArena -= DefaultIcon;
    }
    
    //Changes the image to the BigBullet powerup image
    private void changeImage()
    {
        FindObjectOfType<SoundManager>().Play("GetItem");//SFX
        imageSwap.sprite = upgradeImages[1];
    }

    //Changes the image to the question mark
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
