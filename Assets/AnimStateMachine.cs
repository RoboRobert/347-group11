using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimStateMachine : MonoBehaviour
{
    public string sideRun = "";
    public string sideIdle = "";
    public string frontRun = "";
    public string frontIdle = "";
    public string backRun = "";
    public string backIdle = "";
    
    private Animator _animator;
    private string currentAnimation = "";
    private string currentIdle = "";
    private Rigidbody2D _parentBody;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _parentBody = GetComponentInParent<Rigidbody2D>();
        
        //Sets the default animation to be idle
        currentIdle = frontIdle;
        ChangeAnimation(frontIdle);
    }

    private void ChangeAnimation(string animation)
    {
        if (currentAnimation == animation)
            return;
        
        _animator.Play(animation);
        currentAnimation = animation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currentScale = transform.localScale;

        Vector3 vel = _parentBody.velocity;
    
        //If the parent body isn't moving, play the current idle animation.
        if (vel.magnitude == 0)
        {
            ChangeAnimation(currentIdle);
        }
        //If the parent body is moving side to side, change to the side run animation
        if (Mathf.Abs(vel.x) > 0f)
        {
            currentIdle = sideIdle;
            ChangeAnimation(sideRun);
        }
        //If the parent body moves down, play the down run animation and set the idle
        if (vel.y < 0)
        {
            currentIdle = frontIdle;
            ChangeAnimation(frontRun);
        }
        //If the parent body moves up, play the up run animation and set the idle
        if (vel.y > 0)
        {
            currentIdle = backIdle;
            ChangeAnimation(backRun);
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
