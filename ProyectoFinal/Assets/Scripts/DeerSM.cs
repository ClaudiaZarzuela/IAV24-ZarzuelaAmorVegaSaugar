using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DeerSM : StateMachine
{
    [SerializeField]
    private GameObject deerHouse = null;

    private bool assignHouse = false;

    private void Awake()
    {
        base.Awake();
        blackboard.Set("searchedBush", typeof(bool), false);
        blackboard.Set("bush", typeof(GameObject), null);
        blackboard.Set("arrivedAtBush", typeof(bool), false);
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

    protected override void Eat()
    {
        if ((bool)blackboard.Get("searchedBush", typeof(bool)) && (GameObject)blackboard.Get("bush", typeof(GameObject)) == null)
        {
            Debug.Log("No hay arbustos disponibles");
            behaviorExecutorList[(int)States.WANDER].enabled = true;
        }
        else if ((bool)blackboard.Get("arrivedAtBush", typeof(bool)))
        {
            Debug.Log("He llegado");
            ((GameObject)blackboard.Get("bush", typeof(GameObject))).GetComponent<BushBehaviour>().StartEating();
            currentState = States.RECHARGE;
        }
    }

    protected override void GoHome()
    {  
        behaviorExecutorList[(int)currentState].SetBehaviorParam("target", deerHouse);
    }

    public GameObject getHouse()
    {
        return deerHouse;
    }
}
