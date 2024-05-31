using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DeerSM : StateMachine
{
    [SerializeField]
    private GameObject deerHouse = null;

    [SerializeField]
    private BehaviorExecutor eat = null;

    private void Awake()
    {
        base.Awake();
        blackboard.Set("searchedBush", typeof(bool), false);
        blackboard.Set("bush", typeof(GameObject), null);
        blackboard.Set("arrivedAtBush", typeof(bool), false);
        blackboard.Set("hasSlept", typeof(bool), false);
    }

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

    public override void DeactivateAction(int action)
    {
        if (action == (int)States.EAT)
        {
            eat.enabled = false;
        }
        else base.DeactivateAction(action);
    }

    protected override void Eat()
    {
        eat.enabled = true;
        if ((bool)blackboard.Get("searchedBush", typeof(bool)) && (GameObject)blackboard.Get("bush", typeof(GameObject)) == null)
        {
            //Debug.Log("No hay arbustos disponibles");
            currentState = States.WANDER;
            eat.enabled = false;
            ChangeAction();
        }
        else if ((bool)blackboard.Get("arrivedAtBush", typeof(bool)))
        {
            //Debug.Log("He llegado");
            ((GameObject)blackboard.Get("bush", typeof(GameObject))).GetComponent<BushBehaviour>().StartEating();

            blackboard.Set("searchedBush", typeof(bool), false);
            blackboard.Set("bush", typeof(GameObject), null);
            blackboard.Set("arrivedAtBush", typeof(bool), false);
            eat.enabled = false;

            currentState = States.RECHARGE;
        }
    }

    protected override void GoHome()
    {
        //Debug.Log("Hola");
        if ((bool)blackboard.Get("hasSlept", typeof(bool)))
        {
            //Debug.Log("DURMIO");
            currentState = States.WANDER;
            ChangeAction();
            blackboard.Set("hasSlept", typeof(bool), false);
        }
    }

    public override GameObject GetHouse()
    {
        return deerHouse;
    }
}
