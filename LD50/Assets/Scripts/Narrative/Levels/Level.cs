using UnityEngine;

/// <summary>
/// the base Level class to be derived by each level for specific requirements
/// </summary>
public class Level : MonoBehaviour
{
    [Tooltip("the index of this level")]
    public int levelIndex;

    const string playAnimSFXName = "PlayAnim";


    /// <summary>
    /// public method to be attached to the hud play anim button
    /// </summary>
    public virtual void CheckAnimConditions()
    {
        GameManager.instance.audioManager.PlayIfHasAudio(playAnimSFXName, 0f);
    }

}
