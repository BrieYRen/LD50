using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// this is the script to manage all sounds in game including init 2d sounds when game start, 
/// init 3d sounds when a scene loaded and release them when a scene is unloaded, 
/// play or stop playing certain sound with fade effect,
/// and change a two-layer background music seamlessly at the end of the current bar
/// </summary>
public class AudioManager : MonoBehaviour
{
    [Tooltip("2d sounds that will play on camera")]
    [SerializeField]
    List<Sound> flatSounds;

    [Tooltip("3d sounds that will play on certain gameObject")]
    [SerializeField]
    List<SpacialSound> spacialSounds;

    /// <summary>
    /// the audio dictionary to save all initiated audio sources ready to play
    /// </summary>
    Dictionary<string, AudioSource> audioDict;

    /// <summary>
    /// the current playing background music melody layer's name
    /// </summary>
    [HideInInspector]
    public string currentMelody = "";

    /// <summary>
    /// the current playing background music accompany layer's name
    /// </summary>
    [HideInInspector]
    public string currentAccompany = "";

    [Tooltip("the tempo of the bgm, how many beats per minute")]
    [SerializeField]
    double beatsPerMin = 100;

    [Tooltip("the first number of the time signature of the bgm")]
    [SerializeField]
    double beatsPerBar = 3;
 

    private void Start()
    {
        audioDict = new Dictionary<string, AudioSource>();

        Init2DSounds();

        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }

    /// <summary>
    /// this will only be called once when game starts to initiate all 2d sounds
    /// </summary>
    void Init2DSounds()
    {
        if (flatSounds == null)
        {
            flatSounds = new List<Sound>();
        }
        else
        {
            foreach (Sound s in flatSounds)
            {
                GameObject gameObject = new GameObject(s.name);
                gameObject.transform.SetParent(transform);

                AudioSource audioSource = gameObject.AddComponent<AudioSource>();
                audioSource.clip = s.audioClip;
                audioSource.outputAudioMixerGroup = s.audioMixerGroup;
                audioSource.playOnAwake = s.playOnAwake;
                audioSource.loop = s.isLoop;
                audioSource.volume = s.volumn;
                audioSource.pitch = s.pitch;
                audioSource.panStereo = s.stereo;

                audioDict.Add(s.name, audioSource);

                CoroutineContainer container = gameObject.AddComponent<CoroutineContainer>();
                container.audioSourceKey = s.name;
            }
        }
    }

    /// <summary>
    /// this should be called by the scripts which will call PlayIfHasAudio() method 
    /// to play a spatial sound later when a new scene is loaded 
    /// </summary>
    /// <param name="gameObject"></param>
    public string Init3DSound(string newName, GameObject gameObject)
    {
        if (spacialSounds == null)
            return null;

        string audioSourceKeyToCall = null;

        for(int i = 0; i < spacialSounds.Count; i++)
        {
            if (spacialSounds[i].name == newName)
            {
                AudioSource audioSource;
                if (!gameObject.TryGetComponent<AudioSource>(out audioSource))
                    audioSource = gameObject.AddComponent<AudioSource>();

                audioSource.clip = spacialSounds[i].audioClip;
                audioSource.outputAudioMixerGroup = spacialSounds[i].audioMixerGroup;
                audioSource.playOnAwake = spacialSounds[i].playOnAwake;
                audioSource.loop = spacialSounds[i].isLoop;
                audioSource.volume = spacialSounds[i].volumn;
                audioSource.pitch = spacialSounds[i].pitch;
                audioSource.spatialBlend = spacialSounds[i].spatialBlend;
                audioSource.minDistance = Mathf.Max(0f, spacialSounds[i].minDistance);
                audioSource.maxDistance = Mathf.Max(spacialSounds[i].minDistance, spacialSounds[i].maxDistance);

                bool hadKey = true;               
                while(hadKey)
                {
                    int tempIndex = Random.Range(0, 999);
                    string tempString = spacialSounds[i].name + tempIndex.ToString();
                    if (!audioDict.ContainsKey(tempString))
                    {
                        audioSourceKeyToCall = tempString;
                        hadKey = false;
                    }                       
                }

                audioDict.Add(audioSourceKeyToCall, audioSource);
            }
        }

        return audioSourceKeyToCall;
      
    }

    /// <summary>
    /// when a previous scene is unloaded, find out all null spacial audio sources in that unloaded scene
    /// and remove them from the audio dictionary
    /// </summary>
    /// <param name="scene"></param>
    /// <param name="mode"></param>
    void OnSceneUnloaded(Scene scene)
    {
        RemoveMissingSpacialSources();
    }

    void RemoveMissingSpacialSources()
    {
        List<string> audioDictNames = audioDict.Keys.ToList<string>();
        List<AudioSource> audioDictSources = audioDict.Values.ToList<AudioSource>();
        
        for (int i = 0; i < audioDictSources.Count; i++)
        {
            if (audioDictSources[i] == null)
                audioDict.Remove(audioDictNames[i]);
        }
        
    }

    /// <summary>
    /// begin to play an audio with optional fade in effect
    /// this method is use for sfx and ambience not for music
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fadeInSec"></param>
    public void PlayIfHasAudio(string name, float fadeInSec)
    {
        if (name == null)
            return;

        if (!audioDict.ContainsKey(name))
        {
            Debug.Log(name + " not founded!");
            return;
        }

        if (audioDict[name].isPlaying && audioDict[name].loop)
            return;

        StartCoroutine(FadeInCertainSound(name, fadeInSec));
 
    }

    IEnumerator FadeInCertainSound(string name, float fadeInDuration)
    {
        float currentTime = 0f;
        float targetVolumn = audioDict[name].volume;

        audioDict[name].volume = 0f;
        audioDict[name].Play();

        while (currentTime < fadeInDuration)
        {
            currentTime += Time.deltaTime;
            audioDict[name].volume = Mathf.Lerp(0f, targetVolumn, currentTime / fadeInDuration);
            yield return null;
        }

        audioDict[name].volume = targetVolumn;
        yield break;
    }

    /// <summary>
    /// stop playing a certain audio with optional fade out effect
    /// this method is use for sfx and ambience not for music
    /// </summary>
    /// <param name="name"></param>
    /// <param name="fadeOutSec"></param>
    public void StopPlayCertainAudio(string name, float fadeOutSec)
    {
        if (name == null)
            return;

        if (!audioDict.ContainsKey(name))
        {
            Debug.Log(name + " not founded!");
            return;
        }

        if (!audioDict[name].isPlaying)
        {
            Debug.Log(name + " is not playing!");
            return;
        }

        StartCoroutine(FadeOutCertainSound(name, fadeOutSec));
    }

    IEnumerator FadeOutCertainSound(string name, float fadeOutDuration)
    {
        float currentTime = 0f;
        float startVolumn = audioDict[name].volume;

        while (currentTime < fadeOutDuration)
        {
            currentTime += Time.deltaTime;
            audioDict[name].volume = Mathf.Lerp(startVolumn, 0f, currentTime / fadeOutDuration);
            yield return null;
        }

        audioDict[name].Stop();
        audioDict[name].volume = startVolumn;
        yield break;
    }

    
    /// <summary>
    /// begin to play a two-layer background music
    /// </summary>
    /// <param name="melodyLayer"></param>
    /// <param name="accompanyLayer"></param>
    /// <param name="melodyFirst"></param>
    public void PlayIfHasTwoLayerMusic(string melodyLayer, string accompanyLayer, bool melodyFirst, int delayBars)
    {
        if (melodyLayer == null || accompanyLayer == null || !audioDict.ContainsKey(melodyLayer) || !audioDict.ContainsKey(accompanyLayer))
            return;

        if (currentMelody == "")
        {
            audioDict[melodyLayer].PlayScheduled(AudioSettings.dspTime);
            audioDict[accompanyLayer].PlayScheduled(AudioSettings.dspTime);

            currentMelody = melodyLayer;
            currentAccompany = accompanyLayer;
        }
        else
        {
            double barDuration = CalculateBarTime(beatsPerMin, beatsPerBar);
            double firstToPlayTime = CalculatePlayTime(audioDict[currentMelody], beatsPerMin, beatsPerBar);
            double secondToPlayTime = firstToPlayTime + barDuration * delayBars;
            
            double melodyToPlayTime = melodyFirst ? firstToPlayTime : secondToPlayTime;
            double accompanyToPlayTime = melodyFirst ? secondToPlayTime : firstToPlayTime;

            float melodyDelayTime = (float)(melodyToPlayTime - AudioSettings.dspTime);
            float accompanyDelayTime = (float)(accompanyToPlayTime - AudioSettings.dspTime);

            var melodyContainer = audioDict[melodyLayer].gameObject.GetComponent<CoroutineContainer>();
            var accompanyContainer = audioDict[accompanyLayer].gameObject.GetComponent<CoroutineContainer>();

            if (melodyLayer == currentMelody)
            {              
                StartCoroutine(melodyContainer.ReplayMusicInSec(melodyDelayTime));               
                StartCoroutine(accompanyContainer.ReplayMusicInSec(accompanyDelayTime));
            }
            else
            {
                if (audioDict[melodyLayer].isPlaying)
                {
                    audioDict[melodyLayer].Stop();
                    melodyContainer.StopAllCoroutines();
                }
                    
                if (audioDict[accompanyLayer].isPlaying)
                {
                    audioDict[accompanyLayer].Stop();
                    accompanyContainer.StopAllCoroutines();
                }

                audioDict[currentMelody].SetScheduledEndTime(melodyToPlayTime);
                audioDict[currentAccompany].SetScheduledEndTime(accompanyToPlayTime);

                audioDict[melodyLayer].PlayScheduled(melodyToPlayTime);
                audioDict[accompanyLayer].PlayScheduled(accompanyToPlayTime);

                StartCoroutine(melodyContainer.ChangeCurrentMelodyString(melodyDelayTime));
                StartCoroutine(accompanyContainer.ChangeCurrentAccompanyString(accompanyDelayTime));
            }
        }
    }


    /// <summary>
    /// calculate when to change music so as to change it seamlessly at the end of current bar
    /// </summary>
    /// <param name="currentAudio"></param>
    /// <param name="currentTempo"></param>
    /// <param name="timeSignatureFirstNum"></param>
    /// <param name="timeSignatureSecondNum"></param>
    /// <returns></returns>
    double CalculatePlayTime(AudioSource currentAudio, double currentTempo, double timeSignatureFirstNum)
    {
        double barDuration = CalculateBarTime(currentTempo, timeSignatureFirstNum);
        double remainder = ((double)currentAudio.timeSamples / currentAudio.clip.frequency) % barDuration;
        double nextBarTime = AudioSettings.dspTime + barDuration - remainder;

        return nextBarTime;
    }

    double CalculateBarTime(double currentTempo, double timeSignatureFirstNum)
    {
        double barDuaration = (60d / currentTempo) * timeSignatureFirstNum; //* (timeSignatureFirstNum / timeSignatureSecondNum);

        return barDuaration;
    }


}
