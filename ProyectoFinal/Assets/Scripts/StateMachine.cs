using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using JetBrains.Annotations;

public class StateMachine : MonoBehaviour
{
    public Blackboard blackboard = null;
    protected enum States { WANDER, GO_HOME, RECHARGE, EAT, NONE };

    private EnergyController energyController = null;

    protected States currentState = States.WANDER;

    private float elapsedTime = 0;
    private float rechargeTime = 2;

    [SerializeField]
    protected List<BehaviorExecutor> behaviorExecutorList = null;

    public virtual void AnimalDied()
    {
        for(int i = 0; i < behaviorExecutorList.Count; i++)
        {
            Destroy(behaviorExecutorList[i]);
        }
        currentState = States.NONE;
    }
    public virtual bool CheckActiveAction(int action)
    {
        return action == (int)currentState;
    }

    public void ChangeAction()
    {
        behaviorExecutorList[(int)currentState].enabled = true;
    }
    public virtual void DeactivateAction(int action)
    {
        behaviorExecutorList[action].enabled = false;
    }
    protected void Awake()
    {
        energyController = gameObject.GetComponent<EnergyController>();
        blackboard = new Blackboard(null);

        blackboard.Set("minHunger",typeof(float), 75.0f);
        blackboard.Set("minEnergy",typeof(float), 25.0f);
    }

    protected void Start()
    {
    }
    protected void Wander()
    {
        if(energyController.GetHunger() <= (float)blackboard.Get("minHunger", typeof(float)))
        {
            currentState = States.EAT;
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
        if (elapsedTime >= rechargeTime)
        {
            elapsedTime = 0;
            energyController.Stop(true);
            currentState = States.WANDER;
            ChangeAction();
        }
        else
        {
            elapsedTime += Time.deltaTime;
            energyController.RestoreMaxHunger();
            energyController.Stop(false);
        }
    }
   
    protected virtual void GoHome(){}
    protected virtual void Eat() {}
    public virtual GameObject GetHouse() { return null; }
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
        }

        
    }
}
