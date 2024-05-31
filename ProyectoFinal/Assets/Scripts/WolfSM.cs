using BBUnity.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WolfSM : StateMachine
{
    private enum WolfStates { SMELLING, HUNT, EAT, NONE };
    private WolfStates m_State = WolfStates.NONE;

    [SerializeField]
    private GameObject wolfHouse = null;

    [SerializeField]
    BehaviorExecutor smelling = null;
    [SerializeField]
    BehaviorExecutor trace = null;

    SmellArea area = null;

    public override void AnimalDied()
    {
        Destroy(smelling);
        Destroy(trace);
        base.AnimalDied();
    }
    public bool IsTracking()
    {
        return m_State == WolfStates.HUNT;
    }

    private void Awake()
    {
        base.Awake();
        blackboard.Set("target", typeof(GameObject), null);
        blackboard.Set("hasEat", typeof(bool), false);
        blackboard.Set("hasSlept", typeof(bool), false);
    }

    public bool CheckIfHunting() {
        GameObject aux = (GameObject)blackboard.Get("target", typeof(GameObject));
        return aux.GetComponent<GenerateSmell>() != null;
    
    }
    private new void Start()
    {
        area = gameObject.GetComponent<SmellArea>();
        base.Start();
    }

    public override bool CheckActiveAction(int action)
    {
        if (m_State != WolfStates.NONE)
        {
            return action == (int)m_State;
        }
        else if ((bool)blackboard.Get("hasEat", typeof(bool)))
        {
            blackboard.Set("hasEat", typeof(bool), false);
            trace.enabled = false;
            return false;
        }
        else return base.CheckActiveAction(action);
    }

    public void SwitchAction()
    {
        if (m_State == WolfStates.SMELLING)
        {
            smelling.enabled = true;
        }
        else
        {
            trace.enabled = true;
        }
    }
    public override void DeactivateAction(int action)
    {
        if(m_State != WolfStates.NONE)
        {
            if (action == 0)
                smelling.enabled = false;
            else trace.enabled = false;
        }
        else base.DeactivateAction(action);
    }

    private new void Update()
    {
        if(m_State != WolfStates.NONE)
        {
            WolfSmelling();
            if (m_State == WolfStates.HUNT) WolfHunt();
        }

        base.Update();   
    }

    private void WolfSmelling()
    {
        if (area.HasDetectedSmell())
        {
            m_State = WolfStates.HUNT;
        }
        else
        {
            m_State = WolfStates.SMELLING;
            SwitchAction();
        }
    }
    public GameObject GetTarget()
    {
        return (GameObject)blackboard.Get("target", typeof(GameObject));
    }
    private void WolfHunt()
    {
        GameObject target = area.GetScent();
        if (target != null)
        {
            float maxIntensity = 0.7f;
            if (target.GetComponent<Scent>().GetOriginator().tag == "Stag") maxIntensity = 0.9f;
            if (target.GetComponent<Scent>().GetIntensity() >= maxIntensity && area.GetPrey() != null)
            {
                blackboard.Set("target", typeof(GameObject), area.GetPrey());
            }
            else
            {
                blackboard.Set("target", typeof(GameObject), area.GetScent());
            }
            SwitchAction();
        }
    }

    public void WolfHasPrey()
    {
        //Vector3 lookPos = area.GetPrey().transform.position;
        //gameObject.transform.LookAt(lookPos);
        //gameObject.transform.position = lookPos - Vector3.forward *gameObject.transform.localScale.z;
        LifeController life = area.GetPrey().GetComponent<LifeController>();
        life.Die();


        m_State = WolfStates.NONE;
        blackboard.Set("hasEat", typeof(bool), true);
        currentState = States.RECHARGE;
    }
    protected override void Eat()
    {
        if (m_State == WolfStates.NONE)
        {
            m_State = WolfStates.SMELLING;
            SwitchAction();
        }
    }

    protected override void GoHome()
    {
        if ((bool)blackboard.Get("hasSlept", typeof(bool)))
        {
            currentState = States.WANDER;
            ChangeAction();
            blackboard.Set("hasSlept", typeof(bool), false);
        }
    }

    public override GameObject GetHouse()
    {
        return wolfHouse;
    }
}
