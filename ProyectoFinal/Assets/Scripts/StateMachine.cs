using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;

public class StateMachine : MonoBehaviour
{
    public Blackboard blackboard = null;
    protected enum States { WANDER, RECHARGE, DIE, GO_HOME, EAT};

    private EnergyController energyController = null;

    protected States currentState = States.WANDER;

    [SerializeField]
    private List<BehaviorExecutor> behaviorExecutorList = null;

    private void Awake()
    {
        energyController = GetComponent<EnergyController>();
        blackboard = new Blackboard(null);
    }

    // Start is called before the first frame update
    void Start()
    {
        DeactivateAllBehaviours();
    }

    protected void DeactivateAllBehaviours()
    {
        foreach (BehaviorExecutor executor in behaviorExecutorList) {
            executor.enabled = false;
        }
    }

    private void ChangeState()
    {
        DeactivateAllBehaviours();
        behaviorExecutorList[(int)currentState].enabled = true;

    }

    protected States Wander(float dt)
    {
        //DeactivateAllBehaviours();
        //behaviorExecutorList[(int)currentState].enabled = true;
        return States.WANDER;
    }
    protected States Recharge(float dt)
    {
    
        return States.RECHARGE;
    }
    protected States Die(float dt)
    {

        return States.WANDER;
    }
    protected States GoHome(float dt)
    {

        return States.WANDER;
    }
    protected States Eat(float dt)
    {

        return States.WANDER;
    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case States.WANDER:
                currentState = Wander(Time.deltaTime);
                break;
            case States.RECHARGE:
                currentState = Recharge(Time.deltaTime);
                break;

        }

        if(Input.GetKeyDown(KeyCode.Alpha1)) { 
            currentState = States.RECHARGE; 
            ChangeState(); 
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentState = States.WANDER;
            ChangeState();
        }

    }
}
