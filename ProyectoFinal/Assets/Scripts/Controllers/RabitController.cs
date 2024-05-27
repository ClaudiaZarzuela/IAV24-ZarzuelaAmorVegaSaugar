using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RabitController : AnimalController
{
    [SerializeField]
    public GameObject rabbit = null;

    public void InstanciateRabbits()
    {
        Vector3 randomPoint;
        NavMeshHit hit;
        Transform parent = new GameObject("Rabbits").transform;

        for (int i = currRabitsNum; i < rabitsNum; i++)
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
            rabbits.Add(newRabbit);
            currRabitsNum++;

        }
    }

    public void RabbitDied(GameObject obj)
    {
        currRabitsNum--;
        rabbits.Remove(obj);
        InstanciateRabbits();
    }

}
