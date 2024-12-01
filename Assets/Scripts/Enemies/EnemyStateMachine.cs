using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    public AnimationClip run;
    public AnimationClip idle;
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
        currentIdle = idle;
        ChangeAnimation(idle);
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
        // If dead, play the dead animation and deactivate the collider
        if(GetComponentInParent<StatManager>().dead)
        {
            ChangeAnimation(death);
            GetComponentInParent<BoxCollider2D>().enabled = false;
            return;
        }

        Vector3 currentScale = transform.localScale;

        Vector3 vel = _parentBody.velocity;

        //If the parent body isn't moving, play the current idle animation.
        if (vel.magnitude == 0)
        {
            ChangeAnimation(currentIdle);
        }
        else
        {
            ChangeAnimation(run);
        }

        //Change scale based on movement
        if (vel.x < 0)
        {
            currentScale.x = -1;
        }
        else if (vel.x > 0) currentScale.x = 1;

        transform.localScale = currentScale;
    }
}
