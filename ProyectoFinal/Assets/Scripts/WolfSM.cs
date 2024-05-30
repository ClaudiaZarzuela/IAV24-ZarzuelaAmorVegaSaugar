using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSM : StateMachine
{
    [SerializeField]
    private GameObject wolfHouse = null;

    private bool assignHouse = false;

    // Start is called before the first frame update
    private void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();   
    }

    protected override States Eat()
    {
        Debug.Log("Comiendo");
        return States.DIE;
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
