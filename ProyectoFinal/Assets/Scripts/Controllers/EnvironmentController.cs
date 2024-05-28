using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{

    #region references
    static private EnvironmentController _instance;
    static public EnvironmentController Instance
    {
        get
        {
            return _instance;
        }
    }
    #endregion
    #region methods
    private void Awake()
    {
        _instance = this;
    }
    #endregion

    public static int rabitsNum = 10;
    private List<GameObject> rabbits = new List<GameObject>();
    private List<GameObject> stags = new List<GameObject>();
    private List<GameObject> wolfs = new List<GameObject>();
    private List<GameObject> bushes = new List<GameObject>();

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

        Debug.Log(stag);
    }

    public void RegisterRabbits(GameObject rabbit)
    {
        rabbits.Add(rabbit);
    }

    public void RemoveRabbit(GameObject rabbit)
    {
        rabbits.Remove(rabbit);
    }

    public void RegisterBush(GameObject bush)
    {
        bushes.Add(bush);
    }

    public void RemoveBush(int bush)
    {
        Destroy(bushes[bush]);
        bushes.RemoveAt(bush);
    }

    public int GetRabitsNum() { return rabbits.Count; }

    public int GetBushesNum() { return bushes.Count; }
}
