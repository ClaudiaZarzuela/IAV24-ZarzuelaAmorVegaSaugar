using BBUnity.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WolfSM : StateMachine
{
    private enum WolfStates { SMELLING, TRACK_TRACE, HUNT, EAT, NONE };
    private WolfStates m_State = WolfStates.NONE;

    [SerializeField]
    private GameObject wolfHouse = null;
    GameObject target = null;

    [SerializeField]
    BehaviorExecutor smelling = null;
    [SerializeField]
    BehaviorExecutor trace = null;

    SmellArea area = null;
    VisionSensor vision = null;

    // Start is called before the first frame update
    private new void Start()
    {
        area = gameObject.GetComponent<SmellArea>();
        vision = gameObject.GetComponent<VisionSensor>();
        base.Start();
    }

    public override bool CheckActiveAction(int action)
    {
        if (m_State != WolfStates.NONE)
        {
            Debug.Log("Lobo Action " + (WolfStates)action);
            return action == (int)m_State;
        }
        else return base.CheckActiveAction(action);
    }

    public void SwitchAction()
    {
        if (m_State == WolfStates.SMELLING)
        {
            smelling.enabled = true;
        }
        else trace.enabled = true;
    }
    public override void DeactivateAction(int action)
    {
        if(m_State != WolfStates.NONE)
        {
            Debug.Log("Lobo Deactivate " + (WolfStates)action);
            if (action == 0)
                smelling.enabled = false;
            else trace.enabled = false;
        }
        else base.DeactivateAction(action);
    }

    private new void Update()
    {
        switch (m_State)
        {
            case WolfStates.SMELLING: WolfSmelling(); break;
            case WolfStates.TRACK_TRACE: WolfTrace(); break;
            case WolfStates.HUNT: WolfHunt(); break;
            case WolfStates.EAT: WolfEat(); break;
        }

        base.Update();   
    }

    private void WolfSmelling()
    {
        if (area.HasDetectedSmell())
        {
            m_State = WolfStates.TRACK_TRACE;
        }
    }

    public GameObject GetTarget()
    {
        Debug.Log(target);
        return target;
    }
    private void WolfTrace()
    {
        target = area.GetPray();
        if(target != null)
        {
            if (vision.DetectClosestTarget(target))
            {
                m_State = WolfStates.HUNT;
            }
            else
            {
                target = area.GetScent();
            }
            SwitchAction();
        }
    }

    private void WolfHunt()
    {
        target = area.GetPray();
    }

    private void WolfEat()
    {
        m_State = WolfStates.NONE;
        currentState = States.RECHARGE;
    }
    public void WolfHasPrey()
    {
        m_State = WolfStates.EAT;
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
        behaviorExecutorList[(int)currentState].SetBehaviorParam("target", wolfHouse);
    }
}
