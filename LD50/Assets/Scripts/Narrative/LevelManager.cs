using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public int currentLevel = 0;

    [SerializeField]
    Button playAnimButton;

    SceneLoader sceneLoader;


    private void Start()
    {
        sceneLoader = GameManager.instance.sceneLoader;
    }

    // update the current index
    public void InitCurrentLevel(Level level)
    {
        currentLevel = level.levelIndex;

        SaveLevelProgression();
        UpdatePlayFunction(level);
    }

    // auto save and load certain level
    void SaveLevelProgression()
    {
        sceneLoader.SaveSceneProgression(true);
    }

    // config the current play button
    void UpdatePlayFunction(Level level)
    {
        playAnimButton.onClick.AddListener(level.CheckAnimConditions);
    }
 
}
