using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] musicsPublic;
    public static AudioClip[] musics;
    public float[] maxVolumePublic;
    public float[] volumePublic;
    public static float[] volume;

    static AudioSource audioSource;

    private void Awake()
    {
        musics = new AudioClip[musicsPublic.Length];
        for (int i = 0; i < musics.Length; i++)
        {
            musics[i] = musicsPublic[i];
        }

        volumePublic = new float[maxVolumePublic.Length];
        for (int i = 0; i < volumePublic.Length; i++)
        {
            volumePublic[i] = 1;
        }

        volume = new float[volumePublic.Length];
        for (int i = 0; i < volume.Length; i++)
        {
            volume[i] = volumePublic[i] * maxVolumePublic[i];
        }

        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < volume.Length; i++)
        {
            volume[i] = volumePublic[i] * maxVolumePublic[i];
        }
    }

    public static void MusicPlay(int i)
    {
        audioSource.Stop();
        audioSource.clip = musics[i];
        audioSource.Play();
    }

    public static void MusicStop()
    {
        audioSource.Stop();
    }
}
