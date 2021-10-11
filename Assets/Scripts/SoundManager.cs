using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get => _instance; }
    static SoundManager _instance;
    [SerializeField] AudioClip[] clips;
    AudioSource _audiosource;

    // Start is called before the first frame update
    private void Awake()
    {
        if (_instance != null & _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        _audiosource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(int index)
    {
        if (!_audiosource.isPlaying)
        {
            _audiosource.PlayOneShot(clips[index]);
        }
    }
}
