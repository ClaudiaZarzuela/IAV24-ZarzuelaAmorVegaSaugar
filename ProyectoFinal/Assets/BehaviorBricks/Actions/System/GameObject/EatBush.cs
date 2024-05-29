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
        public Blackboard blackboard;
        bool ciervoDetected = true;
        

        private void Awake()
        {
            blackboard.Set("booleano", typeof(bool), ciervoDetected);
        }

        public override void OnStart()
        {

            GameObject hola = (GameObject)(blackboard.Get("player", typeof(GameObject)));
            blackboard.Set("booleano", typeof(bool), ciervoDetected);
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
