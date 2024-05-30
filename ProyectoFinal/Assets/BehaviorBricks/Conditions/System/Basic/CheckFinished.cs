using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;
using BBUnity.Conditions;
using Unity.VisualScripting;
using static UnityEditor.VersionControl.Asset;

namespace BBCore.Conditions
{
    [Condition("Basic/CheckFinished")]
    public class CheckFinished : GOCondition
    {
        [InParam("Action")]
        private int action;
        StateMachine stateMachine = null;
        bool finished = false;
        public override bool Check()
        {
            if (gameObject.tag == "Wolf")
            {
                stateMachine = gameObject.GetComponent<WolfSM>();
                finished = !stateMachine.CheckActiveAction(action);
            }
            else if (gameObject.tag == "Stag")
            {
                stateMachine = gameObject.GetComponent<StateMachine>();
                finished = !stateMachine.CheckActiveAction(action);
            }
            else return false;

            if (finished)
            {
                Debug.Log("Sleep Forever: " + action);
                stateMachine.DeactivateAction(action);
            }

            return finished;
        }
    }
}