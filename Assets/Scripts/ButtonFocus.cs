using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Tobii.Gaming;

public class ButtonFocus : MonoBehaviour
{
    private GazeAware _gazeAware;
    private float focusDuration = 0f; //s
    [SerializeField]
    private float focusThreshold = 3f; //s
    [SerializeField] Image progress;
    private bool isClear = false;
    private TMPro.TextMeshPro text;
    Scene thisScene;
    [SerializeField] FailSceneManager sceneManager;

    // Start is called before the first frame update
    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        thisScene = SceneManager.GetActiveScene();
        //text = GetComponent<TMPro.TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gazeAware.HasGazeFocus)
        {
            //text.fontSize = 1200;
            transform.localScale = new Vector3(1.1f, 1.1f, 1f);
            focusDuration += Time.deltaTime;
            UpdateProgress();
            isClear = false;
        }
        else
        {
            //text.fontSize = 1000;
            if (!isClear)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                focusDuration = 0f;
                progress.fillAmount = 0f;
                isClear = true;
            }
        }

        if (focusDuration >= focusThreshold)
        {
            if (thisScene.name.Equals("Menu"))
            {
                if (CompareTag("Finish"))
                {
                    GameManager.Instance.EndGame();
                }
                else
                {
                    GameManager.Instance.NextScene();
                }
            }
            else
            {
                if (CompareTag("Restart"))
                {
                    sceneManager.RestartLevel();
                }
                else
                {
                    sceneManager.BackToMenu();
                }
            }
        }

    }
    void UpdateProgress()
    {
        progress.fillAmount = focusDuration / focusThreshold;
    }
}
