using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class BackGroundManager : MonoBehaviour
{
    public List<Transform> BG;

    public List<float> speed;
    private Vector3 VSpeed;
    GazePoint _lastGazePoint = GazePoint.Invalid;
    private GazePoint gazePoint;
    public Canvas canvas;
    public List<Vector3> startPos = new List<Vector3>();

    private void Start()
    {
        for(int i=0;i<BG.Count;i++)
        {
            startPos.Add(BG[i].position);
        }
    }

    private void FixedUpdate()
    {
        Parallax();
    
    }

    private void Parallax()
    {
        gazePoint = TobiiAPI.GetGazePoint();
        for(int i = 0; i < BG.Count; i++)
        {
            
            Vector3 target = ChangeGazePointToTarget(gazePoint, speed[i], startPos[i]);
            //BG[i].localPosition = Vector3.Lerp(BG[i].localPosition, target, 0.5f);
            BG[i].position = target;
        }
        _lastGazePoint = gazePoint;


    }

    private Vector3 ChangeGazePointToTarget(GazePoint gazePoint, float speed, Vector3 BG)
    {
        Vector2 viewPort = gazePoint.Viewport - new Vector2(.5f, .5f);
        Vector2 eyePoint = viewPort ;
        Vector3 TargetPos = BG + new Vector3(Mathf.Atan(eyePoint.x) * speed, 0, 0);
        return TargetPos;
    }


}
