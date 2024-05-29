using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfSM : StateMachine
{
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
}
