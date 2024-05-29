using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BBUnity.Actions
{

    [Action("GameObject/FindClosestBush")]
    public class FindClosestBush : GOAction
    {
        public List<GameObject> list;
        [OutParam("target")]
        public GameObject foundGameObject;

        public override void OnStart()
        {
            list = EnvironmentController.Instance.GetBushesList();

            float dist = float.MaxValue;
            foreach(GameObject go in list)
            {
                BushBehaviour bush = go.GetComponent<BushBehaviour>();
                if (bush.GetIsAvailable())
                {
                    float newdist = Vector3.Distance(gameObject.transform.position, bush.GetPosition());
                    if(newdist < dist)
                    {
                        dist = newdist;
                        foundGameObject = go;
                    }
                }
            }
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
