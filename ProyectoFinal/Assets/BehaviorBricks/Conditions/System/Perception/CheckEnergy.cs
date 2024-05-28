using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    [Condition("Perception/CheckEnergy")]
    public class CheckEnergy : GOCondition
    {
        EnergyController energy;
        [InParam("minEnergy")]
        public float minEnergy;

        public void OnStart()
        {
            energy = gameObject.GetComponent<EnergyController>();
        }
        public override bool Check()
        {
            if (energy == null) return false;
            return energy.GetEnergy() <= minEnergy;
        }
    }
}
