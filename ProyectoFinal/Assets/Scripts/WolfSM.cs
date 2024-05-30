using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSM : StateMachine
{
    private Eat eatComponent = null;
    [SerializeField]
    private GameObject wolfHouse = null;

    private bool assignHouse = false;

    // Start is called before the first frame update
    private void Start()
    {
        eatComponent = gameObject.GetComponent<Eat>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();   
    }

    protected override void Eat()
    {
        Debug.Log("Empezamos");
        if (eatComponent.DetectedTrace())
        {
            Debug.Log("Primero");
            eatComponent.StartBehavior();
            eatComponent.active = true;
        }
        else if (eatComponent.IsEatingPrey())
        {
            Debug.Log("Segundo");
            currentState = States.RECHARGE;
        }
        else behaviorExecutorList[(int)States.WANDER].enabled = true;
    }

    protected override void GoHome()
    {
        if (!assignHouse)
        {
            assignHouse = true;
            behaviorExecutorList[(int)currentState].SetBehaviorParam("target", wolfHouse);
        }

    }
}
