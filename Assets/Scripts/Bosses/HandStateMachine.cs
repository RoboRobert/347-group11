using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandStateMachine : MonoBehaviour
{
    public AnimationClip handIdle;
    public AnimationClip handSmash;
    public AnimationClip death;

    public GameObject target;

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
        //_parentBody = GetComponentInParent<Rigidbody2D>();

        //Sets the default animation to be front idle
        ChangeAnimation(handIdle);
    }

    private void ChangeAnimation(AnimationClip animation)
    {
        if (currentAnimation == animation)
            return;

        _animator.Play(animation.name);
        currentAnimation = animation;
        
    }

    // Update is called once per frame
    void Update()
    {
        // Do nothing if dead.
        if (_state == "DEAD")
        {
            return;
        }
        if(_state == "SMASH") {
            ChangeAnimation(handSmash);
            transform.position = Vector2.MoveTowards(transform.position, targetPos, 0.1f);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, target.transform.position.y, 0);
        }
    }
}
