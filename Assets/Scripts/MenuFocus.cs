using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

public class MenuFocus : MonoBehaviour
{
    private GazeAware _gazeAware;
    private float focusDuration = 0f; //s
    [SerializeField]
    private float focusThreshold = 5f; //s
    [SerializeField] Image progress;
    private TMPro.TextMeshPro text;

    // Start is called before the first frame update
    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        text = GetComponent<TMPro.TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gazeAware.HasGazeFocus)
        {
            text.fontSize = 1200;
            focusDuration += Time.deltaTime;
            UpdateProgress();
        }
        else
        {
            text.fontSize = 1000;
            focusDuration = 0f;
            progress.fillAmount = 0f;
        }

        if (focusDuration >= focusThreshold)
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

    }
    void UpdateProgress()
    {
        progress.fillAmount = focusDuration / focusThreshold;
    }
}
