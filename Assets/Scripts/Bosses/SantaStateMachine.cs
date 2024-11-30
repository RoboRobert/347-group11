using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SantaStateMachine : MonoBehaviour
{
    public AnimationClip attackUp;
    public AnimationClip attackDown;
    public AnimationClip sideRun;
    public AnimationClip sideIdle;
    public AnimationClip frontRun;
    public AnimationClip frontIdle;
    public AnimationClip backRun;
    public AnimationClip backIdle;
    public AnimationClip death;

    public GameObject attack;

    public float speed = 3f;

    //Follow distance in grid squares of 32 pixels each
    public float followDistance = 2f;

    private Animator _animator;
    private AnimationClip currentAnimation;
    private Rigidbody2D _rb;
    private GameObject _target;

    private string _state = "IDLE";
    private string _substate = "FRONT";

    private float attackOffset = 0.3f;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
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

        if (_state == "DEAD")
        {
            _rb.velocity = Vector2.zero;
            ChangeAnimation(death);
            return;
        }
        if(_state == "IDLE")
        {
            _rb.velocity = Vector2.zero;
            if (_substate == "UP")
            {
                ChangeAnimation(backIdle);
            }
            if (_substate == "DOWN")
            {
                ChangeAnimation(frontIdle);
            }
            if (_substate == "SIDE")
            {
                ChangeAnimation(sideIdle);
            }
        }
        if (_state == "RUN")
        {
            movement_func();
            if (_substate == "UP")
            {
                ChangeAnimation(backRun);
            }
            if (_substate == "DOWN")
            {
                ChangeAnimation(frontRun);
            }
            if (_substate == "SIDE")
            {
                ChangeAnimation(sideRun);
            }
        }
        if (_state == "ATTACK")
        {
            _rb.velocity = Vector2.zero;
            if (_substate == "UP")
            {
                attackOffset = 0.3f;
                ChangeAnimation(attackUp);
            }
            if (_substate == "DOWN")
            {
                attackOffset = -0.3f;
                ChangeAnimation(attackDown);
            }
            if (_substate == "SIDE")
            {
                attackOffset = -0.3f;
                ChangeAnimation(attackDown);
            }
        }
    }

    void ChangeState()
    {
        if (GetComponentInParent<StatManager>().dead)
            _state = "DEAD";

        if (_state == "ATTACK" || _state == "DEAD")
            return;
        Vector3 currentScale = transform.localScale;

        Vector3 difference = _target.transform.position - _rb.transform.position;

        Vector3 vel = _rb.velocity;

        //If the player is close enough, attack them.
        if (difference.magnitude < followDistance * 0.32)
        {
            _state = "ATTACK";
            Invoke("FinishAttack", 2.5f);
            return;
        }
        // Otherwise 
        else _state = "RUN";

        // If the X difference is larger, use SIDE substates
        if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
        {
            _substate = "SIDE";
        }

        // If the Y difference is larger, use DOWN or UP substates
        else
        {
            if (difference.y < 0)
            {
                _substate = "DOWN";
            }
            if (difference.y > 0)
            {
                _substate = "UP";
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

    // Enemy movement logic
    void movement_func()
    {
        //Amount to move the current enemy toward the target
        Vector3 movementVector = Vector3.zero;
        // Gets a distance to the target.
        Vector3 targetDir = _target.transform.position - _rb.transform.position;

        //If the distance to the target is large enough, move toward the target
        if (targetDir.magnitude > 0)
        {
            movementVector = targetDir.normalized * (speed * Time.fixedDeltaTime);
        }

        //Apply the movement vector
        _rb.velocity = movementVector;
    }

    void CreateAttack()
    {
        GameObject newAttack = Instantiate(attack, transform);
        newAttack.transform.localPosition = new Vector2(0, attackOffset);
    }

    //Breaks out of the attack state
    void FinishAttack()
    {
        _state = "IDLE";
        ChangeAnimation(frontIdle);
    }
}
