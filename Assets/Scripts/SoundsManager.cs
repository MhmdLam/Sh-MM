using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance {get; private set;}

    [SerializeField] private List<SoundsManagerItem> soundsList;
    private Dictionary<string, List<AudioClip>> soundsDict = new Dictionary<string, List<AudioClip>>();
    private AudioSource[] audioSources;
    private int sourcesI = 0;

    [SerializeField] private List<SpatialAudioSource> spatialSources;
    private int spatialI = 0;

    void Awake()
    {
        Instance = this;
        audioSources = GetComponents<AudioSource>();

        // load the list into the dictionary
        foreach (var item in soundsList)
        {
            soundsDict.Add(item.key, item.audioClips);
        }
    }

    // plays the wanted audio as a none spatial audio
    public void PlaySound(string key)
    {
        audioSources[sourcesI].clip = GetAudio(key);
        audioSources[sourcesI].Play();

        sourcesI++;
        if (sourcesI>=audioSources.Length) sourcesI = 0;
    }

    // plays the wanted audio as a spatial audio at the given position
    public void PlaySoundSpatial(string key, Vector3 pos)
    {
        spatialSources[spatialI].transform.position = pos;
        spatialSources[spatialI].audioSource.clip = GetAudio(key);
        spatialSources[spatialI].audioSource.Play();

        spatialI++;
        if (spatialI>=spatialSources.Count) spatialI = 0;
    }

    // returns an audio clip whith the given key (chooses one at random if more than one is available)
    public AudioClip GetAudio(string key)
    {
        if (!soundsDict.ContainsKey(key))
        {
            Debug.Log("Key doesn't excist!");
            return null;
        }

        var list = soundsDict[key];
        return list[UnityEngine.Random.Range(0, list.Count)];
    }


    [System.Serializable]
    private class SoundsManagerItem
    {
        [SerializeField] public string key;
        [SerializeField] public List<AudioClip> audioClips;
    }
}
