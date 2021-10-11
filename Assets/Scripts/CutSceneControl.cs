using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneControl : MonoBehaviour
{
    float waitTime = 3f;
    [SerializeField] string nextScene;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("WaitNLoad");
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
}
