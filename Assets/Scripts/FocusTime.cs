using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
public class FocusTime : MonoBehaviour
{
    private GazeAware _gazeAware;
    private float focusDuration = 0f; //s
    [SerializeField]
    private float focusThreshold = 5f; //s
    [SerializeField]
    private float penaltyRatio = 0.1f;
    private bool gotPenalty = false;
    private int penaltyCount = 0;
    public bool continuousPenalty;
    [SerializeField]
    private float decreaseRate = 0.5f;
    private bool completed = false;
    [SerializeField] Image progress;
    // Start is called before the first frame update
    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_gazeAware.HasGazeFocus)
        {
            if (gotPenalty)
            {
                gotPenalty = false;
            }
            focusDuration += Time.deltaTime;
            UpdateProgress();
            if(focusDuration >= focusThreshold)
            {
                completed = true;
            }
        }
        else
        {
            if (continuousPenalty)
            {
                PenaltyThroughTime();
            }
            else
            {
                PenaltyOnce();
            }

        }
    }
    void PenaltyOnce()
    {
        if (!gotPenalty)
        {
            if (penaltyCount == 2)
            {
                Debug.Log("Game Over");
            }

            focusDuration -= focusThreshold * penaltyRatio;
            UpdateProgress();
            gotPenalty = true;
            penaltyCount += 1;
        }
    }
    void PenaltyThroughTime()
    {
        focusDuration -= Time.deltaTime * decreaseRate;
        UpdateProgress();
    }

    void UpdateProgress()
    {
        progress.fillAmount = focusDuration / focusThreshold;
    }

    public bool IsComplete()
    {
        return completed;
    }
}
