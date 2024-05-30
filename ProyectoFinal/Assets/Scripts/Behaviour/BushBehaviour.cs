using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class BushBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject bushMesh;
    [SerializeField]
    private GameObject bushBerriesMesh;

    private float berrieTimer = 10.0f;
    private float auxTimer = 0;
    private bool isAvailable = true;
    private bool hasBerries = true;

    // Start is called before the first frame update

    public void SetFocus()
    {
        isAvailable = false;
    }

    public void StartEating()
    {
        bushBerriesMesh.SetActive(false);
        hasBerries = false;
    }

    public bool GetIsAvailable()
    {
        return isAvailable;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isAvailable && !hasBerries)
        {
            auxTimer += Time.deltaTime;

            if(auxTimer >= berrieTimer)
            {
                auxTimer = 0;
                bushBerriesMesh.SetActive(true);
                isAvailable = true;
                hasBerries = true;
            }
        }
    }
}
