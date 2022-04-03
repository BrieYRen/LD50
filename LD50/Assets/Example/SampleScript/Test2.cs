using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    [SerializeField]
    string spellImpactSfxName;

    [SerializeField]
    GameObject targetObject;

    AudioManager audioManager;
    string spellImpactSoundKey;

    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = GameManager.instance.sceneLoader;
        audioManager = GameManager.instance.audioManager;
        spellImpactSoundKey = audioManager.Init3DSound(spellImpactSfxName, targetObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
            TestPlaySound();

        if (Input.GetKeyDown(KeyCode.Keypad1))
            sceneLoader.LoadNextScene();
    }

    void TestPlaySound()
    {
        if (spellImpactSoundKey != null)
            audioManager.PlayIfHasAudio(spellImpactSoundKey, 1f);
    }
}
