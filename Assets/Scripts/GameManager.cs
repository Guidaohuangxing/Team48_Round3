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
    [SerializeField] GameObject[] keyMask;

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
            //Debug.Log("hori input: " + horiInput + " veti input: " + vertInput);
            UpdateKeyMask();
            PlayerSetting.EyeOffset += new Vector2(horiInput, vertInput);
            Debug.Log("eye offset: " + PlayerSetting.EyeOffset);
            playerGazePoint.GetComponent<GazePointPlot>().offset = PlayerSetting.EyeOffset;
        }
    }

    private void UpdateKeyMask()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            keyMask[0].SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W))
        {
            keyMask[0].SetActive(false);
        }

        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            keyMask[1].SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S))
        {
            keyMask[1].SetActive(false);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            keyMask[2].SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            keyMask[2].SetActive(false);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            keyMask[3].SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            keyMask[3].SetActive(false);
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
