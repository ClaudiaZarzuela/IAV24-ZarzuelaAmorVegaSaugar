using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BBUnity.Actions
{

    [Action("GameObject/EatBush")]
    public class EatBush : GOAction
    {
        [InParam("target")]
        public GameObject target;

        public override void OnStart()
        {   
            //BushBehaviour bush = target.GetComponent<BushBehaviour>();
            //bush.StartEating();
            //gameObject.GetComponent<EnergyController>().RestoreMaxEnergy();
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
