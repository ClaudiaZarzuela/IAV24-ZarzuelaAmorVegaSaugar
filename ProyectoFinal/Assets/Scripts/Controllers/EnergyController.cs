using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyController : MonoBehaviour
{
    [SerializeField]
    public string typeOfAnimal;

    private bool decrease = true;

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
    private float timeToDecrease = 0.1f;

    public void Start()
    {
        if(typeOfAnimal == "Stag")
            EnvironmentController.Instance.RegisterStag(gameObject);
        else
            EnvironmentController.Instance.RegisterWolf(gameObject);
    }

    public float GetHunger() { return currentHunger; }
    public float GetEnergy() { return currentEnergy; }
    public bool IsAlive() { return currentEnergy > 0 && currentHunger > 0; }
    public void Stop(bool active) {  decrease = active; }
    public void AnimalDied()
    {
        LifeController life = gameObject.GetComponent<LifeController>();
        life.Die();

        if (typeOfAnimal == "Stag")
            EnvironmentController.Instance.RemoveStag(gameObject);
        else
            EnvironmentController.Instance.RemoveWolf(gameObject);
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
    public void RestoreMaxEnergy()
    {
        currentEnergy = maxEnergy;
    }

    public void Update()
    {
        if (decrease)
        {
            if (elapsedTime >= timeToDecrease)
            {
                elapsedTime = 0;
                DecreaseHunger();
                DecreaseEnergy();
            }
            else elapsedTime += Time.deltaTime;

            if(currentHunger <= 0 || currentEnergy <= 0)
            {
                decrease = false;
                AnimalDied();
            }
        }
    }
}
