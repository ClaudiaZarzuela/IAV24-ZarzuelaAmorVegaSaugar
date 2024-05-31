using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LifeController : MonoBehaviour
{
    private float elapsedTime = 0;
    private float timeToDie = 5;

    public bool alive = true;
   
    public void Die()
    {
        Debug.Log("Muerto");
        Animator animator = gameObject.GetComponent<Animator>();
        animator.Play("Death");
        NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = 0;
        agent.isStopped = true;
        alive = false;
    }
    public void RemoveAnimal()
    {
        if (gameObject.tag == "Stag")
            EnvironmentController.Instance.RemoveStag(this.gameObject);
        else if(gameObject.tag == "Wolf")
            EnvironmentController.Instance.RemoveWolf(this.gameObject);
        else 
            EnvironmentController.Instance.RemoveRabbit(this.gameObject);
        alive = true;

    }
    // Update is called once per frame
    void Update()
    {
        if (!alive)
        {
            if (elapsedTime >= timeToDie)
            {
                RemoveAnimal();
            }
            else elapsedTime += Time.deltaTime;
        }
    }
}
