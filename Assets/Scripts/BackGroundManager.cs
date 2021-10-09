using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class BackGroundManager : MonoBehaviour
{

    public Transform BG1;
    public Transform BG2;
    public Transform BG3;
    public Transform BG4;

    public float speed1 = 1;
    public float speed2 = 2;
    public float speed3 = 3;
    public float speed4 = 4;

    private GazePoint gazePoint;
    public Canvas canvas;

    private void FixedUpdate()
    {
        Parallax();
    }

    private void Parallax()
    {
        gazePoint = TobiiAPI.GetGazePoint();
        Vector3 BG1TargetPos = ChangeGazePointToTarget(gazePoint, speed1);
        Vector3 BG2TargetPos = ChangeGazePointToTarget(gazePoint, speed2);
        Vector3 BG3TargetPos = ChangeGazePointToTarget(gazePoint, speed3);
        Vector3 BG4TargetPos = ChangeGazePointToTarget(gazePoint, speed4);
        BG1.position = Vector3.Lerp(BG1.position, BG1TargetPos, 0.1f);
        BG2.position = Vector3.Lerp(BG2.position, BG2TargetPos, 0.1f);
        BG3.position = Vector3.Lerp(BG3.position, BG3TargetPos, 0.1f);
        BG4.position = Vector3.Lerp(BG4.position, BG4TargetPos, 0.1f);


    }

    private Vector3 ChangeGazePointToTarget(GazePoint gazePoint, float speed)
    {
        Vector2 viewPort = gazePoint.Viewport - new Vector2(-.5f, -.5f);
        Vector2 eyePoint = viewPort * new Vector2(canvas.renderingDisplaySize.x, canvas.renderingDisplaySize.y);
        Vector3 TargetPos = new Vector3(Mathf.Atan(eyePoint.x) * speed, 0, 0);
        return TargetPos;
    }

}
