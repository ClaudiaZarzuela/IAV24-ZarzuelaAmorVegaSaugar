using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

namespace BBUnity.Actions
{

    [Action("GameObject/WakeUp")]
    public class WakeUp : GOAction
    {
        public GameObject house;
        private StateMachine sM;


        public override void OnStart()
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = true;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            gameObject.GetComponent<EnergyController>().Stop(true);

            sM = gameObject.GetComponent<StateMachine>();
            if (sM != null)
            {
                house = sM.GetHouse().transform.GetChild(0).gameObject;
                gameObject.transform.position = house.transform.position;
                sM.blackboard.Set("hasSlept", typeof(bool), true);
            }
            gameObject.GetComponent<EnergyController>().RestoreMaxEnergy();
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
