using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager Instance {get; private set;}

    [SerializeField] private List<SoundsManagerItem> soundsList;
    private Dictionary<string, List<AudioClip>> soundsDict = new Dictionary<string, List<AudioClip>>();
    private AudioSource audioSource;

    void Awake()
    {
        Instance = this;
        audioSource = GetComponent<AudioSource>();

        // load the list into the dictionary
        foreach (var item in soundsList)
        {
            soundsDict.Add(item.key, item.audioClips);
        }
    }

    // plays the wanted audio as a none spatial audio
    public void PlaySound(string key)
    {
        audioSource.clip = GetAudio(key);
        audioSource.Play();
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
