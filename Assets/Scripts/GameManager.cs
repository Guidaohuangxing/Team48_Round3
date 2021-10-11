using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerSetting
{
    public static Vector2 EyeOffset { get; set; }
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get => _instance; }
    static GameManager _instance;
    GameObject playerGazePoint;
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
        playerGazePoint = GameObject.FindGameObjectWithTag("Player");
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
        if (SceneManager.GetActiveScene().name.Equals("Menu"))
        {
            float horiInput = Input.GetAxis("Horizontal");
            float vertInput = Input.GetAxis("Vertical");
            Debug.Log("hori input: " + horiInput + " veti input: " + vertInput);
            PlayerSetting.EyeOffset += new Vector2(horiInput, vertInput);
            Debug.Log("eye offset: " + PlayerSetting.EyeOffset);
            playerGazePoint.GetComponent<GazePointPlot>().offset = PlayerSetting.EyeOffset;
        }
    }

    public void NextScene()
    {
        if (sceneIndex == scenes.Length)
            EndGame();
        string next = scenes[sceneIndex];
        SceneManager.LoadScene(next);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
