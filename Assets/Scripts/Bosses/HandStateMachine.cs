using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandStateMachine : MonoBehaviour
{
    public AnimationClip handIdle;
    public AnimationClip handSmash;
    public AnimationClip death;

    public GameObject attackEffect;

    public float maxSpeed = 0.01f;

    public int timeOffset = 5;

    private GameObject target;

    private Animator _animator;
    private AnimationClip currentAnimation;
    private string _state = "IDLE";

    private GameObject _target;
    private Vector2 targetPos = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        _animator = GetComponent<Animator>();

        InvokeRepeating("SwitchState", timeOffset, 5);
    }

    private void ChangeAnimation(AnimationClip animation)
    {
        if (currentAnimation == animation)
            return;

        _animator.Play(animation.name);
        currentAnimation = animation;
        
    }

    // Handles moving the hands in FixedUpdate
    void FixedUpdate()
    {
        if (GetComponent<StatManager>().dead)
        {
            _state = "DEAD";
            ChangeAnimation(death);
            GetComponent<BoxCollider2D>().enabled = false;
        }
            
        // Do nothing if dead.
        if (_state == "DEAD")
        {
            return;
        }
        if(_state == "SMASH") {
            ChangeAnimation(handSmash);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, maxSpeed);
        }
        if(_state == "IDLE")
        {
            Vector2 moveVec = target.transform.position;
            moveVec.x = transform.position.x;
            transform.position = Vector2.MoveTowards(transform.position, moveVec, maxSpeed/5);
            ChangeAnimation(handIdle);
        }
    }

    void SwitchState()
    {
        if(_state == "IDLE")
        {
            _state = "SMASH";
            targetPos = target.transform.position;
            GameObject effect = Instantiate(attackEffect);
            effect.transform.position = targetPos;
        }
        else if (_state == "SMASH")
        {
            _state = "IDLE";
        }
    }
}
