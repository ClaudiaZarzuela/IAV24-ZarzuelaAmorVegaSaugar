using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyController : MonoBehaviour
{
    [SerializeField]
    public string typeOfAnimal;

    private bool stillAlive = true;

    [SerializeField]
    private float currentHunger = 100;
    [SerializeField]
    private float maxHunger = 100;

    [SerializeField]
    private float currentEnergy = 100;
    [SerializeField]
    private float maxEnergy = 100;

    private float elapsedTime = 0;
    [SerializeField]
    private float timeToDecrease = 1;

    public void Start()
    {
        if(typeOfAnimal == "Stag")
            EnvironmentController.Instance.RegisterStag(this.gameObject);
        else
            EnvironmentController.Instance.RegisterWolf(this.gameObject);
    }

    public float GetHunger() { return currentHunger; }
    public float GetEnergy() { return currentEnergy; }
    public bool IsAlive() {  return stillAlive; }
    public void AnimalDied()
    {
        if (typeOfAnimal == "Stag")
            EnvironmentController.Instance.RemoveStag(this.gameObject);
        else
            EnvironmentController.Instance.RemoveWolf(this.gameObject);
    }
    public void DecreaseHunger()
    {
        currentHunger-=2;
    }

    public void DecreaseEnergy()
    {
        currentEnergy--;
    }

    public void RestoreMaxHunger()
    {
        currentHunger = maxHunger;
    }
    public void RestoreHunger(int h)
    {
        currentHunger += h;
        if (currentHunger > maxHunger) currentHunger = maxHunger;
    }

    public void RestoreMaxEnergy()
    {
        currentEnergy = maxEnergy;
    }

    public void RestoreEnergy(int e)
    {
        currentEnergy += e;
        if (currentEnergy > maxEnergy) currentEnergy = maxEnergy;
    }

    public void Update()
    {
        if (stillAlive)
        {
            if (elapsedTime >= timeToDecrease)
            {
                elapsedTime = 0;
                //DecreaseHunger();
                DecreaseEnergy();
            }
            else elapsedTime += Time.deltaTime;

            if(currentHunger <= 0 || currentEnergy <= 0)
            {
                stillAlive = false;
                AnimalDied();
            }
        }
    }
}
