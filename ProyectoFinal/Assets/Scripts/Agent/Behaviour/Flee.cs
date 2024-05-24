using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;
using UCM.IAV.Movimiento;

public class Flee : MonoBehaviour
{
    [SerializeField]
    public GameObject home;
    private NavMeshAgent navAgent;
    public float runningSpeed;
    bool flee = false;
    public void Start()
    {
        navAgent = gameObject.GetComponent<NavMeshAgent>();
    }

    public void StopFlee()
    {
        flee = false;
    }

    public bool GotHome() { return !flee; }

    public void StartFlee()
    {
        navAgent.speed = runningSpeed;
        navAgent.SetDestination(home.transform.position);
        flee = true;
    }
    public void Update()
    {
        if (flee && navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            Debug.Log("He llegado a casa");
            flee = false;
        }
    }
}
