using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;

public class StateMachine : MonoBehaviour
{
    public Blackboard blackboard = null;
    protected enum States { WANDER, RECHARGE, DIE, GO_HOME, EAT };

    private EnergyController energyController = null;

    protected States currentState = States.WANDER;

    [SerializeField]
    private List<BehaviorExecutor> behaviorExecutorList = null;

    public bool CheckIfRunningAction(int action)
    {
        return action == (int)currentState;
    }

    public void ChangeAction()
    {
        behaviorExecutorList[(int)currentState].enabled = true;
    }
    public void DeactivateAction(int action)
    {
        behaviorExecutorList[action].enabled = false;
    }
    private void Awake()
    {
        energyController = gameObject.GetComponent<EnergyController>();
        blackboard = new Blackboard(null);

        blackboard.Set("minHunger",typeof(float), 75.0f);
        blackboard.Set("minEnergy",typeof(float), 25.0f);
    }

    // Start is called before the first frame update
    protected void Start()
    {
    }
    protected States Wander()
    {
        if(energyController.GetHunger() <= (float)blackboard.Get("minHunger", typeof(float)))
        {
            currentState = States.EAT;
            //ChangeAction();
            return States.EAT;
        }
        else if (energyController.GetEnergy() <= (float)blackboard.Get("minEnergy", typeof(float)))
        {
            currentState = States.GO_HOME;
            //ChangeAction();
            return States.GO_HOME;
        }
        return States.WANDER;
    }
    protected States Recharge()
    {
    
        return States.RECHARGE;
    }
    protected States Die()
    {

        return States.DIE;
    }
    protected States GoHome()
    {

        return States.WANDER;
    }
    protected virtual States Eat()
    {
        return States.WANDER;
    }

    protected void Update()
    {
        switch (currentState)
        {
            case States.WANDER:
                currentState = Wander();
                break;
            case States.RECHARGE:
                currentState = Recharge();
                break;
            case States.EAT:
                currentState = Eat();
                break;
            case States.GO_HOME:
                currentState = GoHome();
                break;
            case States.DIE:
                currentState = Die();
                break;

        }

    }
}
