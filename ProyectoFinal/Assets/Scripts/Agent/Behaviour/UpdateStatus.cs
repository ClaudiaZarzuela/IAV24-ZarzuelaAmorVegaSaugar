using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UpdateStatus : MonoBehaviour
{
    [SerializeField]
    private Image fillImageHunger;
    [SerializeField]
    private Image fillImageEnergy;
    
    [SerializeField]
    private EnergyController energyController;

    public void Start()
    {
        fillImageHunger.fillAmount = 1;
        fillImageEnergy.fillAmount = 1;
    }

    public void DecreaseHunger()
    {
        fillImageHunger.fillAmount = energyController.GetHunger() / 100;
    }

    public void DecreaseEnergy()
    {
        fillImageEnergy.fillAmount = energyController.GetEnergy() / 100;
    }

    public void Update()
    {
        if (energyController != null)
        {
            DecreaseEnergy();
            DecreaseHunger();
        }
    }
}
