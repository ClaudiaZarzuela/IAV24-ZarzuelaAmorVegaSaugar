using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;
using UnityEngine.AI;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to move through random points of the envirnonment to simulate an animal wandering around.
    /// </summary>
    [Action("GameObject/GetRandomPoint")]
    [Help("Moves through random points of the envirnonment to simulate an animal wandering around")]
    public class GetRandomPoint : GOAction
    {
        [OutParam("randomPosition")]
        [Help("Position randomly taken from the area")]
        public Vector3 randomPosition { get; set; }
        public override void OnStart()
        {
            Vector3 randomPos = Random.insideUnitSphere * 25;
            randomPos += gameObject.transform.position;
            if (NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 25, 1))
            {
                randomPosition =  hit.position;
            }
            else randomPosition = gameObject.transform.position;
        }

        public override TaskStatus OnUpdate()
        {
            return TaskStatus.COMPLETED;
        }
    }
}