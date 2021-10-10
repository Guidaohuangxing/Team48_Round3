using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private float RandBlinkTime = 0f;
    [SerializeField] Animator _animator;
    [SerializeField] GameObject angryMark;

    void Start()
    {
        //_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(RandBlinkTime == 0f)
        {
            RandBlinkTime = Random.Range(1f, 5f);
            StartCoroutine("waitNBlink");
        }
    }

    IEnumerator waitNBlink()
    {
        yield return new WaitForSeconds(RandBlinkTime);
        _animator.SetTrigger("blink");
        RandBlinkTime = 0f;
    }

    public void SetAnimator(Animator ani)
    {
        _animator = ani;
    }

    public void ActivateAngryMark()
    {
        angryMark.SetActive(true);
    }
    public void DeactivateAngryMark()
    {
        angryMark.SetActive(false);
    }
}
