using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabitController : MonoBehaviour
{
    public static int rabitsNum = 10;
    public int currRabitsNum = 0;
    public List<GameObject> rabbits = new List<GameObject>();

    enum States
    {
       WANDER = 0, DEAD = 1
    }

    Animator animator;
    States currentState;
    Wander wanderComponent;


    private void Start()
    {
        wanderComponent = gameObject.GetComponent<Wander>();
        animator = gameObject.GetComponent<Animator>();
        currentState = States.WANDER;
        StartAction(currentState);
    }

    private void StartAction(States state)
    {
        if(currentState == States.WANDER)
        {
            animator.Play("Walk");
            wanderComponent.StartWander();
        }
        else
        {
            animator.Play("Death");
            wanderComponent.StopWander();
        }
    }
    public void RabbitDied(GameObject obj)
    {
        currRabitsNum--;
        rabbits.Remove(obj);
        GameManager.Instance.InstanciateRabbits();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentState = States.DEAD;
            StartAction(currentState);
        }
    }
}
