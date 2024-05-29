using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionSensor : MonoBehaviour
{
    [SerializeField]
    private float sightAngle = 90.0f;
    [SerializeField]
    private float sightDistance = 8.0f;

    public bool DetectClosestTarget(GameObject target)
    {
        bool wasDetected = false;

        Vector3 sightOrigin = transform.position + Vector3.up * 0.5f;

        Vector3 targetPos = target.GetComponent<Collider>().bounds.center;
        Vector3 dir = targetPos - sightOrigin;
        Vector3 planarDir = new Vector3(dir.x, 0f, dir.z);

        if (planarDir.sqrMagnitude > sightDistance * sightDistance) return wasDetected;

        if (Mathf.Abs(Vector3.Angle(-target.transform.forward, planarDir)) < sightAngle / 2)
        {
            RaycastHit hit;
            if (Physics.Raycast(sightOrigin, dir, out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject == target)
                {
                    float d = dir.sqrMagnitude;
                    if (d < sightDistance)
                    {
                        wasDetected = true;
                    }
                }
            }
        }
        return wasDetected;
    }
}
