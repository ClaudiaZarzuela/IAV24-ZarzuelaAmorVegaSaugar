using Pada1.BBCore;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace BBUnity.Conditions
{
    [Condition("Perception/CheckVision")]
    public class CheckVision : GOCondition
    {
        [OutParam("target")]
        public GameObject target;
        public override bool Check()
        {
            VisionSensor sensor = gameObject.GetComponent<VisionSensor>();
            SmellArea area = gameObject.GetComponent<SmellArea>();

            if (sensor == null || area == null) return false;
            
            target = area.GetPray();
            Debug.Log(target);

            return sensor.DetectClosestTarget(target);
        }
    }
}
