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
        private DeerSM dSM;
        private WolfSM wSM;

        public override void OnStart()
        {
            gameObject.GetComponent<NavMeshAgent>().enabled = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = true;

            dSM = gameObject.GetComponent<DeerSM>();
            wSM = gameObject.GetComponent<WolfSM>();
            if (dSM != null)
            {
                house = dSM.getHouse();
                gameObject.transform.position = house.transform.position;
            }
            if (wSM != null)
            {
                house = wSM.getHouse();
                gameObject.transform.position = house.transform.position;
            }

        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
