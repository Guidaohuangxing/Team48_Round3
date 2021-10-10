using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FocusManager : MonoBehaviour
{
    private float focusDuration = 0f; //s
    [SerializeField]
    private float focusThreshold = 5f; //s
    [SerializeField]
    private float penaltyRatio = 0.1f;
    private bool gotPenalty = false;
    [SerializeField]
    private float decreaseRate = 0.5f; // for continuous panelty
    public bool continuousPenalty;
    private bool completed = false;
    [SerializeField] Image progress;
    [SerializeField] GameObject character;
    [SerializeField] bool multiEye;
    [SerializeField] MultiEyeFocus[] eyes;
    private int openEyeIndex = 0;
    private float changeEyeFreq = 5f;
    private bool eyeFlag = false;
    private bool[] hasFocus;


    // Start is called before the first frame update
    void Start()
    {
        if (multiEye)
        {
            openEyeIndex = Random.Range(0, eyes.Length);
            hasFocus = new bool[eyes.Length];
            OpenEye(openEyeIndex);
            StartCoroutine("ChangeEye");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (multiEye)
        {
            CheckHaveFocus();
            Penalty();
        }
        if(multiEye && !eyeFlag)
        {
            OpenEye(openEyeIndex);
            StartCoroutine("ChangeEye");
        }
    }

    public void FocusOnTarget()
    {
        if (gotPenalty)
        {
            gotPenalty = false;
            character.GetComponent<CharacterMovement>().DeactivateAngryMark();
        }
        focusDuration += Time.deltaTime;
        UpdateProgress();
        if (focusDuration >= focusThreshold)
        {
            focusDuration = focusThreshold;
            completed = true;
        }
    }
    void PenaltyOnce()
    {
        if (!gotPenalty)
        {
            focusDuration -= focusThreshold * penaltyRatio;
            UpdateProgress();
            gotPenalty = true;
        }
        character.GetComponent<CharacterMovement>().ActivateAngryMark();
    }
    void PenaltyThroughTime()
    {
        focusDuration -= Time.deltaTime * decreaseRate;
        if (focusDuration < 0)
        {
            focusDuration = 0f;
        }
        UpdateProgress();
        character.GetComponent<CharacterMovement>().ActivateAngryMark();
    }

    void UpdateProgress()
    {
        progress.fillAmount = focusDuration / focusThreshold;
    }

    public bool IsComplete()
    {
        return completed;
    }

    //MultiEye
    void OpenEye(int index)
    {
        for(int i = 0; i<eyes.Length; i++)
        {
            if (i == index)
            {
                eyes[index].SetOpen();
            }
            else
            {
                eyes[i].SetClose();
            }
        }
    }

    IEnumerator ChangeEye()
    {
        float time = Random.Range(2, changeEyeFreq);
        //Debug.Log("channge eye time: " + time);
        eyeFlag = true;
        yield return new WaitForSeconds(time);
        if(eyes.Length == 2)
        {
            if (openEyeIndex == 0)
            {
                openEyeIndex = 1;
            }
            else
            {
                openEyeIndex = 0;
            }
        }
        else
        {
            openEyeIndex = Random.Range(0, eyes.Length);
        }
        eyeFlag = false;
    }

    void CheckHaveFocus()
    {
        for(int i = 0; i<eyes.Length; i++)
        {
            if (eyes[i].GetComponent<MultiEyeFocus>().hasFocus)
            {
                hasFocus[i] = true;
            }
            else
            {
                hasFocus[i] = false;
            }
        }
    }
    public void Penalty()
    {
        if (multiEye)
        {
            bool stat = false;
            foreach (bool b in hasFocus)
            {
                if (b == true)
                {
                    stat = b;
                    break;
                }
            }

            if (!stat)
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
}
