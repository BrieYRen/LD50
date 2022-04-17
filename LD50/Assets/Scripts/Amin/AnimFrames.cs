using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class AnimFrames : MonoBehaviour
{
    [Tooltip("Drag and drop the to-play anim frames here")]
    public Sprite[] sprites;

    const float defaultFrameRate = 5;
    float interval;

    Image image;


    private void Awake()
    {
        image = GetComponent<Image>();

        interval = 1 / defaultFrameRate;
    }

    /// <summary>
    /// public method to play the anim from start frame to end frame at frame rate for once
    /// </summary>
    /// <param name="startFrame"></param>
    /// <param name="endFrame"></param>
    /// <param name="frameRate"></param>
    public void PlayOnce(int startFrame, int endFrame, float frameRate)
    {
        interval = 1 / frameRate;

        StartCoroutine(UpdateFrame(interval, startFrame, endFrame, false));
    }

    /// <summary>
    /// public method to play the anim from start frame to end frame at frame rate looply
    /// </summary>
    /// <param name="startFrame"></param>
    /// <param name="endFrame"></param>
    /// <param name="frameRate"></param>
    public void AlwaysPlay(int startFrame, int endFrame, float frameRate)
    {
        interval = 1 / frameRate;

        StartCoroutine(UpdateFrame(interval, startFrame, endFrame, true));
    }

    /// <summary>
    /// public method to stop this anim
    /// </summary>
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

            if (index == endIndex)
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
