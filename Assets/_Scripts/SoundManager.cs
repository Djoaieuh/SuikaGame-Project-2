using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

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

    [SerializeField] AudioClip[] SoundFX;

    AudioSource source;


    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    void Update()
    {

    }

    public void PlaySound(string soundName)
    {
        for (int i = 0; i < SoundFX.Length; i++)
        {
            if (SoundFX[i].name == soundName)
            {
                AudioClip newSound = SoundFX[i];
                source.clip = newSound;
                source.Play();
                i = SoundFX.Length - 1;

            }
        }
    }


}
