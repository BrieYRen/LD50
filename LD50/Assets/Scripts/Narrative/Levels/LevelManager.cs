using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// manage the levels including init, save progression and update hud function for each level
/// </summary>
public class LevelManager : MonoBehaviour
{
    /// <summary>
    /// the current activated level
    /// </summary>
    [HideInInspector]
    public int currentLevel = 0;

    [Tooltip("drag and drop the HUD button - play anim here")]
    [SerializeField]
    Button playAnimButton;

    SceneLoader sceneLoader;


    private void Start()
    {
        sceneLoader = GameManager.instance.sceneLoader;
    }


    /// <summary>
    /// public method to init the current level at the beginning of each level
    /// </summary>
    /// <param name="level"></param>
    public void InitCurrentLevel(Level level)
    {
        currentLevel = level.levelIndex;

        SaveLevelProgression();
        UpdatePlayFunction(level);
    }

    void SaveLevelProgression()
    {
        sceneLoader.SaveSceneProgression(true);
    }

    void UpdatePlayFunction(Level level)
    {
        playAnimButton.onClick.AddListener(level.CheckAnimConditions);
    }
 
}
