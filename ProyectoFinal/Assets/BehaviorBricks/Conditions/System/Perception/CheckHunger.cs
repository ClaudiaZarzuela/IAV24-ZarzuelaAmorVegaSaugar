using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    [Condition("Perception/HasDetectedSmell")]
    [Help("Checks if the wolf has detected a scent of an animal")]
    public class CheckHunger : GOCondition
    {
        SmellArea area;

        public void OnStart()
        {
            area = gameObject.GetComponent<SmellArea>();
        }
        public override bool Check()
        {
            if (area == null) return false;
            return area.HasDetectedSmell();
        }
    }
}
