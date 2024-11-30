using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrampusStateMachine : MonoBehaviour
{
    public AnimationClip sideIdle;
    public AnimationClip frontIdle;
    public AnimationClip backIdle;
    public AnimationClip death;

    private Animator _animator;
    private AnimationClip currentAnimation;
    //private Rigidbody2D _parentBody;
    private GameObject _target;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        //_parentBody = GetComponentInParent<Rigidbody2D>();
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
        if(GetComponentInParent<KrampusController>().dead)
        {
            ChangeAnimation(death);
            return;
        }
        Vector3 currentScale = transform.localScale;

        Vector3 difference = _target.transform.position - transform.position;

        // If the X difference is larger, use side animation.
        if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
        {
            ChangeAnimation(sideIdle);
        }

        // If the Y difference is larger, use front or back animation
        else {
            if (difference.y < 0)
            {
                ChangeAnimation(frontIdle);
            }
            if (difference.y > 0)
            {
                ChangeAnimation(backIdle);
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
