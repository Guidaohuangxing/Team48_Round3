using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get => _instance; }
    static GameManager _instance;
    int sceneIndex = 0;
    [SerializeField]
    string[] scenes;

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
        Scene thisScene = SceneManager.GetActiveScene();
        Debug.Log(thisScene.name);
        if (!thisScene.name.Equals("Menu"))
        {
            sceneIndex = int.Parse(thisScene.name[thisScene.name.Length - 1].ToString());
            Debug.Log("scene index: " + sceneIndex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScene()
    {
        string next = scenes[sceneIndex];
        SceneManager.LoadScene(next);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
