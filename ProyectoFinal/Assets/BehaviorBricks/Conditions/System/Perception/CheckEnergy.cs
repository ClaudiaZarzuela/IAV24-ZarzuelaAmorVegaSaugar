using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    [Condition("Perception/CheckEnergy")]
    public class CheckEnergy : GOCondition
    {
        [InParam("minEnergy")]
        public float minEnergy;
        public override bool Check()
        {
            EnergyController energy = gameObject.GetComponent<EnergyController>();
            if (energy == null) return false;
            return energy.GetEnergy() <= minEnergy;
        }
    }
}
