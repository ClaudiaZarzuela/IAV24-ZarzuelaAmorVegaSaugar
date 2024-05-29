using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    [Condition("Perception/CheckVision")]
    public class CheckVision : GOCondition
    {
        public override bool Check()
        {
            Debug.Log("Comprobando vision");
            return true;
        }
    }
}
