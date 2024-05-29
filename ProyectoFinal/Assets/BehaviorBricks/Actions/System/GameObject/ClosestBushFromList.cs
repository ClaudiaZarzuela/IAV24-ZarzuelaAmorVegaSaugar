using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BBUnity.Actions
{

    [Action("GameObject/ClosestBushFromList")]
    public class ClosestBushFromList : GOAction
    {
        [InParam("list")]
        public List<GameObject> list;
        [OutParam("foundGameObject")]
        public GameObject foundGameObject;

        public override void OnStart()
        {
            float dist = float.MaxValue;
            foreach(GameObject go in list)
            {
                BushBehaviour bush = go.GetComponent<BushBehaviour>();
                if (bush.GetIsAvailable())
                {
                    float newdist = (go.transform.position + bush.GetPosition()).sqrMagnitude;
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
