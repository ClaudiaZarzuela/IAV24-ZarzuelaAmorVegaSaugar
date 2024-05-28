using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RabitController : MonoBehaviour 
{
    [SerializeField]
    public GameObject rabbit = null;

    public void InstanciateRabbits()
    {
        Vector3 randomPoint;
        NavMeshHit hit;
        Transform parent = new GameObject("Rabbits").transform;

        for (int i = AnimalController.Instance.GetRabitsNum(); i < AnimalController.rabitsNum; i++)
        {

            randomPoint = transform.position + Random.insideUnitSphere * 20;
            if (NavMesh.SamplePosition(randomPoint, out hit, 20, NavMesh.AllAreas))
            {
                randomPoint = hit.position;
            }
            else
            {
                randomPoint = transform.position + Random.insideUnitSphere * Random.Range(-5.0f, 5.0f);
            }
            randomPoint.y = 0.0f;
            GameObject newRabbit = Instantiate(rabbit, randomPoint, Quaternion.identity);
            newRabbit.transform.SetParent(parent);
            AnimalController.Instance.RegisterRabbits(newRabbit);
        }
    }

    public void RabbitDied(GameObject obj)
    {
        AnimalController.Instance.RemoveRabbit(obj);
        InstanciateRabbits();
    }

}
