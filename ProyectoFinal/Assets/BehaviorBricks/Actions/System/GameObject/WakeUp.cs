using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

namespace BBUnity.Actions
{

    [Action("GameObject/WakeUp")]
    public class WakeUp : GOAction
    {
        public GameObject house;
        private DeerSM dSM;
        private WolfSM wSM;

        public override void OnStart()
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;

            dSM = gameObject.GetComponent<DeerSM>();
            wSM = gameObject.GetComponent<WolfSM>();
            if (dSM != null)
            {
                house = dSM.getHouse().transform.GetChild(0).gameObject;
                gameObject.transform.position = house.transform.position;
                dSM.blackboard.Set("hasSlept", typeof(bool), true);
            }
            if (wSM != null)
            {
                house = wSM.getHouse().transform.GetChild(0).gameObject;
                gameObject.transform.position = house.transform.position;
                wSM.blackboard.Set("hasSlept", typeof(bool), true);
            }
            gameObject.GetComponent<EnergyController>().RestoreMaxEnergy();
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
