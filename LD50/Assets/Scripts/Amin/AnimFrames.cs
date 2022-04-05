using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AnimFrames : MonoBehaviour
{
    public Sprite[] sprites;

    const float defaultFrameRate = 5;
    float interval;

    Image image;



    private void Awake()
    {
        image = GetComponent<Image>();

        interval = 1 / defaultFrameRate;
    }

    public void PlayOnce(int startFrame, int endFrame, float frameRate)
    {
        interval = 1 / frameRate;

        StartCoroutine(UpdateFrame(interval, startFrame, endFrame, false));
    }

    public void AlwaysPlay(int startFrame, int endFrame, float frameRate)
    {
        interval = 1 / frameRate;

        StartCoroutine(UpdateFrame(interval, startFrame, endFrame, true));
    }

    public void StopPlay()
    {
        StopAllCoroutines();
    }


    IEnumerator UpdateFrame(float interval, int startFrame, int endFrame, bool alwaysAnim)
    {
        int index = startFrame;
        int endIndex = endFrame;

        if (index >= sprites.Length)
            yield break;

        if (endIndex >= sprites.Length)
            endIndex = sprites.Length - 1;

        while (true)
        {
            image.sprite = sprites[index];
            index++;

            if (index == endIndex - 1)
            {
                if (alwaysAnim)
                    index = startFrame;
                else
                    yield break;                    
            }
            
            yield return new WaitForSecondsRealtime(interval);         
        }
    }

}
