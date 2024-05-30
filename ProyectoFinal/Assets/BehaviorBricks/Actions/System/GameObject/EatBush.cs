using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

namespace BBUnity.Actions
{

    [Action("GameObject/EatBush")]
    public class EatBush : GOAction
    {
        [InParam("target")]
        public GameObject target;
        private DeerSM dSM;

        public override void OnStart()
        {
            dSM = gameObject.GetComponent<DeerSM>();
            dSM.blackboard.Set("arrivedAtBush", typeof(bool), true);
            gameObject.transform.LookAt(target.transform.position);
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
