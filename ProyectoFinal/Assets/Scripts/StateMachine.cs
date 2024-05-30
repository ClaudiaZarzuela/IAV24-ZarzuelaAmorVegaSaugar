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
    protected List<BehaviorExecutor> behaviorExecutorList = null;

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

        blackboard.Set("minHunger",typeof(float), 95.0f);
        blackboard.Set("minEnergy",typeof(float), 25.0f);
    }

    // Start is called before the first frame update
    protected void Start()
    {
    }
    protected void Wander()
    {
        if(energyController.GetHunger() <= (float)blackboard.Get("minHunger", typeof(float)))
        {
            currentState = States.EAT;
            //ChangeAction();
        }
        else if (energyController.GetEnergy() <= (float)blackboard.Get("minEnergy", typeof(float)))
        {
            currentState = States.GO_HOME;
            ChangeAction();
        }
        else
            currentState = States.WANDER;
    }
    protected void Recharge()
    {
        Debug.Log("Recharging");
    }
    protected void Die()
    {

    }
    protected virtual void GoHome()
    {
    }
    protected virtual void Eat()
    {
    }

    protected void Update()
    {
        switch (currentState)
        {
            case States.WANDER:
                Wander();
                break;
            case States.RECHARGE:
                Recharge();
                break;
            case States.EAT:
                Eat();
                break;
            case States.GO_HOME:
                GoHome();
                break;
            case States.DIE:
                Die();
                break;

        }

    }
}
