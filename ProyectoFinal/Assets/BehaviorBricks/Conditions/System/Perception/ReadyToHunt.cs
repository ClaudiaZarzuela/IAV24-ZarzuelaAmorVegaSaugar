using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    [Condition("Perception/ReadyToHunt")]
    [Help("Checks if the wolf has detected a scent of an animal")]
    public class ReadyToHunt : GOCondition
    {
        [InParam("minHunger")]
        public float minHunger;
        public override bool Check()
        {
            SmellArea area = gameObject.GetComponent<SmellArea>();
            EnergyController hunger = gameObject.GetComponent<EnergyController>();

            if (hunger == null || area == null) return false;
            return area.HasDetectedSmell() && hunger.GetHunger() <= minHunger;
        }
    }
}