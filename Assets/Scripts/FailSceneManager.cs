using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FailSceneManager : MonoBehaviour
{
    [SerializeField] float waitTime = 3f;
    [SerializeField] string nextScene;
    [SerializeField] GameObject panel;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ShowPanel");
    }

    // Update is called once per frame
    IEnumerator ShowPanel()
    {
        yield return new WaitForSeconds(waitTime);
        panel.SetActive(true);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(nextScene);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
