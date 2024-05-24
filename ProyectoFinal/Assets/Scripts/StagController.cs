using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagController : AnimalController
{
    enum AnimationStates
    {
        IDLE = 0, WALKING = 1, RUNNING = 2, DEAD = 3, EAT =4
    }
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void StartAnimation(int state)
    {
        switch(state){
            case (int)AnimationStates.IDLE:
                animator.Play("Idle");
                break;
            case (int)AnimationStates.WALKING:
                animator.Play("Walk");
                break;
            case (int)AnimationStates.RUNNING:
                animator.Play("Run");
                break;
            case (int)AnimationStates.DEAD:
                animator.Play("Death");
                break;
            case (int)AnimationStates.EAT:
                animator.Play("Eat");
                break;
        }
    }
}
