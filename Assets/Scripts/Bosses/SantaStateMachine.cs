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
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _parentBody = GetComponentInParent<Rigidbody2D>();

        //Sets the default animation to be idle
        //currentIdle = frontIdle;
        //ChangeAnimation(frontIdle);
        currentIdle = frontIdle;
        ChangeAnimation(death);
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
        //ChangeState();
    }

    void ChangeState()
    {
        Vector3 currentScale = transform.localScale;

        Vector3 vel = _parentBody.velocity;

        //If the parent body isn't moving, play the current idle animation.
        if (vel.magnitude == 0)
        {
            ChangeAnimation(currentIdle);
        }
        //If the parent body is moving horizontally more than vertically
        if (Mathf.Abs(vel.x) > Mathf.Abs(vel.y))
        {
            currentIdle = sideIdle;
            ChangeAnimation(sideRun);
        }
        //If the parent body is moving vertically more than horizontally
        else
        {
            // Handle moving down
            if (vel.y < 0)
            {
                currentIdle = frontIdle;
                ChangeAnimation(frontRun);
            }
            //Handle moving up
            if (vel.y > 0)
            {
                currentIdle = backIdle;
                ChangeAnimation(backRun);
            }
        }

        //Change scale based on movement
        if (vel.x > 0)
        {
            currentScale.x = -1;
        }
        else if (vel.x < 0) currentScale.x = 1;

        transform.localScale = currentScale;
    }
}
