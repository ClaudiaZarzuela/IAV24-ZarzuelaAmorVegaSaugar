using System;
using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    [Action("GameObject/CheckVision")]
    public class CheckVision : GOAction
    {
        [OutParam("target")]
        public GameObject target;

        private VisionSensor sensor;
        private SmellArea area;
        public override void OnStart()
        {
            sensor = gameObject.GetComponent<VisionSensor>();
            area = gameObject.GetComponent<SmellArea>();
            GameObject _target = area.GetPray();
            if (sensor.DetectClosestTarget(_target)) target = _target;
            else target = null;
        }

        public override TaskStatus OnUpdate()
        {
            Debug.Log(area.HasDetectedSmell());
            if(area.HasDetectedSmell()) return TaskStatus.COMPLETED;
            else return TaskStatus.FAILED;
        }
    }
}
