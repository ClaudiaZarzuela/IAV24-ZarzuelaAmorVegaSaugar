using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;
using BBUnity.Conditions;
using Unity.VisualScripting;

namespace BBCore.Conditions
{
    [Condition("Basic/CheckFinished")]
    public class CheckFinished : GOCondition
    {
        [InParam("Action")]
        private int action;
        StateMachine stateMachine = null;
        public override bool Check()
        {
            stateMachine = gameObject.GetComponent<StateMachine>();

            bool finished = !stateMachine.CheckIfRunningAction(action);
            if (finished)
            {
                stateMachine.DeactivateAction(action);
            }
            return finished;
        }
    }
}