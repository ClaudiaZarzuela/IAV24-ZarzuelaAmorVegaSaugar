using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    [Condition("Perception/CheckEnergy")]
    public class CheckEnergy : GOCondition
    {
        public override bool Check()
        {
            Debug.Log("Muy buenas Campeones");
            return true;
        }
    }
}
