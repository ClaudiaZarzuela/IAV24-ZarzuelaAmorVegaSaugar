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
            Scent newScent = Instantiate(scent, new Vector3(gameObject.transform.position.x, 0.02f, gameObject.transform.position.z),
                             Quaternion.identity).GetComponent<Scent>();
            newScent.transform.SetParent(parent);
            newScent.SetOriginator(this.gameObject);

        }
        else elapsedTime += Time.deltaTime;
    }

}
