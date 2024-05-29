using UnityEngine;
using System.Collections.Generic;

public class SmellArea : MonoBehaviour
{
    private List<Scent> listScent = new List<Scent>();
    private float maxTime = 2.0f;
    private float elapsedTime;

    public bool HasDetectedSmell() { return listScent.Count > 0; }
    public GameObject GetPray() {
        if (listScent.Count > 0) return listScent[0].GetOriginator();
        else return null;
    }
    private void OnTriggerEnter(Collider other)
    {
        Scent otherScent = other.gameObject.GetComponent<Scent>();
        if (otherScent != null)
        {
            CheckIfStillSmells();
            if (!CheckIfAlreadyInList(otherScent)) listScent.Add(otherScent);
           
            listScent.Sort(); 
            //LogScentOrder();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject != null) { 
            Scent otherScent = other.gameObject.GetComponent<Scent>();
            if (otherScent != null)
            {
                CheckIfStillSmells();
                listScent.Remove(otherScent);
                //LogScentOrder();
            }
        }
    }

    public void Update()
    {
        if (elapsedTime >= maxTime)
        {
            if(listScent.Count > 0)
            {
                if (listScent[0] == null || listScent[0].gameObject == null)
                    listScent.Remove(listScent[0]);
                //LogScentOrder();
            }

            elapsedTime = 0;
        }
        else elapsedTime += Time.deltaTime;
    }

    public bool CheckIfAlreadyInList(Scent newScent)
    {
        foreach (Scent scent in listScent)
        {
            if (scent.GetOriginator() == newScent.GetOriginator())
            {
                if (scent.CompareTo(newScent) > 0)
                {
                    listScent.Add(newScent);
                    listScent.Remove(scent);
                }
                return true;
            }
        }
        return false;
    }
    public void CheckIfStillSmells()
    {
        listScent.RemoveAll(scent => scent == null || scent.gameObject == null);
    }

    //public void LogScentOrder()
    //{
    //    string list = " ";
    //    foreach (Scent scent in listScent)
    //    {
    //        list += "Scent Type:" + scent.GetOriginator() + ", Intensity:" + scent.GetIntensity();
    //    }
    //    Debug.Log(list);
    //}
}

