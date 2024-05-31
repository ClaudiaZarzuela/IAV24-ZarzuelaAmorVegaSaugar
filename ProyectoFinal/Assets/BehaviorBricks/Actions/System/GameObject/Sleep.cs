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

    [Action("GameObject/Sleep")]
    public class Sleep : GOAction
    {
        public GameObject house;
        private StateMachine sM;


        public override void OnStart()
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<EnergyController>().Stop(false);
            sM = gameObject.GetComponent<StateMachine>();
            if (sM!= null)
            {
                house = sM.GetHouse();
                gameObject.transform.position = house.transform.position;
            }
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
