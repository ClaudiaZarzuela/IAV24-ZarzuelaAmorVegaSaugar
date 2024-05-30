using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Eat : MonoBehaviour
{
    public bool active = false;
    public bool tracing = true;
    public bool eating = false;
    SmellArea area = null;
    VisionSensor vision = null;
    [SerializeField]
    BehaviorExecutor hunt;


    public bool IsEatingPrey()
    {
        return eating;
    }
    public void EatingPrey()
    {
        active = false;
        eating = true;
        hunt.enabled = false;
    }

    public void StartBehavior()
    {
        hunt.enabled = true;
    }
    public bool DetectedTrace()
    {
        return area.HasDetectedSmell();
    }
    // Start is called before the first frame update
    void Start()
    {
        area = gameObject.GetComponent<SmellArea>();
        vision = gameObject.GetComponent<VisionSensor>();
        //hunt.enabled = false;
    }

    private void MoveToTarget(GameObject target)
    {
        hunt.SetBehaviorParam("target", target);
    }
    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            GameObject _target = area.GetPray();
            if (vision.DetectClosestTarget(_target))
            {
                tracing = false;
                MoveToTarget(_target);
            }
            else MoveToTarget(area.GetScent());
        }
    }
}
