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
    private Button button = null;
    [SerializeField]
    private EnergyController energyController;

    private bool active = true;

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

    public void DeactivateButton()
    {
        if (button != null)
        {
            button.GetComponent<Image>().color = Color.white;
            button.interactable = false;
        }
    }
    public void Update()
    {
        if (active)
        {
            if (energyController != null && energyController.IsAlive())
            {
                DecreaseEnergy();
                DecreaseHunger();
            }
            else if(energyController == null || !energyController.IsAlive()) 
            {
                DeactivateButton();
                active = false;
            }
        }
    }
}
