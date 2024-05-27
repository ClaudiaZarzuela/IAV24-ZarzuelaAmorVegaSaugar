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

    private Transform parent;

    private void Start()
    {
        parent = new GameObject("ScentTrail").transform;
    }

    private void Update()
    {
        if (elapsedTime >= timeToSpawn)
        {
            elapsedTime = 0;
            GameObject scentParticle = Instantiate(scent, new Vector3(gameObject.transform.position.x, 0.02f, gameObject.transform.position.z), Quaternion.identity);
            scentParticle.transform.SetParent(parent);
        }
        else elapsedTime += Time.deltaTime;
    }

}
