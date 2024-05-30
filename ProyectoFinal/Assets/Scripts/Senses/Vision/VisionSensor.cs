using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class VisionSensor : MonoBehaviour
{
    [SerializeField]
    private float sightAngle = 90.0f;
    [SerializeField]
    private float sightDistance = 8.0f;
 
    public bool DetectClosestTarget(GameObject target)
    {
        Vector3 targetPos = (target.transform.position - transform.position).normalized;

        if (Vector3.Angle(transform.forward, targetPos) <= sightAngle / 2)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);

            if (distanceToTarget <= sightDistance)
            {
                RaycastHit hit;

                Physics.Raycast(transform.position, target.transform.position - transform.position, out hit, sightDistance);

                if (hit.collider.gameObject.tag == "Stag" || hit.collider.gameObject.tag == "Rabbit")
                {
                    Debug.Log("Detectado");
                    return true;
                }
            }
        }
        return false;
    }

}
