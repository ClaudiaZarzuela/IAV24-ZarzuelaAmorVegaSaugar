using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagController : AnimalController
{
    [SerializeField]
    AnimalController animalController;

    Flee fleeComponent;
    Wander wanderComponent;
    enum AnimationStates
    {
        IDLE = 0, WALKING = 1, RUNNING = 2, DEAD = 3, EAT = 4
    }
    enum States
    {
        REST = 0, EAT = 1, WANDER = 2, DEAD = 3, FLEE = 4
    }

    Animator animator;
    int currentState;

    public void Start()
    {
        fleeComponent = gameObject.GetComponent<Flee>();
        wanderComponent = gameObject.GetComponent<Wander>();
        animator = gameObject.GetComponent<Animator>();

        animalController.RegisterStag(this.gameObject);
        currentState = (int)States.WANDER;
        StartAction(currentState);
    }

    public void StartAction(int state)
    {
        DeActivateAllActions();
        switch (state)
        {
            case (int)States.REST:
                break;
            case (int)States.EAT:
                break;
            case (int)States.WANDER:
                wanderComponent.StartWander();
                StartAnimation(AnimationStates.WALKING);
                break;
            case (int)States.DEAD:
                break;
            case (int)States.FLEE:
                fleeComponent.StartFlee();
                StartAnimation(AnimationStates.RUNNING);
                break;
        }
    }

    public void DeActivateAllActions()
    {
        fleeComponent.StopFlee();
        wanderComponent.StopWander();
    }

    private void StartAnimation(AnimationStates anim)
    {
        switch (anim)
        {
            case AnimationStates.IDLE:
                animator.Play("Idle");
                break;
            case AnimationStates.WALKING:
                animator.Play("Walk");
                break;
            case AnimationStates.RUNNING:
                animator.Play("Run");
                break;
            case AnimationStates.DEAD:
                animator.Play("Death");
                break;
            case AnimationStates.EAT:
                animator.Play("Eat");
                break;
        }
    }

    public void Update()
    {
        switch (currentState)
        {
            case (int)States.REST:
            {
                break;
            }
            case (int)States.EAT:
            {
                break;
            }
            case (int)States.WANDER:
            {
                break;
            }
            case (int)States.DEAD:
            {
                break;
            }
            case (int)States.FLEE:
            {
                if (fleeComponent.GotHome())
                {
                    Debug.Log("AAAAA");
                    currentState = (int)States.WANDER;
                    StartAction(currentState);
                }
                break;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("He detectado algo!");
            currentState = (int)States.FLEE;
            StartAction(currentState);

        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("A wandrear se ha dicho!");
            currentState = (int)States.WANDER;
            StartAction(currentState);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
        }
    }
}
