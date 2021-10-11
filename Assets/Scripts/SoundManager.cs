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
    private List<AudioSource> addAS = new List<AudioSource>();
    
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
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
      for(int i = 0; i < addAS.Count; i++)
        {
            if(!addAS[i].isPlaying)
            {
                Destroy(addAS[i]);
                addAS.RemoveAt(i);
            }
        }   
    }

    public void PlaySound(int index)
    {
        AudioSource AS = this.gameObject.AddComponent<AudioSource>();
        AS.PlayOneShot(clips[index]);
        addAS.Add(AS);

    }

    public void PlaySound(int index,float volum)
    {
        AudioSource AS = this.gameObject.AddComponent<AudioSource>();
        AS.PlayOneShot(clips[index], volum);
        addAS.Add(AS);
    }


}
