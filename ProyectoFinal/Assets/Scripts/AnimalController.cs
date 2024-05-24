using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    private List<GameObject> stags = new List<GameObject>();
    private List<GameObject> wolfs = new List<GameObject>();

    public void Start()
    {
    }
    public void RegisterWolf(GameObject wolf)
    {
        wolfs.Add(wolf);
    }

    public void RegisterStag(GameObject stag)
    {
        stags.Add(stag);
    }
    
}
