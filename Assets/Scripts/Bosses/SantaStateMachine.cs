using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaStateMachine : MonoBehaviour
{
    public AnimationClip sideRun;
    public AnimationClip sideIdle;
    public AnimationClip frontRun;
    public AnimationClip frontIdle;
    public AnimationClip backRun;
    public AnimationClip backIdle;
    public AnimationClip death;

    private Animator _animator;
    private AnimationClip currentAnimation;
    private AnimationClip currentIdle;
    private Rigidbody2D _parentBody;
    private GameObject _target;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _parentBody = GetComponentInParent<Rigidbody2D>();
        _target = GameObject.FindWithTag("Player");

        //Sets the default animation to be front idle
        ChangeAnimation(frontIdle);
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
        ChangeState();
    }

    void ChangeState()
    {
        Vector3 currentScale = transform.localScale;

        Vector3 difference = _target.transform.position - transform.position;

        Vector3 vel = _parentBody.velocity;

        //If the parent body isn't moving, play the current idle animation.
        if (vel.magnitude == 0)
        {
            ChangeAnimation(currentIdle);
        }

        //If the parent body is moving horizontally more than vertically
        if (Mathf.Abs(vel.x) > Mathf.Abs(vel.y))
        {
            ChangeAnimation(sideRun);
        }
        //If the parent body is moving vertically more than horizontally
        else
        {
            // Handle moving down
            if (vel.y < 0)
            {
                ChangeAnimation(frontRun);
            }
            //Handle moving up
            if (vel.y > 0)
            {
                ChangeAnimation(backRun);
            }
        }

        // If the X difference is larger, use side idle
        if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
        {
            currentIdle = sideIdle;
        }

        // If the Y difference is larger, use front or back idle
        else
        {
            // Front Idle
            if (difference.y < 0)
            {
                currentIdle = frontIdle;
            }
            // Back Idle
            if (difference.y > 0)
            {
                currentIdle = backIdle;
            }
        }

        //Change scale based on position of target
        if (difference.x > 0)
        {
            currentScale.x = -1;
        }
        else if (difference.x < 0) currentScale.x = 1;

        transform.localScale = currentScale;
    }
}
