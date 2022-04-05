using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level1 : MonoBehaviour
{
    [Header("First Anim")]

    [SerializeField]
    AnimFrames animStartCat;

    [SerializeField]
    AnimFrames animStartBird;

    const float firstRate = 5;

    const int startFrame = 0;
    const int endFrame = 35;
    const int frozeFrame = 17;  
    
    const int frozeFrameBird = 0;

    const float firstAnimTime = 7.5f;

    [Header("Second Anim")]


    ToggleHUD toggleHUD;


    private void Start()
    {
        toggleHUD = GameManager.instance.toggleHUD;

        PlayStartAnim();
    }

    IEnumerator ResumeHUDInSec(float waitTime)
    {
        yield return new WaitForSecondsRealtime(waitTime);

        toggleHUD.ShowHUD();
    }

    IEnumerator SetAnimToIndex(AnimFrames anim, int index, float waitSec)
    {
        yield return new WaitForSecondsRealtime(waitSec);

        anim.GetComponent<Image>().sprite = anim.sprites[index];
    }

    void PlayStartAnim()
    {       
        toggleHUD.HideHUD();

        animStartCat.PlayOnce(startFrame, endFrame,firstRate);
        animStartBird.PlayOnce(startFrame, endFrame, firstRate);
        
        StartCoroutine(ResumeHUDInSec(firstAnimTime));
        StartCoroutine(SetAnimToIndex(animStartCat, frozeFrame, firstAnimTime));
        StartCoroutine(SetAnimToIndex(animStartBird, frozeFrameBird, firstAnimTime));
    }


}
