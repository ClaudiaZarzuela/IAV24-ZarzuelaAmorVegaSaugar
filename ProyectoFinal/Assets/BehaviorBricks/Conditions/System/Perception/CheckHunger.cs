using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    [Condition("Perception/CheckHunger")]
    public class CheckHunger : GOCondition
    {
        EnergyController hunger;
        [InParam("minHunger")]
        public float minHunger;

        public void OnStart()
        {
            hunger = gameObject.GetComponent<EnergyController>();
        }
        public override bool Check()
        {
            if (hunger == null) return false;
            return hunger.GetHunger() <= minHunger;
        }
    }
}
