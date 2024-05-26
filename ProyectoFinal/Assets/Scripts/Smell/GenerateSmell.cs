using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateSmell : MonoBehaviour
{
    [SerializeField]
    public float timeToSpawn;
    [SerializeField]
    public GameObject scent;
    private float elapsedTime;

    private void Update()
    {
        if (elapsedTime >= timeToSpawn)
        {
            elapsedTime = 0;
            Instantiate(scent, new Vector3(gameObject.transform.position.x, 0.02f, gameObject.transform.position.z), Quaternion.identity);
        }
        else elapsedTime += Time.deltaTime;
    }

}
