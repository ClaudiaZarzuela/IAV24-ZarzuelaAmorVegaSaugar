using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    [Condition("Perception/CheckHunger")]
    public class CheckHunger : GOCondition
    {
        [InParam("minHunger")]
        public float minHunger;

        public override bool Check()
        {
            EnergyController hunger = gameObject.GetComponent<EnergyController>();
            if (hunger == null) return false;
            return hunger.GetHunger() <= minHunger;
        }
    }
}
