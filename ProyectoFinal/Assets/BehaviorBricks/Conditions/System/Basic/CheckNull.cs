using Pada1.BBCore.Framework;
using Pada1.BBCore;
using UnityEngine;

namespace BBCore.Conditions
{
    /// <summary>
    /// It is a basic condition to check if Booleans have the same value.
    /// </summary>
    [Condition("Basic/CheckNull")]
    [Help("Checks if gameObject is null")]
    public class CheckNull : ConditionBase
    {
        ///<value>Input First Boolean Parameter.</value>
        [InParam("gameObject")]
        [Help("gameObject to be compared")]
        public GameObject value;

		public override bool Check()
        {
            return value != null;
        }
    }
}