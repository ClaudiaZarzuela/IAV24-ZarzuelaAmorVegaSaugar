using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LifeController : MonoBehaviour
{
    private float elapsedTime = 0;
    private float timeToDie = 3;

    public bool alive = true;
    private Animator animator = null;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }
    public void Die()
    {
        animator.Play("Death");

        if (gameObject.tag == "Stag")
            gameObject.GetComponent<DeerSM>().AnimalDied();
        else if (gameObject.tag == "Wolf")
            gameObject.GetComponent<WolfSM>().AnimalDied();


        NavMeshAgent agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = 0;
        agent.isStopped = true;
        alive = false;
    }
    public void RemoveAnimal()
    {
        if(gameObject != null)
        {
            if(gameObject == GameManager.Instance.GetAnimal())
            {
                GameManager.Instance.ChangeToMainCamera();
            }

            if (gameObject.tag == "Stag")
                EnvironmentController.Instance.RemoveStag(this.gameObject);
            else if(gameObject.tag == "Wolf")
                EnvironmentController.Instance.RemoveWolf(this.gameObject);
            else 
                EnvironmentController.Instance.RemoveRabbit(this.gameObject);


        }
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
