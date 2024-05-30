using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BBUnity.Actions
{

    [Action("GameObject/Eating")]
    public class Eating : GOAction
    {
        public override void OnStart()
        {
            WolfSM eatComponent = gameObject.GetComponent<WolfSM>();
            if(!eatComponent.CheckActiveAction(2))
                eatComponent.WolfHasPrey();
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
