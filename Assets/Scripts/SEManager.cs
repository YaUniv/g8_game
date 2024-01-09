using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    public static SEManager instance;

    public AudioClip[] soundsPublic;
    public static AudioClip[] sounds;
    public float[] maxVolumePublic;
    public float[] volumePublic;
    public static float[] volume;

    private void Awake()
    {
        sounds = new AudioClip[soundsPublic.Length];
        for (int i = 0; i < sounds.Length; i++)
        {
            sounds[i] = soundsPublic[i];
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


        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //AudioSource.PlayClipAtPoint(SEManager.sounds[0], Vector3.zero, SEManager.volume[0]);
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < volume.Length; i++)
        {
            volume[i] = volumePublic[i] * maxVolumePublic[i];
        }
    }
}
