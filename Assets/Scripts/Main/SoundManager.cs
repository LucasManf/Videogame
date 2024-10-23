using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    //Variable para mantener el manager entre escenas
    public static SoundManager instance;
    //El static permite referenciar a la misma clase, en este caso SoundManager
   
    public AudioSource[] soundEffects;

    [Header("Audio Source")]
    [SerializeField] AudioSource musicSrc;
    [SerializeField] AudioSource sfxSrc;
    [SerializeField] AudioSource backgrdSrc;


    [Header("Audio Clips")]
    public AudioClip music;
    public AudioClip bckgrd;
    public AudioClip jump;
    public AudioClip fire;
    public AudioClip changeSound;
    public AudioClip interactSound;


    void Awake()
    {
        //Código para que la instancia del Sound Manager no se repita por error
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
   
    void Start()
    {
        
    }


    public void PlaySFX(int clip)
    {
        soundEffects[clip].Play();
    }
}
