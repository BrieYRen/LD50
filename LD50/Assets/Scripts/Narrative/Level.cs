using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int levelIndex;
    
    public virtual void CheckAnimConditions()
    {
        // to be overrided
    }
}
