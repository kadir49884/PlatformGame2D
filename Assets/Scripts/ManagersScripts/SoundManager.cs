using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{

    private AudioSource audioSource;
    [SerializeField] public List<AudioClipsProperty> soundList = new List<AudioClipsProperty>();


    private static SoundManager instance;

    // sahnede sadece bir örnek olmasýný saðlama
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        audioSource = transform.GetComponents<AudioSource>()[0];
    }


    public void PlaySound(int getMusicIndex)
    {
        if (soundList[getMusicIndex] != null)
        {
            audioSource.clip = soundList[getMusicIndex].clip;
            audioSource.Play();
        }


    }




}
[System.Serializable]
public class AudioClipsProperty
{
    public string name;
    public int id;
    public AudioClip clip;
}