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
            if (eatComponent.CheckIfHunting())
            {
                eatComponent.WolfHasPrey();
                Animator animator = gameObject.GetComponent<Animator>();
                animator.Play("Eat");
            }
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}
