using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class MultiEyeFocus : MonoBehaviour
{
    private GazeAware _gazeAware;
    [SerializeField] FocusManager fm;
    [SerializeField] bool isOpen;
    [SerializeField] Sprite[] eyeSprites;
    [SerializeField] CharacterMovement cm;
    public bool hasFocus;
    SpriteRenderer _spriteRenderer;
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_gazeAware.HasGazeFocus && isOpen)
        {
            fm.FocusOnTarget();
            hasFocus = true;
        }
        else
        {
            hasFocus = false;
        }
    }

    public void SetOpen()
    {
        isOpen = true;
        _spriteRenderer.sprite = eyeSprites[0];
        _animator.enabled = true;
        cm.SetAnimator(_animator);
    }

    public void SetClose()
    {
        isOpen = false;
        //Debug.Log(eyeSprites.Length);
        _spriteRenderer.sprite = eyeSprites[1];
        _animator.enabled = false;
    }
}
