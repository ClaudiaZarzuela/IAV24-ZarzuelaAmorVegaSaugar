using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.AI;
using UnityEditor;

public class BushesController : MonoBehaviour
{
    public static int bushesNum = 10;
    
    [SerializeField]
    public GameObject arbusto;

    [SerializeField]
    public GameObject area;

    private List<GameObject> arbustos;
    private Vector3 centerPoint = new Vector3(-33.0f, -0.5f, -8.8f);
    private int maxNumBushes = 50;

    public void InstanciateBushes() {

        Transform parent = new GameObject("Arbustos").transform;
        arbustos = new List<GameObject>();
        float scaleX = area.transform.localScale.x;
        float scaleZ = area.transform.localScale.z;
        int instanciatedBushes = 0;

        for (int x = 0; x < scaleX; ++x)
        {
            for(int z = 0; z < scaleZ; ++z)
            {
                float noiseValue = Mathf.PerlinNoise((float)x / scaleX * 15 * Random.Range(0.5f, 1.5f), (float)z / scaleZ * 15 * Random.Range(0.5f, 1.5f));

                if(noiseValue > 0.75 && instanciatedBushes < maxNumBushes)
                {
                    Vector3 pos = new Vector3(x , 0.5f, z);
                    GameObject bush = Instantiate(arbusto, pos, Quaternion.identity);
                    arbustos.Add(bush);
                    bush.transform.SetParent(parent);
                    instanciatedBushes++;
                }
            }
        }

        while (arbustos.Count > bushesNum)
        {
            int index = Random.Range(0, arbustos.Count);
            Destroy(arbustos[index]);
            arbustos.RemoveAt(index);
        }

        parent.SetPositionAndRotation(centerPoint, Quaternion.identity);
    }
}
