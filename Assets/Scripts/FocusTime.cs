using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.Gaming;

[RequireComponent(typeof(GazeAware))]
public class FocusTime : MonoBehaviour
{
    private GazeAware _gazeAware;
    [SerializeField] FocusManager fm;
    
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
            fm.FocusOnTarget();
        }
        else
        {
            fm.Penalty();
        }
    }
}
