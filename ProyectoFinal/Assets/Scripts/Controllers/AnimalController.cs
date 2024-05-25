using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    private List<GameObject> stags = new List<GameObject>();
    private List<GameObject> wolfs = new List<GameObject>();
    protected enum States
    {
        RESTING = 0, WALKING = 1, RUNNING = 2, DEAD = 3, EAT = 4
    }
  
    public void RegisterWolf(GameObject wolf)
    {
        wolfs.Add(wolf);
    }

    public void RegisterStag(GameObject stag)
    {
        stags.Add(stag);
    }

    protected void Rest(GameObject animal)
    {

    }
    
}
