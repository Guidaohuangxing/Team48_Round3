using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneControl : MonoBehaviour
{
    [SerializeField] float waitTime = 4f;
    [SerializeField] string nextScene;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        if (!nextScene.Equals(""))
        {
            StartCoroutine("WaitNLoad");
        }
        if (CompareTag("Finish"))
        {
            //audioSource = GetComponent<AudioSource>();
            //StartCoroutine("WaitNEnd");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator WaitNLoad()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(nextScene);
    }

    IEnumerator WaitNEnd()
    {
        yield return new WaitForSeconds(audioSource.clip.length+1);
        Application.Quit();
    }
}
