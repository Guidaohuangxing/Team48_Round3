using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distractor : MonoBehaviour
{
    //generate position
    public Vector3 pos = Vector3.zero;
    //generate Target Pos
    public Vector3 target = Vector3.zero;
    //speed
    public float speed = 3f;
    public bool canMove = false;
    //rotate
    public float rotateSpeed = .01f;
    public bool isRotate = true;
    public float coroTime = 3f;
    private float startTime = 0;
    private float endTime = 0;
    private float wholeRotateAngle = 0;
    private Quaternion targetAngle = Quaternion.identity;
    private float lerpRatio = 1f;
    private Vector3 dir;
    private void OnEnable()
    {
        RotateInitial();
    }

    private void FixedUpdate()
    {
        Move();
    }


    public void RotateInitial()
    {
        startTime = Time.time;
        wholeRotateAngle = Vector3.Angle(target, transform.right);
        targetAngle = Quaternion.Euler(new Vector3(0, 0, wholeRotateAngle));
    }

    //Move
    public void Move()
    {
        Vector3 dir = target - transform.position;
        if (isRotate)
        {
            lerpRatio = (Time.time - startTime) / (wholeRotateAngle / rotateSpeed);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetAngle, lerpRatio);
            if(Vector3.Angle(target, transform.right) < 0.1f)
            {
                isRotate = false;
            }
        }
        else if (canMove)
        { 
            dir = dir.normalized;
            if(dir.magnitude > 0)
            {
                this.transform.position += dir * speed * Time.deltaTime;
                if(Vector3.Distance(target, transform.position) < 0.1f)
                {
                    canMove = false;
                }
            }
        }
    }

    //the distractor 
    public void Dead()
    {
        gameObject.SetActive(false);
    }

    public void StartMoveDistractor(string animateEnd)
    {
        if(animateEnd == "AnimateEnd")
        {
            print("start move");
            StartCoroutine(StartMove());
        }
    }

    IEnumerator StartMove()
    {
        yield return new WaitForSeconds(coroTime);
        canMove = true;
        this.GetComponent<Animator>().SetBool("isMove", true);
    }
}
