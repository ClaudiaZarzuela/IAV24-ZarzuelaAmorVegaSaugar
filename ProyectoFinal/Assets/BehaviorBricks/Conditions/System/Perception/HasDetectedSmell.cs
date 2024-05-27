using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    [Condition("Perception/HasDetectedSmell")]
    [Help("Checks if the wolf has detected a scent of an animal")]
    public class HasDetectedSmell : GOCondition
    {
        [InParam("SmellRange")]
        [Help("The maximun distance to smell diferent animals")]
        public float closeSmellRangeDistance;

        public override bool Check()
        {
            return true;
            //return (gameObject.transform.position - target.transform.position).sqrMagnitude < closeDistance * closeDistance;
        }
    }
}