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
        StateMachine stateMachine = null;
        public override bool Check()
        {
            if (gameObject.tag == "Wolf")
            {
                stateMachine = gameObject.GetComponent<WolfSM>();
            }
            else
                Debug.Log("Habra que hacerlo");

            bool finished = !stateMachine.CheckIfRunningAction(0);
            if (finished)
            {
                stateMachine.DeactivateAction(0);
            }
            return finished;
        }
    }
}